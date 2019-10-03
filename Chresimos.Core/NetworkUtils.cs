using System.Linq;
using System.Net.NetworkInformation;

namespace Chresimos.Core
{
    public static class NetworkUtils
    {
        public static bool IsPortAvailable (int port)
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var activeUdps = ipGlobalProperties.GetActiveUdpListeners();

            return activeUdps.All(tcpi => tcpi.Port != port);
        }
    }
}