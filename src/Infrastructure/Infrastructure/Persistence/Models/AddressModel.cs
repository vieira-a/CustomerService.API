namespace Infrastructure.Persistence.Models;

public sealed class AddressModel : BaseModel
{
    public Guid AddressId { get; init; }
    
    public required string Street { get; init; }
    
    public required string City { get; init; }
    
    public required string State { get; init; }
    
    public required string ZipCode { get; init; }
    
    public required string Country { get; init; }
    
    public required Guid CustomerId { get; init; }
    
    public CustomerModel? Customer { get; init; }
}