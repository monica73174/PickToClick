using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Stat
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int Runs { get; set; }
        public int Doubles { get; set; }
        public int Triples { get; set; }
        public int HomeRuns { get; set; }
        public int BaseOnBalls { get; set; }
        public int Hits { get; set; }
        public int HitByPitch { get; set; }
        public int RBI { get; set; }


    }
}
