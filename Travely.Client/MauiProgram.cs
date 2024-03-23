using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlite($"Filename={DatabasePath.GetDatabasePath()}",
                infrastructure => infrastructure.MigrationsAssembly(nameof(Travely.Domain))));

            return builder.Build();
        }
    }
}
