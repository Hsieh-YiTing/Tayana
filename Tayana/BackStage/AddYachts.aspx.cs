using CKFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.BackStage
{
    public partial class AddYachts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCKE();
            }
        }

        // CKE參數
        protected void LoadCKE()
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(OverView);
        }

        // 使用正則表達式移除<p>標籤，並移除開頭與結尾的空白字符
        protected string RemovePtags(string text)
        {
            string pattern = @"</?p[^>]*>";
            string result = Regex.Replace(text, pattern, string.Empty);
            return result.Trim();
        }

        // 新增型號
        protected void AddNewYachts_Click(object sender, EventArgs e)
        {
            string model = AddYachtsModel.Text;
            bool defaultBox = DefaultBox.Checked;
            string overView = RemovePtags(OverView.Text);

            string filePath = string.Empty;
            string imagePath = string.Empty;
            string layoutPath = string.Empty;

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 單一檔案上傳
            if (AddFile.HasFile)
            {
                string fileName = AddFile.FileName;
                string serverPath = Server.MapPath($"~/BackStage/YachtsFile/{fileName}");
                AddFile.SaveAs(serverPath);
                filePath = $"BackStage/YachtsFile/{fileName}";
            }

            // 檢查型號不能為空
            if (!string.IsNullOrEmpty(model))
            {
                // 先寫入Yachts型號，再透過輸出的ID寫入OverView，並將輸出的ID儲存在HiddenField
                string insertYachts = "Insert Into Yachts (Yachts, NewDesign) Output Inserted.ID Values (@Yachts, @NewDesign)";
                int insertID = helper.InsertYachts(insertYachts, model, defaultBox);
                YachtsID.Value = insertID.ToString();

                if (insertID > 0)
                {
                    string insertOverView = "Insert Into OverView (YachtsID, OverView, SpecFile) Values (@YachtsID, @OverView, @SpecFile)";
                    helper.InsertOverView(insertOverView, insertID, overView, filePath);

                    // 每當新增型號時，就依據ID新增SpecCategory
                    string[] categories = { "Hull", "Deck/Hardware", "Engine/Machinery", "Steering", "Spars/Rigging", "Sails", "Interior", "Electrical", "Plumbing" };
                    string insertSpecCategory = "Insert Into SpecCategory (YachtsID, Category) Values (@YachtsID, @Category)";
                    helper.AddCategory(insertSpecCategory, insertID, categories);
                }
            }
            else
            {
                Response.Write("<script>alert('型號不能為空')</script>");
                return;
            }

            int id = Convert.ToInt32(YachtsID.Value);

            // Yachts多圖上傳
            if (AddYachtsAlbum.HasFiles)
            {
                string sql = "Insert Into YachtsAlbum (YachtsID, YachtsImages) Values (@YachtsID, @YachtsImages)";

                foreach (var images in AddYachtsAlbum.PostedFiles)
                {
                    string fileName = images.FileName;
                    string serverPath = Server.MapPath($"~/BackStage/YachtsAlbum/{fileName}");
                    images.SaveAs(serverPath);
                    imagePath = $"BackStage/YachtsAlbum/{fileName}";
                    helper.InsertYachtsAlbum(sql, id, imagePath);
                }
            }

            // Layout多圖上傳
            if (AddLayout.HasFiles)
            {
                string sql = "Insert Into LayoutAlbum (YachtsID, ImagePath) Values (@YachtsID, @ImagePath)";

                foreach (var images in AddLayout.PostedFiles)
                {
                    string fileName = images.FileName;
                    string serverPath = Server.MapPath($"~/BackStage/LayoutAlbum/{fileName}");
                    images.SaveAs(serverPath);
                    layoutPath = $"BackStage/LayoutAlbum/{fileName}";
                    helper.InsertLayout(sql, id, layoutPath);
                }
            }

            Response.Write("<script>alert('型號新增成功')</script>");
            Response.Redirect($"AddDimensions.aspx?ID={id}");
        }

        protected void ReturnIndex_Click(object sender, EventArgs e)
        {
            AddYachtsModel.Text = string.Empty;
            OverView.Text = string.Empty;
            Response.Redirect("Yachts_BS.aspx");
        }
    }
}