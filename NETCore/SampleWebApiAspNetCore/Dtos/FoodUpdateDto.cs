
namespace SampleWebApiAspNetCore.Dtos
{
    public class ShoeUpdateDto
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Type { get; set; }
        public DateTime Created { get; set; }
    }
}
