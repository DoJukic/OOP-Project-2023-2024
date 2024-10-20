using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupWpf.Signals
{
    public interface ISignalReciever
    {
        public void RecieveSignal(String signalSignature);
    }
}
