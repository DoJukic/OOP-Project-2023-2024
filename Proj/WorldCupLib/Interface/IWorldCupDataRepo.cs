﻿using TooManyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupLib
{
    public interface IWorldCupDataRepo
    {
        public List<String> GetErrorList();
        public ReadonlySortedSet<CupGroup> GetCupGroups();
        public ReadonlySortedSet<CupMatch> GetCupMatches();
        public ReadonlySortedSet<CupTeam> GetCupTeams();
    }
}
