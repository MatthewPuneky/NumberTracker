using System;
using System.Collections.Generic;
using System.Text;

namespace NumberTracker.Core.Dtos.Categories
{
    public class CategoryGetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
