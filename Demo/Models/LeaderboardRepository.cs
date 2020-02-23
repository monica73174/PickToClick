using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class LeaderboardRepository : ILeaderboardData
    {
        
        public PluralsightDemoContext PluralsightDemoContext { get; }
        public LeaderboardRepository(PluralsightDemoContext pluralsightDemoContext)
        {
            PluralsightDemoContext = pluralsightDemoContext;
        }

       

        public IEnumerable<Participant> GetAllPlayersByRank()
        {
            var participants = (from p in PluralsightDemoContext.Participant
                                select p).ToList();
            return participants;
        }

        public IEnumerable<Participant> GetTopTenPlayersByRank()
        {
            var participants = (from p in PluralsightDemoContext.Participant                                          
                                select p).ToList();
            return participants;
        }
    }
}

