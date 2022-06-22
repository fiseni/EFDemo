namespace EFDemo.Domain;

public class Store
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public Address? Address { get; set; }

    public StoreImage? Image { get; set; }
    public List<Product> Products { get; set; } = new();

    public List<Brand> Brands { get; set; } = new();
}
