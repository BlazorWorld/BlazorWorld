using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorWorld.Domain.Entities.Content;
using BlazorWorld.Shared.Constants.Application;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorWorld.Application.Features.Nodes.Queries.GetAll
{
    public class GetAllNodesQuery : IRequest<Result<List<GetAllNodesResponse>>>
    {
        public GetAllNodesQuery()
        {
        }
    }

    internal class GetAllBrandsCachedQueryHandler : IRequestHandler<GetAllNodesQuery, Result<List<GetAllNodesResponse>>>
    {
        private readonly IUnitOfWork<string> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllBrandsCachedQueryHandler(IUnitOfWork<string> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllNodesResponse>>> Handle(GetAllNodesQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Node>>> getAllNodes = () => _unitOfWork.Repository<Node>().GetAllAsync();
            var brandList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllNodesCacheKey, getAllNodes);
            var mappedBrands = _mapper.Map<List<GetAllNodesResponse>>(brandList);
            return await Result<List<GetAllNodesResponse>>.SuccessAsync(mappedBrands);
        }
    }
}