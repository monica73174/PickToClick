using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public interface IGameData
    {
        Task<Game> GetCurrentGame(string dateOfGame);
    }
}
