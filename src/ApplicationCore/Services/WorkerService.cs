using ApplicationCore.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCore.Services;

public class WorkerService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public WorkerService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CleanseReservationsAsync();
            await Task.Delay(600000);
        }
    }

    public async Task CleanseReservationsAsync()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();
            var showService = scope.ServiceProvider.GetRequiredService<IShowService>();

            var reservations = await reservationService.GetAllReservationsAsync();

            foreach (var reservation in reservations)
            {
                var show = await showService.GetShowAsync(reservation.ShowId);
                if (show.ShowDateAndTime < DateTime.Now.AddDays(-14))
                {
                    await reservationService.RemoveReservationAsync(reservation);
                }
            }

        }
    }
}