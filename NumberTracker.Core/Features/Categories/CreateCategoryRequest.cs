using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using NumberTracker.Core.Common;
using NumberTracker.Core.Data;
using NumberTracker.Core.Data.Entities;
using NumberTracker.Core.Dtos.Categories;

namespace NumberTracker.Core.Features.Categories
{
    public class CreateCategoryRequest : IRequest<CategoryGetDto>
    {
        public string Name { get; set; }
    }

    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, CategoryGetDto>
    {
        private readonly IDataContext _context;

        public CreateCategoryRequestHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<CategoryGetDto> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = Mapper.Map<Category>(request);

            _context.Set<Category>().Add(category);
            _context.SaveChanges();

            return Mapper.Map<CategoryGetDto>(category);
        }
    }

    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .Must(name => context.Set<Category>().Any(category => category.Name == name))
                .WithMessage(ErrorMessages.Categories.NameAlreadyExists);
        }
    }
}
