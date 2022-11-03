using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW.Models
{
    public class StudentInfo
    {
        [Key]
        public string StudentId { get; set; }
        public string UserId { get; set; }
        public string StudentName { get; set; }
        public ICollection<StudentRoom> Rooms { get; set; }
        public int TotalView { get; set; }
        public int TotalAccepted { get; set; }
        public int TotalError { get; set; }
        public int TotalWarning { get; set; }
    }
}
