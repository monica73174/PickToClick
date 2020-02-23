using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class RosterRepository : IRosterData
    {
        public PluralsightDemoContext PluralsightDemoContext { get; }
        public RosterRepository(PluralsightDemoContext pluralsightDemoContext)
        {
            PluralsightDemoContext = pluralsightDemoContext;
                
        }
        public int Commit()
        {
            throw new NotImplementedException();
        }

        public Player DeletePlayerFromRoster(string id)
        {
            throw new NotImplementedException();
        }

        public Player GetPlayerById(string id)
        {
            throw new NotImplementedException();
        }



        public Player SavePlayerToRoster(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
