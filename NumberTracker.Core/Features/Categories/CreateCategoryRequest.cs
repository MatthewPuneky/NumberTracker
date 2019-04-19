using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
            var categoryToAdd = new Category
            {
                Name = request.Name
            };

            _context.Categories.Add(categoryToAdd);
            _context.SaveChanges();

            var categoryToReturn = new CategoryGetDto
            {
                Id = categoryToAdd.Id,
                Name = categoryToAdd.Name
            };

            return categoryToReturn;
        }
    }
}
