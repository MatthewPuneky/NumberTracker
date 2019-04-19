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

namespace NumberTracker.Core.Features.Users
{
    public class DeleteUserRequest 
        : IRequest, IId
    {
        [JsonIgnore]
        public int Id { get; set; }
    }

    public class DeleteUserRequestHandler 
        : IRequestHandler<DeleteUserRequest>
    {
        private readonly IDataContext _context;

        public DeleteUserRequestHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            DeleteUserRequest request, 
            CancellationToken cancellationToken)
        {
            var user = _context.Set<User>().Find(request.Id);

            _context.Set<User>().Remove(user);
            _context.SaveChanges();

            return new Unit();
        }
    }

    public class DeleteUserRequestValidator 
        : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator(
            IDataContext context)
        {
            RuleFor(x => x.Id)
                .Must(id => context.Set<User>().Any(x => id == x.Id))
                .WithMessage(ErrorMessages.Users.UserDoesNotExist);
        }
    }
}
