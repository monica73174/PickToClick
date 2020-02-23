using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class PickToClick
    {
        public IEnumerable<PickToClick> LastTen { get; set; }

        public IEnumerable<PickToClick> AllPicks { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string ParticipantId { get; set; }
        public int GameId { get; set; }

        public string DateOfGame { get; set; }

    }
}
