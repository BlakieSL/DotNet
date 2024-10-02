namespace InterviewPreparation.Model;

public class Pyament
{
    public int IdPayment { get; set; }
    public DateTime Date { get; set; }
    public Client Client { get; set; }
    public int IdClient { get; set; }
    public Subscription Subscription { get; set; }
    public int IdSubscription { get; set; }
    public decimal Value { get; set; }
}