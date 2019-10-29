using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace General
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private int maxFileSize;
        private string maxFileDisplay;
        public MaxFileSizeAttribute(int maxFileSize, string maxFileDisplay)
        {
            this.maxFileSize = maxFileSize;
            this.maxFileDisplay = maxFileDisplay;
        }
        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                return true;
            }
            return file.Length <= maxFileSize;
        }
        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(maxFileDisplay);
        }
    }
}
