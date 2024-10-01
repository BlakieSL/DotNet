namespace TestPrep2.Models.DTO;

public class ReservationDto
{
 public int IdReservation { get; set; }
 public int IdClient { get; set; }
 public DateTime DateFrom { get; set; }
 public DateTime DateTo { get; set; }
 public int Capacity { get; set; }
 public int NumOfBoats { get; set; }
 public bool FulFilled { get; set; }
 public float Price { get; set; }
 public string CancelReason { get; set; }
 public int IdBoatStandard { get; set; }
}