namespace EFDemo.Domain;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public int? ParentId { get; set; }
    public Customer Parent { get; set; } = default!;

    public List<Customer> Children { get; set; } = new();
}
