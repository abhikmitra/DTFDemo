using Domains;
using DurableTask.Core;
using DurableTask.ServiceBus;
using DurableTask.ServiceBus.Settings;
using DurableTask.ServiceBus.Tracking;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DTFDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            startAsync();
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        private static async Task startAsync()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://127.0.0.1;");

            IOrchestrationServiceInstanceStore store = new AzureTableInstanceStore("TestTaskHub", "UseDevelopmentStorage=true;DevelopmentStorageProxyUri= http://127.0.0.1;");
            var settings = new ServiceBusOrchestrationServiceSettings();
            var service = new ServiceBusOrchestrationService("<servicebus>", "TestTaskHub", store, null, settings);
            TaskHubWorker hubWorker = new TaskHubWorker(service);
            hubWorker.AddTaskOrchestrations(typeof(TestOrchestration));
            hubWorker.AddTaskActivities(typeof(TestActivity1));
            await service.CreateIfNotExistsAsync();
            await hubWorker.StartAsync();
        }
    }
}
