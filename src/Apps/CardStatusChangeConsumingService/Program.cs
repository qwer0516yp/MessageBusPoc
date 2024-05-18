using CardStatusChangeConsumingService;
using MassTransit;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);

//configure MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CardStatusChangeConsumer>(c =>
    {
        c.UseMessageRetry(r =>
        {
            r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        });
    });

    var entryAssembly = Assembly.GetEntryAssembly();
    x.AddSagaStateMachines(entryAssembly);
    x.AddSagas(entryAssembly);
    x.AddActivities(entryAssembly);

    var massTransitConfig = builder.Configuration.GetSection("MassTransit");
    var transportType = massTransitConfig?.GetValue<string>("TransportType");

    //Use one of the transports below
    switch (transportType)
    {
        /******* demo 2, AmazonSQS **********/
        //Reference: https://masstransit.io/quick-starts/amazon-sqs
        case "AmazonSQS":
            x.UsingAmazonSqs((context, config) =>
            {
                config.Host(builder.Configuration["AmazonSQS:Region"], h =>
                {
                    h.AccessKey(builder.Configuration["AmazonSQS:AccessKey"]);
                    h.SecretKey(builder.Configuration["AmazonSQS:SecretKey"]);
                });
                config.ConfigureEndpoints(context);
            });
            break;

        /******* demo 3, Azure Service Bus **********/
        //Reference: https://masstransit.io/quick-starts/azure-service-bus
        case "AzureServiceBus":
            x.UsingAzureServiceBus((context, config) =>
            {
                config.Host(builder.Configuration["AzureServiceBus:ConnectionString"],
                    h => 
                    {
                        h.TransportType = Azure.Messaging.ServiceBus.ServiceBusTransportType.AmqpWebSockets;
                    });

                config.ConfigureEndpoints(context);
            });
            break;

        /******* demo 4, RabbitMQ **********/
        //Reference: https://masstransit.io/quick-starts/rabbitmq
        case "RabbitMQ":
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
            break;

        /******** demo 1, InMemory ***********/
        case "InMemory":
        default:
            x.UsingInMemory((context, config) =>
            {
                config.ConfigureEndpoints(context);
            });
            transportType = "InMemory";
            break;
    }

    // Print the transportType
    Console.WriteLine($"============= TransportType applied: {transportType} ===============");
});

var host = builder.Build();
host.Run();
