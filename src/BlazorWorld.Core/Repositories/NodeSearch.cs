namespace BlazorWorld.Core.Repositories
{
    public class NodeSearch
    {
        public string Slug { get; set; }
        public string Module { get; set; }
        public string Type { get; set; }
        public string CategoryId { get; set; }
        public string Path { get; set; }
        public string GroupId { get; set; }
        public string ParentId { get; set; }
        public bool RootOnly { get; set; } = false;
        public string[] OrderBy { get; set; }
        public int PageSize { get; set; }
    }
}
