using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Application.UseCases.Customers.Create.Input;

namespace API.Controllers.Requests;

[DataContract]
public sealed class CreateCustomerRequest
{
    [DataMember]
    [JsonInclude]
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [DataMember]
    [JsonInclude]
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    
    [DataMember]
    [JsonInclude]
    [JsonPropertyName("street")]
    public string? Street { get; set; }
    
    [DataMember]
    [JsonInclude]
    [JsonPropertyName("city")]
    public string? City { get; set; }
    
    [DataMember]
    [JsonInclude]
    [JsonPropertyName("state")]
    public string? State { get; set; }
    
    [DataMember]
    [JsonInclude]
    [JsonPropertyName("zipCode")]
    public string? ZipCode { get; set; }
    
    [DataMember]
    [JsonInclude]
    [JsonPropertyName("country")]
    public string? Country { get; set; }
    
    [DataMember]
    [JsonInclude]
    [JsonPropertyName("address")]
    public AddressInput? Address { get; set; }
    
    public CreateCustomerInput ToInput() => new(
        Name,
        Email,
        Address
    );
}