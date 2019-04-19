using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NumberTracker.Core.Features.Categories;
using NUnit.Framework;

namespace NumberTracker.Tests.Features.Categories
{
    [TestFixture]
    public class CreateCategoryRequestTests : BaseTest
    {
        public CreateCategoryRequestHandler GenerateHandler =>
            new CreateCategoryRequestHandler(DataContext);

        public CreateCategoryRequest GenerateValidRequest() => 
            new CreateCategoryRequest {Name = $"{NextId}"};

        [Test]
        public async Task Handle_AddCategoryToDatabase_DatabaseHasCountOf1()
        {
            var request = GenerateValidRequest();
            await GenerateHandler.Handle(request, new CancellationToken());

            FreshContext(context =>
            {
                context.Categories.Count().ShouldBe(1);
            });
        }

        [Test]
        public async Task Handle_AddCategoryToDatabase_DatabaseValueHasCorrectFirstName()
        {
            var request = GenerateValidRequest();
            await GenerateHandler.Handle(request, new CancellationToken());

            FreshContext(context =>
            {
                context.Categories.First().Name.ShouldBe(request.Name);
            });
        }

        [Test]
        public async Task Handle_AddCategoryToDatabase_ReturnedCategoryHasCorrectFirstName()
        {
            var request = GenerateValidRequest();
            var result = await GenerateHandler.Handle(request, new CancellationToken());

            result.Name.ShouldBe(request.Name);
        }
    }

    public static class ShouldBeAsserts
    {
        public static void ShouldBe(this int actual, int expected)
        {
            Assert.AreEqual(expected, actual);
        }

        public static void ShouldBe(this string actual, string expected)
        {
            Assert.AreEqual(expected, actual);
        }
    }
}
