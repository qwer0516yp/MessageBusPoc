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
                config.Host(massTransitConfig["AmazonSQS:Region"], h =>
                {
                    h.AccessKey(massTransitConfig["AmazonSQS:AccessKey"]);
                    h.SecretKey(massTransitConfig["AmazonSQS:SecretKey"]);
                });
                config.ConfigureEndpoints(context);
            });
            break;

        /******* demo 3, Azure Service Bus **********/
        //Reference: https://masstransit.io/quick-starts/azure-service-bus
        case "AzureServiceBus":
            x.UsingAzureServiceBus((context, config) =>
            {
                config.Host(massTransitConfig["AzureServiceBus:ConnectionString"]);
                config.ConfigureEndpoints(context);
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
