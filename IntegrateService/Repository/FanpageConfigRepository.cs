using General;
using Main.Model;
using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    public class FanpageConfigRepository : SqlDAL
    {
        public DataTable GetFanpageConfig(FanpageConfigFilter filter)
        {
            IDataParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",filter.Id),
                new SqlParameter("@PageId",filter.PageId),
                new SqlParameter("@Active",filter.Active),
            };
            return ExecuteQuery("usp_GetFanpageConfig", parameters);
        }

    }
}
