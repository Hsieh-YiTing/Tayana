using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.BackStage
{
    public partial class Dealers_BS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCountryBind(ddlCountry);
                ddlAreaBind(ddlArea, 0);

                string countryValue = ddlCountry.SelectedValue;
                string areaValue = ddlArea.SelectedValue;
                DealersGV_Bind(countryValue, areaValue);
            }
        }

        #region 國家選單CRUD

        // 國家選單綁定，新增"未選擇"選項
        protected void ddlCountryBind(DropDownList dropDownList)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From DealersCountry";
            using (SqlDataReader reader = helper.SerachDB(sql))
            {
                helper.CategoryList(reader, dropDownList, "Country", "ID");
            }

            dropDownList.Items.Insert(0, new ListItem("未選擇", "0"));
        }

        // 開啟CreateCountryPanel
        protected void AddCountryPanel_Click(object sender, EventArgs e)
        {
            CreateCountry.Visible = true;
        }

        // 刪除國家，若未選擇會跳出Alert
        protected void DeleteCountry_Click(object sender, EventArgs e)
        {
            int countryID = Convert.ToInt32(ddlCountry.SelectedValue);

            if (countryID == 0)
            {
                Response.Write("<script>alert('請選擇國家')</script>");
            }
            else
            {
                string sql = "Delete From DealersCountry Where ID = @ID";
                string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
                DBHelper helper = new DBHelper(connectionString);
                helper.DeleteDB(sql, countryID);
                Response.Write("<script>alert('刪除成功')</script>");
                ddlCountryBind(ddlCountry);
            }

            int countryNum = Convert.ToInt32(ddlCountry.SelectedItem.Value);
            ddlAreaBind(ddlArea, countryNum);

            string countryValue = ddlCountry.SelectedValue;
            string areaValue = ddlArea.SelectedValue;
            DealersGV_Bind(countryValue, areaValue);
        }

        // 新增國家，新增後關閉CreateCountryPanel
        protected void InsertCountry_Click(object sender, EventArgs e)
        {
            string newCountry = InputNewCountry.Text;
            string sql = "Insert Into DealersCountry(Country) Values (@Country)";
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            helper.InsertCountry(sql, newCountry);
            Response.Write("<script>alert('新增成功')</script>");
            InputNewCountry.Text = "";
            ddlCountryBind(ddlCountry);
            CreateCountry.Visible = false;
        }

        // 關閉CreateCountryPanel
        protected void CloseCreateCountry_Click(object sender, EventArgs e)
        {
            CreateCountry.Visible = false;
        }

        // 根據選擇的國家來顯示地區選單，未選擇國家就關閉地區Panel
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            AreaPanel.Visible = true;
            int selectValue = Convert.ToInt32(ddlCountry.SelectedValue);

            // 顯示在新增地區的Label
            string selectCountry = ddlCountry.SelectedItem.Text;
            SelectCountry.Text = $"選擇的國家為 : {selectCountry}";

            ddlAreaBind(ddlArea, selectValue);

            if (selectValue == 0)
            {
                AreaPanel.Visible = false;
            }

            // 當只有選擇國家時，也需要顯示相對應的經銷商
            string countryValue = ddlCountry.SelectedValue;
            string areaValue = ddlArea.SelectedValue;
            DealersGV_Bind(countryValue, areaValue);
        }

        #endregion

        #region 地區選單CRUD

        // 依據選擇的國家，顯示地區，新增"未選擇"選項
        protected void ddlAreaBind(DropDownList dropDownList, int countryValue)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From DealersArea Where DealersCountryID = @DealersCountryID";
            var parameter = new Dictionary<string, object>
            {
                { "@DealersCountryID", countryValue}
            };

            using (SqlDataReader readArea = helper.SerachDB(sql, parameter))
            {
                helper.CategoryList(readArea, dropDownList, "Area", "ID");
            }

            dropDownList.Items.Insert(0, new ListItem("未選擇", "0"));
        }

        // 開啟CreateAreaPanel
        protected void AddAreaPanel_Click(object sender, EventArgs e)
        {
            CreateArea.Visible = true;
        }

        // 刪除地區，未選擇會跳出Alert
        protected void DeleteArea_Click(object sender, EventArgs e)
        {
            int areaID = Convert.ToInt32(ddlArea.SelectedValue);

            if (areaID == 0)
            {
                Response.Write("<script>alert('請選擇地區')</script>");
            }
            else
            {
                string sql = "Delete From DealersArea Where ID = @ID";
                string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
                DBHelper helper = new DBHelper(connectionString);
                helper.DeleteDB(sql, areaID);
                Response.Write("<script>alert('刪除成功')</script>");

                int countryNum = Convert.ToInt32(ddlCountry.SelectedValue);
                ddlAreaBind(ddlArea, countryNum);

                string countryValue = ddlCountry.SelectedValue;
                string areaValue = ddlArea.SelectedValue;
                DealersGV_Bind(countryValue, areaValue);
            }
        }

        // 依照選擇的國家新增地區，未選擇國家則無法新增地區
        protected void InsertArea_Click(object sender, EventArgs e)
        {
            int selectValue = Convert.ToInt32(ddlCountry.SelectedValue);
            string newArea = InputNewArea.Text;

            string sql = "Insert Into DealersArea (DealersCountryID, Area) Values (@DealersCountryID, @Area)";
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            if (selectValue == 0)
            {
                Response.Write("<script>alert('請選擇國家')</script>");
            }
            else
            {
                helper.InsertArea(sql, selectValue, newArea);
                Response.Write("<script>alert('新增成功')</script>");
                InputNewArea.Text = "";
                CreateArea.Visible = false;
                ddlAreaBind(ddlArea, selectValue);
            }
        }

        // 關閉CreateAreaPanel
        protected void CloseCreateArea_Click(object sender, EventArgs e)
        {
            CreateArea.Visible = false;
        }

        // 依據地區的選擇，顯示不同的供應商
        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            string countryValue = ddlCountry.SelectedValue;
            string areaValue = ddlArea.SelectedValue;
            DealersGV_Bind(countryValue, areaValue);
        }

        #endregion

        #region 資料綁定DealersGV、選取與刪除事件

        // 依據國家、地區的選擇，來顯示經銷商
        protected void DealersGV_Bind(string countryValue, string areaValue)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 國家與地區都未選擇時
            if (countryValue == "0" && areaValue == "0")
            {
                string sql = "Select Dealers.ID, DealersCountry.Country, DealersArea.Area, Location, DealerName From Dealers " +
                                      "Inner Join DealersArea On Dealers.DealersAreaID = DealersArea.ID " +
                                      "Inner Join DealersCountry On DealersArea.DealersCountryID = DealersCountry.ID ";
                using (SqlDataReader readDealers = helper.SerachDB(sql))
                {
                    helper.GridViewBind(readDealers, GV_Dealers);
                }
            }
            // 當選擇國家，但是地區未選擇時
            else if (countryValue != "0" && areaValue == "0")
            {
                string sql = "Select Dealers.ID, DealersCountry.Country, DealersArea.Area, Location, DealerName From Dealers " +
                                      "Inner Join DealersArea On Dealers.DealersAreaID = DealersArea.ID " +
                                      "Inner Join DealersCountry On DealersArea.DealersCountryID = DealersCountry.ID " +
                                      "Where DealersCountry.ID = @CountryID";
                var parameters = new Dictionary<string, object>
                {
                    { "@CountryID", countryValue}
                };

                using (SqlDataReader readDealers = helper.SerachDB(sql, parameters))
                {
                    helper.GridViewBind(readDealers, GV_Dealers);
                }
            }
            // 選擇國家與地區時
            else
            {
                string sql = "Select Dealers.ID, DealersCountry.Country, DealersArea.Area, Location, DealerName From Dealers " +
                                       "Inner Join DealersArea On Dealers.DealersAreaID = DealersArea.ID " +
                                       "Inner Join DealersCountry On DealersArea.DealersCountryID = DealersCountry.ID " +
                                       "Where Dealers.DealersAreaID = @AreaID";
                var parameters = new Dictionary<string, object>
                {
                    { "@AreaID", areaValue}
                };

                using (SqlDataReader readDealers = helper.SerachDB(sql, parameters))
                {
                    helper.GridViewBind(readDealers, GV_Dealers);
                }
            }
        }

        // 點擊選取時，取得欄位資訊並載入詳細資料
        protected void GV_Dealers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 點選選取時，開啟DetailsPanel
            DetailsPanel.Visible = true;
            DealersPanel.Visible = false;

            // 取得點選的那行經銷商資料
            GridViewRow selectedRow = GV_Dealers.SelectedRow;

            // 獲取ID
            int dealersID = Convert.ToInt32(selectedRow.Cells[0].Text);

            // 取得欄位資料
            string country = selectedRow.Cells[1].Text;
            string area = selectedRow.Cells[2].Text;
            string dealersName = selectedRow.Cells[3].Text;
            string location = selectedRow.Cells[4].Text;

            // 根據ID顯示詳細資料
            LoadDetailsData(dealersID, country, area, dealersName, location);
        }

        // 刪除事件
        protected void GV_Dealers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int dealersID = Convert.ToInt32(GV_Dealers.DataKeys[e.RowIndex].Value);

            string sql = "Delete From Dealers Where ID = @ID";
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            helper.DeleteDB(sql, dealersID);

            Response.Write("<script>alert('刪除成功')</script>");

            string countryValue = ddlCountry.SelectedValue;
            string areaValue = ddlArea.SelectedValue;
            DealersGV_Bind(countryValue, areaValue);
        }

        // 點擊新增經銷商時，開啟CreateDealersPanel，關閉DealersPanel
        protected void CreateDealers_Click(object sender, EventArgs e)
        {
            DealersPanel.Visible = false;
            CreateDealersPanel.Visible = true;
            LoadTableList();
        }

        #endregion

        #region 詳細資料表格以及編輯

        // 從資料庫與選取事件渲染出詳細資料
        protected void LoadDetailsData(int dealersID, string country, string area, string dealersName, string location)
        {
            #region 渲染原有資料

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From Dealers Where ID = @ID";
            var parameters = new Dictionary<string, object> 
            {
                { "@ID", dealersID}
            };

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            { 
                if (reader.Read()) 
                {
                    // 封面照片
                    string dbPath = reader["CoverPhoto"].ToString();
                    OriginImage.ImageUrl = $"~/{dbPath}";

                    // 先將點選欄位的資料渲染
                    OriginCountry.Text = $"原有國家為 : {country}";
                    OriginArea.Text = $"原有地區為 : {area}";
                    OriginDealersName.Text = dealersName;
                    OriginLocation.Text = location;

                    // 渲染聯絡人資料
                    string person = reader["ContactPerson"].ToString();
                    string phone = reader["Phone"].ToString();
                    string fax = reader["Fax"].ToString();
                    string mail = reader["Mail"].ToString();
                    string link = reader["Link"].ToString();
                    string time = reader["CreateTime"].ToString();

                    OriginContactPerson.Text = person;
                    OriginPhone.Text = phone;
                    OriginFax.Text = fax;
                    OriginMail.Text = mail;
                    OriginLink.Text = link;
                    CreateTime.Text = time;
                }
            }

            #endregion

            //綁定國家與地區的下拉式選單
            ddlCountryBind(UpdateCountry);
            ddlAreaBind(UpdateArea, 0);

            // 將dealersID存在HiddenFiled內，更新資料時會用到
            DealersID.Value = dealersID.ToString();
        }

        // 依照選取的國家，顯示對應的地區選單
        protected void UpdateCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectValue = Convert.ToInt32(UpdateCountry.SelectedValue);
            ddlAreaBind(UpdateArea, selectValue);
        }

        // 透過HiddenFiled獲得ID，更新詳細資料
        protected void UpdateData_Click(object sender, EventArgs e)
        {
            // 更新照片
            string fileName;
            string dbPath = "";

            if (UpdateImage.HasFile)
            { 
                // 獲取檔案名稱
                fileName = UpdateImage.FileName;

                // 轉換為物理路徑
                string serverPath = Server.MapPath($"~/BackStage/Dealers/{fileName}");

                // 儲存到新資料夾
                UpdateImage.SaveAs(serverPath);

                // 寫入資料庫路徑
                dbPath = $"BackStage/Dealers/{fileName}";
            }

            // 透過HiddenFiled獲得ID
            int dealersID = Convert.ToInt32(DealersID.Value);

            // 獲取國家值是為了判斷，防止寫入資料庫的資料沒有選擇國家
            int selectCountry = Convert.ToInt32(UpdateCountry.SelectedItem.Value);
            int selectArea = Convert.ToInt32(UpdateArea.SelectedItem.Value);

            #region 獲取輸入的資料，沒輸入就使用原始值

            string dealersName, location, person, phone, fax, mail, link;

            if (!string.IsNullOrEmpty(UpdateDealersName.Text))
            {
                dealersName = UpdateDealersName.Text;
            }
            else
            {
                dealersName = OriginDealersName.Text;
            }

            if (!string.IsNullOrEmpty(UpdateLocation.Text))
            {
                location = UpdateLocation.Text;
            }
            else
            {
                location = OriginLocation.Text;
            }

            if (!string.IsNullOrEmpty(UpdateContactPerson.Text))
            {
                person = UpdateContactPerson.Text;
            }
            else
            {
                person = OriginContactPerson.Text;
            }

            if (!string.IsNullOrEmpty(UpdatePhone.Text))
            {
                phone = UpdatePhone.Text;
            }
            else
            {
                phone = OriginPhone.Text;
            }

            if (!string.IsNullOrEmpty(UpdateFax.Text))
            {
                fax = UpdateFax.Text;
            }
            else
            {
                fax = OriginFax.Text;
            }

            if (!string.IsNullOrEmpty(UpdateMail.Text))
            {
                mail = UpdateMail.Text;
            }
            else
            {
                mail = OriginMail.Text;
            }

            if (!string.IsNullOrEmpty(UpdateLink.Text))
            {
                link = UpdateLink.Text;
            }
            else
            {
                link = OriginLink.Text;
            }

            #endregion

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 使用兩段語法是因為如果沒有新照片卻更新會造成參數傳遞錯誤
            string sqlWithPhoto = "Update Dealers Set DealersAreaID = @DealersAreaID, CoverPhoto = @CoverPhoto, " +
                                                       "DealerName = @DealerName, Location = @Location, ContactPerson = @ContactPerson, " +
                                                       "Phone = @Phone, Fax = @Fax, Mail = @Mail, Link = @Link Where ID = @ID";

            string sqlWithoutPhoto = "Update Dealers Set DealersAreaID = @DealersAreaID,  DealerName = @DealerName, " +
                                                             "Location = @Location, ContactPerson = @ContactPerson, " +
                                                             "Phone = @Phone, Fax = @Fax, Mail = @Mail, Link = @Link Where ID = @ID";

            // 確保不會寫入空的國家以及地區
            if (selectCountry == 0 || selectArea == 0)
            {
                Response.Write("<script>alert('請選擇國家以及地區')</script>");
                DetailsPanel.Visible = true;
            }
            else
            {
                // 有要更新照片就執行這段
                if (!string.IsNullOrEmpty(dbPath))
                {
                    helper.UpdateDealers(sqlWithPhoto, dealersID, selectArea, dbPath, dealersName, location, person, phone, fax, mail, link);
                }
                else
                {
                    helper.UpdateDealers(sqlWithoutPhoto, dealersID, selectArea, dbPath, dealersName, location, person, phone, fax, mail, link);
                }
                UpdateDealersName.Text = "";
                UpdateLocation.Text = "";
                UpdateContactPerson.Text = "";
                UpdatePhone.Text = "";
                UpdateFax.Text = "";
                UpdateMail.Text = "";
                UpdateLink.Text = "";

                DealersPanel.Visible = true;
                DetailsPanel.Visible = false;

                string countryValue = ddlCountry.SelectedValue;
                string areaValue = ddlArea.SelectedValue;
                DealersGV_Bind(countryValue, areaValue);

                Response.Write("<script>alert('更新成功')</script>");
            }
        }

        // 退出編輯，並開啟DealersPanel
        protected void CancelUpdate_Click(object sender, EventArgs e)
        {
            DetailsPanel.Visible = false;
            DealersPanel.Visible = true;
        }

        #endregion

        #region 新增經銷商表格與取消

        // 綁定新增頁面選單
        protected void LoadTableList()
        {
            //綁定國家與地區的下拉式選單
            ddlCountryBind(SelectCountryList);
            ddlAreaBind(SelectAreaList, 0);
        }

        // 依照選取的國家，顯示對應的地區選單
        protected void SelectCountryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectValue = Convert.ToInt32(SelectCountryList.SelectedValue);
            ddlAreaBind(SelectAreaList, selectValue);
        }

        // 新增經銷商資料
        protected void AddDealers_Click(object sender, EventArgs e)
        {
            int selectCountry = Convert.ToInt32(SelectCountryList.SelectedItem.Value);
            int selectArea = Convert.ToInt32(SelectAreaList.SelectedItem.Value);

            // 有選擇照片就存進Dealers資料夾內並寫入資料庫，若無則寫入預設值圖片
            string dbPath;
            if (UploadImage.HasFile)
            {
                string fileName = UploadImage.FileName;
                string serverPath = Server.MapPath($"~/BackStage/Dealers/{fileName}");
                UploadImage.SaveAs(serverPath);
                dbPath = $"BackStage/Dealers/{fileName}";
            }
            else
            {
                dbPath = $"BackStage/Dealers/default.jpg";
            }

            #region 新增資料欄位

            string dealersName, person, location, phone;
            string fax = AddFax.Text;
            string mail = AddMail.Text;
            string link = AddLink.Text;

            if (!string.IsNullOrEmpty(AddDealersName.Text))
            {
                dealersName = AddDealersName.Text;
            }
            else
            {
                Response.Write("<script>alert('請輸入經銷商名稱')</script>");
                CreateDealersPanel.Visible = true;
                return;
            }

            if (!string.IsNullOrEmpty(AddContactPerson.Text))
            {
                person = AddContactPerson.Text;
            }
            else
            {
                Response.Write("<script>alert('請輸入聯絡人')</script>");
                CreateDealersPanel.Visible = true;
                return;
            }

            if (!string.IsNullOrEmpty(AddLocation.Text))
            {
                location = AddLocation.Text;
            }
            else
            {
                Response.Write("<script>alert('請輸入地址')</script>");
                CreateDealersPanel.Visible = true;
                return;
            }

            if (!string.IsNullOrEmpty(AddPhone.Text))
            {
                phone = AddPhone.Text;
            }
            else
            {
                Response.Write("<script>alert('請輸入電話')</script>");
                CreateDealersPanel.Visible = true;
                return;
            }

            #endregion

            string sql = "Insert Into Dealers (DealersAreaID, CoverPhoto, DealerName, ContactPerson, Location, Phone, Fax, Mail, Link) " +
                                  "Values (@DealersAreaID, @CoverPhoto, @DealerName, @ContactPerson, @Location, @Phone, @Fax, @Mail, @Link)";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            if (selectCountry == 0 || selectArea == 0)
            {
                Response.Write("<script>alert('請選擇國家以及地區')</script>");
                return;
            }
            else
            {
                helper.InsertDealers(sql, selectArea, dbPath, dealersName, person, location, phone, fax, mail, link);
                Response.Write("<script>alert('新增成功')</script>");

                CreateDealersPanel.Visible = false;
                DealersPanel.Visible = true;
                string countryValue = ddlCountry.SelectedValue;
                string areaValue = ddlArea.SelectedValue;
                DealersGV_Bind(countryValue, areaValue);
            }
        }

        // 取消新增經銷商資料
        protected void CancelCreate_Click(object sender, EventArgs e)
        {
            CreateDealersPanel.Visible = false;
            DealersPanel.Visible = true;
        }

        #endregion

    }
}