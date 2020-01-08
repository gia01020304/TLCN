/********************************************************************
	created:	17:12:2019
	filename: 	IWebhooksBusiness	
	author:		Gia Nguyen

*********************************************************************/
using Main.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Interface
{
    public interface IWebhooksBusiness
    {
        bool AddContactMessage(ContactMessageModel model);
        bool UpdateContactMessage(string msgUID, ContactMessageModel model);
        bool AddConversation(ConversationModel conversation);

        ContactConfigModel GetContactConfigByPageId(string pageId);
        ContactMessageModel GetContactMessagesByConversationID(string conversationID);
    }
}
