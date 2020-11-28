using System;

namespace Logic.Errors
{
    public class AuthenticationFailedException : Exception
    {
        public AuthenticationFailedException()
        {
        }

        public AuthenticationFailedException(string message)
            : base(message)
        {
        }

        public AuthenticationFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}