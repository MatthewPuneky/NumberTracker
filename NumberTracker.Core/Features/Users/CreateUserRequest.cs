using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using NumberTracker.Core.Common;
using NumberTracker.Core.Data;
using NumberTracker.Core.Data.Entities;
using NumberTracker.Core.Dtos.Users;

namespace NumberTracker.Core.Features.Users
{
    public class CreateUserRequest 
        : IRequest<UserGetDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class CreateUserRequestHandler 
        : IRequestHandler<CreateUserRequest, UserGetDto>
    {
        private readonly IDataContext _context;

        public CreateUserRequestHandler(
            IDataContext context)
        {
            _context = context;
        }

        public async Task<UserGetDto> Handle(
            CreateUserRequest request, 
            CancellationToken cancellationToken)
        {
            var user = Mapper.Map<User>(request);

            _context.Set<User>().Add(user);
            _context.SaveChanges();

            return Mapper.Map<UserGetDto>(user);
        }
    }

    public class CreateEnrtyRequestValidator 
        : AbstractValidator<CreateUserRequest>
    {
        public CreateEnrtyRequestValidator(
            IDataContext context)
        {
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
                        .Must(x => context.Set<User>().Any(y => x.Email == y.Email))
                        .WithMessage(ErrorMessages.Users.EmailIsAlreadyInUse)
                        .WithName(nameof(EditUserRequest.Email));
                });
        }
    }
}
