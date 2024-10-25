<%@ Page Title="" Language="C#" MasterPageFile="~/BackStage/Site_BS.Master" AutoEventWireup="true" CodeBehind="News_BS.aspx.cs" Inherits="Tayana.BackStage.News_BS" MaintainScrollPositionOnPostback="True" %>

<%--加入CKE控制項後，必須要檢查是否有這行--%>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!--News顯示-->
    <asp:Panel ID="NewsPanel" runat="server" Visible="true">
        <div class="card">
            <div class="card-header">
                <h5>News資料</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <asp:GridView ID="GV_News" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CssClass="table table-hover" BorderColor="White" OnSelectedIndexChanged="GV_News_SelectedIndexChanged" OnRowDeleting="GV_News_RowDeleting">
                        <Columns>
                            <%--ID欄位--%>
                            <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="align-middle"></asp:BoundField>

                            <%--封面照片--%>
                            <asp:TemplateField HeaderText="CoverPhoto">
                                <ItemTemplate>
                                    <asp:Image ID="CoverPhoto" runat="server" ImageUrl='<%#"~/" + Eval("CoverPhoto") %>' Width="200px" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--標題欄位--%>
                            <asp:BoundField DataField="Title" HeaderText="Title" ItemStyle-CssClass="align-middle" />

                            <%--置頂欄位--%>
                            <asp:TemplateField HeaderText="TopStatus">
                                <ItemTemplate>
                                    <asp:CheckBox ID="TopStatus" runat="server" Checked='<%#Eval("IsTop") %>' OnCheckedChanged="TopStatus_CheckedChanged" AutoPostBack="True" />
                                </ItemTemplate>

                                <ItemStyle CssClass="align-middle" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <%--建立時間--%>
                            <asp:BoundField DataField="CreateTime" HeaderText="CreateTime" ItemStyle-CssClass="align-middle" />

                            <%--選取按鈕--%>
                            <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="align-middle" ItemStyle-HorizontalAlign="Center" />

                            <%--刪除按鈕，轉為<ItemTemplate>後，就可以加入OnClientClick--%>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteNews" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定刪除嗎?');"></asp:LinkButton>
                                </ItemTemplate>

                                <ItemStyle CssClass="align-middle" HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    <%--新增後打開CreateNewsPanel--%>
                    <div style="text-align: center; padding: 20px">
                        <asp:Button ID="CreateNews" runat="server" Text="新增新聞" CssClass="btn btn-default" OnClick="CreateNews_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--儲存News的ID--%>
    <asp:HiddenField ID="NewsID" runat="server" />

    <%--修改News資料--%>
    <asp:Panel ID="DetailsPanel" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>編輯News資料</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">

                    <%--標題、內容、封面照、置頂狀態--%>
                    <div style="display: flex; flex-direction: column; gap: 30px; margin-top: 30px;">
                        <div style="display: flex; justify-content: space-evenly; align-items: center;">
                            <asp:Image ID="CoverPhoto" runat="server" Width="200px" />
                            <asp:FileUpload ID="UpdateCover" runat="server" />
                        </div>
                        <div style="display: flex; align-items: center; justify-content: center; gap: 20px; border-top: 1px solid rgba(0,0,0,.1); border-bottom: 1px solid rgba(0,0,0,.1); padding-top: 30px; padding-bottom: 30px">
                            <asp:Label ID="OriginTitle" runat="server" Text="原有名稱 : " Font-Bold="True" Font-Size="Large"></asp:Label>
                            <asp:TextBox ID="EditTitle" runat="server" CssClass="form-control" Width="50%"></asp:TextBox>
                        </div>
                        <div style="text-align: center;">
                            <asp:Label ID="Label6" runat="server" Text="修改內容" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                            <CKEditor:CKEditorControl ID="UpdateContent" runat="server" BasePath="/Scripts/ckeditor/" Height="200px"></CKEditor:CKEditorControl>
                        </div>
                    </div>

                    <%--新增、返回按鈕--%>
                    <div style="display: flex; justify-content: center; gap: 30px; padding: 20px; margin-top: 30px;">
                        <asp:Button ID="EditImage" runat="server" Text="編輯圖片" CssClass="btn btn-info" OnClick="EditImage_Click" />
                        <asp:Button ID="EditFile" runat="server" Text="編輯檔案" CssClass="btn btn-info" OnClick="EditFile_Click" />
                        <asp:Button ID="EditNews" runat="server" Text="修改" CssClass="btn btn-default" OnClick="EditNews_Click" />
                        <asp:Button ID="CloseEditNews" runat="server" Text="返回" CssClass="btn btn-default" OnClick="CloseEditNews_Click" />
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>

    <%--編輯圖片--%>
    <asp:Panel ID="UpdateImagesPanel" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>編輯News圖片</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <div style="display: flex; justify-content: center; align-items: center; gap: 20px; padding-top: 20px; padding-bottom: 20px;">
                        <p style="font-size: 18px; font-weight: bold; margin-bottom: 0px;">新增照片 : </p>
                        <asp:FileUpload ID="AddImage" runat="server" AllowMultiple="True" />
                        <asp:Button ID="InsertImageButton" runat="server" Text="新增" CssClass="btn btn-default" OnClick="InsertImageButton_Click" />
                    </div>
                    <asp:GridView ID="GV_Album" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" BorderColor="white" OnRowDeleting="GV_Album_RowDeleting" DataKeyNames="ID">
                        <Columns>

                            <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="align-middle" ItemStyle-VerticalAlign="NotSet" ItemStyle-HorizontalAlign="Center" />

                            <asp:TemplateField HeaderText="NewsImage">
                                <ItemTemplate>
                                    <asp:Image ID="UpdateImage" runat="server" ImageUrl='<%#"~/" + Eval("NewsImage") %>' Width="200px" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="align-middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteImage" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定刪除嗎?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    <%--新增、返回按鈕--%>
                    <div style="display: flex; justify-content: center; gap: 30px; padding: 20px; margin-top: 30px;">
                        <asp:Button ID="ReturnDetails" runat="server" Text="返回News編輯" CssClass="btn btn-default" OnClick="ReturnDetails_Click" />
                        <asp:Button ID="Button2" runat="server" Text="編輯檔案" CssClass="btn btn-info" OnClick="EditFile_Click" />
                        <asp:Button ID="CloseUpdatePanel" runat="server" Text="返回主畫面" CssClass="btn btn-info" OnClick="CloseUpdatePanel_Click" />
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>

    <%--編輯檔案--%>
    <asp:Panel ID="UpdateFilesPanel" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>編輯News檔案</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <div style="display: flex; justify-content: center; align-items: center; gap: 20px; padding-top: 20px; padding-bottom: 20px;">
                        <p style="font-size: 18px; font-weight: bold; margin-bottom: 0px;">新增檔案 : </p>
                        <asp:FileUpload ID="AddFile" runat="server" AllowMultiple="True" />
                        <asp:Button ID="InsertFileButton" runat="server" Text="新增" CssClass="btn btn-default" OnClick="InsertFileButton_Click" />
                    </div>

                    <asp:GridView ID="GV_Files" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" BorderColor="White" DataKeyNames="ID" OnRowDeleting="GV_Files_RowDeleting">

                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="NewsFile" HeaderText="NewsFile" />

                            <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="align-middle" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteFile" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定刪除嗎?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                    <%--新增、返回按鈕--%>
                    <div style="display: flex; justify-content: center; gap: 30px; padding: 20px; margin-top: 30px;">
                        <asp:Button ID="Button3" runat="server" Text="返回News編輯" CssClass="btn btn-default" OnClick="ReturnDetails_Click" />
                        <asp:Button ID="EditImageButton" runat="server" Text="編輯圖片" CssClass="btn btn-info" OnClick="EditImageButton_Click" />
                        <asp:Button ID="Button5" runat="server" Text="返回主畫面" CssClass="btn btn-info" OnClick="CloseUpdatePanel_Click" />
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>

    <%--新增News--%>
    <asp:Panel ID="CreateNewsPanel" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>News資料</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">

                    <%--標題、內容新增--%>
                    <div style="display: flex; flex-direction: column; gap: 30px; margin-top: 30px;">
                        <div style="display: flex; align-items: center; justify-content: center; gap: 10px;">
                            <asp:Label ID="Label1" runat="server" Text="新增標題 : " Font-Bold="True" Font-Size="Large"></asp:Label>
                            <asp:TextBox ID="AddTitle" runat="server" CssClass="form-control" Width="50%"></asp:TextBox>
                        </div>
                        <div style="text-align: center;">
                            <hr />
                            <asp:Label ID="Label2" runat="server" Text="新增內容" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                            <CKEditor:CKEditorControl ID="ContentEditor" runat="server" BasePath="/Scripts/ckeditor/" Height="200px"></CKEditor:CKEditorControl>
                        </div>
                    </div>

                    <%--新增的資料置頂為false--%>
                    <asp:CheckBox ID="TopDefault" runat="server" Visible="False" />

                    <%--照片、檔案上傳--%>
                    <div style="display: flex; justify-content: space-around; margin-top: 30px;">
                        <div>
                            <asp:Label ID="Label3" runat="server" Text="上傳照片 : "></asp:Label>
                            <asp:FileUpload ID="UploadImage" runat="server" AllowMultiple="True" />
                        </div>
                        <div>
                            <asp:Label ID="Label4" runat="server" Text="上傳檔案 : "></asp:Label>
                            <asp:FileUpload ID="UploadFile" runat="server" AllowMultiple="True" />
                        </div>
                    </div>

                    <%--新增、返回按鈕--%>
                    <div style="display: flex; justify-content: center; gap: 30px; padding: 20px; margin-top: 30px;">
                        <asp:Button ID="AddNews" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddNews_Click" />
                        <asp:Button ID="CloseAddNews" runat="server" Text="返回" CssClass="btn btn-default" OnClick="CloseAddNews_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
