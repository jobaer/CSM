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
    public partial class StockLoading : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        //private INV _Serial;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grvParty.DataBind();
            }
        }
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
            if (string.IsNullOrEmpty(txtSerialNo.Text))
            {
                lblFailure.Text = "Serial No is Required";
                txtSerialNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtbags.Text))
            {
                lblFailure.Text = "No of Bags is Required";
                txtbags.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtchamber.Text))
            {
                lblFailure.Text = "Chamber Number is Required";
                txtchamber.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtfloor.Text))
            {
                lblFailure.Text = "Floor Number is Required";
                txtfloor.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtpocket.Text))
            {
                lblFailure.Text = "Pocket Number is Required";
                txtpocket.Focus();
                return false;
            }
         
            return true;
        }

        private void ClearForm()
        {
            txtSerialNo.Text = System.DateTime.Today.ToShortDateString();
            txtbags.Text = string.Empty;
            txtchamber.Text = string.Empty;
            //ddlCustomer.SelectedIndex = 0;
            txtpocket.Text = string.Empty;
            txtfloor.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }
        #endregion
        //#region Methods for Save
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
               
        //            if (!checkValidity()) return;

        //            SaveSerial();
               
        //        ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
        //        MultiViewSerial.ActiveViewIndex = 1;
        //    }
        //    catch
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
        //        MultiViewSerial.ActiveViewIndex = 2;
        //    }
        //}
        //private void SaveSerial()
        //{
        //    if (new SerialManager().SaveSerial(FormToObject(SerialID)))
        //    {
        //        ClearForm();
        //    }
        //}
        //private INVStockSerial FormToObject(int id)
        //{
        //    if (id <= 0)
        //    {
        //        _Serial = new INVStockSerial();

        //        _Serial.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
        //        _Serial.CreatedDate = System.DateTime.Now;
        //    }

        //    _Serial.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
        //    _Serial.ModifiedDate = System.DateTime.Now;

        //    _Serial.SerialID = id;

        //    _Serial.Serial = Convert.ToInt32(txtSerial.Text);
        //    _Serial.Bags = float.Parse(txtBags.Text);
        //    _Serial.SerialNo = txtSerialNo.Text.ToString();
        //    _Serial.PartyCode = txtCode.Text.ToString();
        //    _Serial.PartyID = Convert.ToInt32(hdnPartyID.Value.ToString());
        //    _Serial.SerialDate = Convert.ToDateTime(txtDate.Text);
        //    _Serial.Remarks = txtRemarks.Text;

        //    return _Serial;
        //}
        //#endregion
        #region Methods for Search
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
        }
        #endregion
        #region Methods For Grid
        protected void grvParty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                //string strID = words[0].Trim().ToString();
                //string strCode = words[1].Trim().ToString();

                //txtparty.Text = text;
                if (words[0] != string.Empty && words[1] != string.Empty && words[2] != string.Empty)
                {
                    txtSerialNo.Text = words[1].Trim();
                    txtbags.Text = words[0].Trim();
                    txtparty.Text = words[2].Trim();
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
                ImageButton btnselect = (ImageButton)e.Row.FindControl("imgselect");
                btnselect.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text + "@" +
                    ((Label)e.Row.Cells[2].Controls[1]).Text + "@" +
                     ((Label)e.Row.Cells[3].Controls[1]).Text;
                //btnselect.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }
        #endregion
    }
}