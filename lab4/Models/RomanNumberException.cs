using System;

namespace Roman
{
    public class RomanNumberException : Exception
    {
        public RomanNumberException() { }
        public RomanNumberException(string message) : base(message) { }

    }
}