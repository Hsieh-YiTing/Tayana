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
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadContent();
        }

        protected void LoadContent()
        {
            StringBuilder builder = new StringBuilder();

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From News Order By IsTop Desc, ID Desc";
            string newsID, title, coverPhoto, createTime;

            using (SqlDataReader reader = helper.SerachDB(sql))
            { 
                while (reader.Read()) 
                {
                    newsID = reader["ID"].ToString();
                    title = reader["Title"].ToString();
                    coverPhoto = reader["CoverPhoto"].ToString();
                    DateTime date = (DateTime)reader["CreateTime"];
                    createTime = date.ToString("yyyy-MM-dd");

                    builder.Append("<li><div class='list01'><ul>");
                    builder.Append($"<li><div><p><img src='/{coverPhoto}' style='width:199px; height:133px'/></p></div></li>");
                    builder.Append($"<span>{createTime}</span><br/><a href='Events.aspx?ID={newsID}'>{title}</a>");
                    builder.Append("</ul></div></li>");
                }
            }

            Content.Text = builder.ToString();
        }
    }
}