using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using NumberTracker.Core.Data;
using NumberTracker.Core.Data.Entities;
using NumberTracker.Core.Dtos.Categories;

namespace NumberTracker.Core.Features.Categories
{
    public class GetAllCategoriesRequest : IRequest<List<CategoryGetDto>>
    {
        public Func<CategoryGetDto, bool> Filter { get; set; }
    }

    public class GetAllCategoriesRequestHandler : IRequestHandler<GetAllCategoriesRequest, List<CategoryGetDto>>
    {
        private readonly IDataContext _context;

        public GetAllCategoriesRequestHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryGetDto>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            return _context.Set<Category>()
                        .ProjectTo<CategoryGetDto>()
                        .Where(x => request.Filter(x))
                        .ToList();
        }
    }

    public class GetAllCategoriesRequestValidator : AbstractValidator<GetAllCategoriesRequest>
    {
        public GetAllCategoriesRequestValidator()
        {

        }
    }
}
