using System.Text;
using RabbitMQ.Client;

// Relevant info: https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Queue declaration is idempotent (it won't be made if it already exists)
string queueName = channel.QueueDeclare().QueueName;
channel.ExchangeDeclare("logs", ExchangeType.Fanout);

// Bind the exchange to the queue
channel.QueueBind(queue: queueName,
                  exchange: "logs",
                  routingKey: string.Empty);

var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);
var properties = channel.CreateBasicProperties();
properties.Persistent = true;

channel.BasicPublish(exchange: "logs",
    routingKey: queueName,
    basicProperties: null,
    body: body);

Console.WriteLine($" [x] Sent {message}");
Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();

static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
}