using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class PlayerRepository : IPlayerData
    {
        private readonly PluralsightDemoContext pluralsightDemoContext;

        public PlayerRepository(PluralsightDemoContext pluralsightDemoContext)
        {
            this.pluralsightDemoContext = pluralsightDemoContext;
        }
        public Player GetPlayerById(int id)
        {
            var player = (from p in pluralsightDemoContext.Player where p.Id == id select p).FirstOrDefault();
            return player;
        }
    }
}
