using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.Utility;
using System.Data.Linq;
namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    public class LoadingDAOLinq : BaseDAORepository<INVStockLoading, CSMSysConfiguration>, ILoadingDAO,IRepository<INVStockLoading>
    {
        protected override System.Linq.Expressions.Expression<Func<INVStockLoading, bool>> GetIDSelector(int id)
        {
            return (item) => item.LoadingID == id;
        }
        public IList<INVStockSerial> GetPartyBySerial(string serialNo)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "INVStockSerial";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT PartyCode FROM " + queryTable;

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
    }
}
