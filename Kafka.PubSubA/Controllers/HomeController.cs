using Kafka.PubSubA.Events;
using MassTransit;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Kafka.PubSubA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebAppController : ControllerBase
    {
        private readonly ITopicProducer<VideoCreatedEvent> _topicProducer;

        public WebAppController(ITopicProducer<VideoCreatedEvent> topicProducer)
        {
            _topicProducer = topicProducer;
        }

        /// <summary>
        /// Send create event
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost("{title}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync(string title)
        {
            // Another way to access the _topicProducer
            // var _topicProducer = HttpContext.RequestServices.GetRequiredService<ITopicProducer<VideoCreatedEvent>>();

            await _topicProducer.Produce(new VideoCreatedEvent
            {
                Title = $"{title}"
            });

            return Ok(title);
        }
    }
}