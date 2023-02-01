using AccountTransaction.Commom.Core.Exceptions;
using AccountTransaction.Transaction.API.Services.Interface;

namespace AccountTransaction.Transaction.API.Services
{
    public abstract class Service : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="LogicalException"></exception>
        public dynamic LogicalException(string message)
        {
            throw new LogicalException(message);
        }
    }
}
