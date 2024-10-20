using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WorldCupLib.Deserialize;

namespace WorldCupLib.Interface
{
    public class CupEvent
    {
        public readonly long? id;
        public readonly string typeOfEvent;
        public readonly CupPlayer player;
        public readonly string time;

        public CupEvent(long? id, string typeOfEvent, CupPlayer player, string time)
        {
            this.id = id;
            this.typeOfEvent = typeOfEvent;
            this.player = player;
            this.time = time;
        }

        public enum SupportedCupEventTypes
        {
            SubstitutionIn,
            SubstitutionOut,
            YellowCard,
            RedCard,
            Goal,
        }

        public static bool CheckCupEvent(CupEvent cupEvent, SupportedCupEventTypes type)
        {
            switch (type)
            {
                case SupportedCupEventTypes.SubstitutionIn:
                    return cupEvent.typeOfEvent == "substitution-in";
                case SupportedCupEventTypes.SubstitutionOut:
                    return cupEvent.typeOfEvent == "substitution-out";
                case SupportedCupEventTypes.YellowCard:
                    return cupEvent.typeOfEvent == "yellow-card";
                case SupportedCupEventTypes.RedCard:
                    return cupEvent.typeOfEvent == "red-card";
                case SupportedCupEventTypes.Goal:
                    return cupEvent.typeOfEvent == "goal";
            }

            return false;
        }

        /// <summary>
        /// Will throw exception if it can't find a player
        /// </summary>
        /// <param name="teamEvents"></param>
        /// <param name="players"></param>
        /// <returns></returns>
        public static List<CupEvent> ConvertTeamEventEnumerableToCupEventList(IEnumerable<TeamEvent> teamEvents, IEnumerable<CupPlayer> players,
            List<String> errorList, String homeTeamFifaCode, String awayTeamFifaCode)
        {
            List<CupEvent> cupEvents = new();

            foreach (var teamEvent in teamEvents)
            {
                CupPlayer player;
                try
                {
                    player = players.Single(p => p.name == teamEvent.Player);
                }
                catch (Exception)
                {
                    //WHYYYYYYYYYY
                    errorList.Add("Match events have players which don't exist - \"" + teamEvent.Player + "\" " +
                        "(" + homeTeamFifaCode + " versus " + awayTeamFifaCode + ")");
                    continue;
                }

                CupEvent cupEvent = new (teamEvent.Id, teamEvent.TypeOfEvent, player, teamEvent.Time);
                cupEvents.Add(cupEvent);
                player._relatedEvents.Add(cupEvent);
            }

            return cupEvents;
        }
    }
}
