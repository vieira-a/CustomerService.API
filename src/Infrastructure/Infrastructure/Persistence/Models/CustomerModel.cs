namespace Infrastructure.Persistence.Models;

public sealed class CustomerModel : BaseModel
{
    public Guid CustomerId { get; set; }
    
    public string? Name { get; set; }
    
    public string? Email { get; set; }
    
    public ICollection<AddressModel>? Addresses { get; set; } = new List<AddressModel>();
}