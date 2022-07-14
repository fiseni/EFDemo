namespace EFDemo.Api.Models;

public class StoreDto
{
    public int Id { get; set; }
    public string StoreName { get; set; } = default!;
    public string? Street { get; set; }
    public string? City { get; set; }
    public List<ProductDto> Products { get; set; } = new();
}
