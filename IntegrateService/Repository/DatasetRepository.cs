using General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository
{
    public class DatasetRepository : SqlDAL
    {
        public int AddDataset(Dataset model)
        {
            IDataParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@Comment",model.Comment),
                new SqlParameter("@ReplyComment",model.ReplyComment),
            };
            return ExecuteNonQuery("usp_AddDataset", parameter);
        }

    }
}
