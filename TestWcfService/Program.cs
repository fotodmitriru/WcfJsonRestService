using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfJsonRestService;

namespace TestWcfService
{
    class Program
    {
        static void Main()
        {
            ServiceHost serviceHost = new ServiceHost(typeof(Service1));
            ServiceDebugBehavior debug = serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            try
            {
                if (debug == null)
                {
                    serviceHost.Description.Behaviors.Add(
                        new ServiceDebugBehavior() {IncludeExceptionDetailInFaults = true});
                }
                else
                {
                    // make sure setting is turned ON
                    if (!debug.IncludeExceptionDetailInFaults)
                    {
                        debug.IncludeExceptionDetailInFaults = true;
                    }
                }

                serviceHost.Open();
                Console.WriteLine(@"Служба WCF запущена");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                serviceHost.Close();
            }

            Console.ReadKey();
        }
    }
}