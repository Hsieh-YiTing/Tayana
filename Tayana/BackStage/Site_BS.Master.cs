using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.BackStage
{
    public partial class Site_BS : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                string name = Request.QueryString["name"];
                BS_Name.Text = name;
            }
        }

        protected void RedirectYachts_Click(object sender, EventArgs e)
        {
            Response.Redirect("Yachts_BS.aspx?name=Yachts Manage");
        }

        protected void RedirectNews_Click(object sender, EventArgs e)
        {
            Response.Redirect("News_BS.aspx?name=News Manage");
        }

        protected void RedirectCompany_Click(object sender, EventArgs e)
        {
            Response.Redirect("Company_BS.aspx?name=Company Manage");
        }

        protected void RedirectDealers_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dealers_BS.aspx?name=Dealers Manage");
        }

        protected void RedirectUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserTable.aspx?name=Users Mange");
        }

        protected void YachtsFront_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontStage/Yachts.aspx");
        }

        protected void ContactPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontStage/Contact.aspx");
        }

        protected void DealersFront_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontStage/Dealers_FS.aspx");
        }

        protected void CompanyFront_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontStage/AboutUs.aspx");
        }

        protected void NewFront_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontStage/News.aspx");
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();

            Session.Clear();

            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
                {
                    Expires = DateTime.Now.AddYears(-1)
                };
                Response.Cookies.Add(cookie);
            }

            Response.Redirect("Login.aspx");
        }
    }
}