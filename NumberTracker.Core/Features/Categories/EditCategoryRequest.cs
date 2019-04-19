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
using NumberTracker.Core.Data.Interfaces;
using NumberTracker.Core.Dtos.Categories;

namespace NumberTracker.Core.Features.Categories
{
    public class EditCategoryRequest 
        : IRequest<CategoryGetDto>, IId
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditCategoryRequestHandler 
        : IRequestHandler<EditCategoryRequest, CategoryGetDto>
    {
        private readonly IDataContext _context;

        public EditCategoryRequestHandler(
            IDataContext context)
        {
            _context = context;
        }

        public async Task<CategoryGetDto> Handle(
            EditCategoryRequest request, 
            CancellationToken cancellationToken)
        {
            var category = _context.Set<Category>().Find(request.Id);

            Mapper.Map(request, category);
            _context.SaveChanges();

            return Mapper.Map<CategoryGetDto>(category);
        }
    }

    public class EditCategoryRequestValidator 
        : AbstractValidator<EditCategoryRequest>
    {
        public EditCategoryRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.Set<Category>().Any(x => id == x.Id))
                .WithMessage(ErrorMessages.Categories.CategoryDoesNotExist);

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .DependentRules(() =>
                {
                    RuleFor(x => x)
                        .Must(x => context.Set<Category>().Any(y => x.Id != y.Id && x.Name == y.Name))
                        .WithMessage(ErrorMessages.Categories.NameAlreadyExists)
                        .WithName(nameof(EditCategoryRequest.Name));
                });
        }
    }
}
