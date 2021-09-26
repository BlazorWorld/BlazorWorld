using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorWorld.Domain.Entities.Content;
using BlazorWorld.Shared.Constants.Application;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorWorld.Application.Features.Nodes.Commands.AddEdit
{
    public partial class AddEditNodeCommand : IRequest<Result<string>>
    {
        public string Id { get; set; }
        [Required]
        public string Module { get; set; }
        [Required]
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string Slug { get; set; }
        public string Path { get; set; }
        public string ParentId { get; set; }
    }

    internal class AddEditNodeCommandHandler : IRequestHandler<AddEditNodeCommand, Result<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditNodeCommandHandler> _localizer;
        private readonly IUnitOfWork<string> _unitOfWork;

        public AddEditNodeCommandHandler(IUnitOfWork<string> unitOfWork, IMapper mapper, IStringLocalizer<AddEditNodeCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(AddEditNodeCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.Id))
            {
                var node = _mapper.Map<Node>(command);
                await _unitOfWork.Repository<Node>().AddAsync(node);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllNodesCacheKey);
                return await Result<string>.SuccessAsync(node.Id, _localizer["Node Saved"]);
            }
            else
            {
                var node = await _unitOfWork.Repository<Node>().GetByIdAsync(command.Id);
                if (node != null)
                {
                    node.Module = command.Module ?? node.Module;
                    node.Type = command.Type ?? node.Type;
                    node.Title = command.Title ?? node.Title;
                    node.Content = command.Content ?? node.Content;
                    node.Status = command.Status ?? node.Status;
                    node.Slug = command.Slug ?? node.Slug;
                    node.Path = command.Path ?? node.Path;
                    node.ParentId = command.ParentId ?? node.ParentId;

                    await _unitOfWork.Repository<Node>().UpdateAsync(node);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllNodesCacheKey);
                    return await Result<string>.SuccessAsync(node.Id, _localizer["Node Updated"]);
                }
                else
                {
                    return await Result<string>.FailAsync(_localizer["Node Not Found!"]);
                }
            }
        }
    }
}