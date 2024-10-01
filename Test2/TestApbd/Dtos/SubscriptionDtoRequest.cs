namespace TestApbd.Dtos;

public class SubscriptionDtoRequest
{
    public int IdClient { get; set; }
    public int IdSubscription { get; set; }
    public int RenewalPeriod { get; set; }
}