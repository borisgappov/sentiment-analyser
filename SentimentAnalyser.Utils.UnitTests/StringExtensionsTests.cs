using Faker;
using Xunit;

namespace SentimentAnalyser.Utils.UnitTests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void GivenString_ExtensionChecksIsStringEmptyOrNull()
        {
            // Arrange
            var str = Lorem.Sentence();

            // Act
            var result = str.IsEmpty();

            // Assert
            Assert.False(result);


            // Arrange
            str = null;

            // Act
            result = str.IsEmpty();

            // Assert
            Assert.True(result);


            // Arrange
            str = string.Empty;

            // Act
            result = str.IsEmpty();

            // Assert
            Assert.True(result);
        }
    }
}