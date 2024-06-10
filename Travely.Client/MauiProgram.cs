using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Travely.BusinessLogic.Services;
using Travely.Client.Utilities;
using Travely.Domain;
using Microcharts.Maui;

namespace Travely.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMicrocharts();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            string databasePath = $"Data Source={DatabasePath.GetDatabasePath()}";

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlite(databasePath,
                infrastructure => infrastructure.MigrationsAssembly("Travely.Domain")));

            builder.Services.AddSingleton<TripService>(provider =>
            {
                var context = provider.GetRequiredService<AppDbContext>();
                return new TripService(context);
            });

            builder.Services.AddSingleton<PackingService>(provider =>
            {
                var context = provider.GetRequiredService<AppDbContext>();
                return new PackingService(context);
            });

            builder.Services.AddSingleton<TripDetailService>(provider =>
            {
                var context = provider.GetRequiredService<AppDbContext>();
                return new TripDetailService(context);
            });

            builder.Services.AddSingleton<StatisticService>(provider =>
            {
                var context = provider.GetRequiredService<AppDbContext>();
                return new StatisticService(context);
            });

            return builder.Build();
        }
    }
}
