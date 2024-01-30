using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class QueryRequest
    {
        [Required,DataType(DataType.MultilineText)]
        public string? Question { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Answer { get; set; }
    }
}
