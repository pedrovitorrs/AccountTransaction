using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTransaction.MessageBus
{
    public class Routing_Keys
    {
        public const string TRANSACTION_CREATED = "TRANSACTION_CREATED";
        public const string TRANSACTION_UPDATED = "TRANSACTION_UPDATED";
    }
}
