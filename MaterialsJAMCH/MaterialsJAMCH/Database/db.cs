using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Add usings to DB
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
//Add usings to Models
using MaterialsJAMCH.Models;

namespace MaterialsJAMCH.Database
{
    public class db
    {
        //Relates the connection string created in the webconfig
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        //Method for add a record in Buildings
        public void CreateBuilding(Buildings bld)
        {
            
            SqlCommand cmd = new SqlCommand("SPCreateBuilding", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Building", bld.Building);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Method for add a record in Customers
        public void CreateCustomer(Customers cus)
        {
            SqlCommand cmd = new SqlCommand("SPCreateCustomer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Customer", cus.Customer);
            cmd.Parameters.AddWithValue("@Prefix", cus.Prefix);
            cmd.Parameters.AddWithValue("@FKBuilding", cus.FKBuilding);
            cmd.Parameters.AddWithValue("@Available", cus.Available);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Method for add a record in PartNumbers
        public void CreatePartNumber(PartNumbers prt)
        {
            SqlCommand cmd = new SqlCommand("SPCreatePartNumber", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PartNumber", prt.PartNumber);
            cmd.Parameters.AddWithValue("@FKCustomer", prt.FKCustomer);
            cmd.Parameters.AddWithValue("@Available", prt.Available);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        //Create the conexion to show records of the PartNumbers in a table
        public DataSet GetPartNumbers(string Part, string Customer)
        {
            SqlCommand cmd = new SqlCommand("SPGetPartNumbers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (Part==null)
            {
                cmd.Parameters.AddWithValue("@PartNumber", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@PartNumber", Part);
            }
            if (Customer==null)
            {
                cmd.Parameters.AddWithValue("@Customer", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Customer", Customer);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

    }
}
