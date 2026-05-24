
using Business.Interfaces;
using Microsoft.Extensions.Hosting;
namespace LärplattformApi.Worker
{
    public class DatabaseWorker : BackgroundService
    {
        public readonly IServiceProvider _serviceProvider;

        public DatabaseWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

      protected override async Task ExecuteAsync(CancellationToken stoppingtoken)
        {
            while(!stoppingtoken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var ControlService = scope.ServiceProvider.GetRequiredService<IDatabaseControllInterface>();
                    await ControlService.CheckForData();
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingtoken);
            }
        }
    }
}
