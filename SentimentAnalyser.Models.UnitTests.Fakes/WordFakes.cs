using System;
using Faker;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser.Models.UnitTests.Fakes
{
    public static class WordFakes
    {
        public static Word Fake(
            this Word self,
            int? id = null,
            string Text = null,
            float? Sentiment = null)
        {
            self.Id = id ?? Helper.FakeId();
            self.Text = Text ?? Lorem.Sentence();
            self.Sentiment = Sentiment ?? ((float)Helper.RandomEnumValue<SentimentRating>()) / 10f;
            return self;
        }
    }
}