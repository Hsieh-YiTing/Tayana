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
    public partial class Company : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                AboutUs();
            }
        }

        protected void AboutUs()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            string sql = "Select * From AboutUs Order By CreateTime Desc";

            using (SqlDataReader reader = helper.SerachDB(sql))
            { 
                if (reader.Read()) 
                {
                    Literal1.Text = reader["AboutUs"].ToString();
                    string imagePath = reader["Image"].ToString();
                    Image1.ImageUrl = $"~/{imagePath}";
                }
            }
        }
    }
}