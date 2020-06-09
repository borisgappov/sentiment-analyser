using Faker;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using SentimentAnalyser.Data;
using SentimentAnalyser.Data.Core;
using SentimentAnalyser.Models.Entities;
using SentimentAnalyser.Models.UnitTests.Fakes;
using Xunit;

namespace SentimentAnalyzer.Data.UnitTests
{
    public class GenericRepositoryUnitTests : BaseRepositoryUnitTests
    {
        [Fact]
        public void GivenDataContect_RepositoryCreated()
        {
            // Arrange

            var context = new Mock<ApplicationDbContext>(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("TestDb" + Helper.FakeId())
                    .Options,
                new Mock<IOptions<OperationalStoreOptions>>().Object);

            context.Setup(m => m.Set<Word>()).Verifiable();

            // Act
            new GenericRepository<Word>(context.Object);

            // Assert
            context.Verify(m => m.Set<Word>(), Times.Once());
        }

        [Fact]
        public void GivenModel_RepositoryCreatesEntity()
        {
            TestDbContext.Database.EnsureDeleted();

            // Arrange
            var word = new Word().Fake();

            // Act
            new GenericRepository<Word>(TestDbContext).Create(word);

            // Assert
            Assert.Equal(EntityState.Added, TestDbContext.Entry(word).State);
        }

        [Fact]
        public void GivenModel_RepositoryDeletesEntity()
        {
            TestDbContext.Database.EnsureDeleted();

            // Arrange
            var word = new Word().Fake();
            // Act
            new GenericRepository<Word>(TestDbContext).Delete(word);
            // Assert
            Assert.Equal(EntityState.Deleted, TestDbContext.Entry(word).State);

            // Arrange
            word = new Word().Fake();
            TestDbContext.Entry(word).State = EntityState.Detached;
            // Act
            new GenericRepository<Word>(TestDbContext).Delete(word);
            // Assert
            Assert.Equal(EntityState.Deleted, TestDbContext.Entry(word).State);
        }

        [Fact]
        public void GivenModel_RepositoryUpdatesEntity()
        {
            TestDbContext.Database.EnsureDeleted();

            // Arrange
            var word = new Word().Fake();

            // Act
            new GenericRepository<Word>(TestDbContext).Update(word);

            // Assert
            Assert.Equal(EntityState.Modified, TestDbContext.Entry(word).State);
        }

        [Fact]
        public async void GivenModelId_RepositoryFindsEntity()
        {
            TestDbContext.Database.EnsureDeleted();

            // Arrange
            var wordId = RandomNumber.Next();
            var word = new Word().Fake(wordId);
            TestDbContext.Add(word);
            TestDbContext.SaveChanges();

            // Act
            var found = await new GenericRepository<Word>(TestDbContext).FindByIdAsync(wordId).ConfigureAwait(false);

            // Assert
            Assert.NotNull(found);
            Assert.Equal(wordId, found.Id);
        }

        [Fact]
        public async void GivenNothing_RepositoryReturnsListOfAllEntities()
        {
            TestDbContext.Database.EnsureDeleted();

            // Arrange
            var words = Helper.FakeEnumerable(() => new Word().Fake(), 10);
            TestDbContext.AddRange(words);
            TestDbContext.SaveChanges();

            // Act
            var items = await new GenericRepository<Word>(TestDbContext).AllAsync().ConfigureAwait(false);

            // Assert
            Assert.NotNull(items);
            Assert.Equal(10, items.Count);
        }
    }
}