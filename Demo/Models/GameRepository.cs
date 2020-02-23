using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class GameRepository : IGameData
    {
        public async Task<Game> GetCurrentGame(string dateOfGame)
        {
            var gameInfo = new Game();
            HttpClient client = new HttpClient();
            var gameUrl = $"http://statsapi.mlb.com/api/v1/schedule?lang=en&sportId=1&hydrate=team(venue(timezone)),venue(timezone),game(seriesStatus,seriesSummary,tickets,promotions,sponsorships,content(summary,media(epg))),seriesStatus,seriesSummary,linescore,tickets,event(tickets),radioBroadcasts,broadcasts(all)&season=2020&teamId=145&scheduleTypes=games,events,xref&startDate={dateOfGame}&endDate={dateOfGame}&eventTypes=primary";
            Stream data2 = await client.GetStreamAsync(gameUrl);
            StreamReader reader3 = new StreamReader(data2);
            
            string s = reader3.ReadToEnd();

            JObject rss = JObject.Parse(s.ToString());

            dynamic dates = (JArray)rss["dates"];

            for (var i = 0; i < dates.Count; i++)
            {
                dynamic game = dates[i];
                dynamic detail = (JArray)game.games;
                for (var j = 0; j < detail.Count; j++)
                {
                    int gameid = detail[j].gamePk;
                    string gameDate = detail[j].gameDate;
                    int awayTeam = detail[j].teams.away.team.id;
                    int homeTeam = detail[j].teams.home.team.id;

                    gameInfo.HomeOrAway = homeTeam == 145 ? "home" : "away";
                    gameInfo.Id = gameid;
                    gameInfo.DateOfGame = DateTime.Parse(gameDate);
                    gameInfo.GameStatus = "";

                    //TODO deal with status of game. Could be a double header
                   // Console.WriteLine($"{gameid} {gameDate} {awayTeam} {homeTeam}");
                }

            }
            return await Task.FromResult(gameInfo);
        }
    }
}
