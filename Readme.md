# ZTP Core

## RawRabbit

> RawRabbit is a modern .NET framework for communication over RabbitMQ. The modular design and middleware oriented architecture makes the client highly customizable while providing sensible default for topology, routing and more. 

### Docker configuration

#### RabbitMQ

    docker run -d -p 8080:15672 -p 5672:5672 rabbitmq:3-management
