﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model.Facebook
{
    public class FBPostComment
    {
        public string message { get; set; }
        public string access_token { get; set; }
    }
    public class FBPostField
    {
        public string access_token { get; set; }
        public string subscribed_fields { get; set; }
    }
}
