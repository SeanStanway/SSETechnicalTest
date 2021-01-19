using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ProductApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File("logFile.txt")
                .CreateLogger();

            Log.Information("Starting web api");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //public IUnityContainer UnitySetup()
        //{
        //    UnityContainer container = new UnityContainer();
        //    container
        //    container.RegisterType<IProductsSql>(return new ProductsSql().SetupSql(new SqlConnection()));
        //}
    }
}
