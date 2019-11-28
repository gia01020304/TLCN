using General;
using Main.Model;
using Org.BouncyCastle.Pkcs;
using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    public class CommentRepository : SqlDAL
    {
        public int UpdateComment(Comment model)
        {
            IDataParameter[] parameter = new SqlParameter[]
           {
                new SqlParameter("@Message",model.Message),
                new SqlParameter("@PageId",model.PageId),
                new SqlParameter("@PostId",model.PostId),
                new SqlParameter("@ParentId",model.ParentId),
                new SqlParameter("@FromId",model.FromId),
                new SqlParameter("@CommentId",model.CommentId),
                new SqlParameter("@Score",model.Score),
                new SqlParameter("@AgentId",model.AgentId),
                new SqlParameter("@IsNegative",model.IsNegative),
                new SqlParameter("@Lock",model.Lock),
                 new SqlParameter("@IsTrain",model.IsTrain),
                new SqlParameter("@DateSend",model.DateSend),
                new SqlParameter("@Id",model.Id),
           };
            return ExecuteNonQuery("usp_UpdateComment", parameter);
        }
        public DataTable GetCommentNegative()
        {
            return ExecuteQuery("usp_GetCommentNegative");
        }
        public int UpdateReplyComment(Comment model)
        {
            IDataParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@ParentId",model.ParentId),
            };
            return ExecuteNonQuery("usp_UpdateReplyCommet", parameter);
        }
        public int AddComment(Comment model)
        {
            IDataParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@Message",model.Message),
                new SqlParameter("@PageId",model.PageId),
                new SqlParameter("@PostId",model.PostId),
                new SqlParameter("@ParentId",model.ParentId),
                new SqlParameter("@FromId",model.FromId),
                new SqlParameter("@CommentId",model.CommentId),
                new SqlParameter("@Score",model.Score),
                new SqlParameter("@Link",model.Link),
                new SqlParameter("@AgentId",model.AgentId),
                new SqlParameter("@Lock",model.Lock),
                new SqlParameter("@IsTrain",model.IsTrain),
                new SqlParameter("@IsNegative",model.IsNegative),
            };
            return ExecuteNonQuery("usp_AddComment", parameter);
        }
        public DataTable GetPostInfo(PostInfoFilter filter)
        {
            IDataParameter[] parameter = new SqlParameter[]
           {
                new SqlParameter("@PageId",filter.PageId),
           };
            return ExecuteQuery("usp_GetPostInfo", parameter);
        }

        public DataTable GetCommentByCommentId(CommentFilter commentFilter)
        {
            IDataParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@CommentId",commentFilter.CommentId),
            };
            return ExecuteQuery("usp_GetCommentByCommentId", parameter);
        }
    }
}
