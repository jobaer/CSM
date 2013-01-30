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
    public partial class Relocate : System.Web.UI.Page
    {
        #region Private Properties
        private INVStockLoading _Loading;
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

        private int LocationID
        {
            get
            {
                if (ViewState["LocationID"] == null)
                    ViewState["LocationID"] = -1;
                return (int)ViewState["LocationID"];
            }
            set
            {
                ViewState["LocationID"] = value;
            }
        }

        //private INVParty _Loading;
        ComboData _ComboData = new ComboData();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            LocationID = Convert.ToInt32(Request.QueryString["LID"]);
            hdnWindowUIMODE.Value = UIMode.ToString();

            if (UIMode == UIMODE.EDIT)
            {
                LoadToAllControlValue(LocationID);

                pnlNew.Visible = true;
                btnSave.Text = "Update";
            }
            MultiViewParty.ActiveViewIndex = 0;

        }
        #region Methods
        private bool checkValidity()
        {
            if (string.IsNullOrEmpty(txtchamber.Text))
            {
                lblFailure.Text = "Chamber no is Required";
                txtchamber.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtfloor.Text))
            {
                lblFailure.Text = "Floor no is Required";
                txtfloor.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtlineno.Text))
            {
                lblFailure.Text = "Line no is Required";
                txtlineno.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtpocket.Text))
            {
                lblFailure.Text = "Pocket no is Required";
                txtpocket.Focus();
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtchamber.Text = string.Empty;
            txtfloor.Text = string.Empty;
            txtlineno.Text = string.Empty;
            txtpocket.Text = string.Empty;
        }
        private void LoadToAllControlValue(int intPartyID)
        {
            if (intPartyID > 0)
            {
                //_Loading = new PartyManager().GetPartyByID(intPartyID);

                //txtCode.Text = _Loading.PartyCode;
                //txtName.Text = _Loading.PartyName;
                //txtFather.Text = _Loading.FatherName;
                //ddlType.SelectedValue = _Loading.PartyType;
                //txtContactNo.Text = _Loading.ContactNo;
                //ddlGender.SelectedValue = _Loading.Gender;
                //ddlReligion.SelectedValue = _Loading.Religion;

                //txtVillage.Text = _Loading.AreaVillageName;
                //txtPO.Text = _Loading.AreaPOName;
                //ddlDistrict.SelectedValue = _Loading.DistrictID.ToString();
                //ddlUpazila.SelectedValue = _Loading.UpazilaPSID.ToString();
                //txtTel.Text = _Loading.Tel;
                //txtCell.Text = _Loading.Cell;
                //txtEmail.Text = _Loading.Email;
            }
        }
        #endregion
        #region Methods for SAVE
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((UIMode == UIMODE.NEW) || (UIMode == UIMODE.EDIT))
                {
                    if (!checkValidity()) return;

                    saveloc();
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
        private void saveloc()
        {
            if (new LoadManager().SaveLocation(FormToObject(LocationID)))
            {
                ClearForm();
            }
        }
        private INVStockLoading FormToObject(int id)
        {
            if (id <= 0)
            {
                _Loading = new INVStockLoading();

                //_Loading.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                //_Loading.CreatedDate = System.DateTime.Now;
            }

            //_Loading.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
            //_Loading.ModifiedDate = System.DateTime.Now;
                       
            _Loading.LoadingID = id;
            _Loading.ChamberNo = txtchamber.Text;
            _Loading.Floor = txtfloor.Text;
            _Loading.Pocket = txtpocket.Text; 
            _Loading.Line = txtlineno.Text;


            _Loading.Remarks = txtremarks.Text;

            //_Loading.PartyCodeName = txtCode.Text + " - " + txtName.Text;
            //_Loading.IsActive = true;

            return _Loading;
        }

        #endregion
    }
}