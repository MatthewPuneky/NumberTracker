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
using NumberTracker.Core.Dtos.Users;

namespace NumberTracker.Core.Features.Users
{
    public class GetUserRequest 
        : IRequest<UserGetDto>, IId
    {
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class GetUserRequestHandler
        : IRequestHandler<GetUserRequest, UserGetDto>
    {
        private readonly IDataContext _context;

        public GetUserRequestHandler(
            IDataContext context)
        {
            _context = context;
        }

        public async Task<UserGetDto> Handle(
            GetUserRequest request, 
            CancellationToken cancellationToken)
        {
            var user = _context.Set<User>().Find(request.Id);
            return Mapper.Map<UserGetDto>(user);
        }
    }

    public class GetUserRequestValidator 
        : AbstractValidator<GetUserRequest>
    {
        public GetUserRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.Set<User>().Any(x => id == x.Id))
                .WithMessage(ErrorMessages.Users.UserDoesNotExist);
        }
    }
}
