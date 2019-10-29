using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Main.Model.Facebook
{
    public class From
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Post
    {
        public string status_type { get; set; }
        public bool is_published { get; set; }
        public DateTime updated_time { get; set; }
        public string permalink_url { get; set; }
        public string promotion_status { get; set; }
        public string id { get; set; }
    }

    public class Value
    {
        public From from { get; set; }
        public Post post { get; set; }
        public string message { get; set; }
        public string post_id { get; set; }
        public string comment_id { get; set; }
        public int created_time { get; set; }
        public string item { get; set; }
        public string parent_id { get; set; }
        public string verb { get; set; }
    }

    public class Change
    {
        public Value value { get; set; }
        public string field { get; set; }
    }

    public class Entry
    {
        [Required]
        public string id { get; set; }
        public int time { get; set; }
        public List<Change> changes { get; set; }
    }

    public class WebhookFB
    {
        public List<Entry> entry { get; set; }
        public string @object { get; set; }
        public CommetType CommetType
        {
            get
            {
                CommetType temp = CommetType.User;
                var mCheck = entry.Where(x => x.changes
                                              .Where(y => y.value.item == "comment" &&
                                                     y.value.verb == "add" &&
                                                     y.value.from.id == x.id
                                               )
                                              .FirstOrDefault() != null
                                       ).FirstOrDefault();
                if (mCheck != null)
                {
                    temp = CommetType.Page;
                }
                return temp;
            }
        }
    }
}
