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
using NumberTracker.Core.Dtos.Entries;

namespace NumberTracker.Core.Features.Entries
{
    public class EditEntryRequest 
        : IRequest<EntryGetDto>, IId
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public decimal Value { get; set; }
        public string Memo { get; set; }
    }

    public class EditEntryRequestHandler 
        : IRequestHandler<EditEntryRequest, EntryGetDto>
    {
        private readonly IDataContext _context;

        public EditEntryRequestHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<EntryGetDto> Handle(
            EditEntryRequest request, 
            CancellationToken cancellationToken)
        {
            var entry = _context.Set<Entry>().Find(request.Id);

            Mapper.Map(request, entry);
            _context.SaveChanges();

            return Mapper.Map<EntryGetDto>(entry);
        }
    }

    public class EditEntryRequestValidator 
        : AbstractValidator<EditEntryRequest>
    {
        public EditEntryRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.Set<Entry>().Any(x => id == x.Id))
                .WithMessage(ErrorMessages.Entries.EntryDoesNotExist);

            RuleFor(x => x.CategoryId)
                .Must(categoryId => context.Set<Category>().Any(x => x.Id == categoryId))
                .WithMessage(ErrorMessages.Categories.CategoryDoesNotExist);

            RuleFor(x => x.Memo)
                .NotNull();
        }
    }
}
