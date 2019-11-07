using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommentAnalysis;
using General;
using General.Extension;
using Main;
using Main.Model;
using Main.Model.Facebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MonitoringSocialNetworkWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacebookController : ControllerBase
    {
        #region Business
        private readonly ISocialConfigBusiness socialBusiness;
        private readonly ICommentBusiness commentBusiness;
        private readonly IFanpageConfigBusiness fanpageConfigBusiness;
        private readonly IFacebookBusiness facebookBusiness;
        private readonly IDatasetBusiness datasetBusiness;
        #endregion
        public FacebookController(ISocialConfigBusiness socialBusiness,
                                  ICommentBusiness commentBusiness,
                                  IFanpageConfigBusiness fanpageConfigBusiness,
                                  IFacebookBusiness facebookBusiness,
                                  IDatasetBusiness datasetBusiness
                                )
        {
            this.socialBusiness = socialBusiness;
            this.commentBusiness = commentBusiness;
            this.fanpageConfigBusiness = fanpageConfigBusiness;
            this.facebookBusiness = facebookBusiness;
            this.datasetBusiness = datasetBusiness;
        }
        /// <summary>
        /// Api for verify linked to the application
        /// </summary>
        /// <param name="hub"></param>
        /// <returns></returns>
        public IActionResult Get([FromQuery] VerifyFB hub)
        {
            IActionResult result = StatusCode(400);
            try
            {
                if (ModelState.IsValid && hub != null)
                {
                    if (hub.mode == "subscribe" && hub.verify_token == SqlDAL.Instance.GetSetting("FBToken").Value)
                    {
                        result = Content(hub.challenge);
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return result;
        }

        // POST: api/Facebook
        [HttpPost]
        public void Post(WebhookFB model)
        {
            try
            {
                if (model != null && model.entry.Count > 0)
                {
                    switch (model.CommetType)
                    {
                        case CommetType.User:
                            ProccessCommentFromUser(model);
                            break;
                        case CommetType.Page:
                            ProcessCommentFromPage(model);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
        }
        private void ProccessCommentFromUser(WebhookFB model)
        {
            try
            {
                var temp = model.entry.FirstOrDefault().changes.FirstOrDefault();
                if (temp != null)
                {
                    var comment = new Comment()
                    {
                        Action = temp.value.verb,
                        Message = temp.value.message,
                        PageId = model.entry[0].id,
                        PostId = temp.value.post_id,
                        CommentId = temp.value.comment_id,
                        FromId = temp.value.from.id,
                        ParentId = temp.value.parent_id
                    };
                    var fanpageConfig = fanpageConfigBusiness.GetFanpageConfig(new FanpageConfigFilter()
                    {
                        PageId = comment.PageId
                    });
                    var socialConfigByPageId = socialBusiness.GetSocialConfig(new FilterSocialConfig()
                    {
                        Id = fanpageConfig.SocialConfigId
                    });
                    var rsAnalysis = MicrosoftTextAnalytics.Instance.TextAnalysisAsync(comment.Message).Result;
                    if (rsAnalysis != null)
                    {
                        comment.Score = rsAnalysis.Score;
                    }
                    var replyComment = MLSimilarCommentAnalysis.Instance.GetReplyComment(comment.Message);
                    if (string.IsNullOrEmpty(replyComment))
                    {
                        replyComment = fanpageConfig.GetCommentConfig(comment.Score.Value);
                        comment.IsTrain = true;
                    }

                    comment.AgentId = fanpageConfig.AgentId;
                    comment.IsNegative = fanpageConfig.IsNegative(comment.Score.Value, "negative");
                    commentBusiness.AddComment(comment);
                    if (socialConfigByPageId != null)
                    {
                        facebookBusiness.ReplyComment(new ReplyComment()
                        {
                            Comment = replyComment,
                            CommentId = comment.CommentId,
                            PageId = comment.PageId,
                            SocialConfig = socialConfigByPageId
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
        }
        private void ProcessCommentFromPage(WebhookFB model)
        {
            try
            {
                var temp = model.entry.FirstOrDefault().changes.FirstOrDefault();
                if (temp != null)
                {
                    var comment = new Comment()
                    {
                        Action = temp.value.verb,
                        Message = temp.value.message,
                        PageId = model.entry[0].id,
                        PostId = temp.value.post_id,
                        CommentId = temp.value.comment_id,
                        FromId = temp.value.from.id,
                        ParentId = temp.value.parent_id
                    };
                    if (!string.IsNullOrEmpty(comment.Message))
                    {
                        var commentOfUser = commentBusiness.GetCommentByCommentId(new CommentFilter()
                        {
                            CommentId = comment.ParentId
                        });
                        if (commentOfUser != null && commentOfUser.IsTrain)
                        {
                            this.datasetBusiness.AddDataset(new Dataset()
                            {
                                Comment = commentOfUser.Message,
                                ReplyComment = comment.Message
                            });
                            MLSimilarCommentAnalysis.Instance.TrainDataSet();
                        }
                        commentBusiness.AddComment(comment);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
        }
    }
}
