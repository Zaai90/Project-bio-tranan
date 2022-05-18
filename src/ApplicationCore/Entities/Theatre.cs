namespace ApplicationCore.Entities;

public class Theatre : BaseEntity
{
    public string TheatreName { get; set; }
    public int AmountOfSeats { get; set; }

    public Theatre(int amountOfSeats, string theatreName)
    {
        AmountOfSeats = amountOfSeats;
        TheatreName = theatreName;
    }
    public Theatre()
    {
        //Empty constructor for EF
    }

}