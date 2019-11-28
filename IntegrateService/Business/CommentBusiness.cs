using General;
using Main;
using Main.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class CommentBusiness : ICommentBusiness
    {
        private readonly CommentRepository repository;
        public CommentBusiness()
        {
            repository = new CommentRepository();
        }
        public bool AddComment(Comment model)
        {
            bool rs = false;
            try
            {
                if (repository.AddComment(model) > 0)
                {
                    rs = true;
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rs;
        }

        public Comment GetCommentByCommentId(CommentFilter commentFilter)
        {
            Comment comment = null;
            try
            {
                comment = repository.GetCommentByCommentId(commentFilter).To<Comment>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return comment;
        }

        public List<Comment> GetCommentNegative()
        {
            List<Comment> models = null;
            try
            {
                models = repository.GetCommentNegative().To<Comment>();
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return models;
        }

        public List<PostInfo> GetPostInfo(PostInfoFilter filter)
        {
            List<PostInfo> models = null;
            try
            {
                models = repository.GetPostInfo(filter).To<PostInfo>();
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return models;
        }

        public bool UpdateComment(Comment model)
        {
            bool rs = false;
            try
            {
                if (repository.UpdateComment(model) > 0)
                {
                    rs = true;
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rs;
        }

        public bool UpdateReplyCommet(Comment model)
        {
            throw new NotImplementedException();
        }
    }
}
