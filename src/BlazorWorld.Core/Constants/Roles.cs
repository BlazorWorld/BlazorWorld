using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWorld.Core.Constants
{
    public static class Roles
    {
        public static string[] All =
        {
            Admin, 
            Manager, 
            Editor, 
            Author, 
            Contributor, 
            Moderator, 
            Member, 
            Subscriber, 
            User,
            Guest
        };

        // everyone: 
        public const string Everyone = "Everyone";
        // admin: manage everything
        public const string Admin = "Admin";
        // manager: manage most aspects of the site
        public const string Manager = "Manager";
        // editor: scheduling and managing content
        public const string Editor = "Editor";
        // author: write important content
        public const string Author = "Author";
        // contributor: authors with limited rights
        public const string Contributor = "Contributor";
        // moderator: moderate user content
        public const string Moderator = "Moderator";
        // member: special user access
        public const string Member = "Member";
        // subscriber: paying average joe
        public const string Subscriber = "Subscriber";
        // user: average joe
        public const string User = "User";
        // guest: 
        public const string Guest = "Guest";
    }
}
