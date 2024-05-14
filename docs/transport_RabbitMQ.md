# RabbitMQ
RabbitMQ is a message broker that implements the Advanced Message Queuing Protocol (AMQP). It is a lightweight, reliable, and scalable enterprise messaging. 

## Installation
You can install RabbitMQ by following website installation step-by-step guide, but if you just want to hit the ground and get it going, using the following two docker commands is the easiest way.  

To install and run it with RabbitMQ management portal:
```
docker pull rabbitmq:3-management
```
```
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:latest
```

The default username and password are the same - `guest`

You should be able to see the following screen if you manage to login.

![rabbitMQ_management_portal](/images/rabbitMQ_management_portal.jpg)