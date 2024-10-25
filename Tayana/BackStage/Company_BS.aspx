<%@ Page Title="" Language="C#" MasterPageFile="~/BackStage/Site_BS.Master" AutoEventWireup="true" CodeBehind="Company_BS.aspx.cs" Inherits="Tayana.BackStage.Company_BS" MaintainScrollPositionOnPostback="true" %>

<%--加入CKE控制項後，必須要檢查是否有這行--%>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--Switch Button--%>
    <div class="card">
        <div class="card-header">
            <h5>Switch Button</h5>
        </div>
        <div class="card-block table-border-style">
            <div class="table-responsive">
                <div style="display: flex; justify-content: space-evenly; padding: 10px;">
                    <div>
                        <asp:Button ID="BtnAboutUsPanel" runat="server" Text="About Us" OnClick="BtnAboutUsPanel_Click" CssClass="btn btn-default" />
                    </div>
                    <div>
                        <asp:Button ID="BtnCertificatePanel" runat="server" Text="Certificate" OnClick="BtnCertificatePanel_Click" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- About Us -->
    <asp:Panel ID="AboutUsPanel" runat="server">
        <div class="card">
            <div class="card-header">
                <h5>About Us Content</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <div>
                        <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server" BasePath="/Scripts/ckeditor/" Height="300px"></CKEditor:CKEditorControl>
                    </div>
                    <div class="card-header" style="margin-top: 50px;">
                        <h5>修改圖片</h5>
                    </div>
                    <div style="display: flex; flex-direction: column; align-items: center; gap: 20px;">
                        <div style="margin-top: 20px; width: 100%; text-align: center;">
                            <h5>原始圖片</h5>
                            <hr/>
                        </div>
                        <div>
                            <asp:Image ID="Image1" runat="server" />
                        </div>
                        <div style="padding: 20px;">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:Button ID="InsertAboutUs" runat="server" Text="修改" CssClass="btn btn-default" OnClick="InsertAboutUs_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- Certificate -->
    <asp:Panel ID="CertificatePanel" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>Certificate Content</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <div>
                        <CKEditor:CKEditorControl ID="CKEditorControl2" runat="server" BasePath="/Scripts/ckeditor/" Height="300px"></CKEditor:CKEditorControl>
                    </div>
                    <div style="text-align: center; padding: 10px; margin-top: 10px;">
                        <asp:Button ID="InsertCertificate" runat="server" Text="修改" CssClass="btn btn-default" OnClick="InsertCertificate_Click" />
                    </div>
                </div>
            </div>
        </div>

        <%--透過HiddenField來獲得Certificate的ID，方便GridView使用--%>
        <asp:HiddenField ID="HiddenCertificateID" runat="server" />

        <!-- Certificate Album -->
        <div class="card">
            <div class="card-header">
                <h5>Certificate Album</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <div style="width: 100%; text-align: center; padding: 20px;">
                        <h5>原始圖片</h5>
                    </div>

                    <%--調整GridView寬度--%>
                    <div style="width: 100%;">
                        <asp:GridView ID="GV_Album" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" ShowHeader="False" BorderColor="White" CssClass="table table-hover" OnRowCancelingEdit="GV_Album_RowCancelingEdit" OnRowDeleting="GV_Album_RowDeleting" OnRowEditing="GV_Album_RowEditing" OnRowUpdating="GV_Album_RowUpdating">
                            <Columns>

                                <%--照片路徑--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div style="text-align: center;">
                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%#"~/" + Eval("CertificateImage") %>' Width="200px" />
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div style="text-align: center;">
                                            <asp:FileUpload ID="FileUpload3" runat="server" />
                                        </div>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <%--編輯按鈕--%>
                                <asp:CommandField ShowEditButton="True" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="align-middle"></asp:CommandField>

                                <%--刪除按鈕轉成TemplateFiled，才可以自定義OnClientClick--%>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" Text="刪除" OnClientClick="return confirm('確定刪除嗎?');" CommandName="Delete"></asp:LinkButton>
                                    </ItemTemplate>

                                    <%--透過ItemStyle可以調整按鈕樣式--%>
                                    <ItemStyle CssClass="align-middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <%--多照片上傳--%>
                    <div style="text-align: center; padding: 20px; margin-top: 20px;">
                        <asp:FileUpload ID="FileUpload2" runat="server" AllowMultiple="True" />
                        <asp:Button ID="InsertImage" runat="server" Text="新增照片" CssClass="btn btn-default" OnClick="InsertImage_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
