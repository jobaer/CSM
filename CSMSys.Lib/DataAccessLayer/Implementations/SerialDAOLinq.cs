using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Configuration;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    public class SerialDAOLinq : BaseDAORepository<INVStockSerial, CSMSysConfiguration>, ISerialDAO
    {
        protected override System.Linq.Expressions.Expression<Func<INVStockSerial, bool>> GetIDSelector(int id)
        {
            return (item) => item.SerialID == id;
        }

        /// <summary>
        /// DA method to search object by params
        /// </summary>
        /// <param name="partyCode"></param>
        /// <param name="partyName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IList<INVStockSerial> SearchSerialByNo(string serialNo)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "INVStockSerial";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(serialNo))
                {
                    whereClause += " WHERE SerialNo = '" + serialNo + "' ";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<INVStockSerial>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        
        /// <summary>
        /// DA method to search object by params
        /// </summary>
        /// <param name="partyCode"></param>
        /// <param name="partyName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IList<INVStockSerial> SearchSerial(string serialNo, float bag)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "INVStockSerial";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(serialNo))
                {
                    whereClause += " WHERE SerialNo = '" + serialNo + "' ";
                }

                if (bag > 0)
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "Bags = " + bag;
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<INVStockSerial>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
