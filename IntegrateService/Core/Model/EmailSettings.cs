using System;
using System.Collections.Generic;
using System.Text;

namespace General
{
    public class EmailSettings
    {
        public string SMTPMailServer { get; set; }
        public int SMTPMailPort { get; set; }
        public string SMTPSenderName { get; set; }
        public string SMTPSender { get; set; }
        public string SMTPPassword { get; set; }
    }
}
