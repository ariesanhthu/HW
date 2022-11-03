using System.Collections.Generic;

namespace HW.Models
{
    public class LibraryViewModel
    {
        public List<LibraryArticalClass> Articals { get; set; }
        public int SubjectId { get; set; }
        public int CreditId { get; set; }
        public int CategoryId { get; set; }
        public string Searching { get; set; }
        public int Level { get; set; }
    }
}
