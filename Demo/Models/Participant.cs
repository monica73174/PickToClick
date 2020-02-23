using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Participant
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Image { get; set; }
    }
}
