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
        public MusicCollaborationUser UserID { get; set; }
        public Collaboration Collaboration { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        ProjectOwner,
        Vocalist,
        Instrumentalist,
        Producer,
        MixingEngineer,
        MasteringEngineer
    }
}
