using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class CinemaContextSeed
{
    public static CinemaContext _context;

    public static async Task SeedAsync(CinemaContext context, ILogger logger)
    {
        context.Database.EnsureCreated();
        _context = context;

        if (!await context.Movies.AnyAsync())
        {
            await context.Movies.AddRangeAsync(GetPreconfiguredMovies());
            await context.SaveChangesAsync();
        }
        if (!await context.Theatres.AnyAsync())
        {
            await context.Theatres.AddRangeAsync(GetPreconfiguredTheatres());
            await context.SaveChangesAsync();
        }
        if (!await context.Shows.AnyAsync())
        {
            await context.Shows.AddRangeAsync(GetPreconfiguredShows());
            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<Theatre> GetPreconfiguredTheatres()
    {
        return new List<Theatre>
        {
            new Theatre(45, "Salong 1")
        };
    }

    private static IEnumerable<Show> GetPreconfiguredShows()
    {
        return new Show[]
        {
            new Show(1, 1, new DateTime(2022, 4, 20, 23, 0, 0), 100),
            new Show(1, 1, new DateTime(2022, 4, 22, 23, 0, 0), 100),
            new Show(1, 1, new DateTime(2022, 4, 21, 23, 0, 0), 100),
            new Show(1, 1, new DateTime(2022, 4, 23, 23, 0, 0), 100),
            new Show(1, 1, new DateTime(2022, 4, 24, 23, 0, 0), 100),
            new Show(1, 1, new DateTime(2022, 4, 25, 23, 0, 0), 100),
            new Show(2,1, new DateTime(2022, 4, 20, 14, 0, 0), 90),
            new Show(2,1, new DateTime(2022, 4, 21, 14, 0, 0), 90),
            new Show(2,1, new DateTime(2022, 4, 22, 14, 0, 0), 90),
            new Show(2,1, new DateTime(2022, 4, 23, 14, 0, 0), 90),
            new Show(2,1, new DateTime(2022, 4, 24, 14, 0, 0), 90),
            new Show(2,1, new DateTime(2022, 4, 25, 14, 0, 0), 90),
            new Show(3,1, new DateTime(2022, 4, 20, 20, 0, 0), 100),
            new Show(3,1, new DateTime(2022, 4, 21, 20, 0, 0), 100),
            new Show(3,1, new DateTime(2022, 4, 22, 20, 0, 0), 100),
            new Show(3,1, new DateTime(2022, 4, 23, 20, 0, 0), 100),
            new Show(3,1, new DateTime(2022, 4, 24, 20, 0, 0), 100),
            new Show(3,1, new DateTime(2022, 4, 25, 20, 0, 0), 100),
            new Show(4,1, new DateTime(2022, 4, 20, 17, 0, 0), 100),
            new Show(4,1, new DateTime(2022, 4, 21, 17, 0, 0), 100),
            new Show(4,1, new DateTime(2022, 4, 22, 17, 0, 0), 100),
            new Show(4,1, new DateTime(2022, 4, 23, 17, 0, 0), 100),
            new Show(4,1, new DateTime(2022, 4, 24, 17, 0, 0), 100),
            new Show(4,1, new DateTime(2022, 4, 25, 17, 0, 0), 100),
            new Show(5,1, new DateTime(2022, 4, 26, 23, 0, 0), 100),
            new Show(5,1, new DateTime(2022, 4, 27, 23, 0, 0), 100),
            new Show(5,1, new DateTime(2022, 4, 28, 23, 0, 0), 100),
            new Show(5,1, new DateTime(2022, 4, 29, 23, 0, 0), 100),
            new Show(5,1, new DateTime(2022, 4, 30, 23, 0, 0), 100),
        };

    }

    private static IEnumerable<Movie> GetPreconfiguredMovies()
    {
        return new List<Movie>
        {
            new Movie()
            {
                Title = "Into the Wild",
                Plot = "After graduating from Emory University, top student and athlete Christopher McCandless abandons his possessions, gives his entire $24,000 savings account to charity and hitchhikes to Alaska to live in the wilderness. Along the way, Christopher encounters a series of characters that shape his life.",
                Duration = 148,
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTAwNDEyODU1MjheQTJeQWpwZ15BbWU2MDc3NDQwNw@@._V1_SX300.jpg",
                Rated = "R",
                Genre = "Adventure, Biography, Drama",
                Director = "Sean Penn",
                Actors = "Emile Hirsch, Vince Vaughn, Catherine Keener",
                Year = "2007",
                TimesToBePlayed = 10,
                TimesPlayed = 6,
                Language = "Engelska",
                Subtitles = "Svenska"
            },
            new Movie()
            {
                Title = "Lejonkungen",
                Plot = "A young lion prince is cast out of his pride by his cruel uncle, who claims he killed his father. While the uncle rules with an iron paw, the prince grows up beyond the Savannah, living by a philosophy: No worries for the rest of your days. But when his past comes to haunt him, the young prince must decide his fate: Will he remain an outcast or face his demons and become what he needs to be?",
                Duration = 88,
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BYTYxNGMyZTYtMjE3MS00MzNjLWFjNmYtMDk3N2FmM2JiM2M1XkEyXkFqcGdeQXVyNjY5NDU4NzI@._V1_SX300.jpg",
                Rated = "G",
                Genre = "Animerad, Äventyr, Drama",
                Director = "Roger Allers, Rob Minkoff",
                Year = "1994",
                TimesToBePlayed = 10,
                TimesPlayed = 6,
                Language = "Svenska",
                Subtitles = "Svenska"
            },
            new Movie()
            {
                Title = "Inception",
                Plot = "Dom Cobb is a skilled thief, the absolute best in the dangerous art of extraction, stealing valuable secrets from deep within the subconscious during the dream state, when the mind is at its most vulnerable. Cobb's rare ability has made him a coveted player in this treacherous new world of corporate espionage, but it has also made him an international fugitive and cost him everything he has ever loved. Now Cobb is being offered a chance at redemption. One last job could give him his life back but only if he can accomplish the impossible, inception. Instead of the perfect heist, Cobb and his team of specialists have to pull off the reverse: their task is not to steal an idea, but to plant one. If they succeed, it could be the perfect crime. But no amount of careful planning or expertise can prepare the team for the dangerous enemy that seems to predict their every move. An enemy that only Cobb could have seen coming.",
                Duration = 148,
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
                Rated = "PG-13",
                Genre = "Action, Äventyr, Sci-Fi",
                Director = "Christopher Nolan",
                Actors = "Leonardo DiCaprio, Joseph Gordon-Levitt, Elliot Page, Tom Hardy",
                Year = "2010",
                TimesToBePlayed = 10,
                TimesPlayed = 6,
                Language = "Engelska",
                Subtitles = "Svenska"
            },

            new Movie()
            {
                Title = "Interstellar",
                Plot = "Earth's future has been riddled by disasters, famines, and droughts. There is only one way to ensure mankind's survival: Interstellar travel. A newly discovered wormhole in the far reaches of our solar system allows a team of astronauts to go where no man has gone before, a planet that may have the right environment to sustain human life.",
                Duration = 169,
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjIxNTU4MzY4MF5BMl5BanBnXkFtZTgwMzM4ODI3MjE@._V1_SY1000_CR0,0,675,1000_AL_.jpg",
                Rated = "PG-13",
                Genre = "Drama",
                Director = "Äventyr, Drama, Sci-Fi",
                Actors = "Matthew McConaughey, Anne Hathaway, Jessica Chastain, Michael Caine",
                Year = "2014",
                TimesToBePlayed = 10,
                TimesPlayed = 6,
                Language = "Engelska",
                Subtitles = "Svenska"
            },
            new Movie()
            {
                Title = "The Dark Knight",
                Plot = "Set within a year after the events of Batman Begins (2005), Batman, Lieutenant James Gordon, and new District Attorney Harvey Dent successfully begin to round up the criminals that plague Gotham City, until a mysterious and sadistic criminal mastermind known only as \"The Joker\" appears in Gotham, creating a new wave of chaos. Batman's struggle against The Joker becomes deeply personal, forcing him to \"confront everything he believes\" and improve his technology to stop him. A love triangle develops between Bruce Wayne, Dent, and Rachel Dawes.",
                Duration = 152,
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_SX300.jpg",
                Rated = "PG-13",
                Genre = "Action, Crime, Drama",
                Director ="",
                Actors = "Christian Bale, Heath Ledger, Aaron Eckhart",
                Year = "2008",
                TimesToBePlayed = 10,
                TimesPlayed = 5,
                Language = "Engelska",
                Subtitles = "Svenska"
            },
            new Movie()
            {
                Title = "Fight Club",
                Plot = "A nameless first person narrator (Edward Norton) attends support groups in attempt to subdue his emotional state and relieve his insomniac state. When he meets Marla (Helena Bonham Carter), another fake attendee of support groups, his life seems to become a little more bearable. However when he associates himself with Tyler (Brad Pitt) he is dragged into an underground fight club and soap making scheme. Together the two men spiral out of control and engage in competitive rivalry for love and power.",
                Duration = 139,
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BNDIzNDU0YzEtYzE5Ni00ZjlkLTk5ZjgtNjM3NWE4YzA3Nzk3XkEyXkFqcGdeQXVyMjUzOTY1NTc@._V1_SX300.jpg",
                Rated = "R",
                Genre = "Drama",
                Director = "David Fincher",
                Actors = "Brad Pitt, Edward Norton, Meat Loaf",
                Year = "1999",
                TimesToBePlayed = 10,
                TimesPlayed = 0,
                Language = "Engelska",
                Subtitles = "Svenska"

            },
            new Movie()
            {
                Title = "Shutter Island",
                Plot = "In 1954, up-and-coming U.S. marshal Teddy Daniels is assigned to investigate the disappearance of a patient from Boston's Shutter Island Ashecliffe Hospital. He's been pushing for an assignment on the island for personal reasons, but before long he thinks he's been brought there as part of a twisted plot by hospital doctors whose radical treatments range from unethical to illegal to downright sinister. Teddy's shrewd investigating skills soon provide a promising lead, but the hospital refuses him access to records he suspects would break the case wide open. As a hurricane cuts off communication with the mainland, more dangerous criminals \"escape\" in the confusion, and the puzzling, improbable clues multiply, Teddy begins to doubt everything - his memory, his partner, even his own sanity.",
                Duration = 138,
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BYzhiNDkyNzktNTZmYS00ZTBkLTk2MDAtM2U0YjU1MzgxZjgzXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg",
                Rated = "R",
                Genre = "Mystery, Thriller",
                Director = "Martin Scorsese",
                Year = "2010",
                TimesToBePlayed = 10,
                TimesPlayed = 0,
                Language = "Engelska",
                Subtitles = "Svenska"
            }

        };
    }
}