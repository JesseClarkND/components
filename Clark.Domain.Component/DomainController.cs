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
    public class DomainController
    {
        public DomainController()
        {}

        public UpdateResult CreateTable()
        {
            var result = new UpdateResult();
            try
            {
                string createQuery = @"CREATE TABLE IF NOT EXISTS
                                      domain (
                                      domain_id INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
                                      domain_name VARCHAR(2048) NULL,
                                      bounty_url VARCHAR(2048) NULL,
                                      bounty_end_date datetime NULL,
                                      last_scan datetime NULL,
                                      private boolean NULL,
                                      platform VARCHAR(50) NULL)";

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

        public UpdateResult Update(Clark.Domain.Data.Domain domain)
        {
            var result = new UpdateResult();

            try
            {
                DbUpdater updater = new DbUpdater();
                BindUpdateParameters(domain, updater, false);
                result.Id = updater.Update("domain", "domain_id", domain.DomainId);
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

        public UpdateResult Insert(Clark.Domain.Data.Domain domain)
        {
            var result = new UpdateResult();

            try
            {
                DbUpdater updater = new DbUpdater();
                BindUpdateParameters(domain, updater, false);
                result.Id = updater.Insert("domain");
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

        public DomainFindResult FindAll()
        {
            return GenericFind("", null);
        }

        public Clark.Domain.Data.Domain FindByDomain(string domain)
        {
            string criteria = "domain_name=@domain_name";
            List<DbField> parameters = new List<DbField>();
            parameters.Add(new DbField("@domain_name", DbType.String, domain));
            return GenericFind(criteria, parameters).Items.FirstOrDefault();
        }

        private DomainFindResult GenericFind(string whereClause, List<DbField> parameters)
        {
            DomainFindResult result = new DomainFindResult();

            try
            {
                DbAccessor connection = new DbAccessor();
                AddAccessorSelectors(connection);

                if (!String.IsNullOrEmpty(whereClause))
                {
                    connection.SetWhereClause(whereClause, parameters);
                }
                List<string> tables = new List<string>();
                tables.Add("domain");

                DataView dataView = connection.FindWhere(tables);

                foreach (DataRowView row in dataView)
                {
                    Clark.Domain.Data.Domain domain = DataRowToDomain(row);
                    result.Items.Add(domain);
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
        private static void BindUpdateParameters(Clark.Domain.Data.Domain domain, DbUpdater updater, bool includeId = true)
        {
            if (includeId)
                updater.BindParameter("domain_id", System.Data.DbType.Int32, domain.DomainId);
            updater.BindParameter("domain_name", System.Data.DbType.String, domain.DomainName);
            updater.BindParameter("bounty_url", System.Data.DbType.String, domain.BountyURL);
            updater.BindParameter("bounty_end_date", System.Data.DbType.DateTime, domain.BountyEndDate);
            updater.BindParameter("last_scan", System.Data.DbType.DateTime, domain.LastScan);
            updater.BindParameter("private", System.Data.DbType.Boolean, domain.Private);
            updater.BindParameter("platform", System.Data.DbType.String, domain.Platform);
        }

        private static Clark.Domain.Data.Domain DataRowToDomain(DataRowView row)
        {
            Clark.Domain.Data.Domain domain = new Clark.Domain.Data.Domain();
            domain.DomainId = DatabaseUtilities.SafeMapToInt32(row["domain_id"], "domain_id");
            domain.DomainName = DatabaseUtilities.SafeMapToString(row["domain_name"]);
            domain.BountyURL = DatabaseUtilities.SafeMapToString(row["bounty_url"]);
            domain.BountyEndDate = DatabaseUtilities.SafeMapToNullableDateTime(row["bounty_end_date"]);
            domain.LastScan = DatabaseUtilities.SafeMapToNullableDateTime(row["last_scan"]);
            domain.Private = DatabaseUtilities.SafeMapToBoolean(row["private"]);
            domain.Platform = DatabaseUtilities.SafeMapToString(row["platform"]);
            return domain;
        }

        private static Clark.Domain.Data.Domain DictionaryToDomain(Dictionary<string, Object> vals)
        {
            var domain = new Clark.Domain.Data.Domain();
            domain.DomainId = DatabaseUtilities.SafeMapToInt32(vals, "domain_id");
            domain.DomainName = DatabaseUtilities.SafeMapToString(vals, "domain_name");
            domain.BountyURL = DatabaseUtilities.SafeMapToString(vals, "bounty_url");
            domain.BountyEndDate = DatabaseUtilities.SafeMapToNullableDateTime(vals, "bounty_end_date");
            domain.LastScan = DatabaseUtilities.SafeMapToNullableDateTime(vals, "last_scan");
            domain.Private = DatabaseUtilities.SafeMapToBoolean(vals, "private");
            domain.Platform = DatabaseUtilities.SafeMapToString(vals, "platform");
            return domain;
        }

        private static void AddAccessorSelectors(DbAccessor connection)
        {
            connection.Select("domain_id", DbType.Int32);
            connection.Select("domain_name", DbType.String);
            connection.Select("bounty_url", DbType.String);
            connection.Select("bounty_end_date", DbType.DateTime);
            connection.Select("last_scan", DbType.DateTime);
            connection.Select("private", DbType.Boolean);
            connection.Select("platform", DbType.String);
        }
        #endregion
    }
}
