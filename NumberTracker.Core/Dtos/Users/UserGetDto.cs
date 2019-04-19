using System;
namespace NumberTracker.Core.Dtos.Users
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
