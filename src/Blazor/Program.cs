using ApplicationCore.Services;
using ApplicationCore.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);

Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddHostedService<WorkerService>();

// builder.Services.Configure<KestrelServerOptions>(options =>
// {
//     options.ConfigureHttpsDefaults(options =>
//         options.ClientCertificateMode = ClientCertificateMode.RequireCertificate);
// });

// builder.Services.AddAuthentication(
//         CertificateAuthenticationDefaults.AuthenticationScheme)
//     .AddCertificate();

builder.Services.AddHttpClient();



var app = builder.Build();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
