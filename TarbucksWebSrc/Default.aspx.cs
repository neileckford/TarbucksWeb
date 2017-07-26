using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;

namespace TarbucksWeb
{
    public partial class _Default : Page
    {
        //MySqlConnection conn = null;
        MySqlDataAdapter adapter = null;
        DataTable dt = new DataTable();
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

                adapter = new MySqlDataAdapter("select transactions.transactiondate as 'Date', customerinfo.accountid as 'Account ID', customerinfo.name as 'Name', transactions.item as 'Item' from customerinfo left outer join transactions on customerinfo.accountid = transactions.accountid where transactions.transactiondate is not null order by transactions.transactiondate desc, transactions.accountid", conn);
                adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }catch(Exception ex)
            {
                
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DropDownList1.Items.Clear();
            using (var cmd = new MySqlCommand("SELECT * FROM customerinfo", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        DropDownList1.DataSource = reader;
                        DropDownList1.DataValueField = "accountid";
                        DropDownList1.DataTextField = "accountid";
                        DropDownList1.DataBind();
                    }
                }
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            adapter = new MySqlDataAdapter("select transactions.transactiondate as 'Date', customerinfo.name, transactions.item from customerinfo left outer join transactions on customerinfo.accountid = transactions.accountid where customerinfo.name like '%mike walter%'", conn);
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        private void transaction()
        {
            try
            {
                tr = conn.BeginTransaction();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tr;

                cmd.CommandText = "insert into customerinfo values(1,'caitlin','monikie','23456')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "insert into customerinfo values(7,'jessica','paisley','23456')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "rollback";
                cmd.ExecuteNonQuery();

                tr.Commit();

                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customerID = DropDownList1.SelectedItem.Value;
            GridView1.DataSource = null;
            GridView1.DataBind();
            adapter = new MySqlDataAdapter("select transactions.transactiondate as 'Date', customerinfo.accountid, customerinfo.name, transactions.item from customerinfo left outer join transactions on customerinfo.accountid = transactions.accountid where customerinfo.accountid like '%"+customerID+"%'", conn);
            //adapter = new MySqlDataAdapter("select transactions.transactiondate as 'Date', customerinfo.accountid, customerinfo.name, transactions.item from customerinfo left outer join transactions on customerinfo.accountid = transactions.accountid group by transactions.accountid order by count(transactions.accountid) desc", conn);
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }
}