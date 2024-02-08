using System.Text;
using RabbitMQ.Client;

// Relevant info: https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Queue declaration is idempotent (it won't be made if it already exists)
channel.QueueDeclare(queue: "hello",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

const string message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish(exchange: string.Empty, routingKey: "hello", basicProperties: null, body: body);
Console.WriteLine($" [x] Sent {message}");
Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();