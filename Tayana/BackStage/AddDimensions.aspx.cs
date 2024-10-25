using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.BackStage
{
    public partial class AddDimensions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // 新增Dimensions
        protected void AddDimension_Click(object sender, EventArgs e)
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
                string sql = "Insert Into Dimensions (YachtsID, [Hull length], [L.W.L.], [B. MAX], [Standard draft], Ballast, Displacement, [Sail area], [Engine diesel], Cutter, Designer, [Shallow Draft Keel], [Head room], [100% Yankee], Staysall, [Genoa 130%], [Flasher 165%], [Design speed], Image) " +
                                  "Values (@YachtsID, @HullLength, @LWL, @BMAX, @StandardDraft, @Ballast, @Displacement, @SailArea, @EngineDiesel, @Cutter, @Designer, @ShallowDraftKeel, @HeadRoom, @Yankee, @Staysall, @Genoa, @Flasher, @DesignSpeed, @Image)";

                string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
                DBHelper helper = new DBHelper(connectionString);
                helper.InsertDimensions(sql, id, dbPath, hullLength, lwl, bMax, standardDraft, ballast, displacement, sailArea, engineDiesel, cutter, designer, shallowDraftKeel, headRoom, yankee, staysall, genoa, flasher, designSpeed);
                Response.Write("<script>alert('Dimensions新增成功')</script>");
            }
            else
            {
                Response.Write("<script>alert('請檢查HullLength, LWL., BMAX, StandardDraft, Ballast, Displacement, SailArea欄位是否有填寫')</script>");
                return;
            }

            Response.Redirect($"AddSpec.aspx?ID={id}");
        }

        protected void ReturnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Yachts_BS.aspx");
        }
    }
}