using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Main.Model
{
    public class SystemConfigure
    {
        [Required]
        [Display(Name = "Facebook Token")]
        public string FBToken { get; set; }
        [Required]
        [Display(Name = "Facebook URL API")]
        public string FBUrlApi { get; set; }
        [Required]
        [Display(Name = "Microsoft End Point")]
        public string MSEndPoint { get; set; }
        [Required]
        [Display(Name = "Microsoft Key")]
        public string MSKey { get; set; }
        [Display(Name = "Sender Name")]
        public string SMTPSenderName { get; set; }
        [Display(Name = "User Name")]
        public string SMTPSender { get; set; }
        [Display(Name = "Password")]
        public string SMTPPassword { get; set; }
        [Display(Name = "Port")]
        [RegularExpression(@"^[0-9]+", ErrorMessage = "Only number")]
        public string SMTPPort { get; set; }
        [Display(Name = "Mail Server")]
        public string MailServer { get; set; }
        [Display(Name = "Micro Accuracy")]
        public int MicroAccuracy { get; set; }
        [Display(Name = "Macro Accuracy")]
        public int MacroAccuracy { get; set; }
        [Display(Name = "Log Loss")]
        public int LogLoss { get; set; }
        [Display(Name = "Log Loss Reduction")]
        public int LogLossReduction { get; set; }
        [Display(Name = "End Point")]
        public string EndPoint { get; set; }
    }
}
