using System;
using System.Collections.Generic;
using System.Linq;
using Faker;
using Enum = System.Enum;

namespace SentimentAnalyser.Models.UnitTests.Fakes
{
    public static class Helper
    {
        public static int FakeId()
        {
            return RandomNumber.Next(1, (int) 1E7);
        }

        public static float FakeFloat()
        {
            return FakeId() + RandomNumber.Next(1, 99) / 100.0f;
        }

        public static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T) v.GetValue(new Random().Next(v.Length));
        }

        public static IEnumerable<T> FakeEnumerable<T>(Func<T> tMaker, int count = 0)
        {
            return Enumerable.Range(0, count == 0 ? Faker.RandomNumber.Next(2, 15) : count).Select(x => tMaker()).ToList();
        }
    }
}