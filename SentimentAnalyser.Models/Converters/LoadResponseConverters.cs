using System.Linq;
using DevExtreme.AspNet.Data.ResponseModel;
using SentimentAnalyser.Models.Entities;
using SentimentAnalyser.Utils;


namespace SentimentAnalyser.Models.Converters
{
    public static class LoadResponseConverters
    {
        public static LoadResponse<T> ToResponse<T>(this LoadResult model)
        {
            return new LoadResponse<T>
            {
                data = model.data.Cast<Word>().Select(x => x.MapTo<T>()).ToArray(),
                groupCount = model.groupCount,
                totalCount = model.totalCount,
                summary = model.summary.Clone<object[]>()
            };
        }
    }
}