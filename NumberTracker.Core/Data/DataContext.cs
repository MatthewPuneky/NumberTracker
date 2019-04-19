using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NumberTracker.Core.Data.Entities;
using NumberTracker.Core.Data.Interfaces;

namespace NumberTracker.Core.Data
{
    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;

        int SaveChanges();
    }

    public class DataContext : DbContext, IDataContext
    {
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity => Set<TEntity>();

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
