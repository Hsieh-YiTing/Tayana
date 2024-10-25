<%@ Page Title="" Language="C#" MasterPageFile="~/FrontStage/Site_FS.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="Tayana.FrontStage.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>News</title>
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
                <p><span>NEWS</span></p>
                <ul>
                    <li><a href="News.aspx">News & Events</a></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb">
            <a href="index.aspx">Home</a> >> <a href="News.aspx">News</a>>>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="News.aspx">News & Events</asp:HyperLink>
        </div>
        <div class="right">
            <div class="right1">
                <div class="title">
                    <span>News & Events</span>
                </div>

                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box2_list">

                    <ul>
                        <asp:Literal ID="Content" runat="server">
                        </asp:Literal>
                    </ul>

                </div>

                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>
        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>

</asp:Content>
