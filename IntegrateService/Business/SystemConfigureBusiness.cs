using General;
using Main.Interface;
using Main.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class SystemConfigureBusiness : ISystemConfigureBusiness
    {
        public SystemConfigure GetSystemConfigure()
        {
            SystemConfigure model = null;
            try
            {
                model = new SystemConfigure();
                model.FBToken = SqlDAL.Instance.GetSetting("FBToken").Value;
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }

        public bool SaveConfigure(SystemConfigure model)
        {
            var rsBool = true;
            try
            {
                if (SqlDAL.Instance.SaveSettingModel(new SettingModel() { Key = "FBToken", Value = model.FBToken }) < 0)
                {
                    rsBool = false;
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
