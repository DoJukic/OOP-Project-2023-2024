using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupLib;

namespace WorldCupViewer
{
    public interface IFavouriteablePlayerHolder
    {
        public CupPlayer GetAssociatedPlayer();
        public void SetIsFavourite(bool isFavourite);
    }
}
