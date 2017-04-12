using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class Item
    {
        [Key]
        public int id { get; set; }
        public string text { get; set; }
    }
}