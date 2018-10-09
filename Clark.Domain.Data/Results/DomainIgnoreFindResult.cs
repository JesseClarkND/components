using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clark.Domain.Data.Results
{
    public class DomainIgnoreFindResult
    {
        public List<DomainIgnore> Items = new List<DomainIgnore>();
        public bool Error = false;
        public string Message = "";
    }
}