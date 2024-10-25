using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.BackStage
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            string userName = UserName.Text;
            string password = Password.Text;
            string checkPassword = ConfirmPassword.Text;
            int defaultPremissions = Convert.ToInt32(DefaultPremissions.Text);

            if (string.IsNullOrEmpty(userName))
            {
                Response.Write("<script>alert('UserName不能為空')</script>");
                return;
            }

            if (CheckUserName(userName))
            {
                Response.Write("<script>alert('UserName已經存在，請輸入其他名稱')</script>");
                UserName.Text = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('密碼不能為空')</script>");
                return;
            }

            if (string.IsNullOrEmpty(checkPassword))
            {
                Response.Write("<script>alert('確認密碼不能為空')</script>");
                return;
            }

            if (password != checkPassword)
            {
                Response.Write("<script>alert('輸入密碼與確認密碼不相符，請重新輸入')</script>");
                return;
            }
            else
            {
                // 使用var會自動解構，如果要手動給類型，需要將每個元素手動宣告
                // (string hashedPassword, string salt) = HashPassword(Password.Text);
                var (hashedPassword, salt) = HashPassword(password);

                string sql = "Insert Into UserTable (PremissionsID, UserName, Password, Salt) Values (@PremissionsID, @UserName, @Password, @Salt)";
                string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
                DBHelper helper = new DBHelper(connectionString);

                helper.InsertUser(sql, defaultPremissions, userName, hashedPassword, salt);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('註冊成功，即將返回登入頁面'); setTimeout(function(){ window.location.href = 'Login.aspx'; }, 2000);", true);
            }
        }

        // 檢查UserName有沒有相同
        protected bool CheckUserName(string userName)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select Count(*) From UserTable Where UserName = @UserName";
            int count = helper.CheckUser(sql, userName);
            return count > 0;
        }

        // 可以使用"元組"來返回多個值
        protected (string hashedPassword, string salt) HashPassword(string password)
        {
            // 透過RNGCryptoServiceProvider生成隨機的16byte鹽值
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            // 使用PBKDF2演算法生成雜湊值，第一個參數為密碼、第二為鹽值、第三個為迭代次數
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // 最後將鹽值與雜湊值組合
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // 將雜湊值與鹽值轉換為Base64 字符串回傳
            string hashedPassword = Convert.ToBase64String(hashBytes);
            string saltString = Convert.ToBase64String(salt);

            return (hashedPassword, saltString);
        }

        // 返回登入頁面
        protected void ReturnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}