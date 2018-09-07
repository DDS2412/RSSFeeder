using System;
using System.Collections.Generic;
using System.Linq;

namespace RSSFeeder.Utils
{
    static class ReflectionHelper
    {
        /// <summary>
        /// Returns a list of elements that implement a particular type
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> CreateAllInstancesOf<T>()
        {
            return typeof(ReflectionHelper).Assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract && t.IsClass)
                .Select(t => (T)Activator.CreateInstance(t));
        }
    }
}
