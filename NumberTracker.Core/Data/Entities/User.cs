using System;
using System.Collections.Generic;
using NumberTracker.Core.Data.Interfaces;

namespace NumberTracker.Core.Data.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<Category> Categories { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
