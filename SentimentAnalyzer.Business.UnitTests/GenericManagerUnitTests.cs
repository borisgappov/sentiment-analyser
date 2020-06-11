using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SentimentAnalyser.Business.Managers;
using SentimentAnalyser.Data.Interfaces;
using SentimentAnalyser.Models.Entities;
using SentimentAnalyser.Models.UnitTests.Fakes;
using Xunit;

namespace SentimentAnalyzer.Business.UnitTests
{
    public class GenericManagerUnitTests : BaseManagerUnitTests
    {
        public GenericManagerUnitTests()
        {
            word = new Word().Fake(betId);
        }

        private readonly int betId = Helper.FakeId();
        private Word word;

        [Fact]
        public async void GivenModel_ManagerWillCreateEntityRecord()
        {
            // Arrange
            var manager = new GenericManager<Word>(UoWFactoryMock.Object);
            UoW.Setup(m => m.CreateRepository<IGenericRepository<Word>>().Create(It.IsAny<Word>())).Verifiable();
            // Act
            word = await manager.CreateAsync(word).ConfigureAwait(false);
            // Assert
            UoW.Verify(m => m.CreateRepository<IGenericRepository<Word>>().Create(It.Is<Word>(it => it.Id == word.Id)));
        }

        [Fact]
        public async void GivenModel_ManagerWillDeleteEntityRecord()
        {
            // Arrange
            var manager = new GenericManager<Word>(UoWFactoryMock.Object);
            UoW.Setup(m => m.CreateRepository<IGenericRepository<Word>>().Delete(It.IsAny<Word>())).Verifiable();
            // Act
            await manager.DeleteAsync(word).ConfigureAwait(false);
            // Assert
            UoW.Verify(m => m.CreateRepository<IGenericRepository<Word>>().Delete(It.Is<Word>(it => it.Id == word.Id)));
        }

        [Fact]
        public async void GivenModel_ManagerWillUpdateEntityRecord()
        {
            // Arrange
            var manager = new GenericManager<Word>(UoWFactoryMock.Object);
            UoW.Setup(m => m.CreateRepository<IGenericRepository<Word>>().Update(It.IsAny<Word>())).Verifiable();
            // Act
            await manager.UpdateAsync(word).ConfigureAwait(false);
            // Assert
            UoW.Verify(m => m.CreateRepository<IGenericRepository<Word>>().Update(It.Is<Word>(it => it.Id == word.Id)));
        }

        [Fact]
        public async void GivenModelId_ManagerFindsEntity()
        {
            // Arrange
            var manager = new GenericManager<Word>(UoWFactoryMock.Object);
            UoW.Setup(m => m.CreateRepository<IGenericRepository<Word>>().FindByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(word)).Verifiable();
            // Act
            await manager.FindByIdAsync(betId).ConfigureAwait(false);
            // Assert
            UoW.Verify(m =>
                m.CreateRepository<IGenericRepository<Word>>().FindByIdAsync(It.Is<int>(it => it == word.Id)));
        }

        [Fact]
        public async void GivenNothing_ManagerReturnsAllRecords()
        {
            // Arrange
            var manager = new GenericManager<Word>(UoWFactoryMock.Object);
            UoW.Setup(m => m.CreateRepository<IGenericRepository<Word>>().AllAsync())
                .Returns(Task.FromResult(new List<Word> {word}));
            // Act
            var records = await manager.GetAllAsync().ConfigureAwait(false);
            // Assert
            Assert.Single(records);
            Assert.Equal(records[0].Id, betId);
        }
    }
}