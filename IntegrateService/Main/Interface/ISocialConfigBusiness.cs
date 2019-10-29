using Main.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main
{
    public interface ISocialConfigBusiness
    {
        /// <summary>
        /// Get Social config from DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        SocialConfig GetSocialConfig(FilterSocialConfig model);

        /// <summary>
        /// Get list social config
        /// </summary>
        /// <returns></returns>
        List<SocialConfig> GetListSocialConfig(FilterSocialConfig filter);
        /// <summary>
        /// Add social config
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddSocialConfigure(SocialConfig model);
        /// <summary>
        /// Edit social config
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditSocialConfigure(SocialConfig model);
        /// <summary>
        /// Del social configure
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool DelSocialConfigure(SocialConfig model);
    }
}
