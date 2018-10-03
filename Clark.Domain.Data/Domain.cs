using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clark.Domain.Data
{
    public class Domain
    {
        public int DomainId = 0;
        public string DomainName = "";
        public string BountyURL = "";
        public DateTime? BountyEndDate;
        public DateTime? LastScan;
        public bool Private = false;
        public string Platform = "";
    }
}