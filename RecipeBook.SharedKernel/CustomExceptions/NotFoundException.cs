using System;

namespace RecipeBook.SharedKernel.CustomExceptions
{
    /// <summary>
    /// Thrown when a record can't be found
    /// </summary>
    public class NotFoundException : BaseException
    {
        public NotFoundException()
        {

        }

        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
