using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Main.Model
{
    public class SystemConfigure
    {
        [Display(Name = "Facebook Token")]
        //[Required]
        public string FBToken { get; set; }
        [Display(Name = "Facebook Url Api")]
        //[Required]
        public string FBUrlApi { get; set; }
        [Display(Name = "Microsoft End Point")]
        //[Required]
        public string MSEndPoint { get; set; }
        [Display(Name = "Microsoft Key")]
        //[Required]
        public string MSKey { get; set; }
    }
}
