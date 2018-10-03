using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clark.Domain.Data
{
    public class Subdomain
    {
        public int SubdomainId = 0;
        public string SubdomainName = "";
        public int DomainId = 0;
        public DateTime? DateFound;
        public string FoundType = "";
    }
}