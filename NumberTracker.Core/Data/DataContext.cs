using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NumberTracker.Core.Data.Entities;

namespace NumberTracker.Core.Data
{
    public interface IDataContext
    {
        DbSet<Category> Categories { get; set; }

        int SaveChanges();
    }

    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
