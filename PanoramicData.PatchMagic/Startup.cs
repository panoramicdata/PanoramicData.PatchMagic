using Microsoft.EntityFrameworkCore;

namespace PanoramicData.PatchMagic;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<CodeContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddControllers();
    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        app.MapControllers();
    }
}
