using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UCTest
{
    public class TaskTest
    {
        public static void Test2()
        {
			Console.WriteLine ("Hello world");
            var tasks = new List<Task<int>>();

            // Define a delegate that prints and returns the system tick count
            Func<object, int> action = (object obj) =>
            {
                int i = (int)obj;

                // Make each thread sleep a different time in order to return a different tick count
                Thread.Sleep(i * 100);

                // The tasks that receive an argument between 2 and 5 throw exceptions
                if (2 <= i && i <= 5)
                {
                    throw new InvalidOperationException("SIMULATED EXCEPTION");
                }

                int tickCount = Environment.TickCount;
                Console.WriteLine("Task={0}, i={1}, TickCount={2}, Thread={3}", Task.CurrentId, i, tickCount, Thread.CurrentThread.ManagedThreadId);

                return tickCount;
            };

            // Construct started tasks
            for (int i = 0; i < 10; i++)
            {
                int index = i;
                tasks.Add(Task<int>.Factory.StartNew(action, index));
            }

            try
            {
                // Wait for all the tasks to finish.
                Task.WaitAll(tasks.ToArray());

                // We should never get to this point
                Console.WriteLine("WaitAll() has not thrown exceptions. THIS WAS NOT EXPECTED.");
            }
            catch (AggregateException e)
            {
                Console.WriteLine("\nThe following exceptions have been thrown by WaitAll(): (THIS WAS EXPECTED)");
                for (int j = 0; j < e.InnerExceptions.Count; j++)
                {
                    Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[j].ToString());
                }
            }
        }
    

        public async static Task<bool> TestAsync()
        {
			Console.WriteLine("Add SSH Key"); 
			 
			 
            Console.WriteLine("TaskTest");
			Console.WriteLine("Task Test again");
            // Call the method that runs asynchronously.
           // await WaitAsynchronouslyAsync("test is cool");

            string[] messages = { "second test is cool", "Test test is not easy" };
            List<Task> tasks = new List<Task>();
            //var task = await Task.Factory.StartNew<string>((message) => WaitSynchronously(obj));
            foreach(var item in messages)
            {
                tasks.Add(Task.Factory.StartNew(()=> WaitAsynchronouslyAsync(item)));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Please wait...");

            Console.WriteLine("Task Test completed");

            var result = true;
             return result;
        }

        // The following method runs asynchronously. The UI thread is not
        // blocked during the delay. You can move or resize the Form1 window 
        // while Task.Delay is running.
        static async Task WaitAsynchronouslyAsync(string message)
        {
            await Task.Delay(5000);
            Console.WriteLine( string.Format("Finished haha {0}", message));
        }

        // The following method runs synchronously, despite the use of async.
        // You cannot move or resize the Form1 window while Thread.Sleep
        // is running because the UI thread is blocked.
        static  void WaitSynchronously(string message)
        {
            // Add a using directive for System.Threading.
            Thread.Sleep(10000);
            Console.WriteLine(string.Format("lala {0}.",  message));
        }
    }
}
