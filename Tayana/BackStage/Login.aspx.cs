using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.BackStage
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            string userName = UserName.Text;
            string password = Password.Text;

            var (verifyUser, premissions) = VerifyIdentity(userName, password);

            if (verifyUser)
            {
                SetAuthenTicket(userName, premissions);
                Response.Redirect("Dealers_BS.aspx");
            }
            else
            {
                Response.Write("<script>alert('用戶名或密碼錯誤');</script>");
            }
        }

        protected (bool verifyUser, string premissions) VerifyIdentity(string userName, string password)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From UserTable Where UserName = @UserName";
            string dbPremissionsID, dbPassword, dbSalt;

            var parameters = new Dictionary<string, object> 
            {
                { "@UserName", userName}
            };

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                if (reader.Read())
                {
                    dbPremissionsID = reader["PremissionsID"].ToString();
                    dbPassword = reader["Password"].ToString();
                    dbSalt = reader["Salt"].ToString();
                }
                else
                {
                    return (false, "-1");
                }
            }

            byte[] saltBytes = Convert.FromBase64String(dbSalt);
            string hashedPassword = HashPasswordWithSalt(password, saltBytes);

            if (dbPassword == hashedPassword)
            {
                return (true, dbPremissionsID);
            }
            else
            {
                return (false, "-1");
            }
        }

        // 輸入密碼時，先取出資料庫的鹽值轉為byte[]，並使用當初註冊的運算方式，最後回傳計算的值
        protected string HashPasswordWithSalt(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        protected void SetAuthenTicket(string userName, string premissions)
        {
            // 宣告新的驗證票證
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (
                    1,
                    userName,
                    DateTime.Now, 
                    DateTime.Now.AddHours(3), 
                    false, 
                    premissions
                );

            // 加密驗證票證，返回加密後的字符串，確保票證內容的安全性
            // 沒有加密的狀況，則是純文本儲存在cookie中
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            // 建立Cookie，默認名稱為.ASPXAUTH
            HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            //將Cookie寫入回應
            Response.Cookies.Add(authenticationcookie);
        }
    }
}