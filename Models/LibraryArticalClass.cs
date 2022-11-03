using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW.Models
{
    public class LibraryArticalClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual SubjectArtical SubjectArtical { get; set; }
        public List<ArticalCredit> TagCreadits { get; set; }
        public List<Category> TagCategories { get; set; }
    }
}
