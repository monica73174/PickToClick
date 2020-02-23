using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public interface ILeaderboardData
    {
        IEnumerable<Participant> GetTopTenPlayersByRank();
        IEnumerable<Participant> GetAllPlayersByRank();
    }
}
