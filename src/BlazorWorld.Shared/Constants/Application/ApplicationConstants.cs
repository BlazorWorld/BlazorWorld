using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Shared.Constants.Application
{
    public static class ApplicationConstants
    {
        public static class Cache
        {
            public const string GetAllNodesCacheKey = "all-nodes";
            public const string GetAllNodeTypesCacheKey = "all-node-types";

            public static string GetAllEntityExtendedAttributesCacheKey(string entityFullName)
            {
                return $"all-{entityFullName}-extended-attributes";
            }

            public static string GetAllEntityExtendedAttributesByEntityIdCacheKey<TEntityId>(string entityFullName, TEntityId entityId)
            {
                return $"all-{entityFullName}-extended-attributes-{entityId}";
            }
        }
    }
}
