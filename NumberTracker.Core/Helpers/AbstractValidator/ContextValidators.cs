using System;
using System.Linq;
using NumberTracker.Core.Data;
using NumberTracker.Core.Data.Interfaces;
using NumberTracker.Core.Features.Categories;

namespace NumberTracker.Core.Helpers.AbstractValidator
{
    public interface IContextValidators
    {
        
    }

    public class ContextValidators : IContextValidators
    {
        private readonly IDataContext _context;

        public ContextValidators(IDataContext context)
        {
            _context = context;
        }
    }
}
