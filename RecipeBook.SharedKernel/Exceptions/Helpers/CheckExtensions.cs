using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.SharedKernel.Exceptions.Helpers
{
    public static class CheckExtensions
    {
        public static T Null<T>(this ICheckClause checkClause, T input, string parameterName, string message = null)
        {
            if (input is null)
            {
                throw new ArgumentNullException(message ?? $"Required input {parameterName} was null.", (Exception)null);
            }

            return input;
        }

        public static string NullOrEmpty(this ICheckClause checkClause, string input, string parameterName, string message = null)
        {
            checkClause.Null(input, parameterName, message);

            if (input == string.Empty)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        public static IEnumerable<T> NullOrEmpty<T>(this ICheckClause checkClause, IEnumerable<T> input, string parameterName, string message = null)
        {
            checkClause.Null(input, parameterName);

            if (!input.Any())
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        public static Guid NullOrEmpty(this ICheckClause checkClause, Guid? input, string parameterName, string message = null)
        {
            checkClause.Null(input, parameterName);

            if (input == Guid.Empty)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input.Value;
        }

        public static string NullOrWhiteSpace(this ICheckClause checkClause, string input, string parameterName, string message = null)
        {
            checkClause.Null(input, parameterName);

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty or white space.", parameterName);
            }

            return input;
        }

        public static T NotFound<T>(this ICheckClause checkClause, string key, T input, string parameterName)
        {
            checkClause.NullOrEmpty(key, nameof(key));

            if (input is null)
            {
                throw new NotFoundException(key, parameterName);
            }

            return input;
        }

        public static T NotFound<TKey, T>(this ICheckClause checkClause, TKey key, T input, string parameterName) where TKey : struct
        {
            checkClause.Null(key, nameof(key));

            if (input is null)
            {
                throw new NotFoundException(key.ToString(), parameterName);
            }

            return input;
        }
    }
}
