namespace SentimentAnalyser.Models
{
    public class WordModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public SentimentRating Sentiment { get; set; }
    }
}