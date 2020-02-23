using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Game
    {
        public int Id { get; set; }
        public int SeasonYear { get; set; }
        public DateTime DateOfGame { get; set; }
        public string Opponent { get; set; }
        public string Pitcher { get; set; }
        public string GameStatus { get; set; }
        public string HomeOrAway { get; set; }


    }
}
