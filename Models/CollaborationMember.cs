using MusicCollaboration.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaboration.Models
{
    public class CollaborationMember
    {
        public string UserID { get; set; }
        public MusicCollaborationUser User { get; set; }
        public int CollaborationID { get; set; }
        public Collaboration Collaboration { get; set; }
        public CollaborationUserRole Role { get; set; }
    }

    public enum CollaborationUserRole
    {
        ProjectOwner,
        Vocalist,
        Instrumentalist,
        Producer,
        MixingEngineer,
        MasteringEngineer
    }
}
