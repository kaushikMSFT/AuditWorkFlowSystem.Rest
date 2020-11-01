using Messaging.Core;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientAPI.Services
{
    public class MessageBusListenerService:IHostedService
    {
        //private readonly ILogger logger;
        private readonly IConfiguration configuration;
        //private TopicClient queueClient;
        private SubscriptionClient subscriptionClient;
        private IMessageBusSubscription topicSubscription;
        private const string TOPIC_PATH = "audittopic";
        private const string SUBSCRIPTION_NAME = "audit-client-subscription1";
        private Func<Message, CancellationToken, Task> handler;
        public MessageBusListenerService(IConfiguration configuration, IMessageBusSubscription busTopicSubscription)//, ILoggerFactory loggerFactory)
        {
           // this.logger = loggerFactory.CreateLogger<BusListenerService>();
            this.configuration = configuration;
            subscriptionClient = new SubscriptionClient(
                configuration.GetConnectionString("ServiceBusConnectionString"),
                TOPIC_PATH,
               SUBSCRIPTION_NAME);

            topicSubscription = busTopicSubscription;
            handler = ProcessMessageAsync;
            
        }

       

        public Task StartAsync(CancellationToken cancellationToken)
        {
           // logger.LogDebug($"BusListenerService starting; registering message handler.");
            //this.queueClient = new TopicClient(configuration.GetValue<string>("ServiceBusConnectionString"), configuration.GetValue<string>("ServiceBusQueueName"));

            var messageHandlerOptions = new MessageHandlerOptions(e => {
                ProcessError(e.Exception);
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = 3,
                AutoComplete = false
           };
            this.subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
                 

           // this.topicSubscription.RegisterOnMessageHandlerAndReceiveMessages(handler);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //logger.LogDebug($"BusListenerService stopping.");
            //await this.subscriptionClient.CloseAsync();
            await this.topicSubscription.CloseSubscriptionClientAsync();
        }

        protected void ProcessError(Exception e)
        {
            //logger.LogError(e, "Error while processing queue item in BusListenerService.");
        }

       
        protected async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var data = Encoding.UTF8.GetString(message.Body);
            //MessageInfo item = JsonConvert.DeserializeObject<MessageInfo>(data);

            // ACK the message right away, since we may take a while
            await this.subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            //logger.LogDebug($"{item.CorrelationId} | BusListenerService received item.");

            // Take a while - renewing lock fails despite message being completed
            // Exception does not stop execution of this message handler
            //await Task.Delay(25000);
            //logger.LogDebug($"{item.CorrelationId} | BusListenerService processed item.");
        }
       
    }
}
