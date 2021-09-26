using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlazorWorld.Shared.Constants.Permission
{
    public static class Permissions
    {
        public static class Nodes
        {
            public const string View = "Permissions.Nodes.View";
            public const string Create = "Permissions.Nodes.Create";
            public const string Edit = "Permissions.Nodes.Edit";
            public const string Delete = "Permissions.Nodes.Delete";
            public const string Search = "Permissions.Nodes.Search";
        }

       /// <summary>
       /// Returns a list of Permissions.
       /// </summary>
       /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permssions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permssions.Add(propertyValue.ToString());
            }
            return permssions;
        }
    }
}