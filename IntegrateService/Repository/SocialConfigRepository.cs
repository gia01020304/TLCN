using General;
using Main.Model;
using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    public class SocialConfigRepository : SqlDAL
    {
        public int SaveSocialConfig(SocialConfig model)
        {
            IDataParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@AppType",model.AppType),
                new SqlParameter("@AppSecret",model.AppSecret),
                new SqlParameter("@AppId",model.AppId),
                new SqlParameter("@Token",model.Token),
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@Deleted",model.Deleted)
            };
            return ExecuteNonQuery("usp_SaveSocialConfig", parameters);
        }
        public DataTable GetSocialConfig(FilterSocialConfig model)
        {
            IDataParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@AppType",model.AppType),
                new SqlParameter("@AppSecret",model.AppSecret),
                new SqlParameter("@AppId",model.AppId),
                new SqlParameter("Deleted",model.Deleted),
                new SqlParameter("@Id",model.Id),
            };
            return ExecuteQuery("usp_GetSocialConfig", parameters);
        }
    }
}
