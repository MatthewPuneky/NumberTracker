using System;
using NumberTracker.Core.Features.Categories;

namespace NumberTracker.Core.Common
{
    public class ErrorMessages
    {
        public class Categories
        {
            public const string CategoryDoesNotExist = "Requested 'Category' does not exist";
            public const string NameAlreadyExists = "A 'Category' already exists with the requested 'Name'";
        }
    }
}
