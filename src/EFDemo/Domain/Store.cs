namespace EFDemo.Domain;

public class Store
{
    public int Id { get; private set; }
    public string Name { get; private set; } = default!;

    public Address? Address { get; private set; }

    public StoreImage? Image { get; set; }
    public List<Product> Products { get; set; } = new();

    public List<Brand> Brands { get; set; } = new();

    public Store(string name)
    {
        UpdateName(name);
    }

    public Store(string name, Address address)
    {
        UpdateName(name);
        Address = address;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}
