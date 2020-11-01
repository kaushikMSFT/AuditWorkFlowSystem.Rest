using ClientAPI.MessageProcessors;
using Messaging.Core;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientAPI.EventSubscriptions
{
    public class ClientSubscription : BackgroundService, IMessageBusSubscription
    {
        private readonly IConfiguration configuration;
        //private TopicClient queueClient;
        private SubscriptionClient subscriptionClient;
        private IMessageBusSubscription topicSubscription;
        private const string TOPIC_PATH = "audittopic";
        private const string SUBSCRIPTION_NAME = "audit-client-subscription1";
        private Func<Message, CancellationToken, Task> handler;
        private IProcessMessage _processData;
        public ClientSubscription(IConfiguration configuration, IProcessMessage processData)
        {
            this.configuration = configuration;
            //subscriptionClient = new SubscriptionClient(
            //    configuration.GetConnectionString("ServiceBusConnectionString"),
            //    TOPIC_PATH,
             //  SUBSCRIPTION_NAME);
            _processData = processData;
           // _processData = new MessageConsumer();

            topicSubscription = new ServiceBusTopicSubscription(processData, configuration);// busTopicSubscription;
            //handler = ProcessMessageAsync;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,
                MaxAutoRenewDuration = TimeSpan.FromHours(2)
                
            };
            
            //_subscriptionClient.RegisterMessageHandler(processMessage, messageHandlerOptions);
            //subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
            RegisterOnMessageHandlerAndReceiveMessages(ProcessMessagesAsync);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            //_logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            //_logger.LogDebug($"- Endpoint: {context.Endpoint}");
            //_logger.LogDebug($"- Entity Path: {context.EntityPath}");
            //_logger.LogDebug($"- Executing Action: {context.Action}");
            string logger = context.Endpoint + " : " + context.EntityPath + " : " + context.Action;
            //subscriptionClient.lo
            return Task.CompletedTask;
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var myPayload = JsonConvert.DeserializeObject<AuditMessagePayload>(Encoding.UTF8.GetString(message.Body));
            await _processData.Process(myPayload);//.GetAwaiter().GetResult();
            //Task.Delay(60000);
            await CompleteAsync(message.SystemProperties.LockToken);
             //subscriptionClient.CompleteAsync(message.SystemProperties.LockToken).GetAwaiter().GetResult();
        }

       

        public Task CloseSubscriptionClientAsync()
        {
            throw new NotImplementedException();
        }

        public void RegisterOnMessageHandlerAndReceiveMessages(Func<Message, CancellationToken, Task> x)
        {
            topicSubscription.RegisterOnMessageHandlerAndReceiveMessages(ProcessMessagesAsync);
        }

        public async Task CompleteAsync(string locktoken)
        {
            await topicSubscription.CompleteAsync(locktoken);
        }
    }
}
