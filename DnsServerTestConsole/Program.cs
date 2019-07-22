using DnsServerCore;
using DnsServerCore.Dns;
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
            Console.WriteLine("Logs directory: " + logsDirectory);

            if (!Directory.Exists(configDirectory))
                Directory.CreateDirectory(configDirectory);

            if (!Directory.Exists(logsDirectory))
                Directory.CreateDirectory(logsDirectory);

            var logManager = new LogManager(logsDirectory);

            var dnsServer = new DnsServer();
            dnsServer.LogManager = logManager;
            dnsServer.Start();

            var dhcpServer = new DhcpServer(configDirectory);
            dhcpServer.LogManager = logManager;
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
