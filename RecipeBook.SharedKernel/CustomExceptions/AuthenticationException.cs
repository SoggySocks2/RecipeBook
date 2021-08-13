using System;

namespace RecipeBook.SharedKernel.CustomExceptions
{
    public class AuthenticationException : BaseException
    {
        public AuthenticationException()
        {

        }

        public AuthenticationException(string message) : base(message)
        {

        }

        public AuthenticationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
