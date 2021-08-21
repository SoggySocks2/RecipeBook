using System;

namespace RecipeBook.SharedKernel.CustomExceptions
{
    /// <summary>
    /// Thrown when attempting to add an item that already exists
    /// </summary>
    public class ExistsException : BaseException
    {
        public ExistsException()
        {

        }

        public ExistsException(string message) : base(message)
        {

        }

        public ExistsException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
