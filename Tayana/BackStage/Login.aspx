<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tayana.BackStage.Login" MaintainScrollPositionOnPostback="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Login</title>
    <!-- HTML5 Shim and Respond.js IE9 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
      <![endif]-->
    <!-- Meta -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="Phoenixcoded">
    <meta name="keywords" content=", Flat ui, Admin , Responsive, Landing, Bootstrap, App, Template, Mobile, iOS, Android, apple, creative app">
    <meta name="author" content="Phoenixcoded">
    <!-- Favicon icon -->

    <link rel="icon" href="mashable-lite/assets/images/favicon.ico" type="image/x-icon">
    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Mada:300,400,500,600,700" rel="stylesheet">
    <!-- Required Fremwork -->
    <link rel="stylesheet" type="text/css" href="mashable-lite/assets/plugins/bootstrap/dist/css/bootstrap.min.css">
    <!-- themify-icons line icon -->
    <link rel="stylesheet" type="text/css" href="mashable-lite/assets/icon/themify-icons/themify-icons.css">
    <!-- ico font -->
    <link rel="stylesheet" type="text/css" href="mashable-lite/assets/icon/icofont/css/icofont.css">
    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="mashable-lite/assets/css/style.css">
</head>
<body>
    <form id="form1" runat="server">
        <body class="fix-menu">
            <section class="login p-fixed d-flex text-center bg-primary common-img-bg">
                <!-- Container-fluid starts -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <!-- Authentication card start -->
                            <div class="login-card card-block auth-body">
                                <form class="md-float-material">
                                    <div class="auth-box">
                                        <div class="row m-b-20">
                                            <div class="col-md-3">
                                                <h3 class="text-center txt-primary">Sign In</h3>
                                            </div>
                                            <div class="col-md-9">
                                                <p class="text-inverse m-t-25 text-left">Don't have an account? <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Register.aspx">Register</asp:HyperLink> here for free!</p>
                                                <%--<p class="text-inverse m-t-25 text-left">Don't have an account? <a href="">Register </a>here for free!</p>--%>
                                            </div>
                                        </div>
                                        <div class="row m-b-20">
                                            <div class="col-md-6">
                                                <button class="btn btn-facebook m-b-20"><i class="icofont icofont-social-facebook"></i>Sign in with facebook</button>
                                            </div>
                                            <div class="col-md-6">
                                                <button class="btn btn-twitter m-b-20"><i class="icofont icofont-social-twitter"></i>Sign in with twitter</button>
                                            </div>
                                        </div>
                                        <div class="input-group">
                                            <asp:TextBox ID="UserName" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                            <%--<input type="email" class="form-control" placeholder="Username">--%>
                                            <span class="md-line"></span>
                                        </div>
                                        <div class="input-group">
                                            <asp:TextBox ID="Password" runat="server" CssClass="form-control" placeholder="password" TextMode="Password"></asp:TextBox>
                                            <%--<input type="password" class="form-control" placeholder="password">--%>
                                            <span class="md-line"></span>
                                        </div>
                                        <div class="row m-t-25 text-left">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="checkbox-fade fade-in-primary">
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12 forgot-phone text-right">
                                            </div>
                                        </div>
                                        <div class="row m-t-30">
                                            <div class="col-md-12">
                                                <asp:Button ID="LoginBtn" runat="server" Text="Login" CssClass="btn btn-info" OnClick="LoginBtn_Click"/>
                                                <%--<button type="button" class="btn btn-primary btn-md btn-block waves-effect text-center m-b-20">LOGIN</button>--%>
                                            </div>
                                        </div>
                                        <!-- <div class="card-footer"> -->
                                        <!-- <div class="col-sm-12 col-xs-12 text-center">
                                    <span class="text-muted">Don't have an account?</span>
                                    <a href="register2.html" class="f-w-600 p-l-5">Sign up Now</a>
                                </div> -->
                                        <!-- </div> -->
                                    </div>
                                </form>
                                <!-- end of form -->
                            </div>
                            <!-- Authentication card end -->
                        </div>
                        <!-- end of col-sm-12 -->
                    </div>
                    <!-- end of row -->
                </div>
                <!-- end of container-fluid -->
            </section>
            <!-- Warning Section Starts -->
            <!-- Older IE warning message -->
            <!--[if lt IE 9]>
<div class="ie-warning">
    <h1>Warning!!</h1>
    <p>You are using an outdated version of Internet Explorer, please upgrade <br/>to any of the following web browsers to access this website.</p>
    <div class="iew-container">
        <ul class="iew-download">
            <li>
                <a href="http://www.google.com/chrome/">
                    <img src="mashable-lite/assets/images/browser/chrome.png" alt="Chrome">
                    <div>Chrome</div>
                </a>
            </li>
            <li>
                <a href="https://www.mozilla.org/en-US/firefox/new/">
                    <img src="mashable-lite/assets/images/browser/firefox.png" alt="Firefox">
                    <div>Firefox</div>
                </a>
            </li>
            <li>
                <a href="http://www.opera.com">
                    <img src="mashable-lite/assets/images/browser/opera.png" alt="Opera">
                    <div>Opera</div>
                </a>
            </li>
            <li>
                <a href="https://www.apple.com/safari/">
                    <img src="mashable-lite/assets/images/browser/safari.png" alt="Safari">
                    <div>Safari</div>
                </a>
            </li>
            <li>
                <a href="http://windows.microsoft.com/en-us/internet-explorer/download-ie">
                    <img src="mashable-lite/assets/images/browser/ie.png" alt="">
                    <div>IE (9 & above)</div>
                </a>
            </li>
        </ul>
    </div>
    <p>Sorry for the inconvenience!</p>
</div>
<![endif]-->
            <!-- Warning Section Ends -->
            <!-- Required Jquery -->
            <script type="text/javascript" src="mashable-lite/assets/plugins/jquery/dist/jquery.min.js"></script>
            <script type="text/javascript" src="mashable-lite/assets/plugins/jquery-ui/jquery-ui.min.js"></script>
            <script type="text/javascript" src="mashable-lite/assets/plugins/tether/dist/js/tether.min.js"></script>
            <script type="text/javascript" src="mashable-lite/assets/plugins/bootstrap/dist/js/bootstrap.min.js"></script>
            <!-- jquery slimscroll js -->
            <script type="text/javascript" src="mashable-lite/assets/plugins/jquery-slimscroll/jquery.slimscroll.js"></script>
            <!-- modernizr js -->
            <script type="text/javascript" src="mashable-lite/assets/plugins/modernizr/modernizr.js"></script>
            <script type="text/javascript" src="mashable-lite/assets/plugins/modernizr/feature-detects/css-scrollbars.js"></script>
            <!-- Custom js -->
            <script type="text/javascript" src="mashable-lite/assets/js/script.js"></script>
            <!---- color js --->
            <script type="text/javascript" src="mashable-lite/assets/js/common-pages.js"></script>
        </body>
    </form>
</body>
</html>
