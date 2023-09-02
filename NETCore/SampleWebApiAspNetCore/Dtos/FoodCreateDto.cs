using System.ComponentModel.DataAnnotations;

namespace SampleWebApiAspNetCore.Dtos
{
    public class ShoeCreateDto
    {
        [Required]
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int Price { get; set; }
        public DateTime Created { get; set; }
    }
}
