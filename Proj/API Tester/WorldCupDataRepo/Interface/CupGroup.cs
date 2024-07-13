using API_Tester.WorldCupDataRepo.Interface;

namespace API_Tester.WorldCupDataRepo
{
    public class CupGroup : IComparable<CupGroup>
    {
        public readonly long ID;
        public readonly String letter;
        internal readonly SortedSet<CupTeam> _sortedTeams;
        public ReadonlySortedSet<CupTeam> SortedTeams { get { return new(_sortedTeams); } }

        public CupGroup(long? iD, string letter, SortedSet<CupTeam>? teams)
        {
            ID = iD ?? -1;
            this.letter = letter;
            this._sortedTeams = teams ?? new();
        }

        public int CompareTo(CupGroup? other)
        {
            int res = ID.CompareTo(other?.ID);

            return res != 0 ? res : letter.CompareTo(other?.letter);
        }
    }
}
