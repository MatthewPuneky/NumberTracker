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
using NumberTracker.Core.Data.Interfaces;

namespace NumberTracker.Core.Features.Entries
{
    public class DeleteEntryRequest 
        : IRequest, IId
    {
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class DeleteEntryRequestHandler 
        : IRequestHandler<DeleteEntryRequest>
    {
        private readonly IDataContext _context;

        public DeleteEntryRequestHandler(
            IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            DeleteEntryRequest request, 
            CancellationToken cancellationToken)
        {
            var entry = _context.Set<Entry>().Find(request.Id);

            _context.Set<Entry>().Remove(entry);
            _context.SaveChanges();

            return new Unit();
        }
    }

    public class DeleteEntryRequestValidator 
        : AbstractValidator<DeleteEntryRequest>
    {
        public DeleteEntryRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.Set<Entry>().Any(x => id == x.Id))
                .WithMessage(ErrorMessages.Entries.EntryDoesNotExist);
        }
    }
}
