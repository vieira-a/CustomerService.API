namespace Infrastructure.Persistence.Models;

public sealed class AddressModel : BaseModel
{
    public Guid AddressId { get; set; }
    
    public string? Street { get; set; }
    
    public string? City { get; set; }
    
    public string? State { get; set; }
    
    public string? ZipCode { get; set; }
    
    public string? Country { get; set; }
    
    public Guid? CustomerId { get; set; }
    
    public CustomerModel? Customer { get; set; }
}