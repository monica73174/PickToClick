using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class StatRepository : IStatData
    {
        private readonly PluralsightDemoContext pluralsightDemoContext;
        private readonly IPickToClickData pickToClickData;

        public StatRepository(PluralsightDemoContext pluralsightDemoContext, IPickToClickData pickToClickData)
        {
            this.pluralsightDemoContext = pluralsightDemoContext;
            this.pickToClickData = pickToClickData;
        }
        public async Task<List<Stat>> GetStatsByGame(Game game)
        {
            var roster = await pickToClickData.GetRosterByGame(game.Id.ToString());
            var statUrl = $"http://statsapi.mlb.com/api/v1.1/game/{game.Id}/feed/live";

            var statList = new List<Stat>();
            // home -- teamStats -- players ID544725 -- stats
            HttpClient client = new HttpClient();
            //var roster = new List<Player>();

            Stream data2 = await client.GetStreamAsync(statUrl);
            StreamReader reader3 = new StreamReader(data2);
            string l = reader3.ReadToEnd();

            JObject rss2 = JObject.Parse(l.ToString());
            dynamic liveData = rss2["liveData"];
            dynamic boxscore = liveData.boxscore.teams;

            dynamic stats = game.HomeOrAway == "home" ? boxscore.home.players : boxscore.away.players;

            foreach(var player in roster)
            {
                var playerKey = $"ID{player.Id}";


                dynamic stat = stats[playerKey].stats.batting;
                var bb = stat.baseOnBalls;
                var doubles = stat.doubles;
                var hbp = stat.hitByPitch;
                var h = stat.hits;
                var hr = stat.homeRuns;
                var rbi = stat.rbi;
                var runs = stat.runs;
                var triples = stat.triples;

                statList.Add(new Stat { PlayerId = player.Id, GameId = game.Id, BaseOnBalls = bb, Doubles = doubles,
                HitByPitch = hbp, Hits = h, HomeRuns = hr, RBI = rbi, Runs = runs, 
                    Triples = triples
                });

            }
            

            //for (var i = 0; i < dates2.Count; i++)
            //{
            //    dynamic game = dates2[i];
            //    dynamic detail = (JArray)game.games;
            //    for (var j = 0; j < detail.Count; j++)
            //    {
            //        dynamic lineup = detail[j];
            //        dynamic homeLineup = (JArray)lineup.lineups.homePlayers;
            //        //dynamic awayLineup = (JArray)lineup.lineups.awayPlayers;


            //        //int awayTeam = detail[j].teams.away.team.id;
            //        //int homeTeam = detail[j].teams.home.team.id;
            //        for (var k = 0; k < homeLineup.Count; k++)
            //        {
            //            int playerid = homeLineup[k].id;
            //            string lastName = homeLineup[k].lastName;
            //            string firstName = homeLineup[k].useName;
            //            // Console.WriteLine($"{playerid} {lastName} {firstName}");
            //            roster.Add(new Player { Id = playerid, FirstName = firstName, LastName = lastName });
            //        }
            //    }

            //}
            //var roster = (from player in PluralsightDemoContext.Player
            //              join r in PluralsightDemoContext.Roster
            //                  on player.Id equals r.PlayerId
            //              where r.GameId.ToString() == id
            //              select player).ToList();
            //roster in pluralsightDemoContext.Roster.Where(p => p.GameId.ToString() == id);

            
            return await Task.FromResult(statList);
        }
    }
}
