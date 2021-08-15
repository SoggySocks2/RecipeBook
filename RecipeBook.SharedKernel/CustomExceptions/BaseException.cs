using System;

namespace RecipeBook.SharedKernel.CustomExceptions
{
    /// <summary>
    /// Inherited by all custom exceptions to ensure a common entry point for shared logic
    /// </summary>
    public abstract class BaseException : Exception
    {
        public BaseException()
        {

        }

        public BaseException(string message) : base(message)
        {

        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
