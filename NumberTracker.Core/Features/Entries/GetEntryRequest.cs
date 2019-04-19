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
    public class GetEntryRequest 
        : IRequest<EntryGetDto>, IId
    {
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class GetEntryRequestHandler
        : IRequestHandler<GetEntryRequest, EntryGetDto>
    {
        private readonly IDataContext _context;

        public GetEntryRequestHandler(
            IDataContext context)
        {
            _context = context;
        }

        public async Task<EntryGetDto> Handle(
            GetEntryRequest request, 
            CancellationToken cancellationToken)
        {
            var entry = _context.Set<Entry>().Find(request.Id);
            return Mapper.Map<EntryGetDto>(entry);
        }
    }

    public class GetEntryRequestValidator 
        : AbstractValidator<GetEntryRequest>
    {
        public GetEntryRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.Set<Entry>().Any(x => id == x.Id))
                .WithMessage(ErrorMessages.Entries.EntryDoesNotExist);
        }
    }
}
