using System.Collections.Generic;

namespace HW.Models
{
    public class StudentClassViewModel
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public List<StudentInfo> Students { get; set; }
        public List<Room> Rooms { get; set; }
        public string TeacherId { get; set; }
        public  string  TeacherName { get; set; }

    }
}
