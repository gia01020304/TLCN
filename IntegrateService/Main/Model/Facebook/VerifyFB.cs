using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Main.Model
{
    public class VerifyFB
    {
        [Required]
        public string mode { get; set; }
        [Required]
        public string challenge { get; set; }
        [Required]
        public string verify_token { get; set; }
    }
}
