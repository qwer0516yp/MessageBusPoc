# MessageBusPoc
This is a dotnet 8 proof-of-concept project to showcase a loosely coupled distributed message processing system. Performance benchmark might be subject to individual machine and network speed. 

It has a dependency on [MassTransit](https://github.com/MassTransit). For more information of MassTransit usage, please check out [MassTransit Website](https://masstransit.io/).  

## Transport
The transport is the core of any message bus. Some mature and common choices can be either cloud based web services (i.e. AWS SQS/SNS, Azure Service Bus) or installed software service (i.e. Apache Kafka, RabbitMQ). Refer to the docs/transport_* sections. 

