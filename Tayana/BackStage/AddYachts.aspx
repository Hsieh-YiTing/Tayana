<%@ Page Title="" Language="C#" MasterPageFile="~/BackStage/Site_BS.Master" AutoEventWireup="true" CodeBehind="AddYachts.aspx.cs" Inherits="Tayana.BackStage.AddYachts" MaintainScrollPositionOnPostback="True" %>

<%--加入CKE控制項後，必須要檢查是否有這行--%>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--新增型號--%>
    <div class="card">
        <div class="card-header">
            <h5>新增Yachts</h5>
        </div>
        <div class="card-block table-border-style">
            <div class="table-responsive">
                <div style="display: flex; flex-direction: column;">

                    <div style="display: flex; justify-content: center; align-items: center; padding-top: 20px;">
                        <%--新增型號--%>
                        <div style="display: flex; align-items: center; justify-content: center; gap: 10px; width: 50%;">
                            <asp:Label ID="Label1" runat="server" Text="新增型號 : " Font-Bold="True" Font-Size="Large"></asp:Label>
                            <asp:TextBox ID="AddYachtsModel" runat="server" CssClass="form-control" Width="50%"></asp:TextBox>
                        </div>

                        <%--檔案下載--%>
                        <div style="display: flex; align-items: center; justify-content: center; gap: 10px;">
                            <asp:Label ID="Label2" runat="server" Text="新增檔案 : " Font-Bold="True" Font-Size="Large"></asp:Label>
                            <asp:FileUpload ID="AddFile" runat="server" />
                        </div>

                    </div>

                    <%-- NewDesign設定為false --%>
                    <asp:CheckBox ID="DefaultBox" runat="server" Visible="False" />

                    <%--新增OverView--%>
                    <div style="text-align: center; margin-bottom: 10px;">
                        <hr />
                        <asp:Label ID="Label3" runat="server" Text="新增OverView" Font-Bold="True" Font-Size="Large"></asp:Label>
                        <CKEditor:CKEditorControl ID="OverView" runat="server" BasePath="/Scripts/ckeditor/" Height="200px"></CKEditor:CKEditorControl>
                    </div>
                </div>

                <div class="card-header">
                    <h5>圖片上傳</h5>
                </div>
                <div class="card-block table-border-style">
                    <div style="display: flex; align-items: flex-start; justify-content: center; gap: 50px; padding: 30px 0; border-bottom: 1px solid rgba(0,0,0,.1);">

                        <%--Yachts相簿--%>
                        <div style="display: flex; align-items: center; justify-content: center; gap: 10px;">
                            <asp:Label ID="Label4" runat="server" Text="新增Yachts圖片 : " Font-Bold="True" Font-Size="Large"></asp:Label>
                            <asp:FileUpload ID="AddYachtsAlbum" runat="server" AllowMultiple="True" />
                        </div>

                        <%--Layout圖--%>
                        <div style="display: flex; align-items: center; justify-content: center; gap: 10px;">
                            <asp:Label ID="Label5" runat="server" Text="新增Layout圖片 : " Font-Bold="True" Font-Size="Large"></asp:Label>
                            <asp:FileUpload ID="AddLayout" runat="server" AllowMultiple="True" />
                        </div>
                    </div>
                </div>

                <%--新增型號--%>
                <div style="display:flex; justify-content: center; align-items: center; gap: 30px; padding: 20px">
                    <asp:Button ID="AddNewYachts" runat="server" Text="新增型號" CssClass="btn btn-default" OnClick="AddNewYachts_Click" />
                    <asp:Button ID="ReturnIndex" runat="server" Text="取消" OnClick="ReturnIndex_Click" CssClass="btn btn-default"/>
                </div>
            </div>
        </div>
    </div>

    <%--儲存輸出的YachtsID--%>
    <asp:HiddenField ID="YachtsID" runat="server" />

</asp:Content>
