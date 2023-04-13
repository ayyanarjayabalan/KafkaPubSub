# Event driven architecture (EDA) - Kafka Publisher and Subscriber microservices using asp.net core web api
Event drive architecture using kafka, zookeeper, rabbitmq, asp.net core web api

### Install zookeeper, kafka and kafkadrop in docker container using docker decompose file
From the command prompt, go to the docker-decompose.yaml file folder and run below command to install the above services in docker container

```
docker compose up -d
```

### kafka commands
```
Download from https://kafka.apache.org/downloads and extract it to local disk. 
```

### Send messages

```
.\bin\windows\kafka-console-producer.bat --broker-list localhost:9092 --topic TestTopic
```
```
.\bin\windows\kafka-console-producer.bat --bootstrap-server localhost:9092 --topic TestTopic
```

### Consume messages
```
.\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic TestTopic  --from-beginning
```
```
.\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic TestTopic
```


### Consume messages (from micro services)
```
.\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic VideoDeletedEvent  --from-beginning
```
```
.\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic VideoCreatedEvent  --from-beginning
```



