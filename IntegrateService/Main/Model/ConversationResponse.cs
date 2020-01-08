using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model
{
    public class ConversationResponse
    {

        public List<ConversationModel> result { get; set; }


        public ContactMessageModel ContactMessage { get; set; }


        public ContactConfigModel ContactConfig { get; set; }


        public ReplyMessageModel ReplyMessage { get; set; }
    }
}
