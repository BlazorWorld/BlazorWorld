using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWorld.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string InvitiationCode { get; set; }
        [PersonalData]
        public int Reputation { get; set; }
        [PersonalData]
        public int Coins { get; set; }
        [PersonalData]
        public string AvatarHash { get; set; }
        [PersonalData]
        public string CustomField1 { get; set; }
        [PersonalData]
        public string CustomField2 { get; set; }
        [PersonalData]
        public string CustomField3 { get; set; }
        [PersonalData]
        public string CustomField4 { get; set; }
        [PersonalData]
        public string CustomField5 { get; set; }
        [PersonalData]
        public string CustomField6 { get; set; }
        [PersonalData]
        public string CustomField7 { get; set; }
        [PersonalData]
        public string CustomField8 { get; set; }
        [PersonalData]
        public string CustomField9 { get; set; }
        [PersonalData]
        public string CustomField10 { get; set; }
        [PersonalData]
        public string CustomField11 { get; set; }
        [PersonalData]
        public string CustomField12 { get; set; }
        [PersonalData]
        public string CustomField13 { get; set; }
        [PersonalData]
        public string CustomField14 { get; set; }
        [PersonalData]
        public string CustomField15 { get; set; }
        [PersonalData]
        public string CustomField16 { get; set; }
        [PersonalData]
        public string CustomField17 { get; set; }
        [PersonalData]
        public string CustomField18 { get; set; }
        [PersonalData]
        public string CustomField19 { get; set; }
        [PersonalData]
        public string CustomField20 { get; set; }
    }
}
