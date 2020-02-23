using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PlayerNumber { get; set; }
    }
}
