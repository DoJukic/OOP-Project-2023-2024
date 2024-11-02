using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupLib.Interface;

namespace WorldCupLib
{
    public class CupMatchTeamInfo
    {
        public readonly CupTeam team;
        public readonly long goals;
        public readonly long penalties;
        public List<CupPlayer> captainList;
        public List<KeyValuePair<CupPlayer, String>> playerPositionPairList;

        public CupMatchTeamInfo(CupTeam team, long? goals, long? penalties, List<CupPlayer>? captainList, List<KeyValuePair<CupPlayer, String>>? playerPositionPairList)
        {
            this.team = team;
            this.goals = goals ?? 0;
            this.penalties = penalties ?? 0;
            this.captainList = captainList ?? new();
            this.playerPositionPairList = playerPositionPairList ?? new();
        }
        public enum SupportedCupPlayerPositions
        {
            Goalkeeper,
            Defence,
            Midfield,
            Forward,
        }

        public static bool CheckPositionString(String position, SupportedCupPlayerPositions targetPosition)
        {
            switch (targetPosition)
            {
                case SupportedCupPlayerPositions.Goalkeeper:
                    return position == "Goalie";
                case SupportedCupPlayerPositions.Defence:
                    return position == "Defender";
                case SupportedCupPlayerPositions.Midfield:
                    return position == "Midfield";
                case SupportedCupPlayerPositions.Forward:
                    return position == "Forward";
            }

            return false;
        }
    }
}
