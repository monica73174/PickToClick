using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class PluralsightUsers
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
