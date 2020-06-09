using System.Linq;
using System.Security.Claims;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SentimentAnalyser.Business.Interfaces;
using SentimentAnalyser.Models;
using SentimentAnalyser.Models.Converters;
using SentimentAnalyser.Models.Entities;
using SentimentAnalyser.Models.UnitTests.Fakes;
using Xunit;

namespace SentimentAnalyser.Controllers.UnitTests
{
    public class WordsControllerUnitTests
    {
        public WordsControllerUnitTests()
        {
            Mapper.Initialize();
            _wordsController = new WordsController(_wordManager.Object);
            _wordsController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, "UnitTest User"),
                        new Claim(ClaimTypes.NameIdentifier, "123")
                    }, "mock"))
                }
            };
        }

        private readonly Mock<IWordManager> _wordManager = new Mock<IWordManager>();
        private readonly WordsController _wordsController;

        [Fact]
        public async void GivenLoadOptions_ControllerReturnsLoadResult()
        {
            // Arrange
            var loadOptions = new DataSourceLoadOptionsBase();
            var words = Helper.FakeEnumerable(() => new Word().Fake()).ToList();
            var loadResult = new LoadResult
            {
                data = words
            };

            // Act
            _wordManager.Setup(m => m.DataSourceLoadAsync(loadOptions))
                .ReturnsAsync(loadResult)
                .Verifiable();

            var result = await _wordsController.GetWords(loadOptions).ConfigureAwait(false);

            // Assert
            _wordManager.Verify(m => m.DataSourceLoadAsync(loadOptions), Times.Once);
            Assert.Equal(typeof(ActionResult<LoadResponse<WordModel>>), result.GetType());
        }
    }
}