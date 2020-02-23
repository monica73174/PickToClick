using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public interface IRosterData
    {
      
        Player GetPlayerById(string id);
        Player SavePlayerToRoster(Player player);
        Player DeletePlayerFromRoster(string id);
        int Commit();
    }
}
