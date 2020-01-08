using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model
{
    public class ContactConfigModel
    {

        public int ContactConfigID { get; set; }

        public string MediaType { get; set; }

        public string ContactType { get; set; }

        public string ContactID { get; set; }

        public string ContactName { get; set; }

        public int Skill { get; set; }

        public string SkillExtension { get; set; }

        public string VDN { get; set; }

        public string PhoneNumber { get; set; }

        public string DateReceived { get; set; }

        public string DateModified { get; set; }

        public string ContactConfigData { get; set; }

        public bool Deleted { get; set; }
    }
}
