<%@ Page Title="" Language="C#" MasterPageFile="~/BackStage/Site_BS.Master" AutoEventWireup="true" CodeBehind="AddDimensions.aspx.cs" Inherits="Tayana.BackStage.AddDimensions" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">
        <div class="card-header">
            <h5>新增Dimensions</h5>
        </div>
        <div class="card-block table-border-style">
            <div class="table-responsive">
                <table class="table table-hover" style="text-align: center;">
                    <tbody>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Hull length : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="HullLength" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">L.W.L. : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="LWL" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">B. MAX : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="BMAX" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Standard draft : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="StandardDraft" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Ballast : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="Ballast" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Displacement : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="Displacement" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Sail area : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="SailArea" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Engine diesel(選填) : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="EngineDiesel" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Cutter(選填) : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="Cutter" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Designer(選填) : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="Designer" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Shallow Draft Keel(選填) : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="ShallowDraftKeel" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Head room(選填) : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="HeadRoom" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">100% Yankee(選填) : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="Yankee" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Staysall(選填) : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="Staysall" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Genoa 130%(選填) : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="Genoa" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Flasher 165%(選填) : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="Flasher" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">Design speed(選填) : </th>
                            <td style="text-align: left;">
                                <asp:TextBox ID="DesignSpeed" runat="server" CssClass="form-control" Width="70%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row" style="padding-left: 10%" class="align-middle">上傳照片(選填) : </th>
                            <td style="text-align: left;">
                                <asp:FileUpload ID="AddImage" runat="server" /></td>
                        </tr>
                    </tbody>
                </table>

                <%--新增Dimensions與取消--%>
                <div style="display:flex; justify-content: center; align-items: center; gap: 30px; padding: 20px">
                    <asp:Button ID="AddDimension" runat="server" Text="新增Dimensions" CssClass="btn btn-default" OnClick="AddDimension_Click" />
                    <asp:Button ID="ReturnIndex" runat="server" Text="取消" OnClick="ReturnIndex_Click" CssClass="btn btn-default"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
