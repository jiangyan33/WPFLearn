using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ZhaoXi.Industrial.DAL
{
    public class DataAccess
    {
        public string dbConfig = ConfigurationManager.ConnectionStrings["db_config"].ToString();


        private MySqlConnection conn;

        private MySqlCommand comm;

        private MySqlDataAdapter adapter;

        private MySqlTransaction trans;

        private void Dispose()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }

            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }

            if (trans != null)
            {
                trans.Dispose();
                trans = null;
            }
        }

        private DataTable GetDatas(string sql)
        {
            var dt = new DataTable();

            try
            {
                conn = new MySqlConnection(dbConfig);
                conn.Open();

                adapter = new MySqlDataAdapter(sql, conn);

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }

            return dt;
        }

        public DataTable GetStorageArea()
        {
            var strSql = "select * from storage_area";
            return this.GetDatas(strSql);
        }

        public DataTable GetDevices()
        {
            var strSql = "select * from devices";
            return this.GetDatas(strSql);
        }

        public DataTable GetMonitorValues()
        {
            var strSql = "select * from monitor_values order by d_id,value_id";
            return this.GetDatas(strSql);
        }

    }
}
