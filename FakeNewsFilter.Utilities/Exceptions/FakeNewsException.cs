using Microsoft.EntityFrameworkCore;
using System;
namespace FakeNewsFilter.Utilities.Exceptions
{
    public class FakeNewsException : DbUpdateException
    {

        public FakeNewsException(string v) : base()
        {
        }


        public FakeNewsException(string message, int topicId)
            : base(message)
        {
        }


        public FakeNewsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
