using System;
using NumberTracker.Core.Features.Categories;

namespace NumberTracker.Core.Common
{
    public class ErrorMessages
    {
        public static class Categories
        {
            public const string CategoryDoesNotExist = "'Category' does not exist";
            public const string NameAlreadyExists = "A 'Category' already exists with the requested 'Name'";
        }

        public static class Entries
        {
            public const string EntryDoesNotExist = "'Entry' does not exist";
        }

        public static class Users
        {
            public const string EmailIsAlreadyInUse = "'Email' is already in use";
            public const string UserDoesNotExist = "'User' does not exist";
        }
    }
}
