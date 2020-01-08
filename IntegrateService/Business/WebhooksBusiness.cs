using General;
using Main.Interface;
using Main.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class WebhooksBusiness : IWebhooksBusiness
    {
        private readonly string apiWebhooks;
        public WebhooksBusiness()
        {
            apiWebhooks = SqlDAL.Instance.GetSetting("EndPoint").Value;
        }
        public bool AddContactMessage(ContactMessageModel model)
        {
            bool rsBool = false;
            try
            {
                var rs = HttpExtension.Instance.InvokePost<ResultResponse, ContactMessageModel>(apiWebhooks + "/contactmessages/", model).Result;
                if (rs != null)
                {
                    rsBool = rs.result;
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }

        public bool AddConversation(ConversationModel conversation)
        {
            bool rsBool = false;
            try
            {
                var rs = HttpExtension.Instance.InvokePost<ConversationResponse, ConversationModel>(apiWebhooks + "/contactmessages/conversation", conversation).Result;
                if (rs != null && rs.result.Count > 0)
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

        /// <summary>
        /// Get contactconfig for page
        /// </summary>
        /// <param name="pageId"></param>
        public ContactConfigModel GetContactConfigByPageId(string pageId)
        {
            ContactConfigModel model = null;
            try
            {
                var rs = HttpExtension.Instance.InvokeGet<ContactConfigResponse>(apiWebhooks + "/contacts/" + pageId).Result;
                if (rs != null)
                {
                    model = rs.result;
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }

        public ContactMessageModel GetContactMessagesByConversationID(string conversationID)
        {
            ContactMessageModel model = null;
            try
            {
                var rs = HttpExtension.Instance.InvokeGet<ContactMessagesResponse>(apiWebhooks + "/contactmessages/conversations/" + conversationID).Result;
                if (rs != null)
                {
                    model = rs.result.FirstOrDefault();
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }

        public bool UpdateContactMessage(string msgUID, ContactMessageModel model)
        {
            bool rsBool = false;
            try
            {
                var rs = HttpExtension.Instance.InvokePut<ResultResponse, ContactMessageModel>(apiWebhooks + "/contactmessages/" + msgUID, model).Result;
                if (rs != null)
                {
                    rsBool = rs.result;
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }
    }
}
