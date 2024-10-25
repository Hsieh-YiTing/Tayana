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
    public partial class Yachts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"]!=null)
                {
                    string id = Request.QueryString["ID"];
                    LeftMenu();
                    RightMenu(id);
                    ShowOverview(id);
                    ShowDimensions(id);
                    LoadAlbum(id);
                }
                else
                {
                    LeftMenu();
                    RightMenu(FirstData());
                    ShowOverview(FirstData());
                    ShowDimensions(FirstData());
                    LoadAlbum(FirstData());
                }
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

        protected string FirstData()
        {
            string sql = "Select ID Top1 From Yachts";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string id = helper.GetFirstData(sql);
            return id;
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

        protected void ShowOverview(string yachtsID)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            string sql = "Select * From OverView Where YachtsID = @YachtsID";
            var parameters = new Dictionary<string, object>
            {
                { "@YachtsID", yachtsID}
            };

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                reader.Read();
                OverviewContet.Text = reader["OverView"].ToString();
                string file = reader["SpecFile"].ToString();
                FileList.Text = $"<li><a href='/{file}' download='{file}'>SpecFile</a></li>";
            }
        }

        protected void ShowDimensions(string yachtsID)
        {
            StringBuilder tableBuilder = new StringBuilder();
            StringBuilder imageBuilder = new StringBuilder();

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);
            string sql = "Select * From Dimensions Where YachtsID = @YachtsID";
            var parameters = new Dictionary<string, object>
            {
                { "@YachtsID", yachtsID}
            };

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                while (reader.Read())
                {
                    string hull = reader["Hull length"].ToString();
                    string lwl = reader["L.W.L."].ToString();
                    string bMax = reader["B. MAX"].ToString();
                    string standardDraft = reader["Standard draft"].ToString();
                    string ballast = reader["Ballast"].ToString();
                    string displacement = reader["Displacement"].ToString();
                    string sailArea = reader["Sail area"].ToString();

                    string engineDiesel = reader["Engine diesel"].ToString();
                    string cutter = reader["Cutter"].ToString();
                    string designer = reader["Designer"].ToString();
                    string shallowDraftKeel = reader["Shallow Draft Keel"].ToString();
                    string headRoom = reader["Head room"].ToString();
                    string yankee = reader["100% Yankee"].ToString();
                    string staysall = reader["Staysall"].ToString();
                    string genoa = reader["Genoa 130%"].ToString();
                    string flasher = reader["Flasher 165%"].ToString();
                    string designSpeed = reader["Design speed"].ToString();
                    string image = reader["Image"].ToString();

                    // 必填資料
                    var fieldPair = new Dictionary<string, string>
                    {
                        { "Hull length", hull},
                        { "L.W.L.", lwl},
                        { "B. MAX", bMax},
                        { "Standard draft", standardDraft},
                        { "Ballast", ballast},
                        { "Displacement", displacement},
                        { "Sail area", sailArea},
                    };

                    // 選填資料
                    var availableFields = new Dictionary<string, string>
                    {
                        { "Engine diesel", engineDiesel},
                        { "Cutter", cutter},
                        { "Designer", designer},
                        { "Shallow Draft Keel", shallowDraftKeel},
                        { "Head room",headRoom},
                        { "100% Yankee", yankee},
                        { "Staysall", staysall},
                        { "Genoa 130%", genoa},
                        { "Flasher 165%", flasher},
                        { "Design speed", designSpeed},
                    };

                    // 當選填的值不為空的時候才加入必填資料的Dictionary內
                    foreach (var field in availableFields.Where(f => !string.IsNullOrEmpty(f.Value)))
                    {
                        fieldPair.Add(field.Key, field.Value);
                    }

                    foreach (var field in fieldPair)
                    {
                        tableBuilder.Append($"<tr class='tr003'><th>{field.Key}</th><td>{field.Value}</td></tr>");
                    }
                    imageBuilder.Append($"<img src='/{image}' style='width:278px; height:305px' />");

                    DimensionsTable.Text = tableBuilder.ToString();
                    Image.Text = imageBuilder.ToString();
                }
            }
        }
    }
}