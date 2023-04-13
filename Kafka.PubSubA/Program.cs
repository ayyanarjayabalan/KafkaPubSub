using Kafka.PubSubA.Consumer;
using Kafka.PubSubA.Events;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));

    x.AddRider(rider =>
    {
        rider.AddConsumer<VideoDeletedEventConsumer>();
        rider.AddProducer<VideoCreatedEvent>(nameof(VideoCreatedEvent));

        rider.UsingKafka((context, k) =>
        {
            k.Host("localhost:9092");

            k.TopicEndpoint<VideoDeletedEvent>(nameof(VideoDeletedEvent), GetUniqueName(nameof(VideoDeletedEvent)), e =>
            {
                // e.AutoOffsetReset = AutoOffsetReset.Latest;
                //e.ConcurrencyLimit = 3;
                e.CheckpointInterval = TimeSpan.FromSeconds(10);
                e.ConfigureConsumer<VideoDeletedEventConsumer>(context);

                e.CreateIfMissing(t =>
                {
                    //t.NumPartitions = 2; //number of partitions
                    //t.ReplicationFactor = 1; //number of replicas
                });
            });
        });
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static string GetUniqueName(string eventName)
{
    string hostName = Dns.GetHostName();
    string callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
    return $"{hostName}.{callingAssembly}.{eventName}";
}
