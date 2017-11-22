using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
    }
}