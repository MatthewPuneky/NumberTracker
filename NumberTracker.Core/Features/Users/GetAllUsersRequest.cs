using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using NumberTracker.Core.Data;
using NumberTracker.Core.Data.Entities;
using NumberTracker.Core.Dtos.Users;

namespace NumberTracker.Core.Features.Users
{
    public class GetAllUsersRequest 
        : IRequest<List<UserGetDto>>
    {
        public Func<UserGetDto, bool> Filter { get; set; }
    }

    public class GetAllUsersRequestHandler 
        : IRequestHandler<GetAllUsersRequest, List<UserGetDto>>
    {
        private readonly IDataContext _context;

        public GetAllUsersRequestHandler(
            IDataContext context)
        {
            _context = context;
        }

        public async Task<List<UserGetDto>> Handle(
            GetAllUsersRequest request, 
            CancellationToken cancellationToken)
        {
            return _context.Set<User>()
                        .ProjectTo<UserGetDto>()
                        .Where(x => request.Filter(x))
                        .ToList();
        }
    }

    public class GetAllUsersRequestValidator 
        : AbstractValidator<GetAllUsersRequest>
    {
        public GetAllUsersRequestValidator()
        {
        }
    }
}
