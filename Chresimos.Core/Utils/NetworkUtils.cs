using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Chresimos.Core.Utils
{
    public static class NetworkUtils
    {
        public static bool IsPortAvailable (int port)
        {
            try
            {
                var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                var activeUdps = ipGlobalProperties.GetActiveUdpListeners();

                return activeUdps.All(tcpi => tcpi.Port != port);
            }
            // On Unix system:
            catch (Exception)
            {
                try
                {
                    var l = new TcpListener(IPAddress.Loopback, port);
                    l.Start();
                    var lPort = ((IPEndPoint)l.LocalEndpoint).Port;
                    l.Stop();
                
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}