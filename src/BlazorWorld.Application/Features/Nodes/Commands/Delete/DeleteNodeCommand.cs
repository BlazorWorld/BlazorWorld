using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorWorld.Application.Interfaces.Repositories;
using BlazorWorld.Domain.Entities.Content;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorWorld.Application.Features.Nodes.Commands.Delete
{
    public class DeleteNodeCommand : IRequest<Result<string>>
    {
        public string Id { get; set; }
    }

    internal class DeleteNodeCommandHandler : IRequestHandler<DeleteNodeCommand, Result<string>>
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IStringLocalizer<DeleteNodeCommandHandler> _localizer;
        private readonly IUnitOfWork<string> _unitOfWork;

        public DeleteNodeCommandHandler(IUnitOfWork<string> unitOfWork, INodeRepository nodeRepository, IStringLocalizer<DeleteNodeCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _nodeRepository = nodeRepository;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(DeleteNodeCommand command, CancellationToken cancellationToken)
        {
            var isNodeUsed = await _nodeRepository.IsNodeUsed(command.Id);
            if (!isNodeUsed)
            {
                var node = await _unitOfWork.Repository<Node>().GetByIdAsync(command.Id);
                if (node != null)
                {
                    await _unitOfWork.Repository<Node>().DeleteAsync(node);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBrandsCacheKey);
                    return await Result<string>.SuccessAsync(node.Id, _localizer["Node Deleted"]);
                }
                else
                {
                    return await Result<string>.FailAsync(_localizer["Node Not Found!"]);
                }
            }
            else
            {
                return await Result<string>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}