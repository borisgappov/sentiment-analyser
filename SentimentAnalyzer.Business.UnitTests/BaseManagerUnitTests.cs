using Moq;
using SentimentAnalyser.Data.Interfaces;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyzer.Business.UnitTests
{
    public class BaseManagerUnitTests
    {
        protected BaseManagerUnitTests()
        {
            var uowFactoryMock = new Mock<IUnitOfWorkFactory>();
            UoW = new Mock<IUnitOfWork>();
            UowSetupCreateRepositoryMethods();
            uowFactoryMock.Setup(p => p.CreateUnitOfWork()).Returns(UoW.Object);
            UoWFactoryMock = uowFactoryMock;
        }

        internal Mock<IUnitOfWorkFactory> UoWFactoryMock { get; }
        internal Mock<IUnitOfWork> UoW { get; }

        private void SetupCreateRepositoryMethod<T>() where T : class
        {
            UoW.Setup(p => p.CreateRepository<T>()).Returns(() => { return new Mock<T>().Object; });
        }

        private void UowSetupCreateRepositoryMethods()
        {
            SetupCreateRepositoryMethod<IGenericRepository<Word>>();
            SetupCreateRepositoryMethod<IWordRepository>();
        }
    }
}