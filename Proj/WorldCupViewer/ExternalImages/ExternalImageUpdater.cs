using SharedDataLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupViewer.Localization;

namespace WorldCupViewer.ExternalImages
{
    /// <summary>
    /// User beware: these updates come from a thread pool context
    /// </summary>
    public static class ExternalImageUpdater
    {
        public static void UpdateAllChildren(Control target)
        {
            foreach (IExternalImage targetImage in LocalUtils.GetAllControls(target).OfType<IExternalImage>())
            {
                targetImage.UpdateExternalImage();
            }
        }

        public static void UpdateOne(IExternalImage target)
        {
            target.UpdateExternalImage();
        }
    }
}
