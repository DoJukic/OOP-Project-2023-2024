using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TooManyUtils;

namespace SharedDataLib
{
    public static class Images
    {
        private const String baseDir = "ExternalImages";

        private readonly static List<InternalImageDetails> internalImages = new();
        private readonly static List<ExternalImageDetails> externalImagesPlsLock = new();

        private static Stream? imgNotFoundStream;
        private static Stream? personNotFoundStream;

        private static Object operationsLock = new();
        private readonly static FileSystemMonitor? fileSystemMonitor;
        private static bool errorDetected = false;

        private static event Action? OnExternalImagesChanged;
        private static event Action? OnExternalImagesError;

        static Images()
        {
            internalImages.Add(new("FifaWorldCup2018Men", new MemoryStream(Properties.Resources.FIFAWorldCup2018Men)));
            internalImages.Add(new("FifaWorldCup2019Women", new MemoryStream(Properties.Resources.FIFAWorldCup2019Women)));

            try
            {
                Directory.CreateDirectory(baseDir);
                fileSystemMonitor = new(baseDir, true, 100, new(ExternalImagesChanged), new(ExternalImagesFailed), new(ExternalImagesFailed));
                ExternalImagesChanged();
            }
            catch (Exception)
            {
                ExternalImagesFailed();
            }
        }

        private static void ExternalImagesChanged()
        {
            List<ExternalImageDetails> deets = new();

            try
            {
                DirectoryInfo info = new(baseDir);
                foreach (var file in info.GetFiles())
                {
                    switch (file.Extension.ToLower())
                    {
                        case ".png":
                        case ".jpeg":
                        case ".tiff":
                        case ".bmp":
                            break;
                        default:
                            continue;
                    }

                    deets.Add(new(file.Name, file.FullName));
                }
            }
            catch (Exception)
            {
                ExternalImagesFailed();
            }

            lock (externalImagesPlsLock)
            {
                externalImagesPlsLock.Clear();
                externalImagesPlsLock.AddRange(deets);
            }

            lock(operationsLock)
            {
                OnExternalImagesChanged?.Invoke();
            }
        }
        public static void SubscribeToExternalImagesChanged(Action action)
        {
            lock (operationsLock)
            {
                OnExternalImagesChanged += action;
            }
        }
        private static void ExternalImagesFailed()
        {
            lock (operationsLock)
            {
                errorDetected = true;
                OnExternalImagesError?.Invoke();
            }
        }
        public static void SubscribeToExternalImagesFailed(Action action)
        {
            lock (operationsLock)
            {
                OnExternalImagesError += action;
            }
        }

        public static byte[] GetImgNotFoundPngBytes()
        {
            return Properties.Resources.ImgNotFound;
        }
        public static Stream GetImgNotFoundPngStream_DO_NOT_DISPOSE_OR_WRITE()
        {
            imgNotFoundStream ??= new MemoryStream(Properties.Resources.ImgNotFound);
            return imgNotFoundStream;
        }

        public static byte[] GetNoDataPngBytes()
        {
            return Properties.Resources.anomalyNoData;
        }
        public static Stream GetNoDataPngStream_DO_NOT_DISPOSE_OR_WRITE()
        {
            personNotFoundStream ??= new MemoryStream(Properties.Resources.anomalyNoData);
            return personNotFoundStream;
        }

        public static Stream? GetInternalImageStream_DO_NOT_DISPOSE_OR_WRITE(string ID)
        {
            var tgt = internalImages.Where((tgt) => tgt.identifier == ID);

            if (tgt.Any())
                return tgt.First().stream_DO_NOT_CLOSE;

            return null;
        }
        public static IEnumerable<String> getAllInternalImageNames(string ID)
        {
            List<String> names = new();

            foreach (var thing in internalImages)
                names.Add(thing.identifier);

            return names;
        }

        public static bool GetExternalImagesHasError()
        {
            return errorDetected;
        }
        public static String? GetExternalImagePath(string ID)
        {
            IEnumerable<ExternalImageDetails> tgt;
            lock (externalImagesPlsLock)
            {
                tgt = externalImagesPlsLock.Where((tgt) => tgt.identifier == ID);

                if (tgt.Any())
                    return tgt.First().path;
            }

            return null;
        }
        public static IEnumerable<String> GetAllExternalImageNames()
        {
            List<String> names = new();

            lock (externalImagesPlsLock)
            {
                foreach (var thing in externalImagesPlsLock)
                    names.Add(thing.identifier);
            }

            return names;
        }

        private class InternalImageDetails
        {
            public readonly String identifier;
            public readonly Stream stream_DO_NOT_CLOSE;

            public InternalImageDetails(String identifier, Stream stream)
            {
                this.identifier = identifier;
                this.stream_DO_NOT_CLOSE = stream;
            }

            public int CompareTo(InternalImageDetails? other)
            {
                throw new NotImplementedException();
            }
        }
        private class ExternalImageDetails
        {
            public readonly string identifier;
            public readonly string path;

            public ExternalImageDetails(string identifier, string path)
            {
                this.identifier = identifier;
                this.path = path;
            }
        }
    }
}
