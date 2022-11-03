using System.Collections.Generic;

namespace HW.Models
{
    public class Room
    {
        public string RoomId { get; set; }
        public string Name { get; set; }
        public string TeacherId { get; set; }
        public string Description { get; set; }
        public ICollection<StudentRoom> StudentInfos { get; set; }
    }
}
