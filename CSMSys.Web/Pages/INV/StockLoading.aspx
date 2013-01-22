<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="StockLoading.aspx.cs"
    Inherits="CSMSys.Web.Controls.INV.StockLoading" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <table width="100%" border="0" cellpadding="2" cellspacing="4">
            <tbody>
                <tr>
                    <td align="left" style="width: 47%;">
                        <h2>
                            Stock Loading</h2>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" style="width: 40%;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                            <tbody>
                                <tr>
                                    <td align="left" style="width: 30%;">
                                        Party Code:
                                    </td>
                                    <td align="left" style="width: 70%;">
                                        <asp:TextBox ID="txtparty" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 30%;">
                                        Serial No:
                                    </td>
                                    <td align="left" style="width: 70%;">
                                        <asp:TextBox ID="txtSerialNo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 30%;">
                                        Bags :
                                    </td>
                                    <td align="left" style="width: 70%;">
                                        <asp:TextBox ID="txtbags" runat="server"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <asp:RequiredFieldValidator ID="rfValidator3" runat="server" ControlToValidate="txtbags"
                                            CssClass="failureNotification" ErrorMessage="Bags Count is required." ToolTip="Bags Count is required."
                                            ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Chamber No :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtchamber" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfValidator1" runat="server" ControlToValidate="txtchamber"
                                            CssClass="failureNotification" ErrorMessage="Chamber No is required." ToolTip="Chamber No is required."
                                            ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Floor No :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtfloor" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfValidator2" runat="server" ControlToValidate="txtfloor"
                                            CssClass="failureNotification" ErrorMessage="Floor no is required." ToolTip="Floor No is required."
                                            ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 30%;">
                                        Pocket No :
                                    </td>
                                    <td align="left" style="width: 70%;">
                                        <asp:TextBox ID="txtpocket" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtpocket"
                                            CssClass="failureNotification" ErrorMessage="Floor no is required." ToolTip="Floor No is required."
                                            ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 30%;">
                                        Line No :
                                    </td>
                                    <td align="left" style="width: 70%;">
                                        <asp:TextBox ID="txtline" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtline"
                                            CssClass="failureNotification" ErrorMessage="Line no is required." ToolTip="Line No is required."
                                            ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 30%;">
                                        Remarks :
                                    </td>
                                    <td align="left" style="width: 70%;">
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="258px"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td align="left" valign="top" style="width: 60%;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                            <tbody>
                                <tr>
                                    <td align="left">
                                        Search Customer by Name or Contact No &nbsp;
                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                                            ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr>
                                                    <td align="left">
                                                        <asp:GridView ID="grvParty" DataKeyNames="PartyID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging"
                                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvParty_RowDataBound" OnRowCommand="grvParty_RowCommand"
                                                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                                            PageSize="6" DataSourceID="dsParty">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Serial No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bags" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblbags" Text='<%# Eval("Bags") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               
                                                                <asp:TemplateField HeaderText="PartyID" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartyID" Text='<%# Eval("PartyID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartyCode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartyName" Text='<%# Eval("PartyName") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Contact" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartyContact" Text='<%# Eval("ContactNo") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Created" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreatedDate" Text='<%# Eval("cdates") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgselect" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png"
                                                                            ToolTip="Select" />
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
                                                            SelectCommand="SELECT Serial As SlNo,ss.Bags,CONVERT(VARCHAR(10), ss.CreatedDate, 103) AS cdates, 
                                                            ip.PartyID,  ip.PartyCode, ip.PartyName,  ip.ContactNo
                                                            FROM INVStockSerial as ss
                                                            INNER JOIN INVParty AS ip ON ss.PartyCode = ip.PartyCode order by serial desc" FilterExpression="PartyName LIKE '%{0}%' OR ContactNo LIKE '{1}%' OR Serial = {2}">
                                                            <FilterParameters>
                                                                <asp:QueryStringParameter Name="PartyName" QueryStringField="PartyName" />
                                                                <asp:QueryStringParameter Name="ContactNo" QueryStringField="ContactNo" />
                                                                <asp:QueryStringParameter Name="Serial" QueryStringField="Serial" />
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
        <div class="feature-box-actionBar">
            <span class="failureNotification">
                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
            </span>
            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="SerialValidationGroup" />
        </div>
    </div>
</asp:Content>
