namespace EFDemo.Domain;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public List<Store> Stores { get; set; } = new();
}
