namespace EFDemo.Domain;

public class StoreImage
{
    public int Id { get; set; }
    public string Url { get; set; } = default!;

    public int StoreId { get; set; }
    public Store Store { get; set; } = default!;
}
