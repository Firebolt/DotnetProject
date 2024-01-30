using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Query
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QID { get; set; }
        [Required,DataType(DataType.MultilineText)]
        public string? Question { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Answer { get; set; }
        // Navigation property for the foreign key relationship
        [ForeignKey("Id")]
        public string Id { get; set; }
        public virtual required User User { get; set; }
    }
}
