using System;
using AutoMapper;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser.Models.Converters
{
    public static class Mapper
    {
        private static IMapper _mapper;

        public static void Initialize()
        {
            _mapper = CreateConfiguration().CreateMapper();
        }


        public static TDest MapTo<TDest>(this object src)
        {
            return src == null
                ? default
                : (TDest) _mapper.Map(src, src.GetType(), typeof(TDest));
        }

        private static MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Word, WordModel>()
                    .ForMember(dst => dst.Sentiment,
                        opt => opt.MapFrom(src => Convert.ToInt32(src.Sentiment * 10)));

                cfg.CreateMap<WordModel, Word>()
                    .ForMember(dst => dst.Sentiment, opt => opt.MapFrom(src => (float) src.Sentiment / 10f));
            });

            config.AssertConfigurationIsValid();

            return config;
        }
    }
}