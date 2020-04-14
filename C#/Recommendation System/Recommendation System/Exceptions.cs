using System;

namespace RSExceptions
{
    // If users attribute is zero or less
    public class InvalidUsersValueException : Exception
    {
        public InvalidUsersValueException()
        {
        }

        public InvalidUsersValueException(string message) : base(message)
        {
        }

        public InvalidUsersValueException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    // If movies attribute is zero or less
    public class InvalidMoviesValueException : Exception
    {
        public InvalidMoviesValueException()
        {
        }

        public InvalidMoviesValueException(string message) : base(message)
        {
        }

        public InvalidMoviesValueException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    // If neighbors attribute is zero or less
    public class InvalidNeighborsValueException : Exception
    {
        public InvalidNeighborsValueException()
        {
        }

        public InvalidNeighborsValueException(string message) : base(message)
        {
        }

        public InvalidNeighborsValueException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    // If RecommendedMoviesCount attribute is zero or less
    public class InvalidTopNrecommendationsValueException : Exception
    {
        public InvalidTopNrecommendationsValueException()
        {
        }

        public InvalidTopNrecommendationsValueException(string message) : base(message)
        {
        }

        public InvalidTopNrecommendationsValueException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

