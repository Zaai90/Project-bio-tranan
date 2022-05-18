using System.Text.Json.Serialization;

namespace ApplicationCore.Entities;

public class Movie : BaseEntity
{
    public string Runtime { get; set; }
    public string Title { get; set; }
    public string Plot { get; set; }
    public string Language { get; set; }
    public string Subtitles { get; set; }
    public int Duration { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Actors { get; set; }
    public string Year { get; set; }
    public string Rated { get; set; }
    [JsonPropertyName("Poster")]
    public string PosterUrl { get; set; }
    public int TimesPlayed { get; set; } = 0;
    public int TimesToBePlayed { get; set; }

    public Movie()
    {
        //Empty constructor for EF
    }

    public string GetAgeRating()
    {
        if (Rated == "G")
        {
            return "Barntillåten";
        }
        else if (Rated == "PG")
        {
            return "7 år";
        }
        else if (Rated == "PG-13")
        {
            return "11 år";
        }
        else if (Rated == "R")
        {
            return "15 år";
        }
        else if (Rated == "NC-17")
        {
            return "18 år";
        }
        else
        {
            return "N/A";
        }
    }
}