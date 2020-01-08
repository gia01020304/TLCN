using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model
{
    public class ContactMessageModel
    {

        public long ContactMessageID { get; set; }

        public long LinkScopeID { get; set; }

        public string ReceiverID { get; set; }

        public string ConversationID { get; set; }


        public string ReceiverName { get; set; }


        public string SenderID { get; set; }


        public string SenderName { get; set; }


        public string MessageUID { get; set; }


        public string KeyPairData { get; set; }


        public string ParentID { get; set; }


        public string Title { get; set; }


        public string Body { get; set; }


        public string Status { get; set; }


        public string PhoneNumber { get; set; }


        public string DateReceived { get; set; }


        public string DateModified { get; set; }


        public bool Deleted { get; set; }


        public char Direction { get; set; }


        public string AgentID { get; set; }

        public string MailCC { get; set; }

        public string MailTo { get; set; }

        public string PostUrl { get; set; }


        public ContactMessageModel RelatedMessages { get; set; }

        //2019/06/20 gnguyen start add

        public string SurveyOfferTime { get; set; }
        //2019/06/20 gnguyen end add
        public string ContactType { get; set; }

    }
}
