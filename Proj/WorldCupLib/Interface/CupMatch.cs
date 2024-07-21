using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

namespace WorldCupLib
{
    public class CupMatch : IComparable<CupMatch>
    {
        public readonly String venue;
        public readonly String location;
        public readonly String status;
        public readonly String time; // full-time, half-time ??? Lots of same strings, but strings have their little pool anyways so it doesn't really matter.
        public readonly String fifaID;
        public readonly CupWeather weather;
        public readonly long attendance;
        internal readonly List<String> officials;
        public List<String> Officials { get { return new(officials); } }
        public readonly String stageName;

        public readonly CupMatchTeamInfo homeTeam;
        public readonly CupMatchTeamStatistics homeTeamStatistics;
        public readonly CupMatchTeamInfo awayTeam;
        public readonly CupMatchTeamStatistics awayTeamStatistics;
        public readonly CupTeam? winningTeam;

        public readonly DateTimeOffset matchDateTime;

        public readonly DateTimeOffset? lastEventUpdateTime;
        public readonly DateTimeOffset? lastScoreUpdateTime;

        public CupMatch(string venue, string location, string status, string time, string fifaID, CupWeather weather, long? attendance, List<string> officials, string stageName,
            CupMatchTeamInfo homeTeam, CupMatchTeamStatistics homeTeamStatistics, CupMatchTeamInfo awayTeam, CupMatchTeamStatistics awayTeamStatistics, CupTeam? winningTeam,
            DateTimeOffset? matchDateTime, DateTimeOffset? lastEventUpdateTime, DateTimeOffset? lastScoreUpdateTime)
        {
            this.venue = venue;
            this.location = location;
            this.status = status;
            this.time = time;
            this.fifaID = fifaID;
            this.weather = weather;
            this.attendance = attendance ?? 0;
            this.officials = officials ?? new();
            this.stageName = stageName;
            this.homeTeam = homeTeam;
            this.homeTeamStatistics = homeTeamStatistics;
            this.awayTeam = awayTeam;
            this.awayTeamStatistics = awayTeamStatistics;
            this.winningTeam = winningTeam;

            if (matchDateTime.Equals(null))
                throw new ArgumentNullException();
            this.matchDateTime = matchDateTime ?? new();

            this.lastEventUpdateTime = lastEventUpdateTime;
            this.lastScoreUpdateTime = lastScoreUpdateTime;
        }

        // For comparisons, exclusively
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CupMatch(DateTimeOffset? matchDateTime, string fifaID)
        {
            if (matchDateTime.Equals(null))
                throw new ArgumentNullException();
            this.matchDateTime = matchDateTime ?? new();

            this.fifaID = fifaID;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int CompareTo(CupMatch? other)
        {
            if (other == null) return 1;
            var comaprisonResult = matchDateTime.CompareTo(other.matchDateTime);
            return comaprisonResult != 0 ? comaprisonResult : fifaID.CompareTo(other?.fifaID);
        }
    }
}
