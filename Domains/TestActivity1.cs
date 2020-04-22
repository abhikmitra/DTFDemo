using DurableTask.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains
{
    public class TestActivity1 : TaskActivity<string, bool>
    {
        protected override bool Execute(TaskContext context, string input)
        {
            
            Console.WriteLine("\n\n***************************\nStarting Task 1, Execution Id = " + context.OrchestrationInstance.ExecutionId + ", Instance id = " + context.OrchestrationInstance.InstanceId +"\n****************\n\n\n");
            Task.Delay(15000).GetAwaiter().GetResult();
            Console.WriteLine("\n\n***************************\nEnding Task 1, Execution Id = " + context.OrchestrationInstance.ExecutionId + ", Instance id = " + context.OrchestrationInstance.InstanceId + "\n****************\n\n\n");
            return true;
        }
    }
}
