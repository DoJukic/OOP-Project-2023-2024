using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace WorldCupWpf
{
    class MainWindowBindings : INotifyPropertyChanged
    {
        private Thickness fieldImageShadowMarginBinding = new(0);
        public Thickness FieldImageShadowMarginBinding {
            get { return fieldImageShadowMarginBinding; }
            set
            {
                OnPropertyChanged(nameof(FieldImageShadowMarginBinding));
                fieldImageShadowMarginBinding = value;
            }
        }

        //below is the boilerplate code supporting PropertyChanged events:
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
