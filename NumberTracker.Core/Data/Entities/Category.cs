using System;
using System.Collections.Generic;
using System.Text;
using NumberTracker.Core.Data.Interfaces;

namespace NumberTracker.Core.Data.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
