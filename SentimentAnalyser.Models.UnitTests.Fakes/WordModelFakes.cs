using Faker;

namespace SentimentAnalyser.Models.UnitTests.Fakes
{
    public static class WordModelFakes
    {
        public static WordModel Fake(
            this WordModel self,
            int? id = null,
            string Text = null,
            SentimentRating? Sentiment = null)
        {
            self.Id = id ?? Helper.FakeId();
            self.Text = Text ?? Lorem.Sentence();
            self.Sentiment = Sentiment ?? Helper.RandomEnumValue<SentimentRating>();
            return self;
        }
    }
}