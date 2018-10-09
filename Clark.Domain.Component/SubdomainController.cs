using Clark.Domain.Data;
using Clark.Domain.Data.Results;
using Core.MySQL.Accessor;
using System;
using System.Collections.Generic;
using System.Data;
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

        public UpdateResult Update(Subdomain subdomain)
        {
            var result = new UpdateResult();

            try
            {
                DbUpdater updater = new DbUpdater();
                BindUpdateParameters(subdomain, updater, false);
                result.Id = updater.Update("sub_domain", "sub_domain_id", subdomain.SubdomainId);
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

        public UpdateResult Insert(Subdomain subdomain)
        {
            var result = new UpdateResult();

            try
            {
                DbUpdater updater = new DbUpdater();
                BindUpdateParameters(subdomain, updater, false);
                result.Id = updater.Insert("sub_domain");
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

        public SubdomainFindResult FindByDomain(int domainId)
        {
            string criteria = "domain_id=@id";
            List<DbField> parameters = new List<DbField>();
            parameters.Add(new DbField("@id", DbType.Int32, domainId));
            return GenericFind(criteria, parameters);
        }

        public SubdomainFindResult FindAll()
        {
            return GenericFind("", null);
        }

        private SubdomainFindResult GenericFind(string whereClause, List<DbField> parameters)
        {
            SubdomainFindResult result = new SubdomainFindResult();

            try
            {
                DbAccessor connection = new DbAccessor();
                AddAccessorSelectors(connection);

                if (!String.IsNullOrEmpty(whereClause))
                {
                    connection.SetWhereClause(whereClause, parameters);
                }
                List<string> tables = new List<string>();
                tables.Add("sub_domain");

                DataView dataView = connection.FindWhere(tables);

                foreach (DataRowView row in dataView)
                {
                    Subdomain subdomain = DataRowToSubdomain(row);
                    result.Items.Add(subdomain);
                }
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

        #region Private
        private static void BindUpdateParameters(Subdomain subdomain, DbUpdater updater, bool includeId = true)
        { 
            if(includeId)
                updater.BindParameter("sub_domain_id", System.Data.DbType.Int32, subdomain.SubdomainId);
            updater.BindParameter("sub_domain_name", System.Data.DbType.String, subdomain.SubdomainName);
            updater.BindParameter("domain_id", System.Data.DbType.Int32, subdomain.DomainId);
            updater.BindParameter("date_found", System.Data.DbType.DateTime, subdomain.DateFound);
            updater.BindParameter("found_type", System.Data.DbType.String, subdomain.FoundType);
        }

        private static Subdomain DataRowToSubdomain(DataRowView row)
        {
            Subdomain subdomain = new Subdomain();
            subdomain.SubdomainId = DatabaseUtilities.SafeMapToInt32(row["sub_domain_id"], "sub_domain_id");
            subdomain.SubdomainName = DatabaseUtilities.SafeMapToString(row["sub_domain_name"]);
            subdomain.DomainId = DatabaseUtilities.SafeMapToInt32(row["domain_id"], "domain_id");
            subdomain.DateFound = DatabaseUtilities.SafeMapToNullableDateTime(row["date_found"]);
            subdomain.FoundType = DatabaseUtilities.SafeMapToString(row["found_type"]);
            
            return subdomain;
        }

        private static Subdomain DictionaryToDomain(Dictionary<string, Object> vals)
        {
            var subdomain = new Subdomain();
            subdomain.SubdomainId = DatabaseUtilities.SafeMapToInt32(vals, "sub_domain_id");
            subdomain.SubdomainName = DatabaseUtilities.SafeMapToString(vals, "sub_domain_name");
            subdomain.DomainId = DatabaseUtilities.SafeMapToInt32(vals, "domain_id");
            subdomain.DateFound = DatabaseUtilities.SafeMapToNullableDateTime(vals, "date_found");
            subdomain.FoundType = DatabaseUtilities.SafeMapToString(vals, "found_type");
            return subdomain;
        }

        private static void AddAccessorSelectors(DbAccessor connection)
        {
            connection.Select("sub_domain_id", DbType.Int32);
            connection.Select("sub_domain_name", DbType.String);
            connection.Select("domain_id", DbType.Int32);
            connection.Select("date_found", DbType.DateTime);
            connection.Select("found_type", DbType.DateTime);
        }
        #endregion
    }
}