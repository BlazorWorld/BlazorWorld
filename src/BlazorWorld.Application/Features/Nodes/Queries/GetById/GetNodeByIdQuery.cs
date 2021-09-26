using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorWorld.Domain.Entities.Content;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorWorld.Application.Features.Nodes.Queries.GetById
{
    public class GetNodeByIdQuery : IRequest<Result<GetNodeByIdResponse>>
    {
        public string Id { get; set; }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetNodeByIdQuery, Result<GetNodeByIdResponse>>
    {
        private readonly IUnitOfWork<string> _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork<string> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetNodeByIdResponse>> Handle(GetNodeByIdQuery query, CancellationToken cancellationToken)
        {
            var node = await _unitOfWork.Repository<Node>().GetByIdAsync(query.Id);
            var mappedBrand = _mapper.Map<GetNodeByIdResponse>(node);
            return await Result<GetNodeByIdResponse>.SuccessAsync(mappedBrand);
        }
    }
}