using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using NumberTracker.Core.Common;
using NumberTracker.Core.Data;
using NumberTracker.Core.Data.Entities;

namespace NumberTracker.Core.Features.Categories
{
    public class DeleteCategoryRequest : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest>
    {
        private readonly IDataContext _context;

        public DeleteCategoryRequestHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = _context.Set<Category>().Find(request.Id);

            _context.Set<Category>().Remove(category);
            _context.SaveChanges();

            return new Unit();
        }
    }

    public class DeleteCategoryRequestValidator : AbstractValidator<GetCategoryRequest>
    {
        public DeleteCategoryRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.Set<Category>().Any(x => id == x.Id))
                .WithMessage(ErrorMessages.Categories.CategoryDoesNotExist);
        }
    }
}
