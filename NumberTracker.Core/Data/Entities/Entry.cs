using System;
using NumberTracker.Core.Data.Interfaces;

namespace NumberTracker.Core.Data.Entities
{
    public class Entry : IEntity
    {
        public int Id { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public decimal Value { get; set; }
        public string Memo { get; set; }
    }
}
