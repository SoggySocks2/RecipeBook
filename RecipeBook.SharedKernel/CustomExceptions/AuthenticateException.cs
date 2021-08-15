using System;

namespace RecipeBook.SharedKernel.CustomExceptions
{
    /// <summary>
    /// Thrown when user authentication fails
    /// </summary>
    public class AuthenticateException : BaseException
    {
        public AuthenticateException()
        {

        }

        public AuthenticateException(string message) : base(message)
        {

        }

        public AuthenticateException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
