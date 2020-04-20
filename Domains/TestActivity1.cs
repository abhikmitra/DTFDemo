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
            Console.WriteLine("Running Task, Execution Id = " + context.OrchestrationInstance.ExecutionId + ", Instance id = " + context.OrchestrationInstance.InstanceId);
            return true;
        }
    }
}
