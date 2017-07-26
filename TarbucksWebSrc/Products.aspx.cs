using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace TarbucksWeb
{
    public partial class Products : System.Web.UI.Page
    {
        MySqlConnection conn;
        MySqlTransaction tr = null;
        MySqlDataAdapter adp;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {

            string server = "localhost";
            string database = "tarbucks";
            
            try
            {
                string name = Session["name"].ToString();
                string password = Session["password"].ToString();
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + name + ";" + "PASSWORD=" + password + ";";
                conn = new MySqlConnection(connectionString);
                conn.Open();
                
                //current products table
                DataTable dt = new DataTable();
                MySqlDataAdapter adapterCurrent = new MySqlDataAdapter("select distinct item as 'Product' from transactions", conn);
                adapterCurrent.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    CurrentProducts.DataSource = dt;
                    CurrentProducts.DataBind();
                }

                //best selling table
                DataTable dt2 = new DataTable();
                MySqlDataAdapter adapterBestselling = new MySqlDataAdapter("select distinct item as 'Product', count(item) as 'Number sold' from transactions group by item order by count(item) desc limit 5", conn);
                //adapter = new MySqlDataAdapter("select transactiondate, accountid, count(1) as cnt from transactions group by transactiondate, accountid order by cnt desc", conn);
                adapterBestselling.Fill(dt2);


                if (dt.Rows.Count > 0)
                {
                    BestSelling.DataSource = dt2;
                    BestSelling.DataBind();
                }

                //combinations table
                /*DataTable dt3 = new DataTable();
                MySqlDataAdapter adapterCombination = new MySqlDataAdapter("select  transactiondate, accountid, count(distinct item) from transactions where item in ('hot chocolate', 'large coffee') group by 1,2 having count(distinct item) > 1", conn);
                //adapter = new MySqlDataAdapter("select transactiondate, accountid, count(1) as cnt from transactions group by transactiondate, accountid order by cnt desc", conn);
                adapterCombination.Fill(dt3);


                if (dt3.Rows.Count > 0)
                {
                    Combination.DataSource = dt3;
                    Combination.DataBind();
                }*/
            }
            catch (Exception ex)
            {
                Response.Write("You do not have privileges");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //combinations table
            DataTable dt3 = new DataTable();
            MySqlDataAdapter adapterCombination = new MySqlDataAdapter("select  transactiondate as 'Date', accountid as 'Account ID', count(distinct item) as 'Items Purchased' from transactions where item in ('" + Items1.SelectedValue + "', '" + Items2.SelectedValue+"') group by 1,2 having count(distinct item) > 1", conn);
            adapterCombination.Fill(dt3);


            if (dt3.Rows.Count > 0)
            {
                Combination.DataSource = dt3;
                Combination.DataBind();
            }
        }

        private void refreshItems()
        {
            Items1.Items.Clear();
            using (var cmd = new MySqlCommand("select distinct item from transactions", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Items1.DataSource = reader;
                        Items1.DataValueField = "item";
                        Items1.DataTextField = "item";
                        Items1.DataBind();
                    }
                }
            }
            Items2.Items.Clear();
            using (var cmd = new MySqlCommand("select distinct item from transactions", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Items2.DataSource = reader;
                        Items2.DataValueField = "item";
                        Items2.DataTextField = "item";
                        Items2.DataBind();
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            refreshItems();
        }
    }
}