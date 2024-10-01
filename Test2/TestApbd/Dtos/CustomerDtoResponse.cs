namespace TestApbd.Dtos;

public class CustomerDtoResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public List<DiscountDtoResponse> DiscountDtoResponses { get; set; }
}