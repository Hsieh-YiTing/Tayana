using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Reflection.Emit;
using System.ComponentModel.Design;
using System.Reflection;

namespace Tayana
{
    public class DBHelper
    {
        private SqlConnection connection;
        private SqlCommand command;

        public DBHelper(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }

        private void ConnectDB()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void CloseDB()
        {
            connection.Close();
        }

        public SqlDataReader SerachDB(string sql, Dictionary<string, object> parameters = null)
        {
            command.CommandText = sql;
            ConnectDB();

            if (parameters != null)
            {
                command.Parameters.Clear();
                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
            }

            // 不使用CloseDB()，是因為SqlDataReader屬於流式讀取，不會儲存資料，在資料庫連結斷開後就無法讀取資料。
            // 透過CommandBehavior.CloseConnection，會在DataBind前維持開啟狀態，直到綁定結束後自動關閉。
            // 也不需要透過Using來管理。
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public void GridViewBind(SqlDataReader reader, GridView gridView)
        {
            gridView.DataSource = reader;
            gridView.DataBind();
        }

        public void RepeaterBind(SqlDataReader reader, Repeater repeater)
        {
            repeater.DataSource = reader;
            repeater.DataBind();
        }

        // 參數 : SearchDB()獲得的資料表、DropDownList、指定的資料表分類、指定的資料表ID。
        public void CategoryList(SqlDataReader reader, DropDownList list, string text, string value)
        {
            // 獲取資料庫來源。
            list.DataSource = reader;

            // 指定DropDownList內的text、value。
            list.DataTextField = text;
            list.DataValueField = value;

            // 綁定資料。
            list.DataBind();
        }

        public void DeleteDB(string sql, int id)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateDealers(string sql, int id, int area, string dbPath, string dealersName, string location, string person, string phone, string fax, string mail, string link)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();

            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@DealersAreaID", area);
            command.Parameters.AddWithValue("@DealerName", dealersName);
            command.Parameters.AddWithValue("@Location", location);
            command.Parameters.AddWithValue("@ContactPerson", person);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Fax", fax);
            command.Parameters.AddWithValue("@Mail", mail);
            command.Parameters.AddWithValue("@Link", link);

            if (!string.IsNullOrEmpty(dbPath))
            {
                command.Parameters.AddWithValue("@CoverPhoto", dbPath);
            }

            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdatePhoto(string sql, int id, string coverPhoto)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@CoverPhoto", coverPhoto);
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateDealersInf(string sql, int id, string contactPerson, string phone, string fax, string mail, string link)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@ContactPerson", contactPerson);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Fax", fax);
            command.Parameters.AddWithValue("@Mail", mail);
            command.Parameters.AddWithValue("@Link", link);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void InsertCountry(string sql, string country)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Country", country);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void InsertArea(string sql, int countryID, string area)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@DealersCountryID", countryID);
            command.Parameters.AddWithValue("@Area", area);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void InsertDealers(string sql, int dealersAreaID, string coverPhoto, string dealerName, string contactPerson, string location, string phone, string fax, string mail, string link)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@DealersAreaID", dealersAreaID);
            command.Parameters.AddWithValue("@CoverPhoto", coverPhoto);
            command.Parameters.AddWithValue("@DealerName", dealerName);
            command.Parameters.AddWithValue("@ContactPerson", contactPerson);
            command.Parameters.AddWithValue("@Location", location);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Fax", fax);
            command.Parameters.AddWithValue("@Mail", mail);
            command.Parameters.AddWithValue("@Link", link);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void InsertAboutUs(string sql, string text, string dbPath)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@AboutUs", text);
            command.Parameters.AddWithValue("@Image", dbPath);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateCertificate(string sql, string text)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Certificate", text);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void InsertCertificateAlbum(string sql, int certificateID, string dbPath)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@CertificateID", certificateID);
            command.Parameters.AddWithValue("@CertificateImage", dbPath);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateCertificateAlbum(string sql, int id, string dbPath)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@CertificateImage", dbPath);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public int InsertNews(string sql, string title, string content, bool isTop)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@NewsContent", content);
            command.Parameters.AddWithValue("@IsTop", isTop);

            int insertedID = (int)command.ExecuteScalar();
            return insertedID;
        }

        public void InsertNewsAlbum(string sql, int id, string image)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@NewsID", id);
            command.Parameters.AddWithValue("@NewsImage", image);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void InsertNewsFile(string sql, int id, string fileName, string file)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@NewsID", id);
            command.Parameters.AddWithValue("@FileName", fileName);
            command.Parameters.AddWithValue("@NewsFile", file);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateNewsCover(string sql, int id, string photoPath)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@CoverPhoto", photoPath);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateDefaultPhoto(string sql, int id, string defaultImage)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@CoverPhoto", defaultImage);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateNews(string sql, int id, string title, string content, string coverPhoto)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@NewsContent", content);
            command.Parameters.AddWithValue("@CoverPhoto", coverPhoto);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateTopStatus(int id, string setTopStatus, string updateTop = null)
        {
            command.CommandText = setTopStatus;
            ConnectDB();
            command.ExecuteNonQuery();

            if (updateTop != null)
            {
                command.CommandText = updateTop;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", id);
            }

            command.ExecuteNonQuery();
            CloseDB();
        }

        public int InsertYachts(string sql, string model, bool newDesign)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Yachts", model);
            command.Parameters.AddWithValue("@NewDesign", newDesign);

            int insertedID = (int)command.ExecuteScalar();
            CloseDB();
            return insertedID;
        }

        public void InsertOverView(string sql, int id, string overView, string specFile)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@YachtsID", id);
            command.Parameters.AddWithValue("@OverView", overView);
            command.Parameters.AddWithValue("@SpecFile", specFile);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void InsertDimensions(string sql, int id, string dbPath, string hullLength, string lwl, string bMax, string standardDraft, string ballast, string displacement, string sailArea, string engineDiesel = null, string cutter = null, string designer = null, string shallowDraftKeel = null, string headRoom = null, string yankee = null, string staysall = null, string genoa = null, string flasher = null, string designSpeed = null)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();

            // 創立Dictionary來儲存參數以及對應的值
            var parameters = new Dictionary<string, object>
            {
                { "@YachtsID", id},
                { "@Image", dbPath},
                { "@HullLength", hullLength},
                { "@LWL", lwl},
                { "@BMAX", bMax},
                { "@StandardDraft", standardDraft},
                { "@Ballast", ballast},
                { "@Displacement", displacement},
                { "@SailArea", sailArea},
                { "@EngineDiesel", engineDiesel},
                { "@Cutter", cutter},
                { "@Designer", designer },
                { "@ShallowDraftKeel", shallowDraftKeel },
                { "@HeadRoom", headRoom },
                { "@Yankee", yankee },
                { "@Staysall", staysall },
                { "@Genoa", genoa },
                { "@Flasher", flasher },
                { "@DesignSpeed", designSpeed }
            };

            // 透過LINQ篩選出不是null的參數
            foreach (var param in parameters.Where(p => p.Value != null))
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }

            command.ExecuteNonQuery();
            CloseDB();
        }

        public void AddCategory(string sql, int id, string[] categories)
        {
            ConnectDB();

            foreach (string category in categories)
            {
                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@YachtsID", id);
                command.Parameters.AddWithValue("@Category", category);
                command.ExecuteNonQuery();
            }

            CloseDB();
        }

        public void InsertYachtsAlbum(string sql, int id, string image)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@YachtsID", id);
            command.Parameters.AddWithValue("@YachtsImages", image);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void InsertLayout(string sql, int id, string image)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@YachtsID", id);
            command.Parameters.AddWithValue("@ImagePath", image);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public int CategoryID(int id, string category)
        {
            string findID = "Select ID From SpecCategory Where YachtsID = @YachtsID And Category = @Category";
            command.CommandText = findID;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@YachtsID", id);
            command.Parameters.AddWithValue("@Category", category);
            int categoryID = (int) command.ExecuteScalar();
            CloseDB();
            return categoryID;
        }

        public void InsertItem(string sql, int id, string item)
        { 
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@SpecCategoryID", id);
            command.Parameters.AddWithValue("@Item", item);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateYachts(string sql, int id, string model)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@Yachts", model);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateOverview(string sql, int id, string overview, string specFile)
        {
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@YachtsID", id);
            command.Parameters.AddWithValue("@OverView", overview);
            command.Parameters.AddWithValue("@SpecFile", specFile);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void UpdateSpecItem(string sql, int id, string item)
        { 
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@Item", item);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public void InsertUser(string sql, int premissionsID, string user, string password, string salt)
        { 
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@PremissionsID", premissionsID);
            command.Parameters.AddWithValue("@UserName", user);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Salt", salt);
            command.ExecuteNonQuery();
            CloseDB();
        }

        public int CheckUser(string sql, string userName)
        { 
            command.CommandText = sql;
            ConnectDB();
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@UserName", userName);
            int result = (int) command.ExecuteScalar();
            return result;
        }

        public string GetFirstData(string sql)
        { 
            command.CommandText = sql;
            ConnectDB();
            string result = command.ExecuteScalar().ToString();
            return result;
        }
    }
}