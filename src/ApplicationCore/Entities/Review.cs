using System.Linq;
namespace ApplicationCore.Entities;


public class Review : BaseEntity
{
    public DateTime ReviewDate { get; set; }
    public int MovieId { get; set; }
    public string ReviewText { get; set; }
    public int Rating { get; set; }
    public string Email { get; set; }

    public Review()
    {
        //Empty constructor for EF
    }
}