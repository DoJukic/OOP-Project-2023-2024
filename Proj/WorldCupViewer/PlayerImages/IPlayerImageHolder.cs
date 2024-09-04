using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupViewer.PlayerImages
{
    public interface IPlayerImageHolder
    {
        public long GetPlayerNumber();
        public String GetPlayerImageID();
        public void SetPlayerImageID(String externalImageID);
    }
}
