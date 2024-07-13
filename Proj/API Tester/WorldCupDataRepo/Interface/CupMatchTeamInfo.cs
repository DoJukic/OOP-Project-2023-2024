using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Tester.WorldCupDataRepo
{
    public class CupMatchTeamInfo
    {
        public readonly CupTeam team;
        public readonly long goals;
        public readonly long penalties;

        public CupMatchTeamInfo(CupTeam team, long? goals, long? penalties)
        {
            this.team = team;
            this.goals = goals ?? 0;
            this.penalties = penalties ?? 0;
        }
    }
}
