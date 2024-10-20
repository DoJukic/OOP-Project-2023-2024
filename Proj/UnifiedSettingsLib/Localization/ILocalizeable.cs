using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WorldCupViewer.Localization.LocalizationHandler;

namespace WorldCupViewer.Localization
{
    public interface ILocalizeable
    {
        public LocalizationOptions GetLocalizationTarget();
        public void SetLocalizedText(String text);
    }
}
