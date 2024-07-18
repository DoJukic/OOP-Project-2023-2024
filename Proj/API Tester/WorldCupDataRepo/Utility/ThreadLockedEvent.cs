using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Tester.WorldCupDataRepo
{

    /// <summary>
    /// All actions are ran on a task thread.
    /// </summary>
    public class ThreadLockedEvent : IDisposable
    {
        private event Action? TheEvent;
        private bool wasDisposed = false;
        private readonly object theLock = new();

        public ThreadLockedEvent()
        {
        }
        public ThreadLockedEvent(Action action)
        {
            this.Subscribe(action);
        }

        public void SafeSubscribe(Action action) { Task.Run(() => this.Subscribe(action)); }
        public void Subscribe(Action action)
        {
            lock (theLock)
            {
                if (!wasDisposed)
                    TheEvent += action;
            }
        }

        internal void SafeTrigger() { Task.Run(this.Trigger); }
        public void Trigger()
        {
            /*lock (theLock) { if (!wasDisposed) TheEvent?.Invoke(); }*/
            lock (theLock)
            {
                if (!wasDisposed)
                    foreach (var del in TheEvent?.GetInvocationList() ?? Array.Empty<Delegate>())
                        Task.Run(() => { del.DynamicInvoke(null); }).ConfigureAwait(false);
            }
        }

        public void SafeDispose() { Task.Run(this.Dispose); }
        public void Dispose()
        {
            lock (theLock)
            {
                wasDisposed = true;
                TheEvent = null;
            }
        }
    }
}
