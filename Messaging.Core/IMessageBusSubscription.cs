using Microsoft.Azure.ServiceBus;
using System;
using System.Threading;
using System.Threading.Tasks;

public interface IMessageBusSubscription
{
    void RegisterOnMessageHandlerAndReceiveMessages(Func<Message, CancellationToken, Task> x );
    Task CloseSubscriptionClientAsync();

    Task CompleteAsync(string locktoken);
}