using Microsoft.AspNetCore.Mvc;

namespace AccountTransaction.WebUI.Configuration.Exceptions
{
    public class LogicalException : Exception
    {
        public LogicalException(string message) : base(message)
        { }
    }
}
