using System.Text;
using RabbitMQ.Client;

// Relevant info: https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Queue declaration is idempotent (it won't be made if it already exists)
string queue_name = "task_queue";
channel.QueueDeclare(queue: queue_name,
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);
var properties = channel.CreateBasicProperties();
properties.Persistent = true;

channel.BasicPublish(exchange: string.Empty,
    routingKey: queue_name,
    basicProperties: null,
    body: body);

Console.WriteLine($" [x] Sent {message}");
Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();

static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
}