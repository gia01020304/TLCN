using General;
using Main.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository
{
    public class DatasetsRepository : SqlDAL
    {
        public int AddDatasets(Dataset model)
        {
            IDataParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Comment",model.Comment),
                new SqlParameter("@ReplyComment",model.ReplyComment),
            };
            return ExecuteNonQuery("usp_AddDatasets", parameters);
        }
    }
}
