using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tayana.BackStage
{
    public partial class AddSpec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddHullItem_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 透過YachtsID與Category搜尋出對應的CategoryID
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string category = "Hull";
            int categoryID = helper.CategoryID(id, category);

            // 透過CategoryID寫入Item表，且TextBox不能為空
            string insertItem = "Insert Into SpecItem (SpecCategoryID, Item) Values (@SpecCategoryID, @Item)";
            string item = string.Empty;

            if (!string.IsNullOrEmpty(HullItem.Text))
            {
                item = HullItem.Text;
            }
            else
            {
                Response.Write("<script>alert('輸入值後再新增')</script>");
                return;
            }

            helper.InsertItem(insertItem, categoryID, item);
            Response.Write("<script>alert('新增成功')</script>");
            HullItem.Text = string.Empty;
        }

        protected void AddDeckItem_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 透過YachtsID與Category搜尋出對應的CategoryID
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string category = "Deck/Hardware";
            int categoryID = helper.CategoryID(id, category);

            // 透過CategoryID寫入Item表，且TextBox不能為空
            string insertItem = "Insert Into SpecItem (SpecCategoryID, Item) Values (@SpecCategoryID, @Item)";
            string item = string.Empty;

            if (!string.IsNullOrEmpty(DeckItem.Text))
            {
                item = DeckItem.Text;
            }
            else
            {
                Response.Write("<script>alert('輸入值後再新增')</script>");
                return;
            }

            helper.InsertItem(insertItem, categoryID, item);
            Response.Write("<script>alert('新增成功')</script>");
            DeckItem.Text = string.Empty;
        }

        protected void AddEngineItem_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 透過YachtsID與Category搜尋出對應的CategoryID
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string category = "Engine/Machinery";
            int categoryID = helper.CategoryID(id, category);

            // 透過CategoryID寫入Item表，且TextBox不能為空
            string insertItem = "Insert Into SpecItem (SpecCategoryID, Item) Values (@SpecCategoryID, @Item)";
            string item = string.Empty;

            if (!string.IsNullOrEmpty(EngineItem.Text))
            {
                item = EngineItem.Text;
            }
            else
            {
                Response.Write("<script>alert('輸入值後再新增')</script>");
                return;
            }

            helper.InsertItem(insertItem, categoryID, item);
            Response.Write("<script>alert('新增成功')</script>");
            EngineItem.Text = string.Empty;
        }

        protected void AddSteeringItem_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 透過YachtsID與Category搜尋出對應的CategoryID
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string category = "Steering";
            int categoryID = helper.CategoryID(id, category);

            // 透過CategoryID寫入Item表，且TextBox不能為空
            string insertItem = "Insert Into SpecItem (SpecCategoryID, Item) Values (@SpecCategoryID, @Item)";
            string item = string.Empty;

            if (!string.IsNullOrEmpty(SteeringItem.Text))
            {
                item = SteeringItem.Text;
            }
            else
            {
                Response.Write("<script>alert('輸入值後再新增')</script>");
                return;
            }

            helper.InsertItem(insertItem, categoryID, item);
            Response.Write("<script>alert('新增成功')</script>");
            SteeringItem.Text = string.Empty;
        }

        protected void AddSparsItem_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 透過YachtsID與Category搜尋出對應的CategoryID
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string category = "Spars/Rigging";
            int categoryID = helper.CategoryID(id, category);

            // 透過CategoryID寫入Item表，且TextBox不能為空
            string insertItem = "Insert Into SpecItem (SpecCategoryID, Item) Values (@SpecCategoryID, @Item)";
            string item = string.Empty;

            if (!string.IsNullOrEmpty(SparsItem.Text))
            {
                item = SparsItem.Text;
            }
            else
            {
                Response.Write("<script>alert('輸入值後再新增')</script>");
                return;
            }

            helper.InsertItem(insertItem, categoryID, item);
            Response.Write("<script>alert('新增成功')</script>");
            SparsItem.Text = string.Empty;
        }

        protected void AddSailsItem_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 透過YachtsID與Category搜尋出對應的CategoryID
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string category = "Sails";
            int categoryID = helper.CategoryID(id, category);

            // 透過CategoryID寫入Item表，且TextBox不能為空
            string insertItem = "Insert Into SpecItem (SpecCategoryID, Item) Values (@SpecCategoryID, @Item)";
            string item = string.Empty;

            if (!string.IsNullOrEmpty(SailsItem.Text))
            {
                item = SailsItem.Text;
            }
            else
            {
                Response.Write("<script>alert('輸入值後再新增')</script>");
                return;
            }

            helper.InsertItem(insertItem, categoryID, item);
            Response.Write("<script>alert('新增成功')</script>");
            SailsItem.Text = string.Empty;
        }

        protected void AddInteriorItem_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 透過YachtsID與Category搜尋出對應的CategoryID
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string category = "Interior";
            int categoryID = helper.CategoryID(id, category);

            // 透過CategoryID寫入Item表，且TextBox不能為空
            string insertItem = "Insert Into SpecItem (SpecCategoryID, Item) Values (@SpecCategoryID, @Item)";
            string item = string.Empty;

            if (!string.IsNullOrEmpty(InteriorItem.Text))
            {
                item = InteriorItem.Text;
            }
            else
            {
                Response.Write("<script>alert('輸入值後再新增')</script>");
                return;
            }

            helper.InsertItem(insertItem, categoryID, item);
            Response.Write("<script>alert('新增成功')</script>");
            InteriorItem.Text = string.Empty;
        }

        protected void AddElectricalItem_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 透過YachtsID與Category搜尋出對應的CategoryID
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string category = "Electrical";
            int categoryID = helper.CategoryID(id, category);

            // 透過CategoryID寫入Item表，且TextBox不能為空
            string insertItem = "Insert Into SpecItem (SpecCategoryID, Item) Values (@SpecCategoryID, @Item)";
            string item = string.Empty;

            if (!string.IsNullOrEmpty(ElectricalItem.Text))
            {
                item = ElectricalItem.Text;
            }
            else
            {
                Response.Write("<script>alert('輸入值後再新增')</script>");
                return;
            }

            helper.InsertItem(insertItem, categoryID, item);
            Response.Write("<script>alert('新增成功')</script>");
            ElectricalItem.Text = string.Empty;
        }

        protected void AddPlumbingItem_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            // 透過YachtsID與Category搜尋出對應的CategoryID
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string category = "Plumbing";
            int categoryID = helper.CategoryID(id, category);

            // 透過CategoryID寫入Item表，且TextBox不能為空
            string insertItem = "Insert Into SpecItem (SpecCategoryID, Item) Values (@SpecCategoryID, @Item)";
            string item = string.Empty;

            if (!string.IsNullOrEmpty(PlumbingItem.Text))
            {
                item = PlumbingItem.Text;
            }
            else
            {
                Response.Write("<script>alert('輸入值後再新增')</script>");
                return;
            }

            helper.InsertItem(insertItem, categoryID, item);
            Response.Write("<script>alert('新增成功')</script>");
            PlumbingItem.Text = string.Empty;
        }

        protected void ReturnIndex_Click(object sender, EventArgs e)
        {
            Response.Redirect("Yachts_BS.aspx");
        }

        protected void CheckData_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            Response.Redirect($"UpdateSpec.aspx?ID={id}");
        }
    }
}