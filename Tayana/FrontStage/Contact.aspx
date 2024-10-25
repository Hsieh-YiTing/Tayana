<%@ Page Title="" Language="C#" MasterPageFile="~/FrontStage/Site_FS.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Tayana.FrontStage.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Contact</title>
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
    <div class="banner">
        <ul>
            <li>
                <img src="/FrontStage/Tayanahtml/html/images/contact.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>

    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>CONTACT</span></p>
                <ul>
                    <li><a href="Contact.aspx">Contacts</a></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->
        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="index.aspx">Home</a> >> <a href="Contact.aspx"><span class="on1">Contact</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title"><span>Contact</span></div>

                <!--------------------------------內容開始---------------------------------------------------->
                <!--表單-->
                <div class="from01">
                    <p>
                        Please Enter your contact information<span class="span01">*Required</span>
                    </p>
                    <br />
                    <table>
                        <tr>
                            <td class="from01td01">Name :</td>
                            <td><span>*</span><asp:TextBox ID="NameBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="from01td01">Email :</td>
                            <td><span>*</span><asp:TextBox ID="MailBox" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="from01td01">Phone :</td>
                            <td><span>*</span><asp:TextBox ID="PhoneBox" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="from01td01">Country :</td>
                            <td>
                                <span>*</span>
                                <asp:DropDownList ID="CountryList" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><span>*</span>Brochure of interest  *Which Brochure would you like to view?</td>
                        </tr>
                        <tr>
                            <td class="from01td01">Yachts : </td>
                            <td>
                                <asp:DropDownList ID="YachtsList" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="from01td01">Comments : </td>
                            <td>
                                <asp:TextBox ID="CommentsBox" runat="server" TextMode="MultiLine" Height="200px" Width="400px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            
                            <td class="from01td01"></td>
                            <td class="f_right">
                                <asp:ImageButton ID="ImageBtn" runat="server" OnClick="ImageBtn_Click" src="/FrontStage/Tayanahtml/html/images/buttom03.gif"/>
                        </tr>
                    </table>
                </div>
                <!--表單-->

                <div class="box1">
                    <span class="span02">Contact with us</span><br />
                    Thanks for your enjoying our web site as an introduction to the Tayana world and our range of yachts.
                As all the designs in our range are semi-custom built, we are glad to offer a personal service to all our potential customers.
                If you have any questions about our yachts or would like to take your interest a stage further, please feel free to contact us.
                </div>

                <div class="list03">
                    <p>
                        <span>TAYANA HEAD OFFICE</span><br />
                        NO.60 Haichien Rd. Chungmen Village Linyuan Kaohsiung Hsien 832 Taiwan R.O.C<br />
                        tel. +886(7)641 2422<br />
                        fax. +886(7)642 3193<br />
                        info@tayanaworld.com<br />
                    </p>
                </div>


                <div class="list03">
                    <p>
                        <span>SALES DEPT.</span><br />
                        +886(7)641 2422  ATTEN. Mr.Basil Lin<br />
                        <br />
                    </p>
                </div>

                <div class="box4">
                    <h4>Location</h4>
                    <p>
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3682.690636333464!2d120.30793527499284!3d22.62802483093726!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x346e0491b7febacd%3A0x24542bac2726199b!2z5a-25oiQ5LiW57SA5aSn5qiT!5e0!3m2!1szh-TW!2stw!4v1722356470677!5m2!1szh-TW!2stw" width="695" height="518" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                    </p>
                </div>
                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>
        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>

</asp:Content>
