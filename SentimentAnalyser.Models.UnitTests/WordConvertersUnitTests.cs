using SentimentAnalyser.Models.Converters;
using SentimentAnalyser.Models.Entities;
using SentimentAnalyser.Models.UnitTests.Fakes;
using Xunit;

namespace SentimentAnalyser.Models.UnitTests
{
    public class WordConvertersUnitTests
    {
        public WordConvertersUnitTests()
        {
            Mapper.Initialize();
        }
        

        [Fact]
        public void GivenWordEntity_ConverterReturnsWordModel()
        {
            // Arrange
            var entity = new Word().Fake();

            // Act
            var result = entity.ToModel();

            // Assert
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Text, result.Text);
            Assert.Equal(entity.Sentiment, ((float)result.Sentiment) / 10f);
        }

        [Fact]
        public void GivenWordModel_ConverterReturnsWordEntity()
        {
            // Arrange
            var model = new WordModel().Fake();

            // Act
            var result = model.ToEntity();

            // Assert
            Assert.Equal(model.Id, result.Id);
            Assert.Equal(model.Text, result.Text);
            Assert.Equal((float)model.Sentiment, result.Sentiment * 10f);
        }
    }
}