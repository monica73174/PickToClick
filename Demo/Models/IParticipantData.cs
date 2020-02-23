using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public interface IParticipantData
    {
        IEnumerable<Participant> AllParticipants { get; }
        Participant GetParticipantById(string id);
        Participant SaveParticipant(Participant participant);
        Participant DeleteParticipant(string id);
        Participant CreateParticipant(Participant participant);
        int Commit();


    }
}
