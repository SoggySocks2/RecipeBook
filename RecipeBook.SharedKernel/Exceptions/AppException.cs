﻿using System;

namespace RecipeBook.SharedKernel.Exceptions
{
    public class AppException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the CustomExceptions.AppException class.
        /// </summary>
        public AppException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the CustomExceptions.AppException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AppException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the CustomExceptions.AppException class with a specified error message and an inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that triggered this exception.</param>
        public AppException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
