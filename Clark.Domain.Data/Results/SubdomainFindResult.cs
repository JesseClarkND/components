using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clark.Domain.Data.Results
{
    public class SubdomainFindResult
    {
        public List<Subdomain> Items = new List<Subdomain>();
        public bool Error = false;
        public string Message = "";
    }
}