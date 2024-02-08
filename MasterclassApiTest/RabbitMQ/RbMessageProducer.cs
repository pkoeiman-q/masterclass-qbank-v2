using RabbitMQ.Client;
using System.Text;
using Serilog;
using System.Threading.Channels;

namespace MasterclassApiTest.RabbitMQ
{
    public class RbMessageProducer
    {
        private readonly ConnectionFactory _factory = new ConnectionFactory { HostName = "localhost" };

        public void ProduceMessage(string message = "Hello, world!")
        {
            using IConnection _connection = _factory.CreateConnection();
            using IModel _channel = _connection.CreateModel();

            string routingKey = "test";
            string exchangeName = "topic_logs";

            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: exchangeName,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);

            Log.Information($"RabbitMQ produced the following message: '{routingKey} - {message}'");
        }
    }
}
