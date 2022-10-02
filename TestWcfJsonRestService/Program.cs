using System;
using WcfJsonRestService.Entities;

namespace TestWcfJsonRestService
{
    class Program
    {
        static void Main()
        {
            Person p = new Person()
            {
                NationalitiesStrings = new []{"RUS", "MDL"},
                Id = 1
            };

            Console.ReadKey();
        }
    }
}
