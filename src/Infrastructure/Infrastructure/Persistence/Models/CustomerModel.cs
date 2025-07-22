namespace Infrastructure.Persistence.Models;

public sealed class CustomerModel : BaseModel
{
    public Guid CustomerId { get; init; }
    
    public required string Name { get; init; }
    
    public required string Email { get; init; }
    
    public ICollection<AddressModel>? Addresses { get; init; } = new List<AddressModel>();
}