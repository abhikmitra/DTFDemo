using DurableTask.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains
{
    public class TestOrchestration : TaskOrchestration<bool, string>
    {
        public override async Task<bool> RunTask(OrchestrationContext context, string input)
        {
            Console.WriteLine("Is Replaying =" + context.IsReplaying+" InstanceId =" + context.OrchestrationInstance.InstanceId + " Execution ID =" + context.OrchestrationInstance.ExecutionId);
            Console.WriteLine("Running Orchestration");
            var firstRetryInterval = TimeSpan.FromSeconds(1);
            var maxNumberOfAttempts = 5;
            var backoffCoefficient = 1.1;

            var options = new RetryOptions(firstRetryInterval, maxNumberOfAttempts)
            {
                BackoffCoefficient = backoffCoefficient,
                Handle = HandleError
            };
            bool result = false;
            try
            {
                Console.WriteLine("Schedule with Retry ");
                var task1 = context.ScheduleWithRetry<bool>(typeof(TestActivity1), options, "Test Input1");
                Console.WriteLine("Schedule with Retry Complete - Task 1");
                var task2 = context.ScheduleWithRetry<bool>(typeof(TestActivity2), options, "Test Input2");
                Console.WriteLine("Schedule with Retry Complete - Task 2");
                await Task.WhenAll(task1, task2);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in Orchestration" + e.ToString());
            }

            Console.WriteLine("Orchestration Finished");
            return result;
        }

        private bool HandleError(Exception e)
        {
            Console.WriteLine(e.ToString());
            return true;
        }
    }
}
