using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WorldCupWpf.Signals
{
    public static class SignalController
    {
        private static Object OperationsLock = new();
        private static Timer? timer = null;
        static SortedDictionary<String, List<WeakReference<ISignalReciever>>> recievers = new();

        public static void SubscribeToSignal(String signalSignature, ISignalReciever subscriber)
        {
            lock (OperationsLock)
            {
                if (recievers.TryGetValue(signalSignature, out List<WeakReference<ISignalReciever>>? recieverList) && recieverList != null)
                    recieverList.Add(new(subscriber));
                else
                    recievers.Add(signalSignature, new() { new(subscriber) });
            }
        }

        public static void UnsubscribeFromSignal(String signalSignature, ISignalReciever subscriber)
        {
            lock(OperationsLock)
            {
                if (recievers.TryGetValue(signalSignature, out List<WeakReference<ISignalReciever>>? recieverList) && recieverList != null)
                {
                    foreach (var thing in recieverList)
                    {
                        if (thing.TryGetTarget(out var reciever) && reciever == subscriber)
                        {
                            recieverList.Remove(thing);

                            if (recieverList.Count <= 0)
                                recievers.Remove(signalSignature);

                            break;
                        }
                        else
                        {
                            SubscriberDead();
                        }
                    }
                }
            }
        }

        public static void SubscriberDead()
        {
            lock (OperationsLock)
            {
                var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                if (timer != null)
                    return;

                timer = new(CleanupTimerCallback, null, 2, Timeout.Infinite);
            }
        }

        public static void CleanupTimerCallback(object? dontcare)
        {
            lock (OperationsLock)
            {
                CleanWeakRefs();

                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }
            }
        }

        public static void CleanWeakRefs()
        {
            lock (OperationsLock)
            {
                foreach(var weakRefList in recievers)
                {
                    List<WeakReference<ISignalReciever>> DeadReferences = new();

                    foreach (var bindWeakRef in weakRefList.Value)
                    {
                        if (bindWeakRef.TryGetTarget(out var target))
                            continue;

                        DeadReferences.Add(bindWeakRef);
                    }

                    List<WeakReference<ISignalReciever>> oldBindings = new(weakRefList.Value);
                    weakRefList.Value.Clear();

                    foreach (var weakRef in oldBindings)
                        if (!DeadReferences.Contains(weakRef))
                            weakRefList.Value.Add(weakRef);
                }
            }

            List<String> deadKeys = new();

            foreach (var weakRefList in recievers)
                if (weakRefList.Value.Count <= 0)
                    deadKeys.Add(weakRefList.Key);

            foreach (var key in deadKeys)
                recievers.Remove(key);
        }

        public static void TriggerSignal(String signalSignature)
        {
            CleanWeakRefs();

            lock (OperationsLock)
                if (recievers.TryGetValue(signalSignature, out List<WeakReference<ISignalReciever>>? recieverList) && recieverList != null)
                    foreach (var reciever in recieverList)
                        if (reciever.TryGetTarget(out var target))
                            target.RecieveSignal(signalSignature);
        }
    }
}
