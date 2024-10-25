<%@ Page Title="" Language="C#" MasterPageFile="~/FrontStage/Site_FS.Master" AutoEventWireup="true" CodeBehind="Certificate.aspx.cs" Inherits="Tayana.FrontStage.Certificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Company</title>
    <link href="Tayanahtml/html/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="Tayanahtml/html/css/homestyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Tayanahtml/html/Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Tayanahtml/html/Scripts/jquery.cycle.all.2.74.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.slideshow').cycle({
                fx: 'fade' // choose your transition type, ex: fade, scrollUp, shuffle, etc...
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--------------------------------換圖開始---------------------------------------------------->

    <div class="banner">
        <ul>
            <li>
                <img src="./Company/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>

    </div>
    <!--------------------------------換圖結束---------------------------------------------------->

    <div class="conbg">

        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>COMPANY</span></p>
                <ul>
                    <li>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="AboutUs.aspx">About Us</asp:HyperLink></li>
                    <li>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Certificate.aspx">Certificate</asp:HyperLink></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb">
            <a href="index.aspx">Home</a> >> <a href="AboutUs.aspx">Company</a>>>
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="Certificate.aspx">Certificate</asp:HyperLink>
        </div>
        <div class="right">
            <div class="right1">
                <div class="title">
                    <span>Certificate</span>
                </div>

                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box3">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <div class="pit" style="margin-top: 20px;">
                        <ul style="display: flex; flex-wrap: wrap;">
                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                        </ul>
                    </div>
                </div>

                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>
        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
