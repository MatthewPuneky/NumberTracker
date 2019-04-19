using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using NumberTracker.Core.Data;
using NUnit.Framework;

namespace NumberTracker.Tests
{
    public class BaseTest
    {
        public DataContext DataContext;
        public ThreadLocal<DbContextOptions<DataContext>> Options;

        [SetUp]
        public void SetUp()
        {
            Options = new ThreadLocal<DbContextOptions<DataContext>>
            {
                Value = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options
            };

            DataContext = new DataContext(Options.Value);
        }

        private readonly ThreadLocal<int> _nextId = new ThreadLocal<int> {Value = 100};
        public int NextId => ++_nextId.Value;

        public void FreshContext(Action<DataContext> action)
        {
            using (var context = new DataContext(Options.Value))
            {
                action(context);
            }
        }
    }
}
