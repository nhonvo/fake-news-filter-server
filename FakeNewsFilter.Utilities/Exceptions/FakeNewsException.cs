using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace FakeNewsFilter.Utilities.Exceptions
{
    public class FakeNewsException : Exception
    {

        public FakeNewsException(string message)  : base(message)
        {
        }

        public FakeNewsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
