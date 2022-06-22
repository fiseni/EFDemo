namespace EFDemo.Api.Models;

public class StoreDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<ProductDto> Products { get; set; } = new();
}
