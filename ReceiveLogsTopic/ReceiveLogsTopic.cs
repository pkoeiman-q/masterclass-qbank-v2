using System.Net.Http.Json;
using System.Text;
using MasterclassApiTest.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
const string createNewKlantRoutingKey = "createNewKlant";
const string klantCreatedRoutingKey = "klantCreated";

channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);
// declare a server-named queue
var queueName = channel.QueueDeclare().QueueName;

if (args.Length < 1)
{
    Console.Error.WriteLine("Usage: {0} [binding_key...]",
                            Environment.GetCommandLineArgs()[0]);
    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
    Environment.ExitCode = 1;
    return;
}

foreach (var bindingKey in args)
{
    channel.QueueBind(queue: queueName,
                      exchange: "topic_logs",
                      routingKey: bindingKey);
}

Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var routingKey = ea.RoutingKey;

    Console.WriteLine($" [x] Received message with routing key '{routingKey}'");
    switch (routingKey)
    {
        case createNewKlantRoutingKey:
            var klant = JsonConvert.DeserializeObject<CreateKlantDTO>(message);
            Console.WriteLine($"Succesfully parsed the Klant JSON, sending request to API to create it...");
            RespondToKlantTopic(klant);
            break;
        case klantCreatedRoutingKey:
            Console.WriteLine($"Received message from API that Klant 'NAME' was created.");
            break;
    }
};
channel.BasicConsume(queue: queueName,
                     autoAck: true,
                     consumer: consumer);

static async void RespondToKlantTopic(CreateKlantDTO klant)
{
    Uri _klantUri = new Uri("https://localhost:7267/api/klanten");
    HttpClient _apiClient = new HttpClient();
    _apiClient.DefaultRequestHeaders.Add("X-Api-Version", "2.0");
    var response = await _apiClient.PostAsJsonAsync(_klantUri, klant);
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();