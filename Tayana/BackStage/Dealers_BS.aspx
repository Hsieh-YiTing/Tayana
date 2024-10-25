<%@ Page Title="" Language="C#" MasterPageFile="~/BackStage/Site_BS.Master" AutoEventWireup="true" CodeBehind="Dealers_BS.aspx.cs" Inherits="Tayana.BackStage.Dealers_BS" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!--選擇國家-->
    <asp:Panel ID="CountryPanel" runat="server">
        <div class="card">
            <div class="card-header">
                <h5>選擇國家或新增國家</h5>
            </div>
            <div style="display: flex; align-items: center; gap: 20%;">
                <div style="padding: 10px; width: 20%">
                    <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Button ID="AddCountryPanel" runat="server" Text="新增國家" CssClass="btn btn-default" OnClick="AddCountryPanel_Click" />
                </div>
                <div>
                    <asp:Button ID="DeleteCountry" runat="server" Text="刪除國家" CssClass="btn btn-danger" OnClick="DeleteCountry_Click" OnClientClick="return confirm('確定刪除嗎?');" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- 新增國家 -->
    <asp:Panel ID="CreateCountry" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>新增國家</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <div>
                        <div style="display: flex; align-items: center; gap: 30px; padding: 10px; justify-content: center;">
                            <asp:Label ID="Label8" runat="server" Text="輸入新的國家 :"></asp:Label>
                            <asp:TextBox ID="InputNewCountry" runat="server" CssClass="form-control" Width="30%"></asp:TextBox>
                            <asp:Button ID="InsertCountry" runat="server" Text="新增" CssClass="btn btn-default" OnClick="InsertCountry_Click" />
                            <asp:Button ID="CloseCreateCountry" runat="server" Text="關閉" CssClass="btn btn-default" OnClick="CloseCreateCountry_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!--選擇地區-->
    <asp:Panel ID="AreaPanel" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>選擇地區或新增地區</h5>
            </div>
            <div style="display: flex; align-items: center; gap: 20%;">
                <div style="padding: 10px; width: 20%">
                    <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Button ID="AddAreaPanel" runat="server" Text="新增地區" CssClass="btn btn-default" OnClick="AddAreaPanel_Click" />
                </div>
                <div>
                    <asp:Button ID="DeleteArea" runat="server" Text="刪除地區" CssClass="btn btn-danger" OnClientClick="return confirm('確定刪除嗎?');" OnClick="DeleteArea_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- 新增地區 -->
    <asp:Panel ID="CreateArea" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>新增地區</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <div>
                        <div style="display: flex; align-items: center; gap: 10px; padding: 10px; justify-content: space-between;">
                            <asp:Label ID="SelectCountry" runat="server" CssClass="form-control" Width="20%"></asp:Label>
                            <div style="display: flex; gap: 20px; align-items: center;">
                                <asp:Label ID="Label9" runat="server" Text="輸入新的地區 :"></asp:Label>
                                <asp:TextBox ID="InputNewArea" runat="server" CssClass="form-control" Width="50%"></asp:TextBox>
                            </div>
                            <div style="width: 30%; display: flex; justify-content: space-around; align-items: center;">
                                <asp:Button ID="InsertArea" runat="server" Text="新增" CssClass="btn btn-default" OnClick="InsertArea_Click" />
                                <asp:Button ID="CloseCreateArea" runat="server" Text="關閉" CssClass="btn btn-default" OnClick="CloseCreateArea_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!--經銷商顯示-->
    <asp:Panel ID="DealersPanel" runat="server" Visible="true">
        <div class="card">
            <div class="card-header">
                <h5>經銷商資料</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <asp:GridView ID="GV_Dealers" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CssClass="table table-hover" BorderColor="White" OnSelectedIndexChanged="GV_Dealers_SelectedIndexChanged" OnRowDeleting="GV_Dealers_RowDeleting">
                        <Columns>
                            <%--ID欄位--%>
                            <asp:BoundField DataField="ID" HeaderText="ID"></asp:BoundField>

                            <%--國家欄位--%>
                            <asp:BoundField DataField="Country" HeaderText="Country" />

                            <%--地區欄位--%>
                            <asp:BoundField DataField="Area" HeaderText="Area" />

                            <%--經銷商名稱--%>
                            <asp:BoundField DataField="DealerName" HeaderText="DealerName" />

                            <%--詳細地址--%>
                            <asp:BoundField DataField="Location" HeaderText="Location" />

                            <%--選取按鈕--%>
                            <asp:CommandField ShowSelectButton="True" />

                            <%--刪除按鈕，轉為<ItemTemplate>後，就可以加入OnClientClick="return confirm('確定刪除嗎?');"--%>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定刪除嗎?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <%--新增後打開CreateDealersPanel--%>
                    <div style="text-align: center; padding: 20px">
                        <asp:Button ID="CreateDealers" runat="server" Text="新增經銷商" CssClass="btn btn-default" OnClick="CreateDealers_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--保存Dealers的ID--%>
    <asp:HiddenField ID="DealersID" runat="server" />

    <%--新增經銷商資料--%>
    <asp:Panel ID="CreateDealersPanel" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>新增經銷商</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <table class="table table-hover" style="text-align: center;">
                        <tbody>
                            <tr>
                                <th scope="row" style="padding-left: 10%" class="align-middle">選擇國家</th>
                                <td style="text-align: left;"><asp:DropDownList ID="SelectCountryList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectCountryList_SelectedIndexChanged" CssClass="form-control" Width="30%"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 10%" class="align-middle">選擇地區</th>
                                <td style="text-align: left;"><asp:DropDownList ID="SelectAreaList" runat="server" CssClass="form-control" Width="30%"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 10%" class="align-middle">選擇照片</th>
                                <td style="text-align: left;"><asp:FileUpload ID="UploadImage" runat="server" /></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 10%" class="align-middle">經銷商名稱</th>
                                <td style="text-align: left;"><asp:TextBox ID="AddDealersName" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 10%" class="align-middle">聯絡人</th>
                                <td style="text-align: left;"><asp:TextBox ID="AddContactPerson" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 10%" class="align-middle">地址</th>
                                <td style="text-align: left;"><asp:TextBox ID="AddLocation" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 10%" class="align-middle">聯絡電話</th>
                                <td style="text-align: left;"><asp:TextBox ID="AddPhone" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 10%" class="align-middle">傳真</th>
                                <td style="text-align: left;"><asp:TextBox ID="AddFax" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 10%" class="align-middle">信箱</th>
                                <td style="text-align: left;"><asp:TextBox ID="AddMail" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 10%" class="align-middle">連結</th>
                                <td style="text-align: left;"><asp:TextBox ID="AddLink" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="display: flex; justify-content: center; gap: 50px; padding: 2%;">
                        <asp:Button ID="AddDealers" runat="server" Text="新增資料" CssClass="btn btn-default" OnClick="AddDealers_Click" />
                        <asp:Button ID="CancelCreate" runat="server" Text="取消" CssClass="btn btn-default" OnClick="CancelCreate_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--詳細資料--%>
    <asp:Panel ID="DetailsPanel" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <h5>編輯詳細資料</h5>
            </div>
            <div class="card-block table-border-style">
                <div class="table-responsive">
                    <table class="table table-hover" style="text-align: center;">
                        <tbody>
                            <tr>
                                <th scope="row" style="padding-left: 5%" class="align-middle"><asp:Label ID="OriginCountry" runat="server"></asp:Label></th>
                                <td style="text-align: left;">選擇國家為 : <asp:DropDownList ID="UpdateCountry" runat="server" CssClass="form-control" Width="40%" AutoPostBack="True" OnSelectedIndexChanged="UpdateCountry_SelectedIndexChanged"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 5%" class="align-middle"><asp:Label ID="OriginArea" runat="server"></asp:Label></th>
                                <td style="text-align: left;">選擇地區為 : <asp:DropDownList ID="UpdateArea" runat="server" CssClass="form-control" Width="40%"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 5%"><asp:Image ID="OriginImage" runat="server" /></th>
                                <td class="align-middle" style="text-align: left;"><asp:FileUpload ID="UpdateImage" runat="server" /></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 5%" class="align-middle">原有名稱為 : <asp:Label ID="OriginDealersName" runat="server"></asp:Label></th>
                                <td style="text-align: left;">更新經銷商名稱 : <asp:TextBox ID="UpdateDealersName" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 5%" class="align-middle">原有地址為 : <asp:Label ID="OriginLocation" runat="server"></asp:Label></th>
                                <td style="text-align: left;">更新地址 : <asp:TextBox ID="UpdateLocation" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 5%" class="align-middle">原有聯絡人為 : <asp:Label ID="OriginContactPerson" runat="server"></asp:Label></th>
                                <td style="text-align: left;">更新聯絡人 : <asp:TextBox ID="UpdateContactPerson" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 5%" class="align-middle">原有電話為 : <asp:Label ID="OriginPhone" runat="server"></asp:Label></th>
                                <td style="text-align: left;">更新聯絡電話 : <asp:TextBox ID="UpdatePhone" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 5%" class="align-middle">原有傳真為 : <asp:Label ID="OriginFax" runat="server"></asp:Label></th>
                                <td style="text-align: left;">更新傳真 : <asp:TextBox ID="UpdateFax" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 5%" class="align-middle">原有信箱為 : <asp:Label ID="OriginMail" runat="server"></asp:Label></th>
                                <td style="text-align: left;">更新郵件 : <asp:TextBox ID="UpdateMail" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 5%" class="align-middle">原有連結為 : <asp:Label ID="OriginLink" runat="server"></asp:Label></th>
                                <td style="text-align: left;">更新連結 : <asp:TextBox ID="UpdateLink" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <th scope="row" style="padding-left: 5%" class="align-middle"><asp:Label ID="Label1" runat="server">建立時間</asp:Label></th>
                                <td style="text-align: left;"><asp:TextBox ID="CreateTime" runat="server" CssClass="form-control" Width="70%" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="display: flex; justify-content: center; gap: 50px; padding: 2%;">
                        <asp:Button ID="UpdateData" runat="server" Text="更新資料" CssClass="btn btn-default" OnClick="UpdateData_Click" />
                        <asp:Button ID="CancelUpdate" runat="server" Text="取消" CssClass="btn btn-default" OnClick="CancelUpdate_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
