using DnsServerCore;
using DnsServerCore.Dhcp;
using System;
using System.IO;

namespace DnsServerConsole
{
    class Program
    {
        private static readonly string defaultScopeName = "Default";
        private static string configDirectory = "";
        private static string logsDirectory = "";

        static void Main(string[] args)
        {
            configDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "config";
            logsDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "logs";

            Console.WriteLine("Config directory: " + configDirectory);
            Console.WriteLine("Logs directory: " + configDirectory);

            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }

            var dhcpServer = new DhcpServer(configDirectory);
            dhcpServer.LogManager = new LogManager(logsDirectory);
            dhcpServer.Start();

            // When a new instance of DhcpServer is created, a default
            // scope is created but not enabled. The following enables
            // it, but only if not already enabled, because if the
            // scope has previously been enabled by the below code,
            // it is activated upon startup, otherwise, by enabling
            // it below, the default scope is then activated.
            var defaultScope = dhcpServer.GetScope(defaultScopeName);
            if (!defaultScope.Enabled)
                dhcpServer.EnableScope(defaultScopeName);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }        
    }
}
