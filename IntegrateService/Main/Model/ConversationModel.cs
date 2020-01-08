using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model
{
    public class ConversationModel
    {
       
        public string ContactMessageUID { get; set; } = string.Empty;

       
        public string MessageId { get; set; } = string.Empty;

       
        public string ReceiverID { get; set; } = string.Empty;

       
        public string ReceiverName { get; set; } = string.Empty;

       
        public string SenderID { get; set; } = string.Empty;

       
        public string SenderName { get; set; } = string.Empty;

       
        public string Seq { get; set; } = string.Empty;

       
        public string Content { get; set; } = string.Empty;

       
        public string Direction { get; set; } = string.Empty;

       
        public string Status { get; set; } = string.Empty;

       
        public string AgentID { get; set; } = string.Empty;

       
        public string Timestamp { get; set; } = string.Empty;

       
        public string DateReceived { get; set; } = string.Empty;

       
        public bool Deleted { get; set; } = false;
        // 2018-04-19 Khoan add start

       
        public string PhoneNumber { get; set; }
        // 2018-04-19 Khoan add end

        // 2018-06-21 Khoan add start
        /// <summary>
        /// Chatbot ask user reason (problem definition) for contacting and store information into this property
        /// </summary>
       
        public string ReasonContact { get; set; }

        /// <summary>
        /// VDN user want to connect, chatbot asks user department user wants to connect and determine VDN from user response
        /// </summary>
       
        public string VDN { get; set; }
        // 2018-06-21 Khoan add end

       
        public long LinkScopeID { get; set; }

    }
}
