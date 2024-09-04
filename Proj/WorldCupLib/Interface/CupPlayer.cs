using TooManyUtils;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using WorldCupLib.Deserialize;
using WorldCupLib.Interface;

namespace WorldCupLib
{
    public class CupPlayer : IComparable<CupPlayer>
    {
        public readonly long shirtNumber;
        public readonly String name;

        public readonly CupTeam team;

        internal readonly SortedSet<CupMatch> _sortedMatches;
        public ReadonlySortedSet<CupMatch> SortedMatches { get { return new(_sortedMatches); } }

        internal readonly List<CupEvent> _relatedEvents = new();
        public List<CupEvent> RelatedEvents { get { return new(_relatedEvents); } }

        public CupPlayer(long? shirtNumber, string name, CupTeam team, SortedSet<CupMatch>? matches)
        {
            this.shirtNumber = shirtNumber ?? -1;
            this.name = name;
            this.team = team;
            this._sortedMatches = matches ?? new();
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable. (these are for comparisons)
        private CupPlayer(long? shirtNumber)
        {
            this.shirtNumber = shirtNumber ?? -1;
        }
        public static CupPlayer getComparable(long? shirtNumber)
        {
            return new(shirtNumber);
        }
#pragma warning restore CS8618

        public int CompareTo(CupPlayer? other)
        {
            if (other == null) return 1;
            return shirtNumber.CompareTo(other?.shirtNumber);
        }

        public static void ExtractListsOfCaptainsAndKeyValuePairOfPlayerPositionsFromSubstitutesAndTopEleven(
            List<StartingEleven> startingEleven, List<StartingEleven> substitutes, CupMatchTeamStatistics teamStatistics,
            List<CupPlayer> teamCaptains, List<KeyValuePair<CupPlayer, String>> teamPositionPairs,
            List<String> errorList, String homeTeamFifaCode, String awayTeamFifaCode)
        {

            foreach (var man in startingEleven.Union(substitutes))
            {
                if (man.ShirtNumber == null)
                {
                    errorList.Add("Match has players which are null (" + homeTeamFifaCode + " versus " + awayTeamFifaCode + ")");
                    continue;
                }

                CupPlayer? cupPlayer =
                    teamStatistics._startingElevenPlayers.
                        Union(teamStatistics._substitutePlayers).
                            SingleOrDefault((player) => player.shirtNumber == man.ShirtNumber);

                if (cupPlayer == null)
                {
                    errorList.Add("Match has players which don't exist (" + homeTeamFifaCode + " versus " + awayTeamFifaCode + ")");
                    continue;
                }

                teamPositionPairs.Add(new(cupPlayer, man.Position));

                if (man.Captain != null && (bool)man.Captain)
                {
                    teamCaptains.Add(cupPlayer);
                }
            }
        }
    }
}
