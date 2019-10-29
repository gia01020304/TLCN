using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Main.Model.Facebook
{
    public class FBAccessToken
    {
        [Required]
        public string access_token { get; set; }
        public string token_type { get; set; }
    }
}
