using General;
using Main;
using Main.Model;
using Main.Model.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class FacebookBusiness : IFacebookBusiness
    {
        private readonly string FBUrlApi;
        public FacebookBusiness()
        {
            FBUrlApi = SqlDAL.Instance.GetSetting("FBUrlApi").Value;
        }
        private FBPageAccessToken GetFBPageAccessToken(SocialConfig socialConfig, string pageId)
        {
            FBPageAccessToken model = null;
            try
            {
                var fbAccess = GetFBAccessToken(socialConfig);
                if (fbAccess != null)
                {
                    var url = $"{FBUrlApi}/{pageId}?fields=access_token&access_token={fbAccess.access_token}";
                    model = HttpExtension.Instance.InvokeGet<FBPageAccessToken>(url).Result;
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }
        private FBAccessToken GetFBAccessToken(SocialConfig socialConfig)
        {
            FBAccessToken model = null;
            try
            {
                var url = $"{FBUrlApi}/oauth/access_token?client_id={socialConfig.AppId}&client_secret={socialConfig.AppSecret}&grant_type=fb_exchange_token";
                model = HttpExtension.Instance.InvokeGet<FBAccessToken>(url).Result;
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }

        public bool ReplyComment(ReplyComment model)
        {
            bool rsBool = false;
            try
            {
                if (!string.IsNullOrEmpty(model.PageAccessToken))
                {
                    var url = $"{FBUrlApi}/{model.CommentId}/comments";
                    var rs = HttpExtension.Instance.InvokePost<FBResponsed, FBPostComment>(url, new FBPostComment
                    {
                        access_token = model.PageAccessToken,
                        message = model.Comment
                    }).Result;
                    if (rs != null)
                    {
                        rsBool = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }

        public FBPageAccessToken IncrementExpiredToken(SocialConfig socialConfig, string currentToken)
        {
            FBPageAccessToken model = null;
            try
            {
                var url = string.Format("{0}/oauth/access_token?field=expires_in&grant_type=fb_exchange_token&client_id={1}&client_secret={2}&fb_exchange_token={3}"
                       , SqlDAL.Instance.GetSetting("FBUrlApi").Value
                       , socialConfig.AppId, socialConfig.AppSecret, currentToken);
                model = HttpExtension.Instance.InvokeGet<FBPageAccessToken>(url).Result;
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }

        public FBPageInfo GetPageId(FBPageAccessToken fBPageAccessToken)
        {
            FBPageInfo model = null;
            try
            {
                var url = string.Format("{0}/me/accounts?access_token={1}"
                       , SqlDAL.Instance.GetSetting("FBUrlApi").Value
                       , fBPageAccessToken.access_token);
                var temp = HttpExtension.Instance.InvokeGet<ListFBPageInfo>(url).Result;
                if (temp != null && temp.data?.Count > 0)
                {
                    model = temp.data.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }

        public FBTokenInfo CheckStatusToken(string token)
        {
            FBTokenInfo model = null;
            try
            {
                var url = string.Format("{0}/debug_token?input_token={1}&access_token={2}"
                       , SqlDAL.Instance.GetSetting("FBUrlApi").Value
                       , token, token);
                model = HttpExtension.Instance.InvokeGet<FBTokenInfo>(url).Result;

            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }

        public FBSubscribedInfo GetSubscribedInfo(FanpageConfig model)
        {
            FBSubscribedInfo modelReturn = null;
            try
            {
                var url = string.Format("{0}/{1}/subscribed_apps?access_token={2}"
                       , SqlDAL.Instance.GetSetting("FBUrlApi").Value
                       , model.PageId, model.PageAccessToken);
                var temp = HttpExtension.Instance.InvokeGet<FBSubscribedInfos>(url).Result;
                if (temp != null && temp.data?.Count > 0)
                {
                    modelReturn = temp.data.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return modelReturn;
        }

        public bool ChangeChangeSubscribed(FanpageConfig model, string field)
        {
            bool rs = false;
            try
            {
                FBResponsed temp = null;
                var url = string.Format("{0}/{1}/subscribed_apps"
                       , SqlDAL.Instance.GetSetting("FBUrlApi").Value
                       , model.PageId);
                if (!string.IsNullOrEmpty(field))
                {
                    temp = HttpExtension.Instance.InvokePost<FBResponsed, FBPostField>(url, new FBPostField
                    {
                        access_token = model.PageAccessToken,
                        subscribed_fields = field
                    }).Result;
                }
                else
                {
                    url += "?access_token=" + model.PageAccessToken;
                    temp = HttpExtension.Instance.InvokeDelete<FBResponsed>(url).Result;
                }
                if (temp != null)
                {
                    rs = temp.success;
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rs;
        }



    }
}
