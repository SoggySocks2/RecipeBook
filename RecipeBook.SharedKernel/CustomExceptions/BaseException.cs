using System;

namespace RecipeBook.SharedKernel.CustomExceptions
{
    public class BaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the CustomExceptions.NotFoundException class.
        /// </summary>
        public BaseException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the CustomExceptions.NotFoundException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public BaseException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the CustomExceptions.NotFoundException class with a specified error message and an inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that triggered this exception.</param>
        public BaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
