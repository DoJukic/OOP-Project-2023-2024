using TooManyUtils;
using System.Collections.ObjectModel;

namespace WorldCupLib
{
    public class CupPlayer : IComparable<CupPlayer>
    {
        public readonly long shirtNumber;
        public readonly String name;

        public readonly CupTeam team;

        internal readonly SortedSet<CupMatch> _sortedMatches;
        public ReadonlySortedSet<CupMatch> SortedMatches { get { return new(_sortedMatches); } }

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
    }
}
