using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface IRegistrationDAO : IRepository<SRVRegistration>
    {
        IList<SRVRegistration> SearchRegistrationByNo(string serialNo);
        IList<SRVRegistration> SearchRegistration(string serialNo, float bag);
    }
}
