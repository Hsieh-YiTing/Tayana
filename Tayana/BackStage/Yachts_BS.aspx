<%@ Page Title="" Language="C#" MasterPageFile="~/BackStage/Site_BS.Master" AutoEventWireup="true" CodeBehind="Yachts_BS.aspx.cs" Inherits="Tayana.BackStage.Yachits_BS" MaintainScrollPositionOnPostback="True" %>

<%--加入CKE控制項後，必須要檢查是否有這行--%>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!--Yachts顯示-->
    <asp:Panel ID="YachtsPanel" runat="server" Visible="true">
        <div class="card">
            <div class="card-header">
                <h5>Yachts資料</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <asp:GridView ID="GV_Yachts" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CssClass="table table-hover" BorderColor="White" OnRowDeleting="GV_Yachts_RowDeleting" OnSelectedIndexChanged="GV_Yachts_SelectedIndexChanged">

                        <Columns>

                            <%--ID欄位--%>
                            <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="align-middle" />

                            <%--型號欄位--%>
                            <asp:BoundField DataField="Yachts" HeaderText="Yachts" ItemStyle-CssClass="align-middle" />

                            <%--是否為最新設計--%>
                            <asp:TemplateField HeaderText="NewDesign">
                                <ItemTemplate>
                                    <asp:CheckBox ID="NewDesign" runat="server" Checked='<%#Eval("NewDesign") %>' OnCheckedChanged="NewDesign_CheckedChanged" AutoPostBack="True" />
                                </ItemTemplate>

                                <ItemStyle CssClass="align-middle" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <%--建立時間--%>
                            <asp:BoundField DataField="CreateTime" HeaderText="CreateTime" ItemStyle-CssClass="align-middle" />

                            <%--選取按鈕--%>
                            <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="align-middle" ItemStyle-HorizontalAlign="Center" />

                            <%--刪除按鈕--%>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteYachts" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定刪除嗎?');"></asp:LinkButton>
                                </ItemTemplate>

                                <ItemStyle CssClass="align-middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                    <%--跳轉到新增型號介面--%>
                    <div style="text-align: center; padding: 20px">
                        <asp:Button ID="CreateYachts" runat="server" Text="新增型號" CssClass="btn btn-default" OnClick="AddYachts_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


</asp:Content>
