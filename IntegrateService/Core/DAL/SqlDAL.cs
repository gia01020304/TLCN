using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace General
{
    public class SqlDAL : IBaseDAL
    {
        private static SqlDAL instance;
        private readonly string connectionString;

        public static SqlDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SqlDAL();
                }
                return instance;
            }
        }

        public SqlDAL()
        {
            connectionString = Configuration.ConnectString;
        }
        public int ExecuteNonQuery(string storeName, IDataParameter[] parameters = null)
        {
            using (SqlConnection _conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = CreateCommand(_conn, storeName, parameters))
            {
                cmd.CommandTimeout = 0;
                _conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public DataTable ExecuteQuery(string storeName, IDataParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = CreateCommand(conn, storeName, parameters))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandTimeout = 0;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                return new DataTable();
            }
        }
        public SettingModel GetSetting(string key)
        {
            SettingModel model = null;
            try
            {
                IDataParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Key",key)
                };
                model = ExecuteQuery("usp_GetSetting", parameters).To<SettingModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(ex);
            }
            if (model == null)
            {
                model = new SettingModel()
                {
                    Key = key,
                    Value = string.Empty
                };
            }
            return model;
        }
        public int SaveSettingModel(SettingModel model)
        {
            int rs = 0;
            try
            {
                IDataParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Key",model.Key),
                     new SqlParameter("@Value",model.Value)
                };
                rs = ExecuteNonQuery("usp_AddSetting",parameters);
            }
            catch (Exception ex)
            {
                rs = -1;
                CoreLogger.Instance.Error(ex);
            }
            return rs;
        }
        public DataSet ExecuteQueryDataSet(string storeName, IDataParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = CreateCommand(conn, storeName, parameters))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandTimeout = 0;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }
        private SqlCommand CreateCommand(SqlConnection conn, string sproc, IDataParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(sproc, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            return cmd;
        }
    }
}
