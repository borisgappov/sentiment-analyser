using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SentimentAnalyser.Business.Interfaces;
using SentimentAnalyser.Business.Managers;
using SentimentAnalyser.Data.Core;
using SentimentAnalyser.Data.Interfaces;
using SentimentAnalyser.Data.Repositories;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser
{
    public static class ServiceExtextensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
        }

        public static void MapRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IRepositoryMapper>(
                new RepositoryMapper(
                    Map<IGenericRepository<Word>, WordRepository>(),
                    Map<IWordRepository, WordRepository>()
                )
            );
        }

        private static KeyValuePair<Type, Type> Map<Type1, Type2>()
        {
            return new KeyValuePair<Type, Type>(typeof(Type1), typeof(Type2));
        }

        public static void AddManagers(this IServiceCollection services)
        {
            services.AddSingleton<IWordManager, WordManager>();
        }
    }
}