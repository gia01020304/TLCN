using General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Main.Model
{
    public class FanpageConfig
    {
        public int? Id { get; set; }
        [Display(Name = "Page Id")]
        public string PageId { get; set; }
        public bool Active { get; set; }
        public string PageTitle { get; set; }
        public string CommentConfig { get; set; }
        public string AgentId { get; set; }
        [Display(Name = "Agent")]
        public string AgentName { get; set; }
        [Display(Name = "Social Config")]
        public int SocialConfigId { get; set; }
        [Required]
        [Display(Name = "Page Access Token")]
        public string PageAccessToken { get; set; }
        public bool IsSubscribe { get; set; }
        public bool Deleted { get; set; }
        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }
        /// <summary>
        /// Analysis string to List AutoComment
        /// </summary>
        /// <returns></returns>
        private List<CommentConfig> GetCommentConfig()
        {
            List<CommentConfig> temp = null;
            try
            {
                if (!string.IsNullOrEmpty(CommentConfig))
                {
                    temp = JsonConvert.DeserializeObject<List<CommentConfig>>(CommentConfig);
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return temp;
        }
        public string GetCommentConfig(int score, string key = "default")
        {
            string commentConfig = string.Empty;
            try
            {
                var temp = GetCommentConfig();
                var model = temp.Where(x => x.From <= score && score <= x.To).FirstOrDefault();
                if (model == null)
                {
                    model = temp.Where(x => x.Key == key).FirstOrDefault();
                }
                commentConfig = model.Value;
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return commentConfig;
        }
        public bool IsNegative(int score, string key = "negative")
        {
            bool rsBool = false;
            try
            {
                var temp = GetCommentConfig();
                var model = temp.Where(x => x.From <= score && score <= x.To && x.Key.ToLower() == key.ToLower()).FirstOrDefault();
                if (model != null)
                {
                    rsBool = true;
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return rsBool;
        }
    }
    public class FanpageConfigFilter
    {
        public int? Id { get; set; }
        public int? SocialConfigId { get; set; }
        public string PageId { get; set; }
        public bool? Active { get; set; } = null;
        public bool? Deleted { get; set; }
        public string AgentId { get; set; }
        public string ConnectionId { get; set; }
    }
}
