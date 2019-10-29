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
