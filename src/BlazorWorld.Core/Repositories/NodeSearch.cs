using System.Collections.Generic;

namespace BlazorWorld.Core.Repositories
{
    public class NodeSearch
    {
        public string Slug { get; set; }
        public string Module { get; set; }
        public string Type { get; set; }
        public string GroupId { get; set; }
        public string Path { get; set; }
        public string ParentId { get; set; }
        public bool RootOnly { get; set; } = false;
        public string OrderBy { get; set; }
        public int PageSize { get; set; }
        public int TruncateContent { get; set; }

        public string ToQueryString()
        {
            return $"slug={Slug}&module={Module}&type={Type}&groupId={GroupId}&path={Path}&parentId={ParentId}&rootOnly={RootOnly}&orderBy={OrderBy}&pageSize={PageSize}&truncateContent={TruncateContent}";
        }

        public string[] OrderByItems()
        {
            if (!string.IsNullOrEmpty(OrderBy))
            {
                return OrderBy.Split(',');
            }
            else
                return new string[0];
        }
    }
}
