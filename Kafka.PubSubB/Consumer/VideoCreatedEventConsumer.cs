using Kafka.PubSubB.Events;
using MassTransit;

namespace Kafka.PubSubB.Consumer
{
    internal class VideoCreatedEventConsumer : IConsumer<VideoCreatedEvent>
    {
        public Task Consume(ConsumeContext<VideoCreatedEvent> context)
        {
            var message = context.Message.Title;
            return Task.CompletedTask;
        }
    }
}
