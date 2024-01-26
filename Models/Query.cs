using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Query
    {
        [Key]
        [Required]
        public int QID { get; set; }
        [Required,DataType(DataType.MultilineText)]
        public string? Question { get; set; }
        [Required,DataType(DataType.MultilineText)]
        public string? Answer { get; set; }
        // Navigation property for the foreign key relationship
        [ForeignKey("UID")]
        public int UID { get; set; }
        public virtual required User User { get; set; }
    }
}
