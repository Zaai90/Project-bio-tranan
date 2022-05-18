namespace ApplicationCore.Entities;

public class Show : BaseEntity
{
    public Movie Movie { get; set; }
    public int MovieId { get; set; }
    public Theatre Theatre { get; set; }
    public int TheatreId { get; set; }
    public DateTime ShowDateAndTime { get; set; }
    public DateTime ShowEnds { get { return ShowDateAndTime.AddMinutes(Movie.Duration); } }
    public int Price { get; set; }
    public string ShowDate => ShowDateAndTime.ToString("d MMMM");
    public string ShowTime => ShowDateAndTime.ToString("HH:mm");
    // public bool IsLimited { get; set; } = false;
    // public int LimitedProcent { get; set; }

    public Show(int movieId, int theatreId, DateTime playingTime, int price)
    {
        MovieId = movieId;
        TheatreId = theatreId;
        ShowDateAndTime = playingTime;
        Price = price;
    }

    public Show()
    {
        //Empty constructor for EF
    }

}

public class ShowRequest
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int TheatreId { get; set; }
    public DateTime ShowDateAndTime { get; set; }
    public int Price { get; set; }

    public ShowRequest()
    {
    }
}