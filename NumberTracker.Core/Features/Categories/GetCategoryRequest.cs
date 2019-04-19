using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using NumberTracker.Core.Common;
using NumberTracker.Core.Data;
using NumberTracker.Core.Data.Entities;
using NumberTracker.Core.Dtos.Categories;

namespace NumberTracker.Core.Features.Categories
{
    public class GetCategoryRequest : IRequest<CategoryGetDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class GetCategoryRequestHandler
        : IRequestHandler<GetCategoryRequest, CategoryGetDto>
    {
        private readonly IDataContext _context;

        public GetCategoryRequestHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<CategoryGetDto> Handle(
            GetCategoryRequest request, 
            CancellationToken cancellationToken)
        {
            var category = _context.Set<Category>().Find(request.Id);
            return Mapper.Map<CategoryGetDto>(category);
        }
    }

    public class GetCategoryRequestValidator : AbstractValidator<GetCategoryRequest>
    {
        public GetCategoryRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.Set<Category>().Any(x => id == x.Id))
                .WithMessage(ErrorMessages.Categories.CategoryDoesNotExist);
        }
    }
}
