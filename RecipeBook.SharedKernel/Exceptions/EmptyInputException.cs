using System;

namespace RecipeBook.SharedKernel.Exceptions
{
    /// <summary>
    /// Thrown when a required parameter is empty or null
    /// </summary>
    public class EmptyInputException : BaseException
    {
        public EmptyInputException()
        {

        }

        public EmptyInputException(string message) : base(message)
        {

        }

        public EmptyInputException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
