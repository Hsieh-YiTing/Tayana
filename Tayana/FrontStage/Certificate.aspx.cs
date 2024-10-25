using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.FrontStage
{
    public partial class Certificate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowCertificate();
            ShowAlbum();
        }

        protected void ShowCertificate()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            string sql = "Select * From Certificate";

            using (SqlDataReader reader = helper.SerachDB(sql))
            {
                if (reader.Read())
                { 
                    Literal1.Text = reader["Certificate"].ToString();
                    HiddenField1.Value = reader["ID"].ToString();
                }
            }
        }

        protected void ShowAlbum()
        {
            StringBuilder builder = new StringBuilder();
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            int id = Convert.ToInt32(HiddenField1.Value);
            string sql = "Select * From CertificateAlbums Where CertificateID = @CertificateID";
            var parameters = new Dictionary<string, object> 
            {
                { "@CertificateID", id}
            };

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            { 
                while (reader.Read()) 
                {
                    string dbPath = reader["CertificateImage"].ToString();
                    string imagePath = ResolveUrl($"~/{dbPath}");
                    builder.Append($"<li><p><img src='{imagePath}' alt='預覽圖片' Width='319px' Height='234px'></p></li>");
                }
            }

            Literal2.Text = builder.ToString();
        }
    }
}