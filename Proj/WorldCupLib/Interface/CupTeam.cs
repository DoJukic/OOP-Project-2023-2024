using WorldCupLib.Utility;
using System.Collections.ObjectModel;

namespace WorldCupLib
{
    public class CupTeam : IComparable<CupTeam>
    {
        public readonly long ID;
        public readonly String countryName;
        public readonly String altName;
        public readonly String fifaCode;
        public readonly CupGroup group;

        // Needs to be collected from matches
        internal readonly SortedSet<CupPlayer> _sortedPlayers;
        public ReadonlySortedSet<CupPlayer> SortedPlayers { get {return new(_sortedPlayers); } }

        public readonly long wins;
        public readonly long draws;
        public readonly long losses;
        public long GamesPlayed { get { return (wins + draws + losses); } }
        public readonly long points;
        public readonly long goalsFor;
        public readonly long goalsAgainst;
        public long GoalsDifferential { get { return (goalsFor - goalsAgainst); } }

        internal readonly SortedSet<CupMatch> _sortedMatches;
        public ReadonlySortedSet<CupMatch> SortedMatches { get { return new(_sortedMatches); } }

        // Can get all of this from results
        public CupTeam(long? ID, string countryName, string altName, string fifaCode, CupGroup group, SortedSet<CupPlayer>? players,
            long? wins, long? draws, long? losses, long? points, long? goalsFor, long? goalsAgainst, SortedSet<CupMatch>? matches)
        {
            this.ID = ID ?? -1;
            this.countryName = countryName;
            this.altName = altName;
            this.fifaCode = fifaCode;
            this.group = group;
            this._sortedPlayers = players ?? new();
            this.wins = wins ?? 0;
            this.draws = draws ?? 0;
            this.losses = losses ?? 0;
            this.points = points ?? 0;
            this.goalsFor = goalsFor ?? 0;
            this.goalsAgainst = goalsAgainst ?? 0;
            this._sortedMatches = matches ?? new();
        }
        // for CompareTo ONLY
        public CupTeam(string fifaCode)
        {
            this.fifaCode = fifaCode;
        }

        public int CompareTo(CupTeam? other)
        {
            return fifaCode.CompareTo(other?.fifaCode);
        }
    }
}
