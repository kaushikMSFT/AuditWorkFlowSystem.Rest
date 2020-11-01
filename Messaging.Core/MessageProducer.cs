using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Core
{
    public class MessageProducer //: IMessageProducer
    {
        private readonly TopicClient _topicClient;
        private readonly IConfiguration _configuration;
        private const string TOPIC_PATH = "audittopic";
        //private readonly ILogger _logger;

        public MessageProducer(IConfiguration configuration
            //ILogger<ServiceBusTopicSender> logger
            )
        {
            _configuration = configuration;
            //_logger = logger;
            _topicClient = new TopicClient(
                _configuration.GetConnectionString("ServiceBusConnectionString"),
                TOPIC_PATH
            );
        }

        public async Task SendMessage(AuditMessagePayload payload)
        {
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));
            message.UserProperties.Add("AuditId", payload.AuditId);

            try
            {
                await _topicClient.SendAsync(message);
            }
            catch (Exception e)
            {
                // _logger.LogError(e.Message);
                string log = e.Message;
            }
        }
    }
}