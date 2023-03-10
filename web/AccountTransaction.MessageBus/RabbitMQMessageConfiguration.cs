using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTransaction.MessageBus
{
    public class RabbitMQMessageConfiguration
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        public IConnection _connection { get; set; }

        public RabbitMQMessageConfiguration(string hostName, string password, string userName)
        {
            _hostName = hostName;
            _password = password;
            _userName = userName;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
        }
    }
}
