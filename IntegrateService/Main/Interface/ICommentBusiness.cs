using Main.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main
{
    public interface ICommentBusiness
    {
        /// <summary>
        /// Update comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateComment(Comment model);
        /// <summary>
        /// Get comment negative for send mail
        /// </summary>
        /// <returns></returns>
        List<Comment> GetCommentNegative();
        /// <summary>
        /// Add comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddComment(Comment model);
        /// <summary>
        /// Upadte comment for parent comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateReplyCommet(Comment model);
        /// <summary>
        /// Get comment by comment id
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Comment GetCommentByCommentId(CommentFilter commentFilter);
    }
}
