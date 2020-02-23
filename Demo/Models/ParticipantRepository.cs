using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Demo.Models
{
    public class ParticipantRepository : IParticipantData
    {
        public PluralsightDemoContext PluralsightDemoContext { get; }

        public ParticipantRepository(PluralsightDemoContext pluralsightDemoContext)
        {
            PluralsightDemoContext = pluralsightDemoContext;
        }

        public IEnumerable<Participant> AllParticipants => PluralsightDemoContext.Participant;

        public Participant CreateParticipant(Participant participant)
        {

            using (var connection = GetOpenConnection())
            { 
                connection.Execute(
                    "insert into Participant([Id]," +
                    "[FirstName]," +
                    "[LastName]," +
                    "[NickName]) " +
                    "Values(@id,@FirstName,@LastName,@NickName)",
                    new
                    {
                        id = participant.Id,
                        FirstName = participant.FirstName,
                        LastName = participant.LastName,
                        NickName = participant.NickName
                    }
                );
            }
            //var p = new Participant()
            //{
            //    Id = id,
            //    FirstName = participant.FirstName,
            //    LastName = participant.LastName,
            //    NickName = participant.NickName,
            //    Image = participant.Image

            //};
            //PluralsightDemoContext.Add(participant);
            return participant;
        }

        public Participant DeleteParticipant(string id)
        {
            throw new NotImplementedException();
        }

        public Participant GetParticipantById(string id)
        {
           
           return PluralsightDemoContext.Participant.FirstOrDefault(p => p.Id == Guid.Parse(id));
        
        }

        public Participant SaveParticipant(Participant participant)
        {

            using (var connection = GetOpenConnection())

            {
                connection.Execute(
                    "UPDATE Participant " +
                    $"SET [FirstName] = '{participant.FirstName}', " +
                    $"[LastName] = '{participant.LastName}', " +
                    $"[NickName] = '{participant.NickName}' " +
                    $"WHERE ID = '{participant.Id}' "   
                );
            }
          
            return participant;
        }

        public int Commit()
        {
            PluralsightDemoContext.SaveChanges();
            return 0;
        }

        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection("Data Source=sql1-p4stl.ezhostingserver.com;Initial Catalog=PluralsightDemo;Persist Security Info=True;User ID=monica73174;Password=Rebuild2020!");
            connection.Open();

            return connection;
        }
    }
}
