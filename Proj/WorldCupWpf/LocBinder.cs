using SharedDataLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using WorldCupViewer.Localization;
using WorldCupWpf.Signals;

namespace WorldCupWpf
{
    public class LocDataBindable : INotifyPropertyChanged, ISignalReciever
    {
        public String assembledString = "";

        private String preceedingText = "";
        private String succeedingText = "";
        private CharacterCasing characterCasing = CharacterCasing.Normal;
        private LocalizationOptions locTarget = LocalizationOptions.TestString;

        private static bool init = false;
        private static String signal = nameof(LocDataBindable) + "|" + "REPORTS_LOCALIZATION_CHANGED";

        public LocDataBindable()
        {
            if (!init)
                LocalizationHandler.SubscribeToLocalizationChanged(() => { Application.Current.Dispatcher.Invoke(() => { SignalController.TriggerSignal(signal); }); });

            SignalController.SubscribeToSignal(signal, this);

            //LocBinder.RegisterBinding(this);
        }

        ~LocDataBindable()
        {
            SignalController.SubscriberDead();
            // LocBinder.RegisteredBindingDead();
        }

        public String AssembledString
        {
            get
            { return assembledString; }
            set
            {
                AssembleText(); // Watchu doin??
            }
        }

        public String PreceedingText
        {
            get
            { return preceedingText; }
            set
            {
                preceedingText = value;
                AssembleText();
                OnPropertyChanged(nameof(PreceedingText));
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
                OnPropertyChanged(nameof(SucceedingText));
            }
        }
        public CharacterCasing CharacterCasing
        {
            get
            { return characterCasing; }
            set
            {
                characterCasing = value;
                AssembleText();
                OnPropertyChanged(nameof(CharacterCasing));
            }
        }
        public LocalizationOptions LocTarget
        {
            get
            { return locTarget; }
            set
            {
                locTarget = value;
                AssembleText();
                OnPropertyChanged(nameof(locTarget));
            }
        }

        public void RecieveSignal(string signalSignature)
        {
            AssembleText();
        }

        public void AssembleText()
        {
            String assembled = PreceedingText + LocalizationHandler.GetCurrentLocOptionsString(locTarget) + SucceedingText;

            switch (CharacterCasing)
            {
                case CharacterCasing.Upper:
                    assembled = assembled.ToUpper();
                    break;
                case CharacterCasing.Lower:
                    assembled = assembled.ToLower();
                    break;
            };

            assembledString = assembled;
            OnPropertyChanged(nameof(AssembledString));
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

        public static void GenerateBindingsForTarget(DependencyObject target, DependencyProperty targetProp, LocalizationOptions localization,
            String? succeedingText = null, String? preceedingText = null, CharacterCasing? casing = null)
        {
            LocDataBindable ldb = new();
            ldb.LocTarget = localization;
            ldb.SucceedingText = succeedingText ?? "";
            ldb.PreceedingText = preceedingText ?? "";
            if (casing != null)
                ldb.CharacterCasing = casing.Value;

            Binding locBinding = new(nameof(ldb.AssembledString));

            locBinding.Source = ldb;
            locBinding.Mode = BindingMode.TwoWay;

            BindingOperations.SetBinding(target, targetProp, locBinding);
        }
    }

    // Next stop: overengineering central
    /// <summary>
    /// Plz only use from ui thread
    /// </summary>
    public static class LocBinder
    {
        private static List<WeakReference<LocDataBindable>> bindings = new();
        private static Object reportsLock = new();
        private static Timer? timer = null;

        public static void LocalizationChanged()
        {
            CleanWeakRefs();

            lock (reportsLock)
                foreach (var bindWeakRef in bindings)
                    if (bindWeakRef.TryGetTarget(out var target))
                        target.AssembleText();
        }

        public static void CleanWeakRefs()
        {
            lock (reportsLock)
            {
                List<WeakReference<LocDataBindable>> DeadReferences = new();

                foreach (var bindWeakRef in bindings)
                {
                    if (bindWeakRef.TryGetTarget(out var target))
                        continue;

                    DeadReferences.Add(bindWeakRef);
                }

                var oldBindings = bindings;
                bindings = new(oldBindings.Count - DeadReferences.Count);

                foreach (var weakRef in oldBindings)
                {
                    if (!DeadReferences.Contains(weakRef))
                        bindings.Add(weakRef);
                }
            }
        }

        public static void CleanupTimerCallback(object? dontcare)
        {
            lock (reportsLock)
            {
                CleanWeakRefs();

                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }
            }
        }

        public static void RegisterBinding(LocDataBindable binding)
        {
            lock (reportsLock)
                bindings.Add(new(binding));
        }

        public static void RegisteredBindingDead()
        {
            lock (reportsLock)
            {
                var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                if (timer != null)
                    return;

                timer = new(CleanupTimerCallback, null, 2, Timeout.Infinite);
            }
        }
    }
}
