using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser.Models.Converters
{
    public static class WordConverters
    {
        public static WordModel ToModel(this Word model)
        {
            return model.MapTo<WordModel>();
        }

        public static Word ToEntity(this WordModel model)
        {
            return model.MapTo<Word>();
        }
    }
}