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
    public partial class Layout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                string id = Request.QueryString["ID"];
                LoadAlbum(id);
                LeftMenu();
                RightMenu(id);
                LoadImage();
            }
        }

        protected void LoadAlbum(string yachtsID)
        {
            StringBuilder builder = new StringBuilder();
            string sql = "Select * From YachtsAlbum Where YachtsID = @YachtsID";
            string imagePath;
            var parameters = new Dictionary<string, object>
            {
                { "@YachtsID", yachtsID}
            };

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                while (reader.Read())
                {
                    imagePath = reader["YachtsImages"].ToString();
                    builder.Append($"<li><a href='/{imagePath}' /><img src='/{imagePath}' style='width:103px; height:63px;'/></a></li>");
                }
            }
            ImageBanner.Text = builder.ToString();
        }

        protected void LeftMenu()
        {
            StringBuilder builder = new StringBuilder();

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From Yachts";
            using (SqlDataReader reader = helper.SerachDB(sql))
            {
                while (reader.Read())
                {
                    string yachtsID = reader["ID"].ToString();
                    string yachts = reader["Yachts"].ToString();
                    bool newDesign = (bool)reader["NewDesign"];

                    if (newDesign == true)
                    {
                        builder.Append($"<li><a href='Yachts.aspx?ID={yachtsID}'>{yachts} (New Design)</a></li>");
                    }
                    else
                    {
                        builder.Append($"<li><a href='Yachts.aspx?ID={yachtsID}'>{yachts}</a></li>");
                    }
                }
            }
            YachtsList.Text = builder.ToString();
        }

        protected void RightMenu(string yachtsID)
        {
            string sql = "Select * From Yachts Where ID = @ID";
            var parameters = new Dictionary<string, object>
                {
                    { "@ID", yachtsID}
                };

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                reader.Read();
                ModelLink.Text = reader["Yachts"].ToString();
                YachtsModel.Text = reader["Yachts"].ToString();
            }

            ModelLink.NavigateUrl = $"Yachts.aspx?ID={yachtsID}";
            OverViewLink.NavigateUrl = $"Yachts.aspx?ID={yachtsID}";
            LayoutLink.NavigateUrl = $"Layout.aspx?ID={yachtsID}";
            SpecificationLink.NavigateUrl = $"Specification.aspx?ID={yachtsID}";
        }

        protected void LoadImage()
        { 
            string id = Request.QueryString["ID"];
            string sql = "Select * From LayoutAlbum Where YachtsID = @YachtsID";
            var parameters = new Dictionary<string, object> 
            {
                { "@YachtsID", id}
            };

            StringBuilder builder = new StringBuilder();
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            { 
                while (reader.Read()) 
                {
                    string image = reader["ImagePath"].ToString();
                    builder.Append($"<li><img src='/{image}' style='width:609px;' /></li>");
                }
            }

            LayoutImage.Text = builder.ToString();
        }
    }
}