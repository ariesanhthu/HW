namespace HW.Models
{
    public class StudentRoom
    {
        public string StudentId { get; set; }
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public StudentInfo StudentInfo { get; set; }
        public Room Room { get; set; }
    }
}
