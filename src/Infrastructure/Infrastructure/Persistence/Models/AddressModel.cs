namespace Infrastructure.Persistence.Models;

public sealed class AddressModel : BaseModel
{
    public Guid AddressId { get; set; }

    public required string Street { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }

    public required string ZipCode { get; set; }

    public required string Country { get; set; }
    
    public required bool IsMain { get; set; }

    public required Guid CustomerId { get; set; }

    public CustomerModel? Customer { get; init; }
}