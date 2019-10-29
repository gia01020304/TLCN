using General;
using Main;
using Main.Model;
using Main.Model.Facebook;
using System;
using System.Collections.Generic;
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
                if (model?.SocialConfig != null && !string.IsNullOrEmpty(model.SocialConfig.Token))
                {
                    var url = $"{FBUrlApi}/{model.CommentId}/comments";
                    var rs = HttpExtension.Instance.InvokePost<FBResponsed, FBPostComment>(url, new FBPostComment
                    {
                        access_token = model.SocialConfig.Token,
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
    }
}
