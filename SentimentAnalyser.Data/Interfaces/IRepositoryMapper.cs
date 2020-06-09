using System;

namespace SentimentAnalyser.Data.Interfaces
{
    public interface IRepositoryMapper
    {
        Type GetImplementationType(Type type);
    }
}