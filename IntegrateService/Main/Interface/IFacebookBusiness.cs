using Main.Model;
using Main.Model.Facebook;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main
{
    public interface IFacebookBusiness
    {
        FBPageInfo GetPageId(FBPageAccessToken fBPageAccessToken);
        FBPageAccessToken IncrementExpiredToken(SocialConfig socialConfig, string currentToken);
        bool ReplyComment(ReplyComment model);
        FBTokenInfo CheckStatusToken(string token);
        FBSubscribedInfo GetSubscribedInfo(FanpageConfig model);
        bool ChangeChangeSubscribed(FanpageConfig model, string field);
    }
}
