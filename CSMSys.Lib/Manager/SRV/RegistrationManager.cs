using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.Manager.SRV
{
    public class RegistrationManager
    {
        #region Properties
        IRegistrationDAO _RegistrationDAOLinq;
        #endregion

        #region Constructor
        public RegistrationManager()
        {
            _RegistrationDAOLinq = new RegistrationDAOLinq();
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Get all Registration from Registration table 
        /// </summary>
        /// <returns></returns>
        public IList<SRVRegistration> GetAllRegistration()
        {
            try
            {
                return _RegistrationDAOLinq.All().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get party object by OID
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public SRVRegistration GetRegistrationByID(int id)
        {
            return _RegistrationDAOLinq.PickByID(id);
        }

        /// <summary>
        /// Method to save (add/edit) party object
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        public bool SaveRegistration(SRVRegistration serial)
        {
            try
            {
                if (serial.RegistrationID == 0)
                {
                    return _RegistrationDAOLinq.Add(serial);
                }
                else
                {
                    return _RegistrationDAOLinq.Edit(serial, true);
                }

                //if (_RegistrationDAOLinq.Exists(serial))
                //{
                //    return _RegistrationDAOLinq.Edit(serial, true);                    
                //}
                //else
                //{
                //    return _RegistrationDAOLinq.Add(serial);
                //}
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteRegistration(SRVRegistration serial)
        {
            try
            {
                if (serial.RegistrationID > 0)
                {
                    return _RegistrationDAOLinq.Delete(serial, true);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IList<SRVRegistration> SearchRegistration(string serialNo, float bag)
        {
            return _RegistrationDAOLinq.SearchRegistration(serialNo, bag);
        }
        #endregion
    }
}
