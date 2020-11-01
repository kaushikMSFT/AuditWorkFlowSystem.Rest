using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI
{
    public class ClientWorker
    {
        //private readonly ILogger<Worker> logger;
        private int number = 0;

        public ClientWorker()
        {
            //this.logger = logger;
        }

        public async Task DoWork()//CancellationToken cancellationToken
        {
            while (true)//!cancellationToken.IsCancellationRequested
            {
                //Interlocked.Increment(ref number);
                //logger.LogInformation($"Worker printing number: {number}");
                await Task.Delay(1000 * 5);
            }
        }
    }
}
