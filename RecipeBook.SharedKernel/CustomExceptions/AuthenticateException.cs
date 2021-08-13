using System;

namespace RecipeBook.SharedKernel.CustomExceptions
{
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
