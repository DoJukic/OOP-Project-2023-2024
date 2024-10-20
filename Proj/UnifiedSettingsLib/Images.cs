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

        private static Object operationsLock = new();
        private readonly static FileSystemMonitor? fileSystemMonitor;
        private static bool errorDetected = false;

        private static event Action? OnExternalImagesChanged;
        private static event Action? OnExternalImagesError;

        static Images()
        {
            internalImages.Add(new("FifaWorldCup2018Men", Properties.Resources.FIFAWorldCup2018Men));
            internalImages.Add(new("FifaWorldCup2019Women", Properties.Resources.FIFAWorldCup2019Women));

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
                DirectoryInfo baseDirInfo = new(baseDir);
                foreach (var file in baseDirInfo.GetFiles())
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
        public static void UnsubscribeFromExternalImagesChanged(Action action)
        {
            lock (operationsLock)
            {
                OnExternalImagesChanged -= action;
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
        public static Stream GetImgNotFoundPngStream()
        {
            return new MemoryStream(Properties.Resources.ImgNotFound);
        }

        public static byte[] GetNoDataPngBytes()
        {
            return Properties.Resources.anomalyNoData;
        }
        public static Stream GetNoDataPngStream()
        {
            return new MemoryStream(Properties.Resources.anomalyNoData); ;
        }

        public static byte[] GetStarPngBytes()
        {
            return Properties.Resources.GoldStarMini;
        }
        public static Stream GetStarPngStream()
        {
            return new MemoryStream(Properties.Resources.GoldStarMini); ;
        }

        public static Stream? GetInternalImageStream(string ID)
        {
            var tgt = internalImages.Where((tgt) => tgt.identifier == ID);

            if (tgt.Any())
                return new MemoryStream(tgt.First().source);

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
        public static String? TryGetExternalImagePath(string ID)
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
        public static byte[]? TryGetExternalImageBytes(string ID)
        {
            IEnumerable<ExternalImageDetails> tgt;
            lock (externalImagesPlsLock)
            {
                tgt = externalImagesPlsLock.Where((tgt) => tgt.identifier == ID);

                if (tgt.Any())
                    return tgt.First().TryGetBytes();
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
            public readonly byte[] source;

            public InternalImageDetails(String identifier, byte[] source)
            {
                this.identifier = identifier;
                this.source = source;
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
            private byte[]? data;

            public ExternalImageDetails(string identifier, string path)
            {
                this.identifier = identifier;
                this.path = path;
            }

            public byte[]? TryGetBytes()
            {
                try
                {
                    if (data == null)
                        data = File.ReadAllBytes(path);
                }
                catch (Exception)
                {
                }

                return data;
            }
        }
    }
}
