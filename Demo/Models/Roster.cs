using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Roster
    {
        public IEnumerable<Player> Players { get; set; }
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }
    }
}
