<%@ Page Title="" Language="C#" MasterPageFile="~/FrontStage/Site_FS.Master" AutoEventWireup="true" CodeBehind="Layout.aspx.cs" Inherits="Tayana.FrontStage.Layout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Layout</title>
    <link href="Tayanahtml/html/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="Tayanahtml/html/css/homestyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/FrontStage/Tayanahtml/html/css/jquery.ad-gallery.css">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript" src="/FrontStage/Tayanahtml/html/Scripts/jquery.ad-gallery.js"></script>
    <script type="text/javascript">
        $(function () {

            var galleries = $('.ad-gallery').adGallery();
            galleries[0].settings.effect = 'slide-hori';



        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--遮罩-->
    <div class="bannermasks">
        <img src="/FrontStage/Tayanahtml/html/images/banner01_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->

    <div class="banner">
        <div id="gallery" class="ad-gallery">
            <div class="ad-image-wrapper">
            </div>
            <div class="ad-controls" style="display: none">
            </div>
            <div class="ad-nav">
                <div class="ad-thumbs">
                    <ul class="ad-thumb-list">
                        <asp:Literal ID="ImageBanner" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <div class="conbg">

        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>YACHTS</span></p>
                <ul>
                    <asp:Literal ID="YachtsList" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb">
            <a href="index.aspx">Home</a> >> <a href="Yachts.aspx">Yachts</a>>>
        <asp:HyperLink ID="ModelLink" runat="server"></asp:HyperLink>
        </div>
        <div class="right">
            <div class="right1">
                <div class="title">
                    <span>
                        <asp:Literal ID="YachtsModel" runat="server"></asp:Literal></span>
                </div>

                <!--次選單-->
                <div class="menu_y">
                    <ul>
                        <li class="menu_y00">YACHTS</li>
                        <li>
                            <asp:HyperLink ID="OverViewLink" runat="server" CssClass="menu_yli01"></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="LayoutLink" runat="server" CssClass="menu_yli02"></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="SpecificationLink" runat="server" CssClass="menu_yli03"></asp:HyperLink></li>
                    </ul>
                </div>
                <!--次選單-->

                <div class="box6">
                    <p>Layout & deck plan</p>
                    <ul>
                        <asp:Literal ID="LayoutImage" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
