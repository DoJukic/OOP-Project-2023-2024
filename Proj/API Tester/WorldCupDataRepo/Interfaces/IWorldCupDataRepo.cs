using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Tester.WorldCupDataRepo.Interfaces
{
    public interface IWorldCupDataRepo
    {
    }

    public class CupTeam
    {
        public readonly int ID;
        public readonly String countryName;
        public readonly String altName;
        public readonly String fifaCode;
        public readonly CupGroup group;
        public readonly SortedSet<CupPlayer> players;

        public readonly int wins;
        public readonly int draws;
        public readonly int losses;
        public int gamesPlayed { get { return (wins + draws + losses); } }
        public readonly int points;
        public readonly int goalsFor;
        public readonly int goalsAgainst;
        public int goalsDifferential { get { return (goalsFor - goalsAgainst); } }

        public CupTeam(int ID, string countryName, string altName, string fifaCode, CupGroup group, SortedSet<CupPlayer> players, int wins, int draws, int losses, int points, int goalsFor, int goalsAgainst)
        {
            this.ID = ID;
            this.countryName = countryName;
            this.altName = altName;
            this.fifaCode = fifaCode;
            this.group = group;
            this.players = players;
            this.wins = wins;
            this.draws = draws;
            this.losses = losses;
            this.points = points;
            this.goalsFor = goalsFor;
            this.goalsAgainst = goalsAgainst;
        }
    }

    public class CupPlayer : IComparable<CupPlayer>
    {
        public readonly int shirtNumber;
        public readonly String name;

        public CupPlayer(int shirtNumber, string name)
        {
            this.shirtNumber = shirtNumber;
            this.name = name;
        }

        public int CompareTo(CupPlayer? other)
        {
            return shirtNumber.CompareTo(other?.shirtNumber);
        }
    }

    public class CupGroup
    {
        public readonly int ID;
        public readonly String letter;
        public readonly SortedSet<CupTeam> teams;
    }
}
