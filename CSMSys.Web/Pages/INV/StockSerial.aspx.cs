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

namespace CSMSys.Web.Pages.INV
{
    public partial class StockSerial : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        private INVStockSerial _Serial;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grvStockSerial.DataBind();
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
        #endregion

        #region Methods For Grid
        protected void grvStockSerial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Delete"))
            {
                int intPartyID = Convert.ToInt32(e.CommandArgument.ToString());

                if (intPartyID > 0)
                {
                    DeleteParty(intPartyID);
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
                btnEdit.Attributes.Add("onClick", "javascript:ShowEditModal('" + ((Label)e.Row.Cells[1].Controls[1]).Text + "');");

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                btnDelete.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }
        #endregion

        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvStockSerial.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvStockSerial.DataBind();
        }
        #endregion

        #region Methods For Delete
        private void DeleteParty(int id)
        {
            _Serial = new INVStockSerial();
            _Serial.SerialID = id;
            if (new SerialManager().DeleteSerial(_Serial))
            {
                grvStockSerial.DataBind();
            }
        }
        #endregion

    }
}