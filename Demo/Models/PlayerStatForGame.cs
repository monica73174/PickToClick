using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class PlayerStatForGame
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int Walks { get; set; }
        public int Singles { get; set; }
        public int Doubles { get; set; }
        public int Triples { get; set; }
        public int HomeRuns { get; set; }
        public int Rbis { get; set; }
    }
}
