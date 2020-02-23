using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class AdminController : Controller
    {
        public IGameData GameData { get; }

        public AdminController(IGameData gameData)
        {
            GameData = gameData;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTodaysGameAsync()
        {
            var game = await GetGameIdAsync();
            return View();
        }

        private async Task<Game> GetGameIdAsync()
        {

            //string today = DateTime.Today.ToString();
            string today = "02/25/2020";
            
            var game = await GameData.GetCurrentGame(today);
            return game;

        }
    }
}