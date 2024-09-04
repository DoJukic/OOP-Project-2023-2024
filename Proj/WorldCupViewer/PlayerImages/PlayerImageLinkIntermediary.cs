using SharedDataLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupViewer.PlayerImages
{
    public static class PlayerImageLinkIntermediary
    {
        public static ICollection<KeyValuePair<long, string>>? PlayerImagePairList { get; set; }
        public static event Action? onPlayerImagePairListChanged;

        public static void Configure(ICollection<KeyValuePair<long, string>> PlayerImagePairList)
        {
            PlayerImageLinkIntermediary.PlayerImagePairList = PlayerImagePairList;
        }

        public static void CreateLink(long playerNumber, string externamImageID)
        {
            if (PlayerImagePairList == null)
                return;

            foreach (var kvp in PlayerImagePairList)
            {
                if (kvp.Key == playerNumber)
                {
                    PlayerImagePairList.Remove(kvp);
                    break;
                }
            }

            PlayerImagePairList.Add(new(playerNumber, externamImageID));
            onPlayerImagePairListChanged?.Invoke();
        }

        public static void UpdateAllChildPlayerImageHolders(Control control)
        {
            if (PlayerImagePairList == null)
                return;

            foreach (var thing in LocalUtils.GetAllControls(control))
            {
                if (thing is not IPlayerImageHolder playerImageHolder)
                    continue;

                var playerNumber = playerImageHolder.GetPlayerNumber();

                foreach (var kvp in PlayerImagePairList)
                {
                    if (kvp.Key == playerNumber)
                    {
                        if (kvp.Value == playerImageHolder.GetPlayerImageID())
                            break;

                        playerImageHolder.SetPlayerImageID(kvp.Value);
                        break;
                    }
                }
            }
        }
    }
}
