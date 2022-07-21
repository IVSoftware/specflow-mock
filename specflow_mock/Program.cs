using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TechTalk.SpecFlow;

namespace specflow_mock
{
    class Program
    {
        static void Main(string[] args)
        {
            // Normal dictionary
            var normal = new Dictionary<string, object>
            {
                { "Apple", "Red" },
                { "Orange", "Orange" },
            };
            // Calls CollectionExtensions.GetValueOrDefault()
            Console.WriteLine(normal.GetValueOrDefault("Apple"));

            // SpecFlowContext dictionary
            var fake = new CustomSpecFlowContext
            {
                { "Apple", "Red" },
                { "Orange", "Orange" },
            };
            Console.WriteLine(fake.GetValueOrDefault("Apple"));
            Console.WriteLine(fake.GetValueOrDefault("Apple", (string)null));
            Console.WriteLine(fake.GetValueOrDefault("Apple", "Fake value C"));
        }
    }

    class CustomSpecFlowContext : SpecFlowContext { }

    static class Extensions
    {
        public static object GetValueOrDefault<TKey>(
            this SpecFlowContext dictionary, 
            TKey key) where TKey : notnull
        {
            return "Fake value A";
        }

        public static object GetValueOrDefault<TKey, TValue>(
            this SpecFlowContext dictionary, 
            TKey key, 
            [AllowNull] TValue defaultValue)
        {
            if(defaultValue is string valid)
            {
                return valid;
            }
            return "Fake value B";
        }
    }
}
