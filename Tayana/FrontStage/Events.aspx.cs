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
    public partial class Events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadEvents();
        }

        protected void LoadEvents()
        { 
            StringBuilder builder = new StringBuilder();
            string newsID = Request.QueryString["ID"];
            string news = "Select * From News Where ID = @NewsID";
            string files = "Select * From NewsFile Where NewsID = @NewsID";
            string images = "Select * From NewsAlbum Where NewsID = @NewsID";
            var parameters = new Dictionary<string, object> 
            {
                { "@NewsID", newsID}
            };

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            using (SqlDataReader reader = helper.SerachDB(news, parameters))
            { 
                reader.Read();
                string title = reader["Title"].ToString();
                string content = reader["NewsContent"].ToString();
                builder.Append($"<h4><span id='ctl00_ContentPlaceHolder1_title'>{title}</span></h4>");
                builder.Append($"<span style='font-size:11px;'>{content}</span>");
                Event.Text = builder.ToString();
            }

            builder.Clear();
            using (SqlDataReader reader = helper.SerachDB(files, parameters))
            {
                while (reader.Read())
                {
                    string fileName = reader["FileName"].ToString();
                    string fileLink = reader["NewsFile"].ToString();
                    builder.Append($"<li><a href='/{fileLink}' download='{fileName}'>{fileName}</a></li>");
                }
                File.Text = builder.ToString();
            }

            builder.Clear();
            using (SqlDataReader reader = helper.SerachDB(images, parameters))
            {
                while (reader.Read())
                {
                    string image = reader["NewsImage"].ToString();
                    builder.Append($"<p><image src='/{image}' style='width: 660px; height: 495px;'/></p>");
                }
                Image.Text = builder.ToString();
            } 
        }
    }
}