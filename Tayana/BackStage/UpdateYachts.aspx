<%@ Page Title="" Language="C#" MasterPageFile="~/BackStage/Site_BS.Master" AutoEventWireup="true" CodeBehind="UpdateYachts.aspx.cs" Inherits="Tayana.BackStage.UpdateYachts" MaintainScrollPositionOnPostback="True" %>

<%--加入CKE控制項後，必須要檢查是否有這行--%>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--Dimensions、Spec頁面--%>
    <asp:Panel ID="PagePanel" runat="server" Visible="true">
        <div class="card">
            <div class="card-header">
                <h5>Dimensions與Spec頁面</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <div style="display: flex; align-items: center; gap: 50px; padding: 20px 20px;">
                        <asp:Button ID="RedirectDimensions" runat="server" Text="Dimensions" CssClass="btn btn-default" OnClick="RedirectDimensions_Click" />
                        <asp:Button ID="RedirectSpec" runat="server" Text="Spec" CssClass="btn btn-default" OnClick="RedirectSpec_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--Yachts Panel--%>
    <asp:Panel ID="YachtsPanel" runat="server" Visible="true">
        <div class="card">
            <div class="card-header">
                <h5>編輯Yachts</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <div style="display: flex; flex-direction: column; align-items: center; justify-content: center;">

                        <%--編輯型號--%>
                        <div style="display: flex; align-items: center; justify-content: center; gap: 10px; width: 100%; border-bottom: 1px solid rgba(0,0,0,.1); padding: 20px 0;">
                            <asp:Label ID="Label1" runat="server" Text="編輯型號 : " Font-Bold="True" Font-Size="Large"></asp:Label>
                            <asp:TextBox ID="EditYachtsModel" runat="server" CssClass="form-control" Width="40%"></asp:TextBox>
                        </div>

                        <%--檔案編輯--%>
                        <div style="display: flex; justify-content: center; align-items: center; gap: 20px; border-bottom: 1px solid rgba(0,0,0,.1); padding: 20px 0; width: 100%;">
                            <p style="font-size: 18px; font-weight: bold; margin-bottom: 0;">原有檔案為 : </p>
                            <asp:Literal ID="FileLiteral" runat="server"></asp:Literal>
                            <asp:FileUpload ID="EditFile" runat="server" />
                        </div>

                        <%--編輯OverView--%>
                        <div style="text-align: center; padding: 20px 0;">
                            <asp:Label ID="Label3" runat="server" Text="編輯OverView" Font-Bold="True" Font-Size="Large"></asp:Label>
                            <CKEditor:CKEditorControl ID="EditOverView" runat="server" BasePath="/Scripts/ckeditor/" Height="300px"></CKEditor:CKEditorControl>
                        </div>

                        <%--編輯按鈕--%>
                        <div style="display: flex; justify-content: center; align-items: center; gap: 30px; padding-bottom: 20px">
                            <asp:Button ID="EditYachts" runat="server" Text="編輯" CssClass="btn btn-default" OnClick="EditYachts_Click" />
                            <asp:Button ID="ReturnIndex" runat="server" Text="返回首頁" CssClass="btn btn-default" OnClick="ReturnIndex_Click" />
                            <asp:Button ID="EditAlbum" runat="server" Text="編輯Yachts Album" CssClass="btn btn-info" OnClick="EditAlbum_Click" />
                            <asp:Button ID="EditLayout" runat="server" Text="編輯Layout" CssClass="btn btn-info" OnClick="EditLayout_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--Yachts Album Panel--%>
    <asp:Panel ID="AlbumPanel" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>Yachts Album</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">

                    <div style="display: flex; justify-content: center; align-items: center; gap: 20px; padding-top: 20px; padding-bottom: 20px;">
                        <p style="font-size: 18px; font-weight: bold; margin-bottom: 0px;">新增照片 : </p>
                        <asp:FileUpload ID="AddImage" runat="server" AllowMultiple="True" />
                        <asp:Button ID="InsertAlbumButton" runat="server" Text="新增" CssClass="btn btn-default" OnClick="InsertAlbumButton_Click" />
                    </div>

                    <asp:GridView ID="GV_Album" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowDeleting="GV_Album_RowDeleting" CssClass="table table-hover" BorderColor="white">
                        <Columns>

                            <asp:BoundField DataField="ID" HeaderText="ID" />

                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="AlbumImage" runat="server" ImageUrl='<%# "~/" + Eval("YachtsImages") %>' Width="200px"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="align-middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定刪除嗎?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <div style="display: flex; justify-content: center; gap: 30px; padding: 20px; margin-top: 30px;">
                        <asp:Button ID="ReturnYachts" runat="server" Text="返回" CssClass="btn btn-default" OnClick="ReturnYachts_Click" />
                        <asp:Button ID="LayoutBtn" runat="server" Text="編輯Layout" CssClass="btn btn-info" OnClick="EditLayout_Click" />
                        <asp:Button ID="Button3" runat="server" Text="返回首頁" CssClass="btn btn-info" OnClick="ReturnIndex_Click" />
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>

    <%--Layout Album Panel--%>
    <asp:Panel ID="LayoutPanel" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>Yachts Layout</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">

                    <div style="display: flex; justify-content: center; align-items: center; gap: 20px; padding-top: 20px; padding-bottom: 20px;">
                        <p style="font-size: 18px; font-weight: bold; margin-bottom: 0px;">新增照片 : </p>
                        <asp:FileUpload ID="AddLayout" runat="server" AllowMultiple="True" />
                        <asp:Button ID="InsertLayoutButton" runat="server" Text="新增" CssClass="btn btn-default" OnClick="InsertLayoutButton_Click" />
                    </div>

                    <asp:GridView ID="GV_Layout" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CssClass="table table-hover" BorderColor="white" OnRowDeleting="GV_Layout_RowDeleting">
                        <Columns>

                            <asp:BoundField DataField="ID" HeaderText="ID" />

                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="LayoutImage" runat="server" ImageUrl='<%# "~/" + Eval("ImagePath") %>' Width="200px"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="align-middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定刪除嗎?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <div style="display: flex; justify-content: center; gap: 30px; padding: 20px; margin-top: 30px;">
                        <asp:Button ID="Button1" runat="server" Text="返回" CssClass="btn btn-default" OnClick="ReturnYachts_Click" />
                        <asp:Button ID="Button2" runat="server" Text="編輯Yachts Album" CssClass="btn btn-info" OnClick="EditAlbum_Click" />
                        <asp:Button ID="Button4" runat="server" Text="返回首頁" CssClass="btn btn-info" OnClick="ReturnIndex_Click" />
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
