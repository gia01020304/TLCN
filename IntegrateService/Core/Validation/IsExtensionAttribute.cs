using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace General
{
    public class IsExtensionAttribute : ValidationAttribute
    {
        private string[] extensions;
        public IsExtensionAttribute(string[] extensions)
        {
            this.extensions = extensions;
        }
        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                return true;
            }
            return extensions.Contains((new FileInfo(file.FileName)).Extension);
        }
        public override string FormatErrorMessage(string name)
        {
            string listExt = "";
            extensions.ToList().ForEach((item) =>
            {
                if (listExt == "")
                {
                    listExt = string.Concat(listExt, item);
                }
                else
                {
                    listExt = string.Concat(listExt, ",", item);
                }
            });
            return base.FormatErrorMessage(listExt);
        }
    }
}
