using Clark.Domain.Data.Results;
using Core.MySQL.Accessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clark.Domain.Component
{
    public class SubdomainController
    {
        public SubdomainController()
        {}

        public UpdateResult CreateTable()
        {
            var result = new UpdateResult();
            try
            {
                string createQuery = @"CREATE TABLE IF NOT EXISTS
                                      sub_domain (
                                      sub_domain_id INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
                                      sub_domain_name VARCHAR(2048) NULL,
                                      domain_id INTEGER NULL,
                                      date_found datetime NULL,
                                      found_type VARCHAR(20) NULL)";

                DbAccessor connection = new DbAccessor();
                connection.SetUpTable(createQuery);
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                if (ex.InnerException != null)
                    result.Message += " Inner: " + ex.InnerException.Message;
            }
            return result;
        }
    }
}