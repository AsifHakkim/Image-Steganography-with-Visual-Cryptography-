using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Cryptic_Watermarking
{
    class DBConnection
    {
        public DataSet ds = new DataSet();
        public SqlDataReader dr;
        public SqlDataReader dr1;
        public SqlDataReader dr2;
        public static string database;


        public SqlConnection con(string db)
        {
            SqlConnection con = new SqlConnection("server="+Program.serverip+";database=Watermarking fam;uid=sa;pwd=yuva");
            con.Open();
            return con;
        }
        public void exec(string str)
        {
            SqlCommand cmd = new SqlCommand(str, con(database));
            cmd.ExecuteNonQuery();
        }
        public int exec1(string str)
        {
            SqlCommand cmd = new SqlCommand(str, con(database));
            return cmd.ExecuteNonQuery();
        }
        public SqlDataReader ret_dr(string str)
        {
            SqlCommand cmd = new SqlCommand(str, con(database));
            return cmd.ExecuteReader();
        }
        public DataSet ret_ds(string str)
        {
            SqlDataAdapter sqlda = new SqlDataAdapter(str, con(database));
            sqlda.Fill(ds);
            return ds;
        }
        public DataSet ret_ds1(string str)
        {
            DataSet ds2 = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(str, con(database));
            sqlda.Fill(ds2);
            return ds2;
        }
    }
}
