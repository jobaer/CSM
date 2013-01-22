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
using CSMSys.Lib.Manager.INV;

namespace CSMSys.Web.Controls.INV
{
    public partial class Party : System.Web.UI.Page
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

        private int PartyID
        {
            get
            {
                if (ViewState["PartyID"] == null)
                    ViewState["PartyID"] = -1;
                return (int)ViewState["PartyID"];
            }
            set
            {
                ViewState["PartyID"] = value;
            }
        }

        private INVParty _Party;
        ComboData _ComboData = new ComboData();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UIUtility.FillDropDownList(ddlGender, _ComboData.GenderList(), "Gender");
                UIUtility.FillDropDownList(ddlReligion, _ComboData.ReligionList(), "Religion");
                UIUtility.FillDropDownList(ddlType, _ComboData.PartyType(), "Type");
                UIUtility.BindDistrictDDL(ddlDistrict, 0, false);
                UIUtility.BindUpazillaPSDDL(ddlUpazila, 0, false);

                string qsUIMODE = string.IsNullOrEmpty(Request.QueryString["UIMODE"]) ? "NEW" : Request.QueryString["UIMODE"];
                if (string.IsNullOrEmpty(qsUIMODE) == false)
                {
                    UIMode = (UIMODE)Enum.Parse(typeof(UIMODE), qsUIMODE);
                    PartyID = Convert.ToInt32(Request.QueryString["PID"]);
                    hdnWindowUIMODE.Value = UIMode.ToString();

                    if (UIMode == UIMODE.EDIT)
                    {
                        LoadToAllControlValue(PartyID);

                        pnlNew.Visible = true;
                        btnSave.Text = "Update";
                    }
                    else if (UIMode == UIMODE.NEW)
                    {
                        pnlNew.Visible = true;
                        btnSave.Text = "Save";
                    }
                }
                MultiViewParty.ActiveViewIndex = 0;
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

                    SaveParty();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                MultiViewParty.ActiveViewIndex = 1;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
                MultiViewParty.ActiveViewIndex = 2;
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

        private INVParty FormToObject(int id)
        {
            if (id <= 0)
            {
                _Party = new INVParty();

                _Party.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                _Party.CreatedDate = System.DateTime.Now;
            }

            _Party.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
            _Party.ModifiedDate = System.DateTime.Now;

            _Party.PartyID = id;

            _Party.PartyCode = txtCode.Text;
            _Party.PartyName = txtName.Text;
            _Party.FatherName = txtFather.Text;
            _Party.PartyType = ddlType.SelectedValue.ToString();
            _Party.ContactNo = txtContactNo.Text;
            _Party.Gender = ddlGender.SelectedValue.ToString();
            _Party.Religion = ddlReligion.SelectedValue.ToString();

            _Party.AreaVillageName = txtVillage.Text;
            _Party.AreaPOName = txtPO.Text;
            _Party.UpazilaPSID = ddlUpazila.SelectedIndex > 0 ? Convert.ToInt32(ddlUpazila.SelectedValue.ToString()) : 0;
            _Party.DistrictID = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue.ToString()) : 0;
            _Party.Tel = txtTel.Text;
            _Party.Cell = txtCell.Text;
            _Party.Email = txtEmail.Text;

            _Party.PartyCodeName = txtCode.Text + " - " + txtName.Text;
            _Party.IsActive = true;

            return _Party;
        }

        private void LoadToAllControlValue(int intPartyID)
        {
            if (intPartyID > 0)
            {
                _Party = new PartyManager().GetPartyByID(intPartyID);

                txtCode.Text = _Party.PartyCode;
                txtName.Text = _Party.PartyName;
                txtFather.Text = _Party.FatherName;
                ddlType.SelectedValue = _Party.PartyType;
                txtContactNo.Text = _Party.ContactNo;
                ddlGender.SelectedValue = _Party.Gender;
                ddlReligion.SelectedValue = _Party.Religion;

                txtVillage.Text = _Party.AreaVillageName;
                txtPO.Text = _Party.AreaPOName;
                ddlDistrict.SelectedValue = _Party.DistrictID.ToString();
                ddlUpazila.SelectedValue = _Party.UpazilaPSID.ToString();
                txtTel.Text = _Party.Tel;
                txtCell.Text = _Party.Cell;
                txtEmail.Text = _Party.Email;
            }
        }
        #endregion

        #region Methods For Save
        private void SaveParty()
        {
            if (new PartyManager().SaveParty(FormToObject(PartyID)))
            {
                ClearForm();
            }
        }
        #endregion
    }
}