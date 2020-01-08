using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommentAnalysis;
using General;
using General.Extension;
using Main;
using Main.Interface;
using Main.Model;
using Main.Model.Facebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MonitoringSocialNetworkWeb.Data;
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
        private readonly IDatasetsBusiness datasetsBusiness;
        private readonly IConfiguration configuration;
        private readonly IWebhooksBusiness webhooksBusiness;
        #endregion
        public FacebookController(ISocialConfigBusiness socialBusiness,
                                  ICommentBusiness commentBusiness,
                                  IFanpageConfigBusiness fanpageConfigBusiness,
                                  IFacebookBusiness facebookBusiness,
                                  IDatasetsBusiness datasetsBusiness,
                                  IConfiguration configuration,
                                  IWebhooksBusiness webhooksBusiness
                                )
        {
            this.socialBusiness = socialBusiness;
            this.commentBusiness = commentBusiness;
            this.fanpageConfigBusiness = fanpageConfigBusiness;
            this.facebookBusiness = facebookBusiness;
            this.datasetsBusiness = datasetsBusiness;
            this.configuration = configuration;
            this.webhooksBusiness = webhooksBusiness;
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
                bool needToSend = false;
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
                        ParentId = temp.value.parent_id,
                        FromName = temp.value.from.name,
                        ReceiverName = model.@object
                    };
                    var commentDuplicate = commentBusiness.GetCommentByCommentId(new CommentFilter() { CommentId = comment.CommentId });
                    if (commentDuplicate != null)
                    {
                        return;
                    }

                    var fanpageConfig = fanpageConfigBusiness.GetFanpageConfig(new FanpageConfigFilter()
                    {
                        PageId = comment.PageId
                    });

                    var rsAnalysis = MicrosoftTextAnalytics.Instance.TextAnalysisAsync(comment.Message).Result;
                    if (rsAnalysis != null)
                    {
                        comment.Score = rsAnalysis.Score;
                    }
                    comment.IsNegative = fanpageConfig.IsNegative(comment.Score.Value, "negative");
                    var replyComment = string.Empty;
                    if (!comment.IsNegative)
                    {
                        replyComment = MLSimilarCommentAnalysis.Instance.GetReplyComment(comment.Message);

                    }
                    else
                    {
                        needToSend = true;
                    }
                    if (string.IsNullOrEmpty(replyComment))
                    {
                        needToSend = true;
                        replyComment = fanpageConfig.GetCommentConfig(comment.Score.Value);
                        comment.IsTrain = true;
                    }
                    var tempCommentId = comment.CommentId.Split("_");
                    comment.Link = model.entry[0].changes[0].value.post.permalink_url + "&comment_id=" + tempCommentId[1];
                    comment.AgentId = fanpageConfig.AgentId;

                    #region Will be remove
                    facebookBusiness.ReplyComment(new ReplyComment()
                    {
                        Comment = replyComment,
                        CommentId = comment.CommentId,
                        PageId = comment.PageId,
                        PageAccessToken = SqlDAL.Instance.GetSetting("DemoToken").Value
                    });
                    #endregion
                    //facebookBusiness.ReplyComment(new ReplyComment()
                    //{
                    //    Comment = replyComment,
                    //    CommentId = comment.CommentId,
                    //    PageId = comment.PageId,
                    //    PageAccessToken = fanpageConfig.PageAccessToken
                    //});

                    //12/16/2019 gnguyen start add
                    if (needToSend)
                    {
                        //Get contact config of page
                        var contactConfig = webhooksBusiness.GetContactConfigByPageId(comment.PageId);
                        if (contactConfig != null)
                        {

                            var rs = webhooksBusiness.AddContactMessage(new ContactMessageModel
                            {
                                PostUrl = comment.Link,
                                Status = "New",
                                Body = comment.Message,
                                SenderName = comment.FromName,
                                SenderID = comment.FromId,
                                ReceiverName = comment.ReceiverName,
                                ReceiverID = comment.PageId,
                                MessageUID = comment.CommentId,
                                ContactType = "EM",
                                ConversationID = comment.CommentId,
                            });
                        }
                    }
                    commentBusiness.AddComment(comment);
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
                        ParentId = temp.value.parent_id,
                        FromName = temp.value.from.name,
                        ReceiverName = model.@object
                    };
                    var tempCommentId = comment.CommentId.Split("_");
                    comment.Link = model.entry[0].changes[0].value.post.permalink_url + "&comment_id=" + tempCommentId[1];
                    if (!string.IsNullOrEmpty(comment.Message))
                    {
                        var commentOfUser = commentBusiness.GetCommentByCommentId(new CommentFilter()
                        {
                            CommentId = comment.ParentId
                        });
                        //12/17/2019 gnguyen start add
                        if (commentOfUser != null)
                        {
                            ConversationModel conversationModel = new ConversationModel
                            {
                                MessageId = Guid.NewGuid().ToString(),
                                Direction = "O",
                                Content = comment.Message,
                                SenderName = comment.FromName,
                                SenderID = comment.FromId,
                                ReceiverName = comment.ReceiverName,
                                ReceiverID = comment.PageId,
                                ContactMessageUID = commentOfUser.CommentId + "_I",
                                Status = "Replied"
                            };

                            webhooksBusiness.AddConversation(conversationModel);
                        }
                        //12/17/2019 gnguyen end add
                        if (commentOfUser != null && commentOfUser.IsTrain)
                        {

                            this.datasetsBusiness.AddDatasets(new Dataset
                            {
                                Comment = commentOfUser.Message,
                                ReplyComment = comment.Message
                            });
                            SqlDAL.Instance.SaveSettingModel(new SettingModel()
                            {
                                Key = "IsTrain",
                                Value = "true"
                            });
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
