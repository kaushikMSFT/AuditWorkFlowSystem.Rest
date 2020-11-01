using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Messaging.Core
{
   

    public class ServiceBusTopicSubscription :  IMessageBusSubscription
    {
        private readonly IProcessMessage _processData;
        private readonly IConfiguration _configuration;
        private readonly SubscriptionClient _subscriptionClient;
        private const string TOPIC_PATH = "audittopic";
        private const string SUBSCRIPTION_NAME = "audit-client-subscription1";
        public Func<Message, CancellationToken,Task> handler;

        //private readonly ILogger _logger;

        public ServiceBusTopicSubscription(IProcessMessage processData,
            IConfiguration configuration
            //ILogger<ServiceBusTopicSubscription> logger
            )
        {
            _processData = processData;
            _configuration = configuration;
            //_logger = logger;

            _subscriptionClient = new SubscriptionClient(
                _configuration.GetConnectionString("ServiceBusConnectionString"),
                TOPIC_PATH,
                SUBSCRIPTION_NAME);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages(Func<Message, CancellationToken, Task> processMessage )
        {
           var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(processMessage, messageHandlerOptions);
            //_subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
            //this.handler = processMessage;
        }

        //public override Task StartAsync(CancellationToken cancellationToken)
       // {
         //   RegisterOnMessageHandlerAndReceiveMessages().GetAwaiter().GetResult();
            //return base.StartAsync(cancellationToken);
       // }
        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var myPayload = JsonConvert.DeserializeObject<AuditMessagePayload>(Encoding.UTF8.GetString(message.Body));
            _processData.Process(myPayload).GetAwaiter().GetResult();
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }


        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            //_logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            //_logger.LogDebug($"- Endpoint: {context.Endpoint}");
            //_logger.LogDebug($"- Entity Path: {context.EntityPath}");
            //_logger.LogDebug($"- Executing Action: {context.Action}");
            string logger = context.Endpoint + " : " + context.EntityPath +" : "+ context.Action;

            return Task.CompletedTask;
        }

        public async Task CloseSubscriptionClientAsync()
        {
            await _subscriptionClient.CloseAsync();
        }

        public async Task CompleteAsync(string locktoken)
        {
            await _subscriptionClient.CompleteAsync(locktoken);
        }


        // protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //throw new NotImplementedException();

        //await RegisterOnMessageHandlerAndReceiveMessages();
        // }


    }
}

