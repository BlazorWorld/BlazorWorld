namespace BlazorWorld.Application.Features.Nodes.Queries.GetAll
{
    public class GetAllNodesResponse
    {
        public string Id { get; set; }
        public string Module { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string Slug { get; set; }
        public string Path { get; set; }
        public string ParentId { get; set; }
    }
}