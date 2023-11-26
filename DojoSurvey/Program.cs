var builder = WebApplication.CreateBuilder(args);
// app configuration

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(    
    name: "default",    
    pattern: "{controller=Home}/{action=Index}/{id?}");



// app.MapGet("/home/state", () => "Hello from /home/state!");
// app.MapGet("/", () => "Hello World! from / ");

namespace DojoSurvey
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

// last line
