using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface ISerialDAO : IRepository<INVStockSerial>
    {
        //IList<INVStockSerial> GetPartyBySerial(string serialNo);
        IList<INVStockSerial> SearchSerialByNo(string serialNo);
        IList<INVStockSerial> SearchSerial(string serialNo, float bag);
    }
}
