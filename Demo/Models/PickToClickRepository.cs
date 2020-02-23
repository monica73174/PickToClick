using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace Demo.Models
{
    public class PickToClickRepository : IPickToClickData
        {
        public PluralsightDemoContext PluralsightDemoContext { get; }
  

        public PickToClickRepository(PluralsightDemoContext pluralsightDemoContext)
        {
            PluralsightDemoContext = pluralsightDemoContext;
        }
            public IEnumerable<PickToClick> AllPickToClicksByGame(string id)
            {
                throw new NotImplementedException();
            }

            public async Task<List<PickDetail>> AllPickToClicksByParticipant(string id)
            {
            var games = (from p in PluralsightDemoContext.PickToClick where p.ParticipantId == id select p).ToList();
            var picks = new List<PickDetail>();

            if (games.Count > 0)
            {
                HttpClient client = new HttpClient();
                foreach (var g in games)
                {
                    var playerUrl = $"http://statsapi.mlb.com/api/v1/people/{g.PlayerId}";
                    Stream data2 = await client.GetStreamAsync(playerUrl);
                    StreamReader reader3 = new StreamReader(data2);
                    string l = reader3.ReadToEnd();

                    JObject rss = JObject.Parse(l.ToString());
                    dynamic p = (JArray)rss["people"];

                    var gameUrl = $"https://statsapi.mlb.com/api/v1/schedule?gamePk={g.GameId}";

                    Stream data3 = await client.GetStreamAsync(gameUrl);
                    StreamReader reader4 = new StreamReader(data3);
                    string gameData = reader4.ReadToEnd();

                    JObject rss2 = JObject.Parse(gameData.ToString());
                    dynamic e = (JArray)rss2["dates"];

                    int playerid = p[0].id;
                    string lastName = p[0].lastName;
                    string firstName = p[0].useName;
                    string gameDate = e[0].date;
                    // Console.WriteLine($"{playerid} {lastName} {firstName}");
                    picks.Add(new PickDetail
                    {
                        PlayerId = playerid.ToString(),
                        FirstName = firstName,
                        LastName = lastName,
                        DateOfGame = gameDate

                    });

                }
            }

            //var picks = (from p in PluralsightDemoContext.Player
            //            join c in PluralsightDemoContext.PickToClick
            //             on p.Id equals c.PlayerId
            //            where (c.ParticipantId == id)
            //            select new Player() { FirstName = p.FirstName, LastName = p.LastName, Id = p.Id }).Take(10).ToList();

            // var pick = PluralsightDemoContext.Player.Select(p >= new PickToClick() { FirstName = })

            return await Task.FromResult(picks);
        }

            public PickToClick CreatePickToClick(PickToClick pickToClick)
            {
                using (var connection = GetOpenConnection())
                {
                    connection.Execute(
                        "insert into PickToClick([PlayerId]," +
                        "[ParticipantId]," +
                        "[GameId])" +
                        "Values(@PlayerId,@ParticipantId,@GameId)",
                        new
                        {
                            PlayerId = pickToClick.PlayerId,
                            ParticipantId = pickToClick.ParticipantId,
                            GameId = pickToClick.GameId

                        }
                    );
                }

                return pickToClick;
            }

            public Participant DeletePickToClick(string id)
            {
                throw new NotImplementedException();
            }


            public static DbConnection GetOpenConnection()
            {
                var connection = new SqlConnection("Data Source=sql1-p4stl.ezhostingserver.com;Initial Catalog=PluralsightDemo;Persist Security Info=True;User ID=monica73174;Password=Rebuild2020!");
                connection.Open();

                return connection;
            }

        public async Task<Player> GetPickToClickForParticipantByGame(string game, string participant)
        {

            var pick = (from p in PluralsightDemoContext.PickToClick where p.GameId == int.Parse(game) && p.ParticipantId == participant select p).FirstOrDefault();
            //var player = (from p in PluralsightDemoContext.Player
            //            join c in PluralsightDemoContext.PickToClick
            //             on p.Id equals c.PlayerId
            //            where (c.GameId == int.Parse(game) && c.ParticipantId == participant)
            //            select new Player() { FirstName = p.FirstName, LastName = p.LastName, Id = p.Id}).FirstOrDefault();

            var player = new Player() { Id = 0};

            if (pick != null)
            {
                HttpClient client = new HttpClient();
                var playerUrl = $"http://statsapi.mlb.com/api/v1/people/{pick.PlayerId}";
                Stream data2 = await client.GetStreamAsync(playerUrl);
                StreamReader reader3 = new StreamReader(data2);
                string l = reader3.ReadToEnd();

                JObject rss = JObject.Parse(l.ToString());
                dynamic p = (JArray)rss["people"];
                
                int playerid = p[0].id;
                string lastName = p[0].lastName;
                string firstName = p[0].useName;
                // Console.WriteLine($"{playerid} {lastName} {firstName}");
                player.Id = playerid;
                player.FirstName = firstName;
                player.LastName = lastName;
            }
            // var pick = PluralsightDemoContext.Player.Select(p >= new PickToClick() { FirstName = })

           return await Task.FromResult(player);
        }

        public PickToClick SavePickToClick(PickToClick pickToClick)
        {
            using (var connection = GetOpenConnection())
            {
                connection.Execute(
                    $"UPDATE PickToClick SET  [PlayerId] = {pickToClick.PlayerId} " +
                    $"WHERE [ParticipantId] = '{pickToClick.ParticipantId}' AND [GameId] = {pickToClick.GameId} "    
                );
            }

            return pickToClick;
        }

        PickToClick IPickToClickData.DeletePickToClick(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Player>> GetRosterByGame(string id)
        {
            HttpClient client = new HttpClient();
            var roster = new List<Player>();
            var urlLineUp = $"https://statsapi.mlb.com/api/v1/schedule?gamePk={id}&language=en&hydrate=xrefId,lineups,broadcasts(all),probablePitcher(note),game(tickets)&useLatestGames=true&fields=dates,games,teams,probablePitcher,note,id,dates,games,broadcasts,type,name,homeAway,isNational,dates,games,game,tickets,ticketType,ticketLinks,dates,games,lineups,homePlayers,awayPlayers,useName,lastName,primaryPosition,abbreviation,dates,games,xrefIds,xrefId,xrefType";
            Stream data2 = await client.GetStreamAsync(urlLineUp);
            StreamReader reader3 = new StreamReader(data2);
            string l = reader3.ReadToEnd();

            JObject rss2 = JObject.Parse(l.ToString());
            dynamic dates2 = (JArray)rss2["dates"];

            for (var i = 0; i < dates2.Count; i++)
            {
                dynamic game = dates2[i];
                dynamic detail = (JArray)game.games;
                for (var j = 0; j < detail.Count; j++)
                {
                    dynamic lineup = detail[j];
                    dynamic homeLineup = (JArray)lineup.lineups.homePlayers;
                    //dynamic awayLineup = (JArray)lineup.lineups.awayPlayers;


                    //int awayTeam = detail[j].teams.away.team.id;
                    //int homeTeam = detail[j].teams.home.team.id;
                    for (var k = 0; k < homeLineup.Count; k++)
                    {
                        int playerid = homeLineup[k].id;
                        string lastName = homeLineup[k].lastName;
                        string firstName = homeLineup[k].useName;
                        // Console.WriteLine($"{playerid} {lastName} {firstName}");
                        roster.Add(new Player { Id = playerid, FirstName = firstName, LastName = lastName });
                    }
                }

            }
            //var roster = (from player in PluralsightDemoContext.Player
            //              join r in PluralsightDemoContext.Roster
            //                  on player.Id equals r.PlayerId
            //              where r.GameId.ToString() == id
            //              select player).ToList();
            //roster in pluralsightDemoContext.Roster.Where(p => p.GameId.ToString() == id);
            return await Task.FromResult(roster);
        }

        IEnumerable<Player> IPickToClickData.AllPickToClicksByGame(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PickDetail>> LastTenPickToClicksByParticipant(string id, Game game)
        {
            var games = (from p in PluralsightDemoContext.PickToClick where p.GameId != game.Id select p).ToList();
            var picks = new List<PickDetail>();

            if (games.Count > 0)
            {
                HttpClient client = new HttpClient();
                foreach (var g in games)
                {
                    var playerUrl = $"http://statsapi.mlb.com/api/v1/people/{g.PlayerId}";
                    Stream data2 = await client.GetStreamAsync(playerUrl);
                    StreamReader reader3 = new StreamReader(data2);
                    string l = reader3.ReadToEnd();

                    JObject rss = JObject.Parse(l.ToString());
                    dynamic p = (JArray)rss["people"];

                    var gameUrl = $"https://statsapi.mlb.com/api/v1/schedule?gamePk={g.GameId}";

                    Stream data3 = await client.GetStreamAsync(gameUrl);
                    StreamReader reader4 = new StreamReader(data3);
                    string gameData = reader4.ReadToEnd();

                    JObject rss2 = JObject.Parse(gameData.ToString());
                    dynamic e = (JArray)rss2["dates"];

                    int playerid = p[0].id;
                    string lastName = p[0].lastName;
                    string firstName = p[0].useName;
                    string gameDate = e[0].date;
                    // Console.WriteLine($"{playerid} {lastName} {firstName}");
                    picks.Add(new PickDetail { 
                        PlayerId = playerid.ToString(),
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfGame = gameDate

                });

                }
            }

            //var picks = (from p in PluralsightDemoContext.Player
            //            join c in PluralsightDemoContext.PickToClick
            //             on p.Id equals c.PlayerId
            //            where (c.ParticipantId == id)
            //            select new Player() { FirstName = p.FirstName, LastName = p.LastName, Id = p.Id }).Take(10).ToList();

            // var pick = PluralsightDemoContext.Player.Select(p >= new PickToClick() { FirstName = })

            return await Task.FromResult(picks);
        }
    }
    
}
