using System.Threading.Tasks;

namespace Messaging.Core
{
    public interface IMessageProducer
    {
        Task SendMessage(AuditMessagePayload payload);
    }
}