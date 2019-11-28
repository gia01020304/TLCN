using General;
using Main.Model;
using System;
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
                new SqlParameter("@SocialConfigId",filter.SocialConfigId),
                new SqlParameter("@Deleted",filter.Deleted),
            };
            return ExecuteQuery("usp_GetFanpageConfig", parameters);
        }
        public int DeleteFanPageOfAgent(FanpageConfigFilter filter)
        {
            IDataParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@AgentId",filter.AgentId),
           };
            return ExecuteNonQuery("usp_DeleteFanPageOfAgent", parameters);
        }
        public int SaveFanPageConfigure(FanpageConfig model)
        {
            IDataParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@PageId",model.PageId),
                new SqlParameter("@CommentConfig",model.CommentConfig),
                new SqlParameter("@AgentName",model.AgentName),
                new SqlParameter("@AgentId",model.AgentId),
                new SqlParameter("@SocialConfigId",model.SocialConfigId),
                new SqlParameter("@Deleted",model.Deleted),
                new SqlParameter("@PageTitle",model.PageTitle),
                new SqlParameter("@Active",model.Active),
                new SqlParameter("@PageAccessToken",model.PageAccessToken),
                new SqlParameter("@Id",model.Id)
            };
            return ExecuteNonQuery("usp_SaveFanPageConfigure", parameters);
        }
    }
}
