using CKFinder;
using CKFinder.Settings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.BackStage
{
    public partial class UpdateYachts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                LoadCKE();
                GV_AlbumBind();
                GV_LayoutBind();
            }
        }

        // CKE參數、顯示型號、檔案、OverView
        protected void LoadCKE()
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(EditOverView);

            string sql = "Select Yachts.ID, Yachts.Yachts, OverView.OverView, OverView.SpecFile From Yachts Inner Join OverView On OverView.YachtsID = Yachts.ID Where Yachts.ID = @ID";
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            var parameters = new Dictionary<string, object>
            {
                { "@ID", id}
            };

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);


            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                reader.Read();
                EditYachtsModel.Text = reader["Yachts"].ToString();
                EditOverView.Text = reader["OverView"].ToString();
                FileLiteral.Text = reader["SpecFile"].ToString();
            }
        }

        // 使用正則表達式移除<p>標籤，並移除開頭與結尾的空白字符
        protected string RemovePtags(string text)
        {
            string pattern = @"</?p[^>]*>";
            string result = Regex.Replace(text, pattern, string.Empty);
            return result.Trim();
        }

        // 依照Url的ID，編輯Yachts型號、OverView，檔案
        protected void EditYachts_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string model = EditYachtsModel.Text;
            string overview = RemovePtags(EditOverView.Text);
            string specFile = FileLiteral.Text;

            if (EditFile.HasFile)
            {
                string fileName = EditFile.FileName;
                string serverPath = Server.MapPath($"~/BackStage/YachtsFile/{fileName}");
                EditFile.SaveAs(serverPath);
                specFile = $"BackStage/YachtsFile/{fileName}";
            }

            string updateYachts = "Update Yachts Set Yachts = @Yachts Where ID = @ID";
            string updateOverview = "Update OverView Set OverView = @OverView, SpecFile = @SpecFile Where YachtsID = @YachtsID";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            helper.UpdateYachts(updateYachts, id, model);
            helper.UpdateOverview(updateOverview, id, overview, specFile);
            Response.Write("<script>alert('編輯成功')</script>");
            LoadCKE();
        }

        // 返回Yachts首頁
        protected void ReturnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Yachts_BS.aspx");
        }

        // 依照Url的ID，綁定GV_Album
        protected void GV_AlbumBind()
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string sql = "Select * From YachtsAlbum Where YachtsID = @YachtsID";
            var parameters = new Dictionary<string, object>
            {
                { "@YachtsID", id}
            };

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                helper.GridViewBind(reader, GV_Album);
            }
        }

        // GV_Album的刪除事件
        protected void GV_Album_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GV_Album.DataKeys[e.RowIndex].Value);
            string sql = "Delete From YachtsAlbum Where ID = @ID";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            helper.DeleteDB(sql, id);
            Response.Write("<script>alert('刪除成功')</script>");
            GV_AlbumBind();
        }

        // 新增照片到GV_Album
        protected void InsertAlbumButton_Click(object sender, EventArgs e)
        {
            if (AddImage.HasFiles)
            {
                int id = Convert.ToInt32(Request.QueryString["ID"]);
                string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
                DBHelper helper = new DBHelper(connectionString);

                string sql = "Insert Into YachtsAlbum (YachtsID, YachtsImages) Values (@YachtsID, @YachtsImages)";

                foreach (var image in AddImage.PostedFiles)
                {
                    string fileName = image.FileName;
                    string serverPath = Server.MapPath($"~/BackStage/YachtsAlbum/{fileName}");
                    image.SaveAs(serverPath);
                    string dbPath = $"BackStage/YachtsAlbum/{fileName}";
                    helper.InsertYachtsAlbum(sql, id, dbPath);
                }

                Response.Write("<script>alert('新增成功')</script>");
            }
            else
            {
                Response.Write("<script>alert('至少選擇一張照片')</script>");
            }

            GV_AlbumBind();
        }

        // 開啟AlbumPanel
        protected void EditAlbum_Click(object sender, EventArgs e)
        {
            AlbumPanel.Visible = true;
            LayoutPanel.Visible = false;
            YachtsPanel.Visible = false;
            PagePanel.Visible = false;
        }

        // 開啟LayoutPanel
        protected void EditLayout_Click(object sender, EventArgs e)
        {
            AlbumPanel.Visible = false;
            LayoutPanel.Visible = true;
            YachtsPanel.Visible = false;
            PagePanel.Visible = false;
        }

        // 開啟YachtsPanel
        protected void ReturnYachts_Click(object sender, EventArgs e)
        {
            AlbumPanel.Visible = false;
            LayoutPanel.Visible = false;
            YachtsPanel.Visible = true;
            PagePanel.Visible = true;
        }

        // 依照Url的ID，綁定GV_Layout
        protected void GV_LayoutBind()
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string sql = "Select * From LayoutAlbum Where YachtsID = @YachtsID";
            var parameters = new Dictionary<string, object>
            {
                { "@YachtsID", id}
            };

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                helper.GridViewBind(reader, GV_Layout);
            }
        }

        // GV_Layout的刪除事件
        protected void GV_Layout_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GV_Layout.DataKeys[e.RowIndex].Value);
            string sql = "Delete From LayoutAlbum Where ID = @ID";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            helper.DeleteDB(sql, id);
            Response.Write("<script>alert('刪除成功')</script>");
            GV_LayoutBind();
        }

        // 新增照片到LayoutAlbum
        protected void InsertLayoutButton_Click(object sender, EventArgs e)
        {
            if (AddLayout.HasFiles)
            {
                int id = Convert.ToInt32(Request.QueryString["ID"]);
                string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
                DBHelper helper = new DBHelper(connectionString);

                string sql = "Insert Into LayoutAlbum (YachtsID, ImagePath) Values (@YachtsID, @ImagePath)";

                foreach (var image in AddLayout.PostedFiles)
                {
                    string fileName = image.FileName;
                    string serverPath = Server.MapPath($"~/BackStage/LayoutAlbum/{fileName}");
                    image.SaveAs(serverPath);
                    string dbPath = $"BackStage/LayoutAlbum/{fileName}";
                    helper.InsertLayout(sql, id, dbPath);
                }

                Response.Write("<script>alert('新增成功')</script>");
            }
            else
            {
                Response.Write("<script>alert('至少選擇一張照片')</script>");
            }

            GV_LayoutBind();
        }

        // 跳轉到Dimensions
        protected void RedirectDimensions_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            Response.Redirect($"UpdateDimensions.aspx?ID={id}");
        }

        // 先檢查有沒有Dimensions的資料，有就跳轉到Spec，沒有就跳轉到新增Dimensions頁面
        protected void RedirectSpec_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string sql = "Select YachtsID From Dimensions Where YachtsID = @YachtsID";
            var parameters = new Dictionary<string, object>
            {
                { "@YachtsID", id}
            };

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                if (!reader.HasRows)
                {
                    string script = @"alert('尚未新增Dimensions，跳轉到新增頁面');
                                                window.location.href = 'AddDimensions.aspx?ID=" + id + @"';";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirectScript", script, true);
                    return;
                }
                else
                {
                    Response.Redirect($"UpdateSpec.aspx?ID={id}");
                }
            }
        }
    }
}