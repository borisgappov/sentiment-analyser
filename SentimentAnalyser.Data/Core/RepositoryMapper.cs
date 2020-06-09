using System;
using System.Collections.Generic;
using System.Linq;
using SentimentAnalyser.Data.Interfaces;

namespace SentimentAnalyser.Data.Core
{
    public class RepositoryMapper : IRepositoryMapper
    {
        private readonly Dictionary<Type, Type> _types;

        public RepositoryMapper(params KeyValuePair<Type, Type>[] types)
        {
            _types = types.ToDictionary(x => x.Key, x => x.Value);
        }

        public Type GetImplementationType(Type type)
        {
            return _types.ContainsKey(type) ? _types[type] : null;
        }
    }
}