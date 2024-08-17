using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupViewer.Localization;

namespace WorldCupViewer.UserControls
{
    public partial class MultilingualCheckBox : System.Windows.Forms.CheckBox, ILocalizeable
    {
        public MultilingualCheckBox() : base()
        {
            Localization = LocalizationOptions.TestString;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override String Text
        {
            get { return base.Text; }
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
            set { AssembleText(); }
#pragma warning restore CS8765
        }

        private LocalizationOptions localization;
        public LocalizationOptions Localization
        {
            get
            { return localization; }
            set
            {
                localization = value;
                LocalizationHandler.LocalizeOne(this);
            }
        }

        private String preceedingText = "";
        private String localizationString = "";
        private String succeedingText = "";
        public String PreceedingText
        {
            get
            { return preceedingText; }
            set
            {
                preceedingText = value;
                AssembleText();
            }
        }
        public String SucceedingText
        {
            get
            { return succeedingText; }
            set
            {
                succeedingText = value;
                AssembleText();
            }
        }

        private CharacterCasing characterCasing = CharacterCasing.Normal;
        public CharacterCasing CharacterCasing
        {
            get
            { return characterCasing; }
            set
            {
                characterCasing = value;
                AssembleText();
            }
        }

        private void AssembleText()
        {
            base.Text = PreceedingText + localizationString + SucceedingText;

            switch (CharacterCasing)
            {
                case CharacterCasing.Upper:
                    base.Text = base.Text.ToUpper();
                    break;
                case CharacterCasing.Lower:
                    base.Text = base.Text.ToLower();
                    break;
            };
        }

        public LocalizationOptions GetLocalizationTarget()
        {
            return Localization;
        }

        public void SetLocalizedText(string text)
        {
            localizationString = text;
            AssembleText();
        }
    }
}
