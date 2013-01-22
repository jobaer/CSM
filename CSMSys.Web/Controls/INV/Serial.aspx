<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Serial.aspx.cs" Inherits="CSMSys.Web.Controls.INV.Serial" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
	<meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <title></title>
    <link href="../../../App_Themes/Default/Styles/Default.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/Default/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function getbacktostepone() {
            window.location = "Serial.aspx";
        }
        function onSuccess() {
            setTimeout(okay, 2000);
        }
        function onError() {
            setTimeout(cancel, 2000);
        }
        function okay() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            if (UIMODE == "EDIT")
                window.parent.document.getElementById('ButtonEditDone').click();
            else {
                window.parent.document.getElementById('ButtonNewDone').click();
                getbacktostepone();
            }
        }
        function cancel() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            if (UIMODE == "EDIT")
                window.parent.document.getElementById('ButtonEditCancel').click();
            else
                window.parent.document.getElementById('ButtonNewCancel').click();
        }
    </script>

    <script type="text/javascript">
        $().ready(function () {
            $('#txtSerial').keyup(function () {

                $('#txtSerialNo').val($('#txtSerial').val() + "/" + $('#txtBags').val());

            });

            $('#txtBags').keyup(function () {

                $('#txtSerialNo').val($('#txtSerial').val() + "/" + $('#txtBags').val());

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" value="" runat="server" id="hdnWindowUIMODE" />
        <input type="hidden" value="" runat="server" id="hdnPartyID" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Stock Serial
                </div>
                <div class="TitlebarRight" onclick="cancel();"></div>
            </div>
            <div class="popup_Body">
                <asp:MultiView ID="MultiViewSerial" runat="server">
                    <asp:View ID="ViewInput" runat="server">
                        <asp:Panel ID="pnlNew" runat="server" width="100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                    <tbody>
			                    <tr>
				                    <td align="left" valign="top" style="width:40%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                                <tbody>
			                                <tr>
				                                <td align="left" style="width:30%;">Serial : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:TextBox ID="txtSerialNo" runat="server"></asp:TextBox>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:30%;">Date : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                                    &nbsp;&nbsp;
                                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                                                        Enabled="True" TargetControlID="txtDate" PopupPosition="BottomLeft">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfValidator3" runat="server" ControlToValidate="txtDate"
                                                    CssClass="failureNotification" ErrorMessage="Serial Date is required." ToolTip="Serial Date is required."
                                                    ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left">Serial No : </td>
				                                <td align="left">
                                                    <asp:TextBox ID="txtSerial" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfValidator1" runat="server" ControlToValidate="txtSerial"
                                                    CssClass="failureNotification" ErrorMessage="Serial No is required." ToolTip="Serial No is required."
                                                    ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left">Bags : </td>
				                                <td align="left"><asp:TextBox ID="txtBags" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfValidator2" runat="server" ControlToValidate="txtBags"
                                                    CssClass="failureNotification" ErrorMessage="No of Bags is required." ToolTip="No of Bags is required."
                                                    ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:30%;">Customer Code : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:30%;">Remarks : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="258px"></asp:TextBox>
                                                </td>
			                                </tr>
			                                </tbody>
		                                </table>
                                    </td>
				                    <td align="left" valign="top" style="width:60%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                                <tbody>
			                                <tr>
				                                <td align="left">
                                                    Search Customer by Name or Contact No &nbsp;
                                                    <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
                                                    <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" />
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
			                                            <tbody>
			                                            <tr>
				                                            <td align="left">
                                                                <asp:GridView ID="grvParty" DataKeyNames="PartyID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                                                    OnRowDataBound="grvParty_RowDataBound" OnRowCommand="grvParty_RowCommand"
                                                                    EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="10" DataSourceID="dsParty">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PartyID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPartyID" Text='<%# Eval("PartyID") %>' runat="server" HorizontalAlign="Left" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPartyType" Text='<%# Eval("PartyType") %>' runat="server" HorizontalAlign="Left" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPartyCode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Contact No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblContactNo" Text='<%# Eval("ContactNo") %>' runat="server" HorizontalAlign="Left" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAddress" Text='<%# Eval("Address") %>' runat="server" HorizontalAlign="Left" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="District" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDistrictName" Text='<%# Eval("DistrictName") %>' runat="server" HorizontalAlign="Left" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imgEdit" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                                                                    <AlternatingRowStyle BackColor="#E5EAE8" />
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <EmptyDataRowStyle ForeColor="#CC0000" />
                                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                                <asp:SqlDataSource ID="dsParty" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" 
                                                                    SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY ip.PartyID) As SlNo, ip.PartyID, ip.PartyType, ip.PartyCode, ip.PartyName, ip.ContactNo, /*ip.Address,*/ ip.DistrictID, ad.DistrictName
                                                                        FROM INVParty AS ip INNER JOIN ADMDistrict AS ad ON ip.DistrictID = ad.DistrictID" 
                                                                        FilterExpression="PartyName LIKE '%{0}%' OR ContactNo LIKE '{1}%'">
                                                                    <FilterParameters>
                                                                        <asp:QueryStringParameter Name="PartyName" QueryStringField="PartyName" />
                                                                        <asp:QueryStringParameter Name="ContactNo" QueryStringField="ContactNo" />
                                                                        <asp:ControlParameter Name="txtSearch" ControlID="txtSearch" PropertyName="Text" />
                                                                    </FilterParameters>
                                                                </asp:SqlDataSource>
                                                            </td>
			                                            </tr>
			                                            </tbody>
		                                            </table>
                                                </td>
			                                </tr>
			                                </tbody>
		                                </table>
                                    </td>
			                    </tr>
			                    </tbody>
		                    </table>
                        </asp:Panel>

                        <div class="feature-box-actionBar">
                            <span class="failureNotification">
                                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                            </span>
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                ValidationGroup="SerialValidationGroup" OnClick="btnSave_Click" />
                        </div>
                    </asp:View>
                    <asp:View ID="ViewSuccess" runat="server">
                        <asp:Panel ID="pnlSuccess" runat="server" width="100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
			                    <tbody>
                                <tr>
                                    <td align="center">
                                        <span class="succesNotification">
                                            Serial Saved/Edited Successfully. <br />
                                            Dialog will Close automatically within 2 Seconds
                                        </span>
                                    </td>
                                </tr>
			                    </tbody>
		                    </table>
                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="ViewError" runat="server">
                        <asp:Panel ID="pnlError" runat="server" width="100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
			                    <tbody>
                                <tr>
                                    <td align="center" valign="middle">
                                        <span class="failureNotification">
                                            Error Occured Saving/Editing Serial<br />
                                            Dialog will Close automatically within 2 Seconds
                                        </span>
                                    </td>
                                </tr>
			                    </tbody>
		                    </table>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </div>
            <div class="popup_Buttons" style="display:none;">
                <asp:Button ID="btnOkay" Text="Done" runat="server" OnClick="btnSave_Click" />
                <input id="btnCancel" value="Cancel" type="button" onclick="cancel();" />
            </div>
        </div>
    </form>
</body>
</html>