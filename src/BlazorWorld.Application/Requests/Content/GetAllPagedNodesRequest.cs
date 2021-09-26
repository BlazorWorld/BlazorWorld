using BlazorHero.CleanArchitecture.Application.Requests;

namespace BlazorWorld.Application.Requests.Content
{
    public class GetAllPagedNodesRequest : PagedRequest
    {
        public string Module { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string ParentId { get; set; }
        public bool RootOnly { get; set; } = false;
        public string OrderBy { get; set; }
        public int TruncateContent { get; set; }
    }
}
