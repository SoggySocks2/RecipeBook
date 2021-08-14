using System;

namespace RecipeBook.SharedKernel.CustomExceptions
{
    public class InvalidValueException : BaseException
    {
        public InvalidValueException()
        {

        }

        public InvalidValueException(string message) : base(message)
        {

        }

        public InvalidValueException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
