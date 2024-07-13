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
            this.DoSubscribe(action);
        }

        public void Subscribe(Action action) { Task.Run(() => this.DoSubscribe(action)); }
        private void DoSubscribe(Action action)
        {
            lock (theLock)
            {
                if (!wasDisposed)
                    TheEvent += action;
            }
        }

        internal void Trigger() { Task.Run(this.DoTrigger); }
        private void DoTrigger()
        {
            /*lock (theLock) { if (!wasDisposed) TheEvent?.Invoke(); }*/
            lock (theLock)
            {
                if (!wasDisposed)
                    foreach (var del in TheEvent?.GetInvocationList() ?? Array.Empty<Delegate>())
                        Task.Run(() => { del.DynamicInvoke(null); }).ConfigureAwait(false);
            }
        }

        public void Dispose() { Task.Run(this.DoDispose); }
        private void DoDispose()
        {
            lock (theLock)
            {
                wasDisposed = true;
                TheEvent = null;
            }
        }
    }
}
