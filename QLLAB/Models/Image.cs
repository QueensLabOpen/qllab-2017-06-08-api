using System.ComponentModel.DataAnnotations;

namespace QLLAB.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Tags { get; set; }
        public string Filename { get; set; }
        public string Url { get; set; }
    }
}
