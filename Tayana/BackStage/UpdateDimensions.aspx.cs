using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.BackStage
{
    public partial class UpdateDimensions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckData();
            }
        }

        // 載入時，先檢查資料庫有沒有該ID，沒有就跳轉到新增頁面
        protected void CheckData()
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
                    LoadData();
                }
            }
        }

        // 載入資料庫
        protected void LoadData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string sql = "Select * From Dimensions Where YachtsID = @YachtsID";
            var parameters = new Dictionary<string, object>
            {
                { "@YachtsID", id}
            };

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                if (reader.Read())
                {
                    string imagePath = reader["Image"].ToString();
                    DimensionsImage.ImageUrl = $"~/{imagePath}";
                    HullLength.Text = reader["Hull length"].ToString();
                    LWL.Text = reader["L.W.L."].ToString();
                    BMAX.Text = reader["B. MAX"].ToString();
                    StandardDraft.Text = reader["Standard draft"].ToString();
                    Ballast.Text = reader["Ballast"].ToString();
                    Displacement.Text = reader["Displacement"].ToString();
                    SailArea.Text = reader["Sail area"].ToString();
                    EngineDiesel.Text = reader["Engine diesel"].ToString();
                    Cutter.Text = reader["Cutter"].ToString();
                    Designer.Text = reader["Designer"].ToString();
                    ShallowDraftKeel.Text = reader["Shallow Draft Keel"].ToString();
                    HeadRoom.Text = reader["Head room"].ToString();
                    Yankee.Text = reader["100% Yankee"].ToString();
                    Staysall.Text = reader["Staysall"].ToString();
                    Genoa.Text = reader["Genoa 130%"].ToString();
                    Flasher.Text = reader["Flasher 165%"].ToString();
                    DesignSpeed.Text = reader["Design speed"].ToString();
                }
                else
                {
                    HullLength.Text = string.Empty;
                    LWL.Text = string.Empty;
                    BMAX.Text = string.Empty;
                    StandardDraft.Text = string.Empty;
                    Ballast.Text = string.Empty;
                    Displacement.Text = string.Empty;
                    SailArea.Text = string.Empty;
                    EngineDiesel.Text = string.Empty;
                    Cutter.Text = string.Empty;
                    Designer.Text = string.Empty;
                    ShallowDraftKeel.Text = string.Empty;
                    HeadRoom.Text = string.Empty;
                    Yankee.Text = string.Empty;
                    Staysall.Text = string.Empty;
                    Genoa.Text = string.Empty;
                    Flasher.Text = string.Empty;
                    DesignSpeed.Text = string.Empty;
                }
            }
        }

        // 編輯事件
        protected void EditDimension_Click(object sender, EventArgs e)
        {
            // 獲取ID
            int id = Convert.ToInt32(Request.QueryString["ID"]);

            // 必填資料
            string hullLength = HullLength.Text;
            string lwl = LWL.Text;
            string bMax = BMAX.Text;
            string standardDraft = StandardDraft.Text;
            string ballast = Ballast.Text;
            string displacement = Displacement.Text;
            string sailArea = SailArea.Text;

            // 選填資料
            string engineDiesel = EngineDiesel.Text;
            string cutter = Cutter.Text;
            string designer = Designer.Text;
            string shallowDraftKeel = ShallowDraftKeel.Text;
            string headRoom = HeadRoom.Text;
            string yankee = Yankee.Text;
            string staysall = Staysall.Text;
            string genoa = Genoa.Text;
            string flasher = Flasher.Text;
            string designSpeed = DesignSpeed.Text;

            string dbPath = string.Empty;
            if (AddImage.HasFile)
            {
                string fileName = AddImage.FileName;
                string serverPath = Server.MapPath($"~/BackStage/DimensionsImage/{fileName}");
                AddImage.SaveAs(serverPath);
                dbPath = $"BackStage/DimensionsImage/{fileName}";
            }
            else
            {
                dbPath = $"BackStage/DimensionsImage/Default.jpg";
            }

            // 使用LINQ的Any方法檢查必填資料
            string[] fields = { hullLength, lwl, bMax, standardDraft, ballast, displacement, sailArea };

            if (!fields.Any(string.IsNullOrEmpty))
            {
                string sql = "Update Dimensions Set [Hull length] = @HullLength, [L.W.L.] = @LWL, [B. MAX] = @BMAX, [Standard draft] = @StandardDraft, Ballast = @Ballast, Displacement = @Displacement, [Sail area] = @SailArea, " +
                                      "[Engine diesel] = @EngineDiesel, Cutter = @Cutter, Designer = @Designer, [Shallow Draft Keel] = @ShallowDraftKeel, [Head room] = @HeadRoom, [100% Yankee] = @Yankee, Staysall = @Staysall, [Genoa 130%] = @Genoa, [Flasher 165%] = @Flasher, [Design speed] = @DesignSpeed, Image = @Image " +
                                      "Where YachtsID = @YachtsID";
                string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
                DBHelper helper = new DBHelper(connectionString);
                helper.InsertDimensions(sql, id, dbPath, hullLength, lwl, bMax, standardDraft, ballast, displacement, sailArea, engineDiesel, cutter, designer, shallowDraftKeel, headRoom, yankee, staysall, genoa, flasher, designSpeed);
                Response.Write("<script>alert('Dimensions編輯成功')</script>");
            }
            else
            {
                Response.Write("<script>alert('請檢查HullLength, LWL., BMAX, StandardDraft, Ballast, Displacement, SailArea欄位是否有填寫')</script>");
                return;
            }

            LoadData();
        }

        // 返回Yachts型號頁面
        protected void ReturnModel_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            Response.Redirect($"UpdateYachts.aspx?ID={id}");
        }

        // 跳轉到編輯Spec頁面
        protected void EditSpec_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            Response.Redirect($"UpdateSpec.aspx?ID={id}");
        }

        // 回到首頁
        protected void ReturnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Yachts_BS.aspx");
        }
    }
}