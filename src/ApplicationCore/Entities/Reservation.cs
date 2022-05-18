namespace ApplicationCore.Entities;


public class Reservation : BaseEntity
{
    public Guid ReservNr { get; set; }
    public int ShowId { get; set; }
    public int BookedSeats { get; set; }
    public int TotalPrice { get; set; }
    public string Email { get; set; }
    public DateTime TimeStamp { get; set; }

    public Reservation()
    {
        //Empty constructor for EF
    }

}

public class ReservationRequest
{
    public int Id { get; set; }
    public int ShowId { get; set; }
    public int BookedSeats { get; set; }
    public string Email { get; set; }

    public ReservationRequest()
    {
    }
}