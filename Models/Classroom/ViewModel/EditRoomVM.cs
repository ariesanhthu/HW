using System.Collections.Generic;

namespace HW.Models
{
    public class EditRoomVM
    {
        public Room Room { get; set; }
        public string RoomId { get; set; }
        public string TeacherId { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
    }
}
