﻿using APITester.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace APITester
{
    public interface IWorldCupDataRepo
    {
        public ReadonlySortedSet<CupGroup> GetCupGroups();
        public ReadonlySortedSet<CupMatch> GetCupMatches();
        public ReadonlySortedSet<CupTeam> GetCupTeams();
    }
}
