using Kafka.PubSubA.Events;
using MassTransit;

namespace Kafka.PubSubA.Consumer
{
    internal class VideoDeletedEventConsumer : IConsumer<VideoDeletedEvent>
    {
        public Task Consume(ConsumeContext<VideoDeletedEvent> context)
        {
            var message = context.Message.Title;
            return Task.CompletedTask;
        }
    }
}
