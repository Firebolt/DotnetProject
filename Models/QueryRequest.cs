using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class QueryRequest
    {
        [Required]
        public int UID { get; set; }
        [Required,DataType(DataType.MultilineText)]
        public string? Question { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string? Answer { get; set; }
    }
}
