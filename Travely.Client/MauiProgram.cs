using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Travely.BusinessLogic.Services;
using Travely.Client.Utilities;
using Travely.Domain;

namespace Travely.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

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

            return builder.Build();
        }
    }
}
