using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TarbucksWeb
{
    public partial class Customers : System.Web.UI.Page
    {
        MySqlConnection conn;
        string server = "localhost";
        string database = "tarbucks";
        
        string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                string name = Session["name"].ToString();
                string password = Session["password"].ToString();
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + name + ";" + "PASSWORD=" + password + ";";
                conn = new MySqlConnection(connectionString);
                conn.Open();

                //all customerstable
                DataTable dt = new DataTable();
                MySqlDataAdapter adapterCustomers = new MySqlDataAdapter("select accountid as 'Account ID', name as 'Name', address as 'Address', phone as 'Phone' from customerinfo", conn);
                adapterCustomers.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    AllCustomers.DataSource = dt;
                    AllCustomers.DataBind();
                }

                //most purchases
                DataTable dt2 = new DataTable();
                MySqlDataAdapter adapterPurchases = new MySqlDataAdapter("select distinct accountid as 'AccountID', count(accountid) as 'Purchases' from transactions group by accountid order by count(accountid) desc", conn);
                adapterPurchases.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    MostPurchases.DataSource = dt2;
                    MostPurchases.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("You do not have privileges");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                addCustomer();
            }catch(Exception ex)
            {
                
            }
            
        }

        private void addCustomer()
        {
            MySqlConnection conn;
            MySqlTransaction tr = null;

            string custName = txtCustName.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;

            conn = new MySqlConnection(connectionString);
            conn.Open();

            try
            {
                tr = conn.BeginTransaction();

                MySqlCommand cmd = new MySqlCommand();
                

                cmd.Connection = conn;
                cmd.Transaction = tr;

                cmd.CommandText = "insert into customerinfo(name, address, phone) values('" + custName + "','" + address+ "','" + phone + "')";
                cmd.ExecuteNonQuery();

                tr.Commit();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location", "Customers.aspx");
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                Response.Write("rolled back");
            }
        }

        protected void btnCustRemove_Click(object sender, EventArgs e)
        {
            refreshCustomers();
        }

        private void refreshCustomers()
        {
            MySqlConnection conn;
            string connectionString;
            string server = "localhost";
            string database = "tarbucks";
            string name = "root";
            string password = "neil84";

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + name + ";" + "PASSWORD=" + password + ";";
            conn = new MySqlConnection(connectionString);
            conn.Open();

            RemoveCustomer.Items.Clear();
            using (var cmd = new MySqlCommand("select * from customerinfo", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        RemoveCustomer.DataSource = reader;
                        RemoveCustomer.DataValueField = "accountid";
                        RemoveCustomer.DataTextField = "accountid";
                        RemoveCustomer.DataBind();
                    }
                }
            }
        }

        protected void RemoveCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //check customer before deleting
                DataTable dtRemove = new DataTable();
                MySqlDataAdapter removeCustomers = new MySqlDataAdapter("select * from customerinfo where accountid = "+RemoveCustomer.Text, conn);
                removeCustomers.Fill(dtRemove);
                if (dtRemove.Rows.Count > 0)
                {
                    CheckCustomer.DataSource = dtRemove;
                    CheckCustomer.DataBind();
                }
                btnDeleteCustomer.Visible = true;
            }
            catch (Exception ex)
            {
                Response.Write("You do not have privileges");
            }
        }

        protected void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            MySqlTransaction tr = null;
            try
            {
                tr = conn.BeginTransaction();

                MySqlCommand cmd = new MySqlCommand();


                cmd.Connection = conn;
                cmd.Transaction = tr;

                cmd.CommandText = "delete from customerinfo where accountid = " + RemoveCustomer.Text;
                cmd.ExecuteNonQuery();

                tr.Commit();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location", "Customers.aspx");
                HttpContext.Current.Response.End();

                Response.Write("transaction completed");
            }
            catch (Exception ex)
            {
                tr.Rollback();
                Response.Write("rolled back");
            }
        }
    }
}