using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace General
{
    public class SettingModel
    {
        private string value;

        [Key]
        public string Key { get; set; }
        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                if (this.value == null)
                {
                    this.value = string.Empty;
                }
            }
        }
    }
}
