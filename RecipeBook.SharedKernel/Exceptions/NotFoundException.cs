using System;

namespace RecipeBook.SharedKernel.Exceptions
{
    public class NotFoundException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the CustomExceptions.NotFoundException class.
        /// </summary>
        public NotFoundException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the CustomExceptions.NotFoundException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public NotFoundException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the CustomExceptions.NotFoundException class with a specified error message and an inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that triggered this exception.</param>
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the NotFoundException class with a specified name of the queried object and its key.
        /// </summary>
        /// <param name="objectName">Name of the queried object.</param>
        /// <param name="key">The value by which the object is queried.</param>
        public NotFoundException(string key, string objectName)
            : base($"The queried object {objectName} was not found, Key: {key}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the NotFoundException class with a specified name of the queried object, its key,
        /// and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="objectName">Name of the queried object.</param>
        /// <param name="key">The value by which the object is queried.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public NotFoundException(string key, string objectName, Exception innerException)
            : base($"The queried object {objectName} was not found, Key: {key}", innerException)
        {
        }
    }
}
