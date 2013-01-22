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
    public partial class Serial : System.Web.UI.Page
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

        private int SerialID
        {
            get
            {
                if (ViewState["SerialID"] == null)
                    ViewState["SerialID"] = -1;
                return (int)ViewState["SerialID"];
            }
            set
            {
                ViewState["SerialID"] = value;
            }
        }

        private INVStockSerial _Serial;
        ComboData _ComboData = new ComboData();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //UIUtility.BindCustomerDDL(ddlCustomer, false);
                txtDate.Text = System.DateTime.Today.ToShortDateString();

                string qsUIMODE = string.IsNullOrEmpty(Request.QueryString["UIMODE"]) ? "NEW" : Request.QueryString["UIMODE"];
                if (string.IsNullOrEmpty(qsUIMODE) == false)
                {
                    UIMode = (UIMODE)Enum.Parse(typeof(UIMODE), qsUIMODE);
                    SerialID = Convert.ToInt32(Request.QueryString["PID"]);
                    hdnWindowUIMODE.Value = UIMode.ToString();

                    if (UIMode == UIMODE.EDIT)
                    {
                        LoadToAllControlValue(SerialID);

                        pnlNew.Visible = true;
                        btnSave.Text = "Update";
                    }
                    else if (UIMode == UIMODE.NEW)
                    {
                        pnlNew.Visible = true;
                        btnSave.Text = "Save";
                    }
                }
                MultiViewSerial.ActiveViewIndex = 0;
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

                    SaveSerial();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                MultiViewSerial.ActiveViewIndex = 1;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
                MultiViewSerial.ActiveViewIndex = 2;
            }
        }
        #endregion


        #region Methods
        public string HighlightText(string InputTxt)
        {
            string SearchStr = txtSearch.Text;
            // Setup the regular expression and add the Or operator.
            Regex RegExp = new Regex(SearchStr.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);
            // Highlight keywords by calling the 
            //delegate each time a keyword is found.
            return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
        }

        public string ReplaceKeyWords(Match m)
        {
            return ("<span class=highlight>" + m.Value + "</span>");
        }

        private bool checkValidity()
        {
            if (string.IsNullOrEmpty(txtSerial.Text))
            {
                lblFailure.Text = "Serial No is Required";
                txtSerial.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtBags.Text))
            {
                lblFailure.Text = "No of Bags is Required";
                txtBags.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblFailure.Text = "Customer Code is Required";
                txtCode.Focus();
                return false;
            }
            //if (ddlCustomer.SelectedIndex <= 0)
            //{
            //    lblFailure.Text = "Customer Code is Required";
            //    ddlCustomer.Focus();
            //    return false;
            //}
            return true;
        }

        private void ClearForm()
        {
            txtDate.Text = System.DateTime.Today.ToShortDateString();
            txtSerialNo.Text = string.Empty;
            txtSerial.Text = string.Empty;
            //ddlCustomer.SelectedIndex = 0;
            txtCode.Text = string.Empty;
            txtBags.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }

        private INVStockSerial FormToObject(int id)
        {
            if (id <= 0)
            {
                _Serial = new INVStockSerial();

                _Serial.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                _Serial.CreatedDate = System.DateTime.Now;
            }

            _Serial.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
            _Serial.ModifiedDate = System.DateTime.Now;

            _Serial.SerialID = id;

            _Serial.Serial = Convert.ToInt32(txtSerial.Text);
            _Serial.Bags = float.Parse(txtBags.Text);
            _Serial.SerialNo = txtSerialNo.Text.ToString();
            _Serial.PartyCode = txtCode.Text.ToString();
            _Serial.PartyID = Convert.ToInt32(hdnPartyID.Value.ToString());
            _Serial.SerialDate = Convert.ToDateTime(txtDate.Text);
            _Serial.Remarks = txtRemarks.Text;

            return _Serial;
        }

        private void LoadToAllControlValue(int intSerialID)
        {
            if (intSerialID > 0)
            {
                _Serial = new SerialManager().GetSerialByID(intSerialID);

                txtSerial.Text = _Serial.Serial.ToString();
                txtBags.Text = _Serial.Bags.ToString();
                txtSerialNo.Text = _Serial.SerialNo;
                //ddlCustomer.SelectedValue = _Serial.PartyID.ToString();
                txtCode.Text = _Serial.PartyCode;
                txtDate.Text = _Serial.SerialDate.ToString();
                txtRemarks.Text = _Serial.Remarks;
            }
        }
        #endregion

        #region Methods For Save
        private void SaveSerial()
        {
            if (new SerialManager().SaveSerial(FormToObject(SerialID)))
            {
                ClearForm();
            }
        }
        #endregion

        #region Methods For Grid
        protected void grvParty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('/');
                string strID = words[0].Trim().ToString();
                string strCode = words[1].Trim().ToString();

                int intPartyID = Convert.ToInt32(strID);

                if (intPartyID > 0)
                {
                    //ddlCustomer.SelectedValue = strID;
                    hdnPartyID.Value = strID;
                    txtCode.Text = strCode;
                }
            }
        }
        
        protected void grvParty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvParty.PageIndex = e.NewPageIndex;
            grvParty.DataBind();
        }

        protected void grvParty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text + " / " + ((Label)e.Row.Cells[3].Controls[1]).Text;
            }
        }
        #endregion
    }
}