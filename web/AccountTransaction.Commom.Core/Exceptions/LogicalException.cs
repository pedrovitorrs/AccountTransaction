using Microsoft.AspNetCore.Mvc;

namespace AccountTransaction.Commom.Core.Exceptions
{
    public class LogicalException : Exception
    {
        public LogicalException(string message) : base(message)
        { }
    }
}
