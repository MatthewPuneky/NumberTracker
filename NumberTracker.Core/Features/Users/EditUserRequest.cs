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
    public class EditUserRequest 
        : IRequest<UserGetDto>, IId
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class EditUserRequestHandler 
        : IRequestHandler<EditUserRequest, UserGetDto>
    {
        private readonly IDataContext _context;

        public EditUserRequestHandler(
            IDataContext context)
        {
            _context = context;
        }

        public async Task<UserGetDto> Handle(
            EditUserRequest request, 
            CancellationToken cancellationToken)
        {
            var user = _context.Set<User>().Find(request.Id);

            Mapper.Map(request, user);
            _context.SaveChanges();

            return Mapper.Map<UserGetDto>(user);
        }
    }

    public class EditUserRequestValidator 
        : AbstractValidator<EditUserRequest>
    {
        public EditUserRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.Set<User>().Any(x => id == x.Id))
                .WithMessage(ErrorMessages.Users.UserDoesNotExist);

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .DependentRules(() =>
                {
                    RuleFor(x => x)
                        .Must(x => context.Set<User>().Any(y => x.Id != y.Id && x.Email == y.Email))
                        .WithMessage(ErrorMessages.Users.EmailIsAlreadyInUse)
                        .WithName(nameof(EditUserRequest.Email));
                });
        }
    }
}
