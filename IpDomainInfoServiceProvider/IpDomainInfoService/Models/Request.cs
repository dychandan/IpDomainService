using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpDomainInfoService.Models
{
    public class Request
    {
        public string IpAddress { get; set; }
        public string Domain { get; set; }
        public List<String> ServiceList { get; set; }
    }
}
