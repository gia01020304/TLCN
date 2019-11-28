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
                model.FBUrlApi = SqlDAL.Instance.GetSetting("FBUrlApi").Value;
                model.MSEndPoint = SqlDAL.Instance.GetSetting("MSEndPoint").Value;
                model.MSKey = SqlDAL.Instance.GetSetting("MSKey").Value;
                model.SMTPPassword = SqlDAL.Instance.GetSetting("SMTPPassword").Value; 
                model.SMTPPort = SqlDAL.Instance.GetSetting("SMTPPort").Value;
                model.SMTPSender = SqlDAL.Instance.GetSetting("SMTPSender").Value;
                model.SMTPSenderName = SqlDAL.Instance.GetSetting("SMTPSenderName").Value;
                model.MailServer = SqlDAL.Instance.GetSetting("MailServer").Value;

                model.MicroAccuracy = int.Parse(SqlDAL.Instance.GetSetting("MicroAccuracy").Value);
                model.MacroAccuracy = int.Parse(SqlDAL.Instance.GetSetting("MacroAccuracy").Value);
                model.LogLoss = int.Parse(SqlDAL.Instance.GetSetting("LogLoss").Value);
                model.LogLossReduction = int.Parse(SqlDAL.Instance.GetSetting("LogLossReduction").Value);
               
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
                if (SqlDAL.Instance.SaveSettingModel(new SettingModel() { Key = "FBUrlApi", Value = model.FBUrlApi }) < 0)
                {
                    rsBool = false;
                }
                if (SqlDAL.Instance.SaveSettingModel(new SettingModel() { Key = "MSEndPoint", Value = model.MSEndPoint }) < 0)
                {
                    rsBool = false;
                }
                if (SqlDAL.Instance.SaveSettingModel(new SettingModel() { Key = "MSKey", Value = model.MSKey }) < 0)
                {
                    rsBool = false;
                }
                if (SqlDAL.Instance.SaveSettingModel(new SettingModel() { Key = "SMTPPassword", Value = model.SMTPPassword }) < 0)
                {
                    rsBool = false;
                }
                if (SqlDAL.Instance.SaveSettingModel(new SettingModel() { Key = "SMTPPort", Value = model.SMTPPort }) < 0)
                {
                    rsBool = false;
                }
                if (SqlDAL.Instance.SaveSettingModel(new SettingModel() { Key = "SMTPSender", Value = model.SMTPSender }) < 0)
                {
                    rsBool = false;
                }
                if (SqlDAL.Instance.SaveSettingModel(new SettingModel() { Key = "SMTPSenderName", Value = model.SMTPSenderName }) < 0)
                {
                    rsBool = false;
                }
                if (SqlDAL.Instance.SaveSettingModel(new SettingModel() { Key = "MailServer", Value = model.MailServer }) < 0)
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
