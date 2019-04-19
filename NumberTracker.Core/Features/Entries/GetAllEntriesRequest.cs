using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using NumberTracker.Core.Common;
using NumberTracker.Core.Data;
using NumberTracker.Core.Data.Entities;
using NumberTracker.Core.Dtos.Entries;

namespace NumberTracker.Core.Features.Entries
{
    public class GetAllEntriesRequest 
        : IRequest<List<EntryGetDto>>
    {
        public Func<EntryGetDto, bool> Filter { get; set; }
    }

    public class GetAllEntriesRequestHandler 
        : IRequestHandler<GetAllEntriesRequest, List<EntryGetDto>>
    {
        private readonly IDataContext _context;

        public GetAllEntriesRequestHandler(
            IDataContext context)
        {
            _context = context;
        }

        public async Task<List<EntryGetDto>> Handle(
            GetAllEntriesRequest request, 
            CancellationToken cancellationToken)
        {
            return _context.Set<Entry>()
                        .ProjectTo<EntryGetDto>()
                        .Where(x => request.Filter(x))
                        .ToList();
        }
    }

    public class GetAllEntriesRequestValidator 
        : AbstractValidator<GetAllEntriesRequest>
    {
        public GetAllEntriesRequestValidator()
        {
        }
    }
}
