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
    public class FanpageConfigBusiness : IFanpageConfigBusiness
    {
        private readonly FanpageConfigRepository repository;
        public FanpageConfigBusiness()
        {
            repository = new FanpageConfigRepository();
        }

        public bool AddFanPageConfigure(FanpageConfig model)
        {
            bool rsBool = false;
            try
            {
                model.Active = true;
                
                if (repository.SaveFanPageConfigure(model) > 0)
                {
                    rsBool = true;
                }
            }
            catch (Exception ex)
            {

                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }

        public bool DelFanPageConfigure(FanpageConfig model)
        {
            bool rsBool = false;
            try
            {
                model.Deleted = true;
                if (repository.SaveFanPageConfigure(model) > 0)
                {
                    rsBool = true;
                }
            }
            catch (Exception ex)
            {

                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }

        public bool DelFanpageOfAgent(FanpageConfigFilter filter)
        {
            bool rsBool = false;
            try
            {
                if (repository.DeleteFanPageOfAgent(filter) > 0)
                {
                    rsBool = true;
                }
            }
            catch (Exception ex)
            {

                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }

        public bool EditFanPageConfigure(FanpageConfig model)
        {
            bool rsBool = false;
            try
            {
                if (repository.SaveFanPageConfigure(model) > 0)
                {
                    rsBool = true;
                }
            }
            catch (Exception ex)
            {

                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }

        public FanpageConfig GetFanpageConfig(FanpageConfigFilter filter)
        {
            FanpageConfig model = null;
            try
            {
                model = repository.GetFanpageConfig(filter).To<FanpageConfig>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }

        public List<FanpageConfig> GetListFanPageConfigure(FanpageConfigFilter filter)
        {
            List<FanpageConfig> models = null;
            try
            {
                models = repository.GetFanpageConfig(filter).To<FanpageConfig>();
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return models;
        }
    }
}
