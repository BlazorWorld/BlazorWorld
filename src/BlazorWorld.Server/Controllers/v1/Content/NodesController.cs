using BlazorWorld.Application.Features.Nodes.Commands.AddEdit;
using BlazorWorld.Application.Features.Nodes.Commands.Delete;
using BlazorWorld.Application.Features.Nodes.Queries.GetAll;
using BlazorWorld.Application.Features.Nodes.Queries.GetById;
using BlazorWorld.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorWorld.Server.Controllers.v1.Content
{
    public class NodesController : BaseApiController<NodesController>
    {
        [Authorize(Policy = Permissions.Nodes.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllNodesQuery query)
        {
            var nodes = await _mediator.Send(query);
            return Ok(nodes);
        }

        [Authorize(Policy = Permissions.Nodes.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var node = await _mediator.Send(new GetNodeByIdQuery() { Id = id });
            return Ok(node);
        }

        [Authorize(Policy = Permissions.Nodes.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditNodeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = Permissions.Nodes.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _mediator.Send(new DeleteNodeCommand { Id = id }));
        }
    }
}