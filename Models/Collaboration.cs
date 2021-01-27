using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MusicCollaboration.Areas.Identity.Data;

namespace MusicCollaboration.Models
{
    public class Collaboration
    {
        public Collaboration()
        {
            Participants = new List<MusicCollaborationUser>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public string OwnerID { get; set; }
        public MusicCollaborationUser Owner { get; set; }
        public ICollection<MusicCollaborationUser> Participants { get; set; }
        public int Bpm { get; set; }
        public string SongKey { get; set; }
        public CollaborationGenre Genre { get; set; }
        public CollaborationType Type { get; set; }
    }
}
