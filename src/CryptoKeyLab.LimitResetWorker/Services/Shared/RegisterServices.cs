using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using Serilog;

namespace CryptoKeyLab.LimitResetWorker.Services.Shared
{
    public class RegisterServices
    {
        //property to set name of the service
        public static string ServiceName { get; set; } = "CryptoKeyLab.LimitResetWorker";
        public static string CompanyName { get; set; } = "HaloRift";


        private static IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        // Registers the application as a Windows service.
        // 
        // Parameters:
        // - args: An array of command-line arguments.
        // 
        // Returns:
        // - A Task<bool> representing the success or failure of the registration/unregistration process.
        public async static Task<bool> ResgisterAsWindowsService(string[] args)
        {
            //check if args is null or length is less than or equal to 0
            if (args is null || args.Length <= 0)
                return false;

            // 3️⃣ Configure Serilog early using config
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            string strExePath = Path.Combine(AppContext.BaseDirectory, $"{ServiceName}.exe");

            if (args[0] is "/Install")
            {
                try
                {
                    //writting log to console
                    Log.Information("Service installation Started...");

                    await Cli.Wrap("sc")
                    .WithArguments(new[] { "create", ServiceName, $"binPath=\"{strExePath}\"", "start=auto" })
                    .ExecuteAsync();
                    await Cli.Wrap("sc.exe")
                    .WithArguments(new[] { "description", ServiceName, CompanyName })
                    .ExecuteAsync();

                    //writting log to console
                    Log.Information("Service installation Completed...");
                    return true;
                }
                catch (Exception ex)
                {
                    //writting log to console
                    Log.Error($"Service installation failed due to error : {ex.Message}...");
                    return false;
                }
            }
            else if (args[0] is "/Uninstall")
            {
                try
                {
                    //writting log to console
                    Log.Information("Uninstalling service...");

                    await Cli.Wrap("sc")
                    .WithArguments(new[] { "stop", ServiceName })
                    .ExecuteAsync();

                    await Cli.Wrap("sc")
                    .WithArguments(new[] { "delete", ServiceName })
                    .ExecuteAsync();

                    //writting log to console
                    Log.Information("Service uninstallation completed...");
                    return true;
                }
                catch (Exception ex)
                {
                    //writting log to console
                    Log.Error($"Service uninstallation failed due to error : {ex.Message}...");
                    return false;
                }
            }
            return true;
        }
    }
}
