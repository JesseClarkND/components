using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clark.Domain.Data.Results
{
    public class UpdateResult
    {
        public int RowsAffected = 0;
        public int Id = 0;
        public bool Error = false;
        public string Message = "";
    }
}