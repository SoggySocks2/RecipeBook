using System;

namespace RecipeBook.SharedKernel.CustomExceptions
{
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
