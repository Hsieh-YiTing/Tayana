using CKEditor.NET;
using CKFinder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.BackStage
{
    public partial class News_BS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GV_NewsBind();
            }
        }

        // 綁定GV_News
        protected void GV_NewsBind()
        {
            InsertDefaultCover();

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From News Order By IsTop Desc, ID Desc";

            using (SqlDataReader reader = helper.SerachDB(sql))
            {
                helper.GridViewBind(reader, GV_News);
            }
        }

        // 更改置頂狀態
        protected void TopStatus_CheckedChanged(object sender, EventArgs e)
        {
            // 獲取點選行的參數
            CheckBox checkBox = (CheckBox)sender;

            // 透過參數獲取該控制項的容器
            GridViewRow row = (GridViewRow)checkBox.NamingContainer;

            // 透過容器獲取ID
            int id = Convert.ToInt32(GV_News.DataKeys[row.RowIndex].Value);

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 在DBHelper內，將updateTop設置為可空
            if (checkBox.Checked)
            {
                // 先將所有News的置頂狀態設為false
                string setTopStatus = "Update News Set IsTop = 0";

                // 再依據NewID來更新置頂
                string updateTop = "Update News Set IsTop = 1 Where ID = @ID";

                helper.UpdateTopStatus(id, setTopStatus, updateTop);
            }
            else
            {
                string setTopStatus = "Update News Set IsTop = 0";
                helper.UpdateTopStatus(id, setTopStatus);
            }

            Response.Write("<script>alert('更新成功')</script>");
            GV_NewsBind();
        }

        // 當News沒有相簿時，資料庫寫入預設照片路徑
        protected void InsertDefaultCover()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 搜尋封面照為空的欄位
            string sql = "Select ID From News Where CoverPhoto Is Null";

            // 依照ID更新封面照為空的欄位
            string updateSql = "Update News Set CoverPhoto = @CoverPhoto Where ID = @ID";

            // 預設照片路徑
            string defaultPath = "BackStage/NewsImage/Default.jpg";

            // 先獲取空的封面照ID
            int id = 0;

            using (SqlDataReader reader = helper.SerachDB(sql))
            {
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["ID"]);
                }
            }

            // 寫入資料庫路徑
            helper.UpdateDefaultPhoto(updateSql, id, defaultPath);

        }

        // CKE參數
        protected void LoadCKE()
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(ContentEditor);
        }

        // 按下新增後，開啟CreateNewsPanel，關閉NewsPanel，並載入CKE
        protected void CreateNews_Click(object sender, EventArgs e)
        {
            LoadCKE();
            NewsPanel.Visible = false;
            CreateNewsPanel.Visible = true;
        }

        // 按下返回後關閉CreateNewsPanel，開啟NewsPanel
        protected void CloseAddNews_Click(object sender, EventArgs e)
        {
            NewsPanel.Visible = true;
            CreateNewsPanel.Visible = false;
        }

        // 新增News，當有上傳照片，選擇第一張為封面照
        protected void AddNews_Click(object sender, EventArgs e)
        {
            string title = AddTitle.Text;
            string content = ContentEditor.Text;
            bool top = TopDefault.Checked;
            content = RemovePtags(content);

            // 新增News至少要有標題
            if (string.IsNullOrEmpty(title))
            {
                Response.Write("<script>alert('請輸入標題')</script>");
                return;
            }

            // 資料插入後輸出ID
            string sql = "Insert Into News (Title, NewsContent, IsTop) Output Inserted.ID Values (@Title, @NewsContent, @IsTop)";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 插入後透過ExecuteScalar()方法返回ID
            int insertedID = helper.InsertNews(sql, title, content, top);

            // 有返回值才執行照片與檔案上傳
            if (insertedID > 0)
            {
                // 照片上傳迴圈
                if (UploadImage.HasFiles)
                {
                    // 依照返回的ID插入對應的相簿
                    string uploadImage = "Insert Into NewsAlbum (NewsID, NewsImage) Values (@NewsID, @NewsImage)";

                    foreach (var images in UploadImage.PostedFiles)
                    {
                        string fileName = images.FileName;
                        string serverPath = Server.MapPath($"~/BackStage/NewsImage/{fileName}");
                        images.SaveAs(serverPath);
                        string dbPath = $"BackStage/NewsImage/{fileName}";
                        helper.InsertNewsAlbum(uploadImage, insertedID, dbPath);
                    }

                    // 選取相簿的第一張照片當作封面照
                    string coverPhoto = "Select Top 1 NewsImage From NewsAlbum Where NewsID = @NewsID Order By ID";
                    string updateCover = "Update News Set CoverPhoto = @CoverPhoto Where ID = @ID";
                    string photoPath = "";

                    using (SqlDataReader reader = helper.SerachDB(coverPhoto))
                    {
                        if (reader.Read())
                        {
                            photoPath = reader["NewsImage"].ToString();
                        }
                    }

                    helper.UpdateNewsCover(updateCover, insertedID, photoPath);
                }

                // 檔案上傳迴圈
                if (UploadFile.HasFiles)
                {
                    string uploadFile = "Insert Into NewsFile (NewsID, FileName, NewsFile) Values (@NewsID, @FileName, @NewsFile)";

                    foreach (var files in UploadFile.PostedFiles)
                    {
                        string fileName = files.FileName;
                        string serverPath = Server.MapPath($"~/BackStage/NewsFile/{fileName}");
                        files.SaveAs(serverPath);
                        string dbPath = $"BackStage/NewsFile/{fileName}";
                        helper.InsertNewsFile(uploadFile, insertedID, fileName, dbPath);
                    }
                }

                // 新增後清空欄位以及重新綁定資料
                Response.Write("<script>alert('新增成功')</script>");
                GV_NewsBind();
                AddTitle.Text = "";
                ContentEditor.Text = "";
                NewsPanel.Visible = true;
                CreateNewsPanel.Visible = false;
            }
        }

        // 使用正則表達式移除<p>標籤，並移除開頭與結尾的空白字符
        protected string RemovePtags(string text)
        {
            string pattern = @"</?p[^>]*>";
            string result = Regex.Replace(text, pattern, string.Empty);
            return result.Trim();
        }

        // 點選選取後，跳出編輯畫面
        protected void GV_News_SelectedIndexChanged(object sender, EventArgs e)
        {
            DetailsPanel.Visible = true;
            NewsPanel.Visible = false;

            GridViewRow row = GV_News.SelectedRow;

            // 儲存ID到Hidden控制項
            NewsID.Value = row.Cells[0].Text;

            int id = Convert.ToInt32(row.Cells[0].Text);
            LoadDetails(id);
            GV_AlbumBind(id);
            GV_FilesBind(id);
        }

        // 依照選取的ID載入資料
        protected void LoadDetails(int id)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From News Where ID = @ID";
            var parameters = new Dictionary<string, object>
            {
                { "@ID", id}
            };
            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                if (reader.Read())
                {
                    string title = reader["Title"].ToString();
                    string content = reader["NewsContent"].ToString();
                    string path = reader["CoverPhoto"].ToString();
                    CoverPhoto.ImageUrl = $"~/{path}";
                    EditTitle.Text = title;
                    UpdateContent.Text = content;
                }
            }
        }

        // GV_News刪除事件
        protected void GV_News_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int newsID = Convert.ToInt32(GV_News.DataKeys[e.RowIndex].Value);

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            string sql = "Delete From News Where ID = @ID";

            helper.DeleteDB(sql, newsID);
            GV_NewsBind();
            Response.Write("<script>alert('刪除成功')</script>");
        }

        // 編輯News標題、內容
        protected void EditNews_Click(object sender, EventArgs e)
        {
            // 獲取NewsID
            int id = Convert.ToInt32(NewsID.Value);

            // 獲取編輯的標題，若沒輸入就儲存原本標題
            string editTitle = EditTitle.Text;

            if (string.IsNullOrEmpty(editTitle))
            {
                Response.Write("<script>alert('請輸入標題')</script>");
                return;
            }

            string editContent = RemovePtags(UpdateContent.Text);

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string updateNews = "Update News Set Title = @Title, CoverPhoto = @CoverPhoto, NewsContent = @NewsContent Where ID = @ID";

            // 依照有無更新圖片來更新資料庫
            if (UpdateCover.HasFile)
            {
                string fileName = UpdateCover.FileName;
                string serverPath = Server.MapPath($"~/BackStage/NewsImage{fileName}");
                UpdateCover.SaveAs(serverPath);

                string newCover = $"BackStage/NewsImage{fileName}";
                helper.UpdateNews(updateNews, id, editTitle, editContent, newCover);
            }
            else
            {
                // 獲取編輯前資料庫圖片路徑，防止寫入資料庫路徑為空的
                string sql = "Select CoverPhoto From News Where ID = @ID";
                var parameters = new Dictionary<string, object>
                {
                    { "@ID", id}
                };

                string oldCover;
                using (SqlDataReader reader = helper.SerachDB(sql, parameters))
                {
                    reader.Read();
                    oldCover = reader["CoverPhoto"].ToString();
                }

                helper.UpdateNews(updateNews, id, editTitle, editContent, oldCover);
            }

            Response.Write("<script>alert('編輯成功')</script>");
            GV_NewsBind();
            DetailsPanel.Visible = false;
            NewsPanel.Visible = true;
        }

        // 關閉編輯Panel
        protected void CloseEditNews_Click(object sender, EventArgs e)
        {
            DetailsPanel.Visible = false;
            NewsPanel.Visible = true;
        }

        // 開啟編輯圖片Panel
        protected void EditImage_Click(object sender, EventArgs e)
        {
            UpdateImagesPanel.Visible = true;
            DetailsPanel.Visible = false;
            UpdateFilesPanel.Visible = false;
        }

        // 開啟編輯檔案Panel
        protected void EditFile_Click(object sender, EventArgs e)
        {
            UpdateFilesPanel.Visible = true;
            DetailsPanel.Visible = false;
            UpdateImagesPanel.Visible = false;
        }

        // 返回GV_News畫面
        protected void CloseUpdatePanel_Click(object sender, EventArgs e)
        {
            UpdateImagesPanel.Visible = false;
            UpdateFilesPanel.Visible = false;
            NewsPanel.Visible = true;
        }

        // 返回News編輯畫面
        protected void ReturnDetails_Click(object sender, EventArgs e)
        {
            UpdateImagesPanel.Visible = false;
            UpdateFilesPanel.Visible = false;
            DetailsPanel.Visible = true;
        }

        // 綁定GV_Album
        protected void GV_AlbumBind(int id)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select ID, NewsImage From NewsAlbum Where NewsID = @NewsID";
            var parameters = new Dictionary<string, object>
            {
                { "NewsID", id}
            };
            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                helper.GridViewBind(reader, GV_Album);
            }
        }

        // 依照NewsID新增照片到對應的相簿
        protected void InsertImageButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(NewsID.Value);

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 照片上傳迴圈
            if (AddImage.HasFiles)
            {
                string addImage = "Insert Into NewsAlbum (NewsID, NewsImage) Values (@NewsID, @NewsImage)";

                foreach (var images in AddImage.PostedFiles)
                {
                    string fileName = images.FileName;
                    string serverPath = Server.MapPath($"~/BackStage/NewsImage/{fileName}");
                    images.SaveAs(serverPath);
                    string dbPath = $"BackStage/NewsImage/{fileName}";
                    helper.InsertNewsAlbum(addImage, id, dbPath);
                }

                Response.Write("<script>alert('新增成功')</script>");
            }
            else
            {
                Response.Write("<script>alert('還沒有選擇照片')</script>");
            }

            GV_AlbumBind(id);
        }

        // 刪除相簿內的照片
        protected void GV_Album_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int newsID = Convert.ToInt32(NewsID.Value);

            // 獲取要刪除的那行的ID
            int id = Convert.ToInt32(GV_Album.DataKeys[e.RowIndex].Value);

            string sql = "Delete From NewsAlbum Where ID = @ID";
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            helper.DeleteDB(sql, id);

            Response.Write("<script>alert('刪除成功')</script>");
            GV_AlbumBind(newsID);
        }

        // 關閉UpdateFilesPanel，開啟UpdateImagesPanel
        protected void EditImageButton_Click(object sender, EventArgs e)
        {
            UpdateFilesPanel.Visible = false;
            UpdateImagesPanel.Visible = true;
        }

        // 綁定GV_Files
        protected void GV_FilesBind(int id)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select ID, NewsFile From NewsFile Where NewsID = @NewsID";
            var parameters = new Dictionary<string, object>
            {
                { "NewsID", id}
            };
            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                helper.GridViewBind(reader, GV_Files);
            }
        }

        // 依照NewsID新增檔案到對應的檔案表
        protected void InsertFileButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(NewsID.Value);

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 檔案上傳迴圈
            if (AddFile.PostedFiles.Count > 0)
            {
                string addFile = "Insert Into NewsFile (NewsID, FileName, NewsFile) Values (@NewsID, @FileName, @NewsFile)";

                foreach (var files in AddFile.PostedFiles)
                {
                    string fileName = files.FileName;
                    string serverPath = Server.MapPath($"~/BackStage/NewsFile/{fileName}");
                    files.SaveAs(serverPath);
                    string dbPath = $"BackStage/NewsFile/{fileName}";
                    helper.InsertNewsFile(addFile, id, fileName, dbPath);
                }

                Response.Write("<script>alert('新增成功')</script>");
            }
            else
            {
                Response.Write("<script>alert('還沒有選擇檔案')</script>");
            }

            GV_FilesBind(id);
        }

        // 刪除檔案
        protected void GV_Files_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int newsID = Convert.ToInt32(NewsID.Value);

            // 獲取要刪除的那行的ID
            int id = Convert.ToInt32(GV_Files.DataKeys[e.RowIndex].Value);

            string sql = "Delete From NewsFile Where ID = @ID";
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            helper.DeleteDB(sql, id);

            Response.Write("<script>alert('刪除成功')</script>");
            GV_FilesBind(newsID);
        }
    }
}