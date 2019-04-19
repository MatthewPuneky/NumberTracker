using System;
using NumberTracker.Core.Data.Interfaces;

namespace NumberTracker.Core.Dtos.Entries
{
    public class EntryGetDto : IId
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public decimal Value { get; set; }
        public string Memo { get; set; }
    }
}
