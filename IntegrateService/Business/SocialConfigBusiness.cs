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
    public class SocialConfigBusiness : ISocialConfigBusiness
    {
        private readonly SocialConfigRepository repository;
        public SocialConfigBusiness()
        {
            repository = new SocialConfigRepository();
        }

        public bool AddSocialConfigure(SocialConfig model)
        {
            var rsBool = false;
            try
            {
                if (repository.SaveSocialConfig(model) > 0)
                {
                    rsBool = true;
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }

        public bool DelSocialConfigure(SocialConfig model)
        {
            var rsBool = false;
            try
            {
                model.Deleted = true;
                if (repository.SaveSocialConfig(model) > 0)
                {
                    rsBool = true;
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }

        public bool EditSocialConfigure(SocialConfig model)
        {
            var rsBool = false;
            try
            {
                if (repository.SaveSocialConfig(model) > 0)
                {
                    rsBool = true;
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }

        public List<SocialConfig> GetListSocialConfig(FilterSocialConfig filter)
        {
            List<SocialConfig> models = null;
            try
            {
                models = repository.GetSocialConfig(filter).To<SocialConfig>();
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return models;
        }

        public SocialConfig GetSocialConfig(FilterSocialConfig model)
        {
            SocialConfig modelReturn = null;
            try
            {
                modelReturn = repository.GetSocialConfig(model).To<SocialConfig>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return modelReturn;
        }
    }
}
