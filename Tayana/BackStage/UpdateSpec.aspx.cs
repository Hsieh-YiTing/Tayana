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
    public partial class UpdateSpec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CategoryListBind();
                string categoryID = CategoryList.SelectedValue;
                GV_SpecItemBind(categoryID);
            }
        }

        // SpecCategory綁定
        protected void CategoryListBind()
        { 
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From SpecCategory Where YachtsID = @YachtsID";
            var parameters = new Dictionary<string, object> 
            {
                { "@YachtsID", id}
            };

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                helper.CategoryList(reader, CategoryList, "Category", "ID");
            }
        }

        // 當選取不同的類別時，顯示對應的資料
        protected void CategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoryID = CategoryList.SelectedValue;
            GV_SpecItemBind(categoryID);
        }

        // GV_SpecItem資料綁定
        protected void GV_SpecItemBind(string categoryID)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From SpecItem Where SpecCategoryID = @SpecCategoryID";
            var parameters = new Dictionary<string, object>
            {
                { "@SpecCategoryID", categoryID}
            };

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            {
                helper.GridViewBind(reader, GV_SpecItem);
            }
        }

        // GV_SpecItem刪除事件
        protected void GV_SpecItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GV_SpecItem.DataKeys[e.RowIndex].Value);
            string sql = "Delete From SpecItem Where ID = @ID";

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            helper.DeleteDB(sql, id);
            Response.Write("<script>alert('刪除成功')</script>");

            string categoryID = CategoryList.SelectedValue;
            GV_SpecItemBind(categoryID);
        }

        // GV_SpecItem編輯事件
        protected void GV_SpecItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_SpecItem.EditIndex = e.NewEditIndex;
            string categoryID = CategoryList.SelectedValue;
            GV_SpecItemBind(categoryID);
        }

        // GV_SpecItem取消事件
        protected void GV_SpecItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_SpecItem.EditIndex = -1;
            string categoryID = CategoryList.SelectedValue;
            GV_SpecItemBind(categoryID);
        }

        // GV_SpecItem更新事件
        protected void GV_SpecItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GV_SpecItem.DataKeys[e.RowIndex].Value);

            GridViewRow row = GV_SpecItem.Rows[e.RowIndex];
            TextBox editBox = (TextBox) row.FindControl("EditBox");
            string item = editBox.Text;

            if (!string.IsNullOrEmpty(item))
            {
                string sql = "Update SpecItem Set Item = @Item Where ID = @ID";
                string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
                DBHelper helper = new DBHelper(connectionString);
                helper.UpdateSpecItem(sql, id, item);
                Response.Write("<script>alert('更新成功')</script>");
                GV_SpecItem.EditIndex = -1;
                string categoryID = CategoryList.SelectedValue;
                GV_SpecItemBind(categoryID);
            }
            else
            {
                Response.Write("<script>alert('不能更新空的值')</script>");
                return;
            }
        }

        // 跳轉到AddSpec頁面
        protected void AddItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            Response.Redirect($"AddSpec.aspx?ID={id}");
        }

        // 返回Yachts型號頁面
        protected void ReturnModel_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            Response.Redirect($"UpdateYachts.aspx?ID={id}");
        }

        // 跳轉到編輯Dimensions頁面
        protected void EditDimensions_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            Response.Redirect($"UpdateDimensions.aspx?ID={id}");
        }

        // 回到首頁
        protected void ReturnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Yachts_BS.aspx");
        }
    }
}