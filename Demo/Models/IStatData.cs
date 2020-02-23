using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public interface IStatData
    {
        Task<List<Stat>> GetStatsByGame(Game game);
    }
}
