using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Manager.SRV;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;

namespace CSMSys.Web.Reports
{
    public partial class AgreementReport : System.Web.UI.Page
    {
        #region Private Properties
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
        private INVBagFair _BagFair;

        ComboData _ComboData = new ComboData();
        #endregion

        String strSearch = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UIUtility.BindTypeDDL(ddlType, 0, false);
                UIUtility.BindBagDDL(ddlWeight, 0, false);
                txtRegistrationID.Text = ((new RegistrationManager().GetRegistrationID()) + 1).ToString();
            }
        }

        protected void btnCode_Click(object sender, EventArgs e)
        {
            int partyID = Convert.ToInt32(txtCode.Text);
            try
            {
                LoadToAllControlValue(partyID);
            }
            catch (InvalidCastException err)
            {
                throw (err);    // Rethrowing exception e
            }
        }

        #region Methods
        private bool checkValidity()
        {
            return true;
        }

        private void ClearForm()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFather.Text = string.Empty;
            txtPartyType.Text = string.Empty;
            txtCellNo.Text = string.Empty;
            txtVillage.Text = string.Empty;
            txtPO.Text = string.Empty;
            txtDistrict.Text = string.Empty;
            txtUpazilla.Text = string.Empty;
            txtBagloans.Text = string.Empty;
            txtCarryingCost.Text = string.Empty;
        }

        private SRVRegistration FormToObject(int id)
        {
            SRVRegistration tsrv = new SRVRegistration();
            tsrv.SerialID = Convert.ToInt32(txtserid.Text);
            tsrv.PartyID = Convert.ToInt32(txtpartycode.Text);
            tsrv.BagLoan = float.Parse(txtBagloans.Text);
            tsrv.CarryingLoan = float.Parse(txtCarryingCost.Text);
            tsrv.Remarks = txtRemarks.Text;
            tsrv.SerialNo = txtAgreementNo.Text;
            tsrv.CreatedDate = DateTime.Now;
            tsrv.CreatedBy = WebCommonUtility.GetCSMSysUserKey();



            return tsrv;
        }

        private void LoadToAllControlValue(int intPartyID)
        {
            if (intPartyID > 0)
            {
                ReportManager rptManager = new ReportManager();
                //_Party = new PartyManager.GetPartyByID(intPartyID);
                _Party = rptManager.GetPartyByID(intPartyID);

                //  txtCode.Text = _Party.PartyID;
                //txtserid.Text=_Party
                txtpartycode.Text = _Party.PartyID.ToString();
                txtCode.Text = _Party.PartyCode;
                txtName.Text = _Party.PartyName;
                txtFather.Text = _Party.FatherName;
                txtPartyType.Text = _Party.PartyType;
                txtVillage.Text = _Party.AreaVillageName;
                txtPO.Text = _Party.AreaPOName;
                txtCellNo.Text = _Party.Cell;
                txtDistrict.Text = rptManager.getdist(Convert.ToInt32(_Party.DistrictID)).DistrictName;
                txtUpazilla.Text = rptManager.getupzilla(Convert.ToInt32(_Party.UpazilaPSID)).UpazilaPSName;

                string partyCode = txtCode.Text;

                if (partyCode.Contains("F") || partyCode.Contains("P"))
                {
                    txtBagloans.Enabled = true;
                    txtCarryingCost.Enabled = true;
                }
                else
                {
                    txtBagloans.Enabled = false;
                    txtCarryingCost.Enabled = false;
                }
            }
        }
        #endregion
        #region Methods
        public string HighlightText(string InputTxt)
        {
            string SearchStr = txtsearch.Text;
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

        //private bool checkValidity()
        //{
        //    if (string.IsNullOrEmpty(txtSerial.Text))
        //    {
        //        lblFailure.Text = "Serial No is Required";
        //        txtSerial.Focus();
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(txtBags.Text))
        //    {
        //        lblFailure.Text = "No of Bags is Required";
        //        txtBags.Focus();
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(txtCode.Text))
        //    {
        //        lblFailure.Text = "Customer Code is Required";
        //        txtCode.Focus();
        //        return false;
        //    }
        //    //if (ddlCustomer.SelectedIndex <= 0)
        //    //{
        //    //    lblFailure.Text = "Customer Code is Required";
        //    //    ddlCustomer.Focus();
        //    //    return false;
        //    //}
        //    return true;
        //}

        //private void ClearForm()
        //{
        //    txtDate.Text = System.DateTime.Today.ToShortDateString();
        //    txtSerialNo.Text = string.Empty;
        //    txtSerial.Text = string.Empty;
        //    //ddlCustomer.SelectedIndex = 0;
        //    txtCode.Text = string.Empty;
        //    txtBags.Text = string.Empty;
        //    txtRemarks.Text = string.Empty;
        //}


        #endregion
        #region Methods For Save                          
        private void saveSRVRegistration()
        {
            if (new ReportManager().SaveReport(FormToObject(PartyID)))
            {
              
            }
        }
        #endregion

        protected void btnReport_Click(object sender, EventArgs e)
        {
            saveSRVRegistration();
            Response.Redirect("TestReport.aspx?code=" + txtpartycode.Text  + "&bagLoans=" + txtBagloans.Text + "&carryingCost=" + txtCarryingCost.Text
                                + "&remarks=" + txtRemarks.Text);
            //Response.Redirect("TestReport.aspx?code=" + txtCode.Text + " &name=" + txtName.Text + "&fName=" + txtFather.Text +
            //                  "&cellNo=" + txtCellNo.Text + "&po=" + txtPO.Text + "&village=" + txtVillage.Text + "&bagLoans=" + txtBagloans.Text
            //                   + "&carryingCost=" + txtCarryingCost.Text + "&agreementNo=" + txtAgreementNo.Text + "&district=" + txtDistrict.Text +
            //                   "&upazila=" + txtUpazilla.Text);

        }
        #region from shad
        #region Methods For Grid
        protected void grvStockSerial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                string strID = words[0].Trim().ToString();
                string strCode = words[1].Trim().ToString();

                string serid = words[2].Trim().ToString();
                //int intPartyID = Convert.ToInt32(strID);

                if (text != null)
                {
                    //ddlCustomer.SelectedValue = strID;
                    //hdnPartyID.Value = strID;
                    txtAgreementNo.Text = strID;
                    txtserid.Text = serid;
                    int partyID = Convert.ToInt32(strCode);
                    try
                    {
                        LoadToAllControlValue(partyID);
                        //txtCode.Text = strCode;
                    }
                    catch (InvalidCastException err)
                    {
                        throw (err);    // Rethrowing exception e
                    }
                }
            }
        }

        protected void grvStockSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvStockSerial.PageIndex = e.NewPageIndex;
            grvStockSerial.DataBind();
        }

        protected void grvStockSerial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.CommandArgument = ((Label)e.Row.FindControl("lblSerialNo")).Text + "@" + ((Label)e.Row.FindControl("lblpid")).Text
                    + "@" + ((Label)e.Row.FindControl(("lblSerialID"))).Text;

            }
        }
        #endregion
        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtsearch.Text;
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtsearch.Text = string.Empty;
            grvStockSerial.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvStockSerial.DataBind();
        }
        #endregion

        protected void ddlWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlWeight.SelectedIndex) > 0)
            {
                int fairID = int.Parse(ddlWeight.SelectedValue);
                BagManager bagManager = new BagManager(true);
                _BagFair = bagManager.GetFairByID(fairID);
                txtBagFair.Text = _BagFair.BagFair.ToString();
                int totalFair = Convert.ToInt32(txtBagNo.Text) * Convert.ToInt32(txtBagFair.Text);
                txtTotalFair.Text = totalFair.ToString();
            }
            txtAdvance.Enabled = true;
        }
        #endregion

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAdvance.Enabled = true;
            txtBagNo.Enabled = true;
            ddlWeight.Enabled = true;
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            if (new ItemDetailsManager(true).SaveItemDetails(ItemToObject(PartyID)))
            {
            }
        }

        private INVItemDetail ItemToObject(int id)
        {
            INVItemDetail item = new INVItemDetail();
            item.RegistrationID = Convert.ToInt32(txtRegistrationID.Text);
            item.BagNo = Convert.ToInt32(txtBagNo.Text);
            item.BagWeight = Convert.ToInt32(ddlWeight.SelectedItem.Text);
            item.BagFair = Convert.ToInt32(txtBagFair.Text);
            item.TotalFair = Convert.ToInt32(txtTotalFair.Text);
            item.Advance = Convert.ToInt32(txtAdvance.Text);
            item.ItemTypeID = Convert.ToInt32(ddlType.SelectedIndex);
            return item;
        }

    }
}