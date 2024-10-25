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
    public partial class Specification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["ID"];
                LoadAlbum(id);
                LeftMenu();
                RightMenu(id);
                LoadSpec();
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

        protected void LoadSpec()
        {
            string id = Request.QueryString["ID"];
            string sql = "Select SpecItem.ID, SpecCategory.Category, SpecItem.Item From SpecItem " +
                                  "Inner Join SpecCategory On SpecItem.SpecCategoryID = SpecCategory.ID " +
                                  "Where SpecCategory.YachtsID = @YachtsID";
            var parameters = new Dictionary<string, object>
            {
                { "@YachtsID", id}
            };

            StringBuilder hullBuilder = new StringBuilder();
            StringBuilder deckBuilder = new StringBuilder();
            StringBuilder engineBuilder = new StringBuilder();
            StringBuilder steeringBuilder = new StringBuilder();
            StringBuilder sparsBuilder = new StringBuilder();
            StringBuilder sailsBuilder = new StringBuilder();
            StringBuilder interiorBuilder = new StringBuilder(); 
            StringBuilder electricalBuilder = new StringBuilder();
            StringBuilder plumbingBuilder = new StringBuilder();

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            using (SqlDataReader reader = helper.SerachDB(sql, parameters))
            { 
                while (reader.Read()) 
                {
                    string category = reader["Category"].ToString();
                    string item = reader["Item"].ToString();

                    switch (category)
                    {
                        case "Hull":
                            hullBuilder.AppendFormat("<li>{0}</li>", item);
                            break;
                        case "Deck/Hardware":
                            deckBuilder.AppendFormat("<li>{0}</li>", item);
                            break;
                        case "Engine/Machinery":
                            engineBuilder.AppendFormat("<li>{0}</li>", item);
                            break;
                        case "Steering":
                            steeringBuilder.AppendFormat("<li>{0}</li>", item);
                            break;
                        case "Spars/Rigging":
                            sparsBuilder.AppendFormat("<li>{0}</li>", item);
                            break;
                        case "Sails":
                            sailsBuilder.AppendFormat("<li>{0}</li>", item);
                            break;
                        case "Interior":
                            interiorBuilder.AppendFormat("<li>{0}</li>", item);
                            break;
                        case "Electrical":
                            electricalBuilder.AppendFormat("<li>{0}</li>", item);
                            break;
                        case "Plumbing":
                            plumbingBuilder.AppendFormat("<li>{0}</li>", item);
                            break;
                    }
                }
            }

            HullItem.Text = hullBuilder.ToString();
            DeckItem.Text = deckBuilder.ToString();
            EngineItem.Text = engineBuilder.ToString();
            SteeringItem.Text = steeringBuilder.ToString();
            SparsItem.Text = sparsBuilder.ToString();
            SailsItem.Text = sailsBuilder.ToString();
            InteriorItem.Text = interiorBuilder.ToString();
            ElectricalItem.Text = electricalBuilder.ToString();
            PlumbingItem.Text = plumbingBuilder.ToString();
        }
    }
}