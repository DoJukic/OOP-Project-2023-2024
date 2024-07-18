using API_Tester.WorldCupDataRepo.Deserialize;
using API_Tester.WorldCupDataRepo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Tester.WorldCupDataRepo
{
    internal class WorldCupMemoryRepo : IWorldCupDataRepo
    {
        readonly SortedSet<CupGroup> _groups = new();
        readonly SortedSet<CupMatch> _matches = new();
        readonly SortedSet<CupTeam> _teams = new();

        /// <summary>
        /// So I'm not gonna lie, this ctor is kinda scuffed. We're translating a LOT of data into something actually useable which means a lot of code, but I'm sure it's going to pay off later.
        /// If the fatal error thingy reports true, it's no good and should not be used.
        /// </summary>
        /// <param name="deserializedGroupResults"></param>
        /// <param name="deserializedMatches"></param>
        /// <param name="noErrors"></param>
        internal WorldCupMemoryRepo(List<GroupResults> deserializedGroupResults, List<Matches> deserializedMatches, ref bool noErrors)
        {
            try
            {
                foreach (var groupResult in deserializedGroupResults)
                {
                    CupGroup cupGroup = new(groupResult.Id, groupResult.Letter, null);
                    _groups.Add(cupGroup);

                    foreach (var team in groupResult.OrderedTeams)
                    {
                        CupTeam cupTeam = new(team.Id, team.Country, team.AlternateName, team.FifaCode.ToString(), cupGroup, null,
                            team.Wins, team.Draws, team.Losses, team.Points, team.GoalsFor, team.GoalsAgainst, null);
                        _teams.Add(cupTeam);
                        cupGroup._sortedTeams.Add(cupTeam);
                    }
                }

                foreach (var match in deserializedMatches)
                {
                    var homeTeam = tryGetTeamFromFifaCode(match.HomeTeam.Code);
                    var awayTeam = tryGetTeamFromFifaCode(match.AwayTeam.Code);

                    CupTeam? chickenDinner = null;
                    if (match.WinnerCode != null && match.WinnerCode != "Draw") // Draw?
                    {
                        chickenDinner = tryGetTeamFromFifaCode(match.WinnerCode);
                        if (chickenDinner == null)
                        {
                            Console.WriteLine("BAD MATCH DATA!");
                            noErrors = false;
                            return; // DANGIT
                        }
                    }

                    if (homeTeam == null || awayTeam == null)
                    {
                        Console.WriteLine("BAD MATCH DATA!!");
                        noErrors = false;
                        return; // DANGIT 2: electric boogaloo
                    }

                    CupWeather cupWeather = new(match.Weather.Humidity, match.Weather.TempCelsius, match.Weather.WindSpeed, match.Weather.Description);

                    CupMatchTeamInfo cupHomeInfo = new(homeTeam, match.HomeTeam.Goals, match.HomeTeam.Penalties);
                    CupMatchTeamStatistics cupHomeStatistics = convertMatchTeamStatisticsToCupMatchTeamStatistics(match.HomeTeamStatistics, homeTeam);
                    CupMatchTeamInfo cupAwayInfo = new(awayTeam, match.AwayTeam.Goals, match.AwayTeam.Penalties);
                    CupMatchTeamStatistics cupAwayStatistics = convertMatchTeamStatisticsToCupMatchTeamStatistics(match.AwayTeamStatistics, awayTeam);

                    CupMatch cupMatch = new(match.Venue, match.Location, match.Status, match.Time, match.FifaId?.ToString(), cupWeather,
                        match.Attendance, match.Officials.ToList(), match.StageName, cupHomeInfo, cupHomeStatistics, cupAwayInfo, cupAwayStatistics,
                        chickenDinner, match.Datetime, match.LastEventUpdateAt, match.LastScoreUpdateAt);

                    _matches.Add(cupMatch);

                    homeTeam._sortedMatches.Add(cupMatch);
                    foreach (var cupPlayer in cupHomeStatistics._startingElevenPlayers)
                        cupPlayer._sortedMatches.Add(cupMatch);
                    foreach (var cupPlayer in cupHomeStatistics._substitutePlayers)
                        cupPlayer._sortedMatches.Add(cupMatch);

                    awayTeam._sortedMatches.Add(cupMatch);
                    foreach (var cupPlayer in cupAwayStatistics._startingElevenPlayers)
                        cupPlayer._sortedMatches.Add(cupMatch);
                    foreach (var cupPlayer in cupAwayStatistics._substitutePlayers)
                        cupPlayer._sortedMatches.Add(cupMatch);
                }
            }
            catch (Exception e)
            {
                noErrors = false;
                Console.WriteLine("Critical fail while loading data.");
                Console.WriteLine(e.Message);
            }
        }

        public ReadonlySortedSet<CupGroup> GetCupGroups()
        {
            return new(_groups);
        }

        public ReadonlySortedSet<CupMatch> GetCupMatches()
        {
            return new(_matches);
        }

        public ReadonlySortedSet<CupTeam> GetCupTeams()
        {
            return new(_teams);
        }

        public CupTeam? tryGetTeamFromFifaCode(String code)
        {
            CupTeam? theReturnable;
            CupTeam theSeeker = new(code);
            _teams.TryGetValue(theSeeker, out theReturnable);
            return theReturnable;
        }

        internal CupMatchTeamStatistics convertMatchTeamStatisticsToCupMatchTeamStatistics(TeamStatistics teamStatistics, CupTeam cupTeam)
        {
            SortedSet<CupPlayer> cupTeamStartingEleven = new();
            foreach (var startingElevenPlayer in teamStatistics.StartingEleven)
            {
                CupPlayer? cupPlayer = null;
                cupTeam._sortedPlayers.TryGetValue(CupPlayer.getComparable(startingElevenPlayer.ShirtNumber), out cupPlayer);

                if (cupPlayer == null)
                {
                    cupPlayer = new(startingElevenPlayer.ShirtNumber, startingElevenPlayer.Name, cupTeam, null);
                    cupTeam._sortedPlayers.Add(cupPlayer);
                }

                cupTeamStartingEleven.Add(cupPlayer);
            }
            SortedSet<CupPlayer> cupTeamStartingSubstitutes = new();
            foreach (var SubstitutePlayer in teamStatistics.Substitutes)
            {
                CupPlayer? cupPlayer = null;
                cupTeam._sortedPlayers.TryGetValue(CupPlayer.getComparable(SubstitutePlayer.ShirtNumber), out cupPlayer);

                if (cupPlayer == null)
                {
                    cupPlayer = new(SubstitutePlayer.ShirtNumber, SubstitutePlayer.Name, cupTeam, null);
                    cupTeam._sortedPlayers.Add(cupPlayer);
                }

                cupTeamStartingSubstitutes.Add(cupPlayer);
            }

            return new(teamStatistics.OnTarget, teamStatistics.OffTarget, teamStatistics.Blocked, teamStatistics.Woodwork,
                teamStatistics.Corners, teamStatistics.Offsides, teamStatistics.BallPossession, teamStatistics.PassAccuracy, teamStatistics.NumPasses, teamStatistics.PassesCompleted,
                teamStatistics.DistanceCovered, teamStatistics.BallsRecovered, teamStatistics.Tackles, teamStatistics.Clearances, teamStatistics.YellowCards, teamStatistics.RedCards,
                teamStatistics.FoulsCommitted, teamStatistics.Tactics, cupTeamStartingEleven, cupTeamStartingSubstitutes);
        }
    }
}
