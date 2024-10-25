using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.FrontStage
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PhotoLoad();
            NewsLaod();
        }

        protected void PhotoLoad()
        {
            string sql = "With RankedPhotos As (Select YachtsAlbum.ID, YachtsID, YachtsAlbum.YachtsImages, Row_Number() Over (Partition By YachtsID Order By YachtsAlbum.ID) As RowNum From YachtsAlbum) " +
                                   "Select ID, YachtsID, YachtsImages From RankedPhotos Where RowNum = 1";

            string yachts = "Select Top 6 * From Yachts Order By ID";

            StringBuilder albumBuilder = new StringBuilder();
            StringBuilder photoBuilder = new StringBuilder();

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            List<string> yachtsModel = new List<string>();
            List<string> yachtImages = new List<string>();
            List<bool> isNewDesign = new List<bool>();

            // 讀取遊艇型號
            using (SqlDataReader reader = helper.SerachDB(yachts))
            {
                while (reader.Read())
                {
                    yachtsModel.Add(reader["Yachts"].ToString());
                    isNewDesign.Add(Convert.ToBoolean(reader["newdesign"]));
                }
            }

            // 讀取對應的圖片
            using (SqlDataReader reader = helper.SerachDB(sql))
            {
                while (reader.Read())
                {
                    yachtImages.Add(reader["YachtsImages"].ToString());
                }
            }

            // 生成大圖展示
            for (int i = 0; i < Math.Min(yachtsModel.Count, yachtImages.Count); i++)
            {
                string newDesignImage = isNewDesign[i] ? "<img src='/FrontStage/Tayanahtml/html/images/newDesign.jpg' style='position:absolute; top:0px; left:0px; width:100px; height:auto;'/>" : "";

                albumBuilder.Append($@"<li class='info'><a href='/{yachtImages[i]}'><div style='position:relative;'><img style='width:966px;height:424px;' src='/{yachtImages[i]}'/>{newDesignImage}</div></a><div class='wordtitle'>{yachtsModel[i]}<br/><p>SPECIFICATION SHEET</p></div></li>");
            }

            // 生成小圖展示
            for (int i = 0; i < yachtImages.Count; i++)
            {
                photoBuilder.Append($"<li><div><p class='bannerimg_p'><img src='/{yachtImages[i]}' style='width:103px; height:63px;' /></p></div></li>");
            }

            ShowAlbum.Text = albumBuilder.ToString();
            SmallPhoto.Text = photoBuilder.ToString();
        }

        protected void NewsLaod()
        {
            string sql = "Select Top 3 * From News Order By IsTop Desc, ID Desc";

            StringBuilder builder = new StringBuilder();
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            using (SqlDataReader reader = helper.SerachDB(sql))
            { 
                while (reader.Read()) 
                {
                    string id = reader["ID"].ToString();
                    string title = reader["Title"].ToString();
                    string image = reader["CoverPhoto"].ToString();
                    DateTime dateTime = (DateTime)reader["CreateTime"];
                   string date = dateTime.ToString("yyyy-MM-dd");

                    builder.Append($"<li><div class='news01'><div class='newstop'><img src='/FrontStage/Tayanahtml/html/images/new_top01.png' /></div><div class='news02p1'><p class='news02p1img'><img src='/{image}' style='width:95px; height:95px;'/></p></div><p class='news02p2'><span>{date}</span><a href='Events.aspx?ID={id}'>{title}</a></p></div></li>");
                }
            }
            NewsList.Text = builder.ToString();
        }
    }
}