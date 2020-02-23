using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demo.Models;
using Demo.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        public IParticipantData ParticipantData { get; }
        public IPlayerData PlayerRoster { get; }
        public IStatData StatData { get; }

        private readonly UserManager<PluralsightUser> userManager;
        private readonly IPickToClickData PickToClickData;
        private readonly IRosterData RosterData;
        private readonly ILeaderboardData LeaderboardData;
        private readonly IGameData gameData;
        private readonly IPlayerData PlayerData;


        public HomeController(UserManager<PluralsightUser> userManager, IParticipantData participantData, IPickToClickData pickToClickData, IRosterData rosterData, IPlayerData playerData, ILeaderboardData leaderboardData, IGameData gameData, IStatData statData)
        {
            ParticipantData = participantData;
            this.userManager = userManager;
            PickToClickData = pickToClickData;
            RosterData = rosterData;
            PlayerRoster = playerData;
            LeaderboardData = leaderboardData;
            this.gameData = gameData;
            StatData = statData;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var id = GetUserId();
            var vm = new HomeViewModel();
            var game = await GetGameIdAsync();

            vm.CurrentGameId = game.Id;
            vm.Participant = ParticipantData.GetParticipantById(id);
            vm.Pick = await PickToClickData.GetPickToClickForParticipantByGame(game.Id.ToString(), id);
            vm.LastTenPicks = await PickToClickData.LastTenPickToClicksByParticipant(id, game);
            vm.LeaderBoard = LeaderboardData.GetTopTenPlayersByRank();

            var stat = await StatData.GetStatsByGame(game);
            //var last10 = PickToClickData.LastTenPickToClicksByParticipant(id);
            // vm.PickToClick.LastTen = new List<PickToClick>();

            //if(last10 != null)
            //{
            //    vm.PickToClick.LastTen = last10;
            //}

            if (vm.Participant == null)
            {
                vm.Participant = new Participant()
                {
                    FirstName = "",
                    LastName = "",
                    NickName = "",
                    Id = Guid.Parse(id),
                    Image = ""
                };
            }
            if(vm.Pick == null)
            {
                vm.Pick = new Player()
                {
                    FirstName = "",
                    LastName = ""
                    
                };
            }
            return View(vm);
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";



                           return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    user = new PluralsightUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName
                    };

                    var result = await userManager.CreateAsync(user, model.Password);
                }

                return View("Success");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    Thread.CurrentPrincipal = claimsPrincipal;
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Invalid UserName or Password");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("cookies");//userManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ProfileForm()
        {
            var id = GetUserId();
            var participant = ParticipantData.GetParticipantById(id);
            if (participant == null)
            {
                participant = new Participant()
                {
                    FirstName = "",
                    LastName = "",
                    NickName = "",
                    Id = Guid.Parse(id),
                    Image = ""
                };
            }
            return View(participant);
        }

        [HttpPost]
        public IActionResult ProfileForm(Participant participant)
        {
            var id = GetUserId();
            
            if (ParticipantData.GetParticipantById(id) == null)
            {
                ParticipantData.CreateParticipant(participant);
            }
            else
            {
                ParticipantData.SaveParticipant(participant);
            }

            ParticipantData.Commit();
            return View(participant);
        }

        [HttpGet]
        public async Task<IActionResult> PickToClickFormAsync()
        {
            var participant = GetUserId();
            var game = await GetGameIdAsync();
            var pick = await PickToClickData.GetPickToClickForParticipantByGame(game.Id.ToString(), participant);
            var pickToClick = new PickToClick();
            pickToClick.Players = await PickToClickData.GetRosterByGame(game.Id.ToString());
            pickToClick.PlayerId = pick == null ? 0 : pick.Id;
            pickToClick.GameId = int.Parse(game.Id.ToString());

            return View(pickToClick);
        }

        [HttpPost]
        public async Task<IActionResult> PickToClickFormAsync(PickToClick pickToClick)
        {
            var participant = GetUserId();
            var pick = new PickToClick() { PlayerId = pickToClick.PlayerId, ParticipantId = participant };
          
            pickToClick.ParticipantId = participant;
            pickToClick.GameId = pickToClick.GameId;
        
            var pTC = await PickToClickData.GetPickToClickForParticipantByGame(pickToClick.GameId.ToString(), participant);
            if(pTC.Id == 0)
            {

                PickToClickData.CreatePickToClick(pickToClick);
            }
            else
            {
                PickToClickData.SavePickToClick(pickToClick);
            }
            
            pick.Players = await PickToClickData.GetRosterByGame(pickToClick.GameId.ToString());

            return View(pick);
        }

        [HttpGet]
        public async Task<IActionResult> AllPicks()
        {
            var participant = GetUserId();
            var game = await GetGameIdAsync();
            var pick = await PickToClickData.AllPickToClicksByParticipant(participant);
          
            return View(pick);
        }

        public IActionResult LeaderBoard()
        {
            var participants = LeaderboardData.GetAllPlayersByRank();
            return View(participants);
        }
        private string GetUserId()
        {
            try
            {
                var claim = (from c in User.Claims
                    where c.Type.Contains("nameidentifier")
                    select c.Value).Single();
                return claim;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private async Task<Game> GetGameIdAsync()
        {

            string today = DateTime.Today.ToString();
            string date = "05/29/2019";
            var game = await gameData.GetCurrentGame(date);
            return game;
  
        }
    }
}
