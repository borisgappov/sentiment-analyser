using System.Collections.Generic;

namespace SentimentAnalyser.Models
{
    public class LoadResponse<T>
    {
        public IEnumerable<T> data { get; set; }

        public int totalCount { get; set; }

        public int groupCount { get; set; }

        public object[] summary { get; set; }
    }
}