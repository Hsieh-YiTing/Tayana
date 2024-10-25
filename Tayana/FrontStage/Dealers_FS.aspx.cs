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
    public partial class Dealers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LeftMenu();

            string countryID = Request.QueryString["ID"];

            if (countryID == null)
            {
                MainContent(1);
            }
            else
            {
                int id = int.Parse(countryID);
                MainContent(id);
            }
        }

        protected void LeftMenu()
        {
            StringBuilder builder = new StringBuilder();

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select * From DealersCountry";
            using (SqlDataReader reader = helper.SerachDB(sql))
            {
                while (reader.Read())
                {
                    string countryID = reader["ID"].ToString();
                    string countryName = reader["Country"].ToString();
                    builder.Append($"<li><a href='Dealers_FS.aspx?ID={countryID}'>{countryName}</a></li>");
                }
            }

            Literal1.Text = builder.ToString();
        }

        protected void MainContent(int countryID)
        {
            StringBuilder builder = new StringBuilder();

            string connectionString = WebConfigurationManager.ConnectionStrings["connectionString_Tayana"].ConnectionString;
            DBHelper helper = new DBHelper(connectionString);

            string sql = "Select Dealers.ID, DealersCountry.Country, DealersArea.Area, CoverPhoto, DealerName, ContactPerson, Location, Phone, Fax, Mail, Link From Dealers " +
                                   "Inner Join DealersArea On Dealers.DealersAreaID = DealersArea.ID " +
                                   "Inner Join DealersCountry On DealersArea.DealersCountryID = DealersCountry.ID " +
                                   "Where DealersCountry.ID = @CountryID ";
            var parameters = new Dictionary<string, object>
            {
                { "@CountryID", countryID}
            };

            SqlDataReader reader = helper.SerachDB(sql, parameters);

            while (reader.Read())
            {
                Literal3.Text = reader["Country"].ToString();
                HyperLink1.Text = reader["Country"].ToString();
                HyperLink1.NavigateUrl = $"Dealers1.aspx?id={countryID}";
                string area = reader["Area"].ToString();
                string photo = reader["CoverPhoto"].ToString();
                string dealersName = reader["DealerName"].ToString();
                string ContactPerson = reader["ContactPerson"].ToString();
                string location = reader["Location"].ToString();
                string phone = reader["Phone"].ToString();
                string fax = reader["Fax"].ToString();
                string mail = reader["Mail"].ToString();
                string link = reader["Link"].ToString();

                builder.Append($"<li><div class='list02'><ul><li class='list02li'><div><p><img src='/{photo}' style='width:199px; height:133px'/></p></div></li>");

                if (!string.IsNullOrEmpty(area))
                {
                    builder.Append($"<li><span>{area}</span><br/>");
                }
                if (!string.IsNullOrEmpty(dealersName))
                {
                    builder.Append($"{dealersName}<br/>");
                }
                if (!string.IsNullOrEmpty(ContactPerson))
                {
                    builder.Append($"Contact : {ContactPerson}<br/>");
                }
                if (!string.IsNullOrEmpty(location))
                {
                    builder.Append($"Address : {location}<br/>");
                }
                if (!string.IsNullOrEmpty(phone))
                {
                    builder.Append($"TEL : {phone} <br/>");
                }
                if (!string.IsNullOrEmpty(fax))
                {
                    builder.Append($"Fax : {fax}<br/>");
                }
                if (!string.IsNullOrEmpty(mail))
                {
                    builder.Append($"E-mail : {mail}<br/>");
                }
                if (!string.IsNullOrEmpty(link))
                {
                    builder.Append($"<a href = '{link}'></a></li>");
                }

                builder.Append("</ul></div></li>");
                Literal2.Text = builder.ToString();
            }
        }
    }
}