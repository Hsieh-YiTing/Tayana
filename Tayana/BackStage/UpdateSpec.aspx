<%@ Page Title="" Language="C#" MasterPageFile="~/BackStage/Site_BS.Master" AutoEventWireup="true" CodeBehind="UpdateSpec.aspx.cs" Inherits="Tayana.BackStage.UpdateSpec" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--選擇類別--%>
    <div class="card">
        <div class="card-header">
            <h5>選擇Spec類別</h5>
        </div>
        <div class="card-block table-border-style">
            <div class="table-responsive">
                <div style="padding: 20px;">
                    <asp:DropDownList ID="CategoryList" runat="server" CssClass="form-control" Width="20%" AutoPostBack="True" OnSelectedIndexChanged="CategoryList_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>

    <%--SpecItem GridView--%>
    <div class="card">
        <div class="card-header">
            <h5>SpecItem</h5>
        </div>
        <div class="card-block table-border-style">
            <div class="table-responsive">
                <asp:GridView ID="GV_SpecItem" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" BorderColor="White" DataKeyNames="ID" OnRowDeleting="GV_SpecItem_RowDeleting" OnRowEditing="GV_SpecItem_RowEditing" OnRowCancelingEdit="GV_SpecItem_RowCancelingEdit" OnRowUpdating="GV_SpecItem_RowUpdating">
                    <Columns>

                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" />

                        <asp:TemplateField HeaderText="SpecItem">
                            <ItemTemplate>
                                <asp:Label ID="OriginItem" runat="server" Text='<%# Eval("Item") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="EditBox" runat="server" Text='<%# Eval("Item") %>' CssClass="form-control"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <div style="display: flex; align-items: center; justify-content: space-evenly; height: 40px;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                                </div>
                            </EditItemTemplate>

                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="align-middle" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteItem" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定刪除嗎?');"></asp:LinkButton>
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" CssClass="align-middle"></ItemStyle>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

                <%--新增SpecItem與取消--%>
                <div style="display: flex; justify-content: center; align-items: center; gap: 30px; padding: 20px">
                    <asp:Button ID="AddItem" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddItem_Click" />
                    <asp:Button ID="ReturnModel" runat="server" Text="返回" CssClass="btn btn-default" OnClick="ReturnModel_Click"/>
                    <asp:Button ID="EditDimensions" runat="server" Text="編輯Dimensions" CssClass="btn btn-info" OnClick="EditDimensions_Click" />
                    <asp:Button ID="ReturnIndex" runat="server" Text="返回首頁" CssClass="btn btn-info" OnClick="ReturnIndex_Click"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
