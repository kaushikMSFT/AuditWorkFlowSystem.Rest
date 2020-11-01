using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace DocumentAPI.Factories
{
    public interface IStorageConnectionFactory
    {
        Task<CloudBlobContainer> GetContainer();
    }
}