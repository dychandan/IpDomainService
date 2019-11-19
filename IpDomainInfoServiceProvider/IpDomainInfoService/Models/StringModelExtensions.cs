using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IpDomainInfoService.Models
{
    public static class StringModelExtensions
    {
        public static bool IsValidDomainName(this string name)
        {
            return Uri.CheckHostName(name) != UriHostNameType.Unknown;
        }

        public static bool IsValidIP(this string ipaddress)
        {
            IPAddress ip = null;
            return IPAddress.TryParse(ipaddress, out ip);
        }
    }
}
