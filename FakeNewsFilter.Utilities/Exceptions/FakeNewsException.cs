using Microsoft.EntityFrameworkCore;
using System;
namespace FakeNewsFilter.Utilities.Exceptions
{
    public class FakeNewsException : DbUpdateException
    {

        public FakeNewsException() : base()
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
