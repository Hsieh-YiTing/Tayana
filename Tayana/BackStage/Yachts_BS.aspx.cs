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
    public partial class Yachits_BS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GV_YachtsBind();
            }
        }

        // 綁定Yachts資料
        protected void GV_YachtsBind()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From Yachts Order By NewDesign Desc, ID";

            using (SqlDataReader reader = helper.SerachDB(sql))
            {
                helper.GridViewBind(reader, GV_Yachts);
            }
        }

        // 點選選取後跳轉頁面
        protected void GV_Yachts_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectRow = GV_Yachts.SelectedRow;
            int id = Convert.ToInt32(selectRow.Cells[0].Text);

            Response.Redirect($"UpdateYachts.aspx?ID={id}");
        }

        protected void GV_Yachts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GV_Yachts.DataKeys[e.RowIndex].Value);
            string sql = "Delete From Yachts Where ID = @ID";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            helper.DeleteDB(sql, id);
            Response.Write("<script>alert('刪除成功')</script>");
            GV_YachtsBind();
        }

        protected void NewDesign_CheckedChanged(object sender, EventArgs e)
        {
            // 獲取點選行的參數
            CheckBox checkBox = (CheckBox)sender;

            // 透過參數獲取該控制項的容器
            GridViewRow row = (GridViewRow)checkBox.NamingContainer;

            // 透過容器獲取ID
            int id = Convert.ToInt32(GV_Yachts.DataKeys[row.RowIndex].Value);

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 在DBHelper內，將updateTop設置為可空
            if (checkBox.Checked)
            {
                // 先將所有News的置頂狀態設為false
                string setTopStatus = "Update Yachts Set NewDesign = 0";

                // 再依據NewID來更新置頂
                string updateTop = "Update Yachts Set NewDesign = 1 Where ID = @ID";

                helper.UpdateTopStatus(id, setTopStatus, updateTop);
            }
            else
            {
                string setTopStatus = "Update Yachts Set NewDesign = 0";
                helper.UpdateTopStatus(id, setTopStatus);
            }

            Response.Write("<script>alert('更新成功')</script>");
            GV_YachtsBind();
        }

        // 跳轉到AddYachts頁面
        protected void AddYachts_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddYachts.aspx");
        }
    }
}