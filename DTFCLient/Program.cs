using Domains;
using DurableTask.Core;
using DurableTask.ServiceBus;
using DurableTask.ServiceBus.Settings;
using DurableTask.ServiceBus.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DTFCLient
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrchestrationServiceInstanceStore store = new AzureTableInstanceStore("TestTaskHub", "UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://127.0.0.1;");
            var settings = new ServiceBusOrchestrationServiceSettings();
            var service = new ServiceBusOrchestrationService("<servicebus>", "TestTaskHub", store, null, settings);
            var client = new TaskHubClient(service);
            try
            {
                var instance = client.CreateOrchestrationInstanceAsync(typeof(TestOrchestration), "InstanceId11", "Test Input").Result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            while(true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
