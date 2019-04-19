using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using NumberTracker.Core.Common;
using NumberTracker.Core.Data;
using NumberTracker.Core.Data.Entities;
using NumberTracker.Core.Dtos.Entries;

namespace NumberTracker.Core.Features.Entries
{
    public class CreateEntryRequest 
        : IRequest<EntryGetDto>
    {
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public decimal Value { get; set; }
        public string Memo { get; set; }
    }

    public class CreateEntryRequestHandler 
        : IRequestHandler<CreateEntryRequest, EntryGetDto>
    {
        private readonly IDataContext _context;

        public CreateEntryRequestHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<EntryGetDto> Handle(
            CreateEntryRequest request, 
            CancellationToken cancellationToken)
        {
            var entry = Mapper.Map<Entry>(request);

            _context.Set<Entry>().Add(entry);
            _context.SaveChanges();

            return Mapper.Map<EntryGetDto>(entry);
        }
    }

    public class CreateEnrtyRequestValidator 
        : AbstractValidator<CreateEntryRequest>
    {
        public CreateEnrtyRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.UserId)
                .Must(userId => context.Set<User>().Any(x => userId == x.Id))
                .WithMessage(ErrorMessages.Users.UserDoesNotExist);

            RuleFor(x => x.CategoryId)
                .Must(categoryId => context.Set<Category>().Any(x => x.Id == categoryId))
                .WithMessage(ErrorMessages.Categories.CategoryDoesNotExist);

            RuleFor(x => x.Memo)
                .NotNull();
        }
    }
}
