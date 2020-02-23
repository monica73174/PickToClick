using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public interface IPickToClickData
    {
        IEnumerable<Player> AllPickToClicksByGame(string id);
        Task<List<PickDetail>> AllPickToClicksByParticipant(string id);
        Task<List<PickDetail>> LastTenPickToClicksByParticipant(string id, Game game);
        Task<Player> GetPickToClickForParticipantByGame(string game, string player);
        PickToClick SavePickToClick(PickToClick pickToClick);
        PickToClick DeletePickToClick(string id);
        PickToClick CreatePickToClick(PickToClick pickToClick);
        Task<List<Player>> GetRosterByGame(string id);

    }
}
