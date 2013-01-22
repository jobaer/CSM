<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockRelocate.aspx.cs"
    Inherits="CSMSys.Web.Controls.INV.StockRelocate" MasterPageFile="~/Default.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ShowEditModal(SerialID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/INV/Relocate.aspx?UIMODE=EDIT&LID=" + SerialID;
            $find('EditModalPopup').show();
        }
        function EditCancelScript() {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/Loading.aspx";
        }
        function EditOkayScript() {
            RefreshDataGrid();
            EditCancelScript();
        }
        function RefreshDataGrid() {
            $get('btnRefresh').click();
        }
        function NewOkayScript() {
            $get('btnRefresh').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="title">
        <table width="100%" border="0" cellpadding="2" cellspacing="4">
            <tbody>
                <tr>
                    <td align="left" style="width: 47%;">
                        <h2>
                            Stock Location</h2>
                    </td>
                    <td align="right" style="width: 44%;">
                        Search by Serial No/ Party Code/ Party Name :
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                            ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="feature-box-full">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <contenttemplate>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
			        <tbody>
			        <tr>
				        <td align="left">
                            <asp:GridView ID="grvStockSerial" DataKeyNames="SerialNo" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvStockSerial_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                OnRowDataBound="grvStockSerial_RowDataBound" OnRowCommand="grvStockSerial_RowCommand"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="10" DataSourceID="dsStockSerial">
                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Loading #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblli" Text='<%# Eval("lid") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Serial No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerialno" Text='<%# (Eval("SerialNo")) %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                  
                                    <asp:TemplateField HeaderText="Bags" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBags" Text='<%# Eval("Bags") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyCode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyName" Text='<%# Eval("PartyName") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Chamber No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChamberNo" Text='<%# Eval("ChamberNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Floor No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfloorno" Text='<%# Eval("Floor") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pocket No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpocketno" Text='<%# Eval("Pocket") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Line No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLineno" Text='<%# Eval("Line") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremarks" Text='<%# Eval("Remarks") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgedit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Relocate" />
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
                            <asp:SqlDataSource ID="dsStockSerial" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" 
                                SelectCommand="select sl.LoadingID as lid,sl.SerialNo,sl.Bags,sl.PartyCode,par.PartyName,par.ContactNo,sl.ChamberNo,sl.Floor,sl.Pocket,sl.Line,sl.Remarks
                                from INVStockLoading sl
                                INNER JOIN INVParty as par on par.PartyCode = sl.PartyCode"
                                     FilterExpression="SerialNo LIKE '%{0}%' OR PartyName LIKE '%{1}%' or PartyCode like '{1}' ">
                                <FilterParameters>
                                    <asp:ControlParameter Name="SerialNo" ControlID="txtSearch" PropertyName="Text" />
                                    <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
                                    <asp:ControlParameter Name="PartyCode" ControlID="txtSearch" PropertyName="Text" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </td>
			        </tr>
			        </tbody>
		        </table>
            </contenttemplate>
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
    <asp:Button ID="ButtonEdit" runat="server" Text="Submit" Style="display: none" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="ModalPopupBG"
        runat="server" CancelControlID="ButtonEditCancel" OkControlID="ButtonEditDone"
        TargetControlID="ButtonEdit" PopupControlID="DivEditWindow" OnCancelScript="EditCancelScript();"
        OnOkScript="EditOkayScript();" BehaviorID="EditModalPopup">
    </cc1:ModalPopupExtender>
    <div class="popup_Buttons" style="display: none">
        <input id="ButtonEditDone" value="Done" type="button" />
        <input id="ButtonEditCancel" value="Cancel" type="button" />
    </div>
    <div id="DivEditWindow" style="display: none;" class="popupStockSerial">
        <iframe id="IframeEdit" frameborder="0" width="950px" height="382px" class="frameborder"
            scrolling="no"></iframe>
    </div>
</asp:Content>
