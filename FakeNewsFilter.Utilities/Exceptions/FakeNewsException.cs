using System;
namespace FakeNewsFilter.Utilities.Exceptions
{
    public class FakeNewsException : Exception
    {

        public FakeNewsException()
        {
        }


        public FakeNewsException(string message)
            : base(message)
        {
        }


        public FakeNewsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
