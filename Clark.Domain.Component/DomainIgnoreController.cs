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
    public class DomainIgnoreController
    {
        public DomainIgnoreController() { }

        public UpdateResult CreateTable()
        {
            {
                var result = new UpdateResult();
                try
                {
                    string createQuery = @"CREATE TABLE IF NOT EXISTS
                                      domain_ignore (
                                      domain_ignore_id INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
                                      domain_to_ignore VARCHAR(2048) NULL,
                                      domain_id INTEGER NULL)";

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

        public UpdateResult Update(DomainIgnore domainIgnore)
        {
            var result = new UpdateResult();

            try
            {
                DbUpdater updater = new DbUpdater();
                BindUpdateParameters(domainIgnore, updater, false);
                result.Id = updater.Update("domain_ignore", "domain_ignore_id", domainIgnore.DomainIgnoreId);
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


        public UpdateResult Insert(DomainIgnore ignore)
        {
            var result = new UpdateResult();

            try
            {
                DbUpdater updater = new DbUpdater();
                BindUpdateParameters(ignore, updater, false);
                result.Id = updater.Insert("domain_ignore");
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

        public DomainIgnoreFindResult FindAll()
        {
            return GenericFind("", null);
        }

        public DomainIgnore FindByIgnoreDomain(string ignore)
        {
            string criteria = "domain_to_ignore=@ignore";
            List<DbField> parameters = new List<DbField>();
            parameters.Add(new DbField("@domain_to_ignore", DbType.String, ignore));
            return GenericFind(criteria, parameters).Items.FirstOrDefault();
        }

        private DomainIgnoreFindResult GenericFind(string whereClause, List<DbField> parameters)
        {
            DomainIgnoreFindResult result = new DomainIgnoreFindResult();

            try
            {
                DbAccessor connection = new DbAccessor();
                AddAccessorSelectors(connection);

                if (!String.IsNullOrEmpty(whereClause))
                {
                    connection.SetWhereClause(whereClause, parameters);
                }
                List<string> tables = new List<string>();
                tables.Add("domain_ignore");

                DataView dataView = connection.FindWhere(tables);

                foreach (DataRowView row in dataView)
                {
                    DomainIgnore ignore = DataRowToDomain(row);
                    result.Items.Add(ignore);
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
        private static void BindUpdateParameters(DomainIgnore domainIgnore, DbUpdater updater, bool includeId = true)
        {
            if (includeId)
                updater.BindParameter("domain_ignore_id", System.Data.DbType.Int32, domainIgnore.DomainIgnoreId);
            updater.BindParameter("domain_to_ignore", System.Data.DbType.String, domainIgnore.DomainToIgnore);
            updater.BindParameter("domain_id", System.Data.DbType.Int32, domainIgnore.DomainId);
        }

        private DomainIgnore DataRowToDomain(DataRowView row)
        {
            DomainIgnore ignore = new DomainIgnore();
            ignore.DomainIgnoreId = DatabaseUtilities.SafeMapToInt32(row["domain_ignore_id"], "domain_ignore_id");
            ignore.DomainToIgnore = DatabaseUtilities.SafeMapToString(row["domain_to_ignore"]);
            ignore.DomainId = DatabaseUtilities.SafeMapToInt32(row["domain_id"], "domain_id");
            return ignore;
        }

        private static DomainIgnore DictionaryToDomainIgnore(Dictionary<string, Object> vals)
        {
            DomainIgnore ignore = new DomainIgnore();
            ignore.DomainIgnoreId = DatabaseUtilities.SafeMapToInt32(vals, "domain_ignore_id");
            ignore.DomainToIgnore = DatabaseUtilities.SafeMapToString(vals, "domain_to_ignore");
            ignore.DomainId = DatabaseUtilities.SafeMapToInt32(vals, "domain_id");
            return ignore;
        }

        private static void AddAccessorSelectors(DbAccessor connection)
        {
            connection.Select("domain_ignore_id", DbType.Int32);
            connection.Select("domain_to_ignore", DbType.String);
            connection.Select("domain_id", DbType.Int32);
    }
        #endregion
    }
}