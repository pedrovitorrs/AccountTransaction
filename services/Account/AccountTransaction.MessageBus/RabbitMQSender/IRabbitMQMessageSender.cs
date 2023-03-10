﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTransaction.MessageBus.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage<T>(BaseMessage baseMessage, string queueName);
    }
}
