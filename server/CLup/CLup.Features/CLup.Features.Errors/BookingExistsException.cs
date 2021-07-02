using System;

namespace CLup.Errors
{
    public class BookingExistsException : Exception
    {
          public BookingExistsException()
        {
        }

        public BookingExistsException(string message)
            : base(message)
        {
        }

        public BookingExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}