using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;

namespace Demo.ViewModels
{
    public class HomeViewModel
    {
        public Participant Participant { get; set; }
        public Player Pick { get; set; }
        public IEnumerable<PickDetail> LastTenPicks { get; set; }
        public IEnumerable<Player> Roster { get; set; }
        public IEnumerable<Participant> LeaderBoard { get; set; }
        public int CurrentGameId { get; set; }





    }
}
