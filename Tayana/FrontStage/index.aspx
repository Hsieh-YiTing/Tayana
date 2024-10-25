<%@ Page Title="" Language="C#" MasterPageFile="~/FrontStage/Site_FS.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Tayana.FrontStage.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Index</title>
    <script type="text/javascript" src="/FrontStage/Tayanahtml/html/Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="/FrontStage/Tayanahtml/html/Scripts/jquery.cycle.all.2.74.js"></script>
    <script type="text/javascript">


        $(function () {

            // 先取得 #abgne-block-20110111 , 必要參數及輪播間隔
            var $block = $('#abgne-block-20110111'),
                timrt, speed = 4000;


            // 幫 #abgne-block-20110111 .title ul li 加上 hover() 事件
            var $li = $('.title ul li', $block).hover(function () {
                // 當滑鼠移上時加上 .over 樣式
                $(this).addClass('over').siblings('.over').removeClass('over');
            }, function () {
                // 當滑鼠移出時移除 .over 樣式
                $(this).removeClass('over');
            }).click(function () {
                // 當滑鼠點擊時, 顯示相對應的 div.info
                // 並加上 .on 樣式

                $(this).addClass('on').siblings('.on').removeClass('on');
                $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().hide().eq($(this).index()).fadeIn(1000);
            });

            // 幫 $block 加上 hover() 事件
            $block.hover(function () {
                // 當滑鼠移上時停止計時器
                clearTimeout(timer);
            }, function () {
                // 當滑鼠移出時啟動計時器
                timer = setTimeout(move, speed);
            });

            // 控制輪播
            function move() {
                var _index = $('.title ul li.on', $block).index();
                _index = (_index + 1) % $li.length;
                $li.eq(_index).click();

                timer = setTimeout(move, speed);
            }

            // 啟動計時器
            timer = setTimeout(move, speed);

        });


    </script>
    <!--[if lt IE 7]>
<script type="text/javascript" src="javascript/iepngfix_tilebg.js"></script>
<![endif]-->
    <link href="/FrontStage/Tayanahtml/html/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/FrontStage/Tayanahtml/html/css/reset.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="/FrontStage/Tayanahtml/html/images/banner00_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->

    <!--------------------------------換圖開始---------------------------------------------------->
    <div id="abgne-block-20110111">
        <div class="bd">
            <div class="banner">
                <ul>
                    <asp:Literal ID="ShowAlbum" runat="server"></asp:Literal>
                </ul>

                <!--小圖開始-->
                <div class="bannerimg title">
                    <ul>
                        <asp:Literal ID="SmallPhoto" runat="server"></asp:Literal>
                    </ul>
                </div>
                <!--小圖結束-->
            </div>
        </div>
    </div>
    <!--------------------------------換圖結束---------------------------------------------------->
    <!--------------------------------最新消息---------------------------------------------------->
    <div class="news">
        <div class="newstitle">
            <p class="newstitlep1">
                <img src="/FrontStage/Tayanahtml/html/images/news.gif" alt="news" /></p>
            <p class="newstitlep2"><a href="News.aspx">More>></a></p>
        </div>
        <ul>
            <asp:Literal ID="NewsList" runat="server"></asp:Literal>
        </ul>
    </div>
    <!--------------------------------最新消息結束---------------------------------------------------->

</asp:Content>
