using API_Tester.WorldCupDataRepo.Interface;
using System.Collections.ObjectModel;

namespace API_Tester.WorldCupDataRepo
{
    public class CupMatchTeamStatistics
    {
        public readonly long attemptsOnGoalOnTarget;
        public readonly long attemptsOnGoalOffTarget;
        public readonly long attemptsOnGoalBlocked;
        public readonly long attemptsOnGoalWoodwork;
        public long AttemptsOnGoalTotal { get { return attemptsOnGoalBlocked + attemptsOnGoalOffTarget + attemptsOnGoalOnTarget + attemptsOnGoalWoodwork; } }

        public readonly long corners;
        public readonly long offsides;
        public readonly long ballPossesion;
        public readonly long passAccuracy;
        public readonly long numPasses;
        public readonly long passesCompleted;
        public readonly long distanceCovered;
        public readonly long ballsRecovered;
        public readonly long tackles;
        public readonly long clearances;
        public readonly long yellowCards;
        public readonly long redCards;
        public readonly long foulsCommitted;

        public readonly String tactics;

        // Needs to be collected from matches
        internal readonly SortedSet<CupPlayer> _startingElevenPlayers;
        public ReadonlySortedSet<CupPlayer> StartingElevenPlayers { get { return new(_startingElevenPlayers); } }
        // Needs to be collected from matches
        internal readonly SortedSet<CupPlayer> _substitutePlayers;
        public ReadonlySortedSet<CupPlayer> SubstitutePlayers { get { return new(_substitutePlayers); } }

        public CupMatchTeamStatistics(long? attemptsOnGoalOnTarget, long? attemptsOnGoalOffTarget, long? attemptsOnGoalBlocked, long? attemptsOnGoalWoodwork,
            long? corners, long? offsides, long? ballPossesion, long? passAccuracy, long? numPasses, long? passesCompleted, long? distanceCovered, long? ballsRecovered,
            long? tackles, long? clearances, long? yellowCards, long? redCards, long? foulsCommitted, string tactics, SortedSet<CupPlayer> startingElevenPlayers, SortedSet<CupPlayer> substitutePlayers)
        {
            this.attemptsOnGoalOnTarget = attemptsOnGoalOnTarget ?? 0;
            this.attemptsOnGoalOffTarget = attemptsOnGoalOffTarget ?? 0;
            this.attemptsOnGoalBlocked = attemptsOnGoalBlocked ?? 0;
            this.attemptsOnGoalWoodwork = attemptsOnGoalWoodwork ?? 0;
            this.corners = corners ?? 0;
            this.offsides = offsides ?? 0;
            this.ballPossesion = ballPossesion ?? 0;
            this.passAccuracy = passAccuracy ?? 0;
            this.numPasses = numPasses ?? 0;
            this.passesCompleted = passesCompleted ?? 0;
            this.distanceCovered = distanceCovered ?? 0;
            this.ballsRecovered = ballsRecovered ?? 0;
            this.tackles = tackles ?? 0;
            this.clearances = clearances ?? 0;
            this.yellowCards = yellowCards ?? 0;
            this.redCards = redCards ?? 0;
            this.foulsCommitted = foulsCommitted ?? 0;
            this.tactics = tactics;
            this._startingElevenPlayers = startingElevenPlayers ?? new();
            this._substitutePlayers = substitutePlayers ?? new();
        }
    }
}
