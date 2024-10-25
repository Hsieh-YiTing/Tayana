<%@ Page Title="" Language="C#" MasterPageFile="~/BackStage/Site_BS.Master" AutoEventWireup="true" CodeBehind="AddSpec.aspx.cs" Inherits="Tayana.BackStage.AddSpec" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h5>填寫Spec資料</h5>
        </div>
        <div class="card-block table-border-style">
            <div class="table-responsive">
                <table class="table table-hover" style="text-align: center;">
                    <tbody>
                        <tr>
                            <th scope="row" style="padding-left: 10%;" class="align-middle">Hull</th>
                            <td style="text-align: left; width: 50%;">
                                <asp:TextBox ID="HullItem" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="AddHullItem" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddHullItem_Click" /></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Deck/Hardware</th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="DeckItem" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="AddDeckItem" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddDeckItem_Click" /></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Engine/Machinery</th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="EngineItem" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="AddEngineItem" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddEngineItem_Click" /></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Steering</th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="SteeringItem" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="AddSteeringItem" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddSteeringItem_Click" /></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Spars/Rigging</th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="SparsItem" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="AddSparsItem" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddSparsItem_Click" /></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Sails</th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="SailsItem" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="AddSailsItem" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddSailsItem_Click" /></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Interior</th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="InteriorItem" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="AddInteriorItem" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddInteriorItem_Click" /></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Electrical</th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="ElectricalItem" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="AddElectricalItem" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddElectricalItem_Click" /></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Plumbing</th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="PlumbingItem" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="AddPlumbingItem" runat="server" Text="新增" CssClass="btn btn-default" OnClick="AddPlumbingItem_Click" /></td>
                        </tr>
                    </tbody>
                </table>

                <div style="display: flex; justify-content: center; align-items: center; gap: 30px; padding: 20px">
                    <asp:Button ID="ReturnIndex" runat="server" Text="返回首頁" OnClick="ReturnIndex_Click" CssClass="btn btn-default" />
                    <asp:Button ID="CheckData" runat="server" Text="查看Spec資料" CssClass="btn btn-default" OnClick="CheckData_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
