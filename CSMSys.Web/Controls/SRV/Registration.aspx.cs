using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.SRV;

namespace CSMSys.Web.Controls.SRV
{
    public partial class Registration : System.Web.UI.Page
    {
        #region Private Properties
        private enum UIMODE
        {
            NEW,
            EDIT
        }

        private UIMODE UIMode
        {
            get
            {
                if (ViewState["UIMODE"] == null)
                    ViewState["UIMODE"] = new UIMODE();
                return (UIMODE)ViewState["UIMODE"];
            }
            set
            {
                ViewState["UIMODE"] = value;
            }
        }

        private int RegistrationID
        {
            get
            {
                if (ViewState["RegistrationID"] == null)
                    ViewState["RegistrationID"] = -1;
                return (int)ViewState["RegistrationID"];
            }
            set
            {
                ViewState["RegistrationID"] = value;
            }
        }

        private SRVRegistration _Registration;
        ComboData _ComboData = new ComboData();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UIUtility.FillDropDownList(ddlGender, _ComboData.GenderList(), "Gender");
                UIUtility.FillDropDownList(ddlReligion, _ComboData.ReligionList(), "Religion");
                //UIUtility.FillDropDownList(ddlType, _ComboData.RegistrationType(), "Type");
                UIUtility.BindDistrictDDL(ddlDistrict, 0, false);
                UIUtility.BindUpazillaPSDDL(ddlUpazila, 0, false);

                string qsUIMODE = string.IsNullOrEmpty(Request.QueryString["UIMODE"]) ? "NEW" : Request.QueryString["UIMODE"];
                if (string.IsNullOrEmpty(qsUIMODE) == false)
                {
                    UIMode = (UIMODE)Enum.Parse(typeof(UIMODE), qsUIMODE);
                    RegistrationID = Convert.ToInt32(Request.QueryString["PID"]);
                    hdnWindowUIMODE.Value = UIMode.ToString();

                    if (UIMode == UIMODE.EDIT)
                    {
                        LoadToAllControlValue(RegistrationID);

                        pnlNew.Visible = true;
                        btnSave.Text = "Update";
                    }
                    else if (UIMode == UIMODE.NEW)
                    {
                        pnlNew.Visible = true;
                        btnSave.Text = "Save";
                    }
                }
                MultiViewRegistration.ActiveViewIndex = 0;
            }
        }

        #region Methods for Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((UIMode == UIMODE.NEW) || (UIMode == UIMODE.EDIT))
                {
                    if (!checkValidity()) return;

                    SaveRegistration();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                MultiViewRegistration.ActiveViewIndex = 1;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
                MultiViewRegistration.ActiveViewIndex = 2;
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlDistrict.SelectedValue) > 0)
            {
                int districtID = int.Parse(ddlDistrict.SelectedValue);
                UIUtility.BindUpazillaPSDDL(ddlUpazila, districtID, false);
            }
        }
        #endregion

        #region Methods
        private bool checkValidity()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblFailure.Text = "Code is Required";
                txtCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblFailure.Text = "Name is Required";
                txtName.Focus();
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFather.Text = string.Empty;
            ddlType.SelectedIndex = 0;
            txtContactNo.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            ddlReligion.SelectedIndex = 0;

            txtVillage.Text = string.Empty;
            txtPO.Text = string.Empty;
            ddlDistrict.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            txtTel.Text = string.Empty;
            txtCell.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private SRVRegistration FormToObject(int id)
        {
            if (id <= 0)
            {
                _Registration = new SRVRegistration();

                _Registration.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                _Registration.CreatedDate = System.DateTime.Now;
            }

            _Registration.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
            _Registration.ModifiedDate = System.DateTime.Now;

            _Registration.RegistrationID = id;

            //_Registration.RegistrationCode = txtCode.Text;
            //_Registration.RegistrationName = txtName.Text;
            //_Registration.FatherName = txtFather.Text;
            //_Registration.RegistrationType = ddlType.SelectedValue.ToString();
            //_Registration.ContactNo = txtContactNo.Text;
            //_Registration.Gender = ddlGender.SelectedValue.ToString();
            //_Registration.Religion = ddlReligion.SelectedValue.ToString();

            //_Registration.AreaVillageName = txtVillage.Text;
            //_Registration.AreaPOName = txtPO.Text;
            //_Registration.UpazilaPSID = ddlUpazila.SelectedIndex > 0 ? Convert.ToInt32(ddlUpazila.SelectedValue.ToString()) : 0;
            //_Registration.DistrictID = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue.ToString()) : 0;
            //_Registration.Tel = txtTel.Text;
            //_Registration.Cell = txtCell.Text;
            //_Registration.Email = txtEmail.Text;

            //_Registration.RegistrationCodeName = txtCode.Text + " - " + txtName.Text;
            //_Registration.IsActive = true;

            return _Registration;
        }

        private void LoadToAllControlValue(int intRegistrationID)
        {
            if (intRegistrationID > 0)
            {
                _Registration = new RegistrationManager().GetRegistrationByID(intRegistrationID);

                //txtCode.Text = _Registration.RegistrationCode;
                //txtName.Text = _Registration.RegistrationName;
                //txtFather.Text = _Registration.FatherName;
                //ddlType.SelectedValue = _Registration.RegistrationType;
                //txtContactNo.Text = _Registration.ContactNo;
                //ddlGender.SelectedValue = _Registration.Gender;
                //ddlReligion.SelectedValue = _Registration.Religion;

                //txtVillage.Text = _Registration.AreaVillageName;
                //txtPO.Text = _Registration.AreaPOName;
                //ddlDistrict.SelectedValue = _Registration.DistrictID.ToString();
                //ddlUpazila.SelectedValue = _Registration.UpazilaPSID.ToString();
                //txtTel.Text = _Registration.Tel;
                //txtCell.Text = _Registration.Cell;
                //txtEmail.Text = _Registration.Email;
            }
        }
        #endregion

        #region Methods For Save
        private void SaveRegistration()
        {
            if (new RegistrationManager().SaveRegistration(FormToObject(RegistrationID)))
            {
                ClearForm();
            }
        }
        #endregion
    }
}