using System.Text.Json.Serialization;

namespace EFDemo.Domain;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public int StoreId { get; set; }

    [JsonIgnore]
    public Store Store { get; set; } = default!;
}
