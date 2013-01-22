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

        //private INVParty _Party;
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
                //_Party = new PartyManager().GetPartyByID(intPartyID);

                //txtCode.Text = _Party.PartyCode;
                //txtName.Text = _Party.PartyName;
                //txtFather.Text = _Party.FatherName;
                //ddlType.SelectedValue = _Party.PartyType;
                //txtContactNo.Text = _Party.ContactNo;
                //ddlGender.SelectedValue = _Party.Gender;
                //ddlReligion.SelectedValue = _Party.Religion;

                //txtVillage.Text = _Party.AreaVillageName;
                //txtPO.Text = _Party.AreaPOName;
                //ddlDistrict.SelectedValue = _Party.DistrictID.ToString();
                //ddlUpazila.SelectedValue = _Party.UpazilaPSID.ToString();
                //txtTel.Text = _Party.Tel;
                //txtCell.Text = _Party.Cell;
                //txtEmail.Text = _Party.Email;
            }
        }
        #endregion
    }
}