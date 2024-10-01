namespace TestApbd.Dtos;

public class DiscountDtoResponse
{
    public int IdDiscount { get; set; }
    public string Description { get; set; }
    public double Value { get; set; }
    public DateTime EndDate { get; set; }
}