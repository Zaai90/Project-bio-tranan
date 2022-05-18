namespace ApplicationCore.Entities;


public class ShowRestriction : BaseEntity
{
    public int ShowId { get; set; }
    public int RemovedSeats { get; set; }

    public ShowRestriction()
    {
        //Empty constructor for EF
    }

}