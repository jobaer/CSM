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
    public class SRVRegistrationDAOLinq : BaseDAORepository<SRVRegistration, CSMSysConfiguration>, ISRVRegistration
    {
        protected override System.Linq.Expressions.Expression<Func<SRVRegistration, bool>> GetIDSelector(int id)
        {
            return (item) => item.RegistrationID == id;
        }
    }
}
