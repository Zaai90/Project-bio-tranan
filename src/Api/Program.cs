using Infrastructure.Data;
using Infrastructure;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;

var builder = WebApplication.CreateBuilder(args);

Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IShowService, ShowService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var scopedProvider = scope.ServiceProvider;
        try
        {
            var cinemaContext = scopedProvider.GetRequiredService<CinemaContext>();
            await CinemaContextSeed.SeedAsync(cinemaContext, app.Logger);
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
