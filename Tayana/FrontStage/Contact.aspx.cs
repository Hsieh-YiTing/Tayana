using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Tayana.FrontStage
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                CountryListBind();
                YachtsListBind();
            }
        }

        protected void CountryListBind()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From DealersArea";
            using (SqlDataReader readArea = helper.SerachDB(sql))
            {
                helper.CategoryList(readArea, CountryList, "Area", "ID");
            }
        }

        protected void YachtsListBind()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From Yachts";
            using (SqlDataReader readArea = helper.SerachDB(sql))
            {
                helper.CategoryList(readArea, YachtsList, "Yachts", "ID");
            }
        }

        protected void SendGmail()
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("TayanaYacht", "Tayana@gmail.com"));

            message.To.Add(new MailboxAddress(NameBox.Text.Trim(), MailBox.Text.Trim()));

            message.Cc.Add(new MailboxAddress("Hsieh", "hsieh.069@gmail.com"));

            message.Subject = "TayanaYacht Auto Email";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
                "<h1>Thank you for contacting us!</h1>" +
                $"<h3>Name : {NameBox.Text.Trim()}</h3>" +
                $"<h3>Email : {MailBox.Text.Trim()}</h3>" +
                $"<h3>Phone : {PhoneBox.Text.Trim()}</h3>" +
                $"<h3>Country : {CountryList.SelectedItem.Text}</h3>" +
                $"<h3>Type : {YachtsList.SelectedItem.Text}</h3>" +
                $"<h3>Comments : </h3>" +
                $"<p>{CommentsBox.Text.Trim()}</p>";

            // 設定郵件內容
            message.Body = bodyBuilder.ToMessageBody(); // 轉成郵件內容格式

            using (var client = new SmtpClient())
            {
                // 有開防毒時需設定 false 關閉檢查
                client.CheckCertificateRevocation = false;

                // 設定連線 gmail ("smtp Server", Port, SSL加密) 
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉 

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("hsieh.069@gmail.com", "ozbgyarnzsytrcew");

                // 發信
                client.Send(message);

                // 結束連線
                client.Disconnect(true);
            }
        }

        protected void ImageBtn_Click(object sender, EventArgs e)
        {
            SendGmail();
            Response.Write("<script>alert('寄信成功')</script>");
            NameBox.Text = string.Empty;
            MailBox.Text = string.Empty;
            PhoneBox.Text = string.Empty;
            CommentsBox.Text = string.Empty;
        }
    }
}