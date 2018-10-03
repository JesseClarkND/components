using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clark.Domain.Data.Results
{
    public class DomainFindResult
    {
        public List<Domain> Items = new List<Domain>();
        public bool Error = false;
        public string Message = "";
    }
}