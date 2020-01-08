using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Main.Model
{
    public class SocialConfig
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "App Id")]
        public string AppId { get; set; }
        [Required]
        [Display(Name = "App Secret")]
        public string AppSecret { get; set; }
     
        [Required]
        [Display(Name = "App Type")]
        public AppType AppType { get; set; }
        [Display(Name = "App Name")]
        public string AppName { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DateModified { get; set; }
    }
    public class FilterSocialConfig
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public AppType AppType { get; set; }
        public int? Id { get; set; }
        public bool Deleted { get; set; }
    }
}
