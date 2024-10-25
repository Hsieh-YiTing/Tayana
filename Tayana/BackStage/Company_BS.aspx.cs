using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Tayana.BackStage
{
    public partial class Company_BS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowAboutUs();
                ShowCertificate();
                GV_AlbumBind();
            }
        }

        // 渲染AboutUs並顯示最新的一筆
        protected void ShowAboutUs()
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditorControl1);

            string sql = "Select * From AboutUs Order By CreateTime Desc";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            using (SqlDataReader reader = helper.SerachDB(sql))
            {
                if (reader.Read())
                {
                    // 讀取文字並渲染到CKE內
                    CKEditorControl1.Text = reader["AboutUs"].ToString();

                    // 讀取圖片路徑並加上根目錄
                    string imagePath = reader["Image"].ToString();
                    Image1.ImageUrl = $"~/{imagePath}";
                }
            }
        }

        // 渲染Certificate並顯示最新的一筆
        protected void ShowCertificate()
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditorControl2);

            string sql = "Select * From Certificate Order By CreateTime Desc";
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            using (SqlDataReader reader = helper.SerachDB(sql))
            {
                if (reader.Read())
                {
                    // 讀取文字並渲染到CKE內
                    CKEditorControl2.Text = reader["Certificate"].ToString();

                    // 獲取Certificate的ID並賦給HiddenFiled
                    HiddenCertificateID.Value = reader["ID"].ToString();
                }
            }
        }

        // 轉換FileUpload內的檔案路徑並寫入資料庫
        protected string DB_Path()
        {
            if (FileUpload1.HasFiles)
            {
                // 獲得檔案名稱
                string fileName = FileUpload1.FileName;

                // 將上傳的檔案轉換為物理路徑並儲存在根目錄內的相簿
                string savePath = Server.MapPath($"~/BackStage/Company/{fileName}");

                // 儲存到新資料夾
                FileUpload1.SaveAs(savePath);

                // 寫入資料庫的路徑
                string dbPath = $"BackStage/Company/{fileName}";
                return dbPath;
            }
            else
            {
                // 只有修改文字時，回傳當下渲染的圖片路徑
                return Image1.ImageUrl.Replace("~/", "");
            }
        }

        // 每次修改都是新增一筆AboutUs的資料
        protected void InsertAboutUs_Click(object sender, EventArgs e)
        {
            // 將CKE內的<p>標籤移除後再寫入資料庫
            string text = CKEditorControl1.Text;
            text = RemovePtags(text);

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Insert Into AboutUs (AboutUs, Image) Values (@AboutUs, @Image)";
            
            // 透過DB_Path()方法，可以做到單純修改文字、圖片或兩者修改
            helper.InsertAboutUs(sql, text, DB_Path());
            Response.Write("<script>alert('修改成功')</script>");

            // 修改後重新綁定資料
            ShowAboutUs();
        }

        // 每次修改都是更新Certificate的資料
        protected void InsertCertificate_Click(object sender, EventArgs e)
        {
            // 將CKE內的<p>標籤移除後再寫入資料庫
            string text = CKEditorControl2.Text;
            text = RemovePtags(text);

            string sql = "Update Certificate Set Certificate = @Certificate";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            helper.UpdateCertificate(sql, text);
            Response.Write("<script>alert('修改成功')</script>");

            ShowCertificate();
        }

        // 相簿GV資料綁定
        protected void GV_AlbumBind()
        {
            string sql = "Select * From CertificateAlbums Where CertificateID = @CertificateID";
            int certificateID = Convert.ToInt32(HiddenCertificateID.Value);

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            var parameters = new Dictionary<string, object> 
            {
                { "@CertificateID", certificateID}
            };

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                GV_Album.DataSource = reader;
                GV_Album.DataBind();
            }
        }

        // 依照CertificateID新增照片
        protected void InsertImage_Click(object sender, EventArgs e)
        {
            if (FileUpload2.HasFiles)
            {
                // var會被解析成HttpPostedFile
                foreach (var images in FileUpload2.PostedFiles)
                {
                    // 儲存到本地資料夾
                    string fileName = images.FileName;
                    string serverPath = Server.MapPath($"~/BackStage/Certificate/{fileName}");

                    // 如果使用FileUpload2.SaveAs則會造成照片都相同但是檔名不同
                    images.SaveAs(serverPath);

                    // 獲得CertificateID與寫入資料庫的路徑
                    int certificateID = Convert.ToInt32(HiddenCertificateID.Value);
                    string dbPath = $"BackStage/Certificate/{fileName}";

                    // 路徑依照CertificateID寫入資料庫
                    string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
                    DBHelper helper = new DBHelper(connectionString);
                    string sql = "Insert Into CertificateAlbums (CertificateID, CertificateImage) Values (@CertificateID, @CertificateImage)";
                    helper.InsertCertificateAlbum(sql, certificateID, dbPath);
                }
            }

            Response.Write("<script>alert('新增成功')</script>");
            GV_AlbumBind();
        }

        protected void GV_Album_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_Album.EditIndex = e.NewEditIndex;
            GV_AlbumBind();
        }

        protected void GV_Album_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_Album.EditIndex = -1;
            GV_AlbumBind();
        }

        protected void GV_Album_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 獲取FileUpload控制項
            GridViewRow row = GV_Album.Rows[e.RowIndex];
            FileUpload fileUpload = (FileUpload) row.FindControl("FileUpload3");

            // 獲取編輯行的ID
            int id = Convert.ToInt32(GV_Album.DataKeys[e.RowIndex].Value);

            if (fileUpload.HasFiles)
            { 
                string fileName = fileUpload.FileName;
                string serverPath = Server.MapPath($"~/BackStage/Certificate/{fileName}");
                fileUpload.SaveAs(serverPath);

                string dbPath = $"BackStage/Certificate/{fileName}";

                string sql = "Update CertificateAlbums Set CertificateImage = @CertificateImage Where ID = @ID";
                string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
                DBHelper helper = new DBHelper(connectionString);
                helper.UpdateCertificateAlbum(sql, id, dbPath);
                Response.Write("<script>alert('新增成功')</script>");

                GV_Album.EditIndex = -1;
                GV_AlbumBind();
            }
        }

        protected void GV_Album_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GV_Album.DataKeys[e.RowIndex].Value);
            string sql = "Delete From CertificateAlbums Where ID = @ID";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            helper.DeleteDB(sql, id);
            Response.Write("<script>alert('刪除成功')</script>");
            GV_AlbumBind();
        }

        protected void BtnAboutUsPanel_Click(object sender, EventArgs e)
        {
            AboutUsPanel.Visible = true;
            CertificatePanel.Visible = false;
        }

        protected void BtnCertificatePanel_Click(object sender, EventArgs e)
        {
            AboutUsPanel.Visible = false;
            CertificatePanel.Visible = true;
        }

        // 使用正則表達式移除<p>標籤，並移除開頭與結尾的空白字符
        protected string RemovePtags(string text)
        {
            string pattern = @"</?p[^>]*>";
            string result = Regex.Replace(text, pattern, string.Empty);
            return result.Trim();
        }
    }
}