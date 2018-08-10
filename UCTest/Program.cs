using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCTest
{
    class Program
    {
        public static void test()
        {
            String[] values = { null, String.Empty, "true", "TrueString",
                          "False", "    false    ", "-1", "0", "2" };
            foreach (var value in values)
            {
                try
                {
                    Console.WriteLine("Converted '{0}' to {1}.", value,
                                      Convert.ToBoolean(value));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '{0}' to a Boolean.", value);
                }
            }

            int[] testValues = { 2, 1, 0, -1, -2 };
            foreach (var value in testValues)
            {
                try
                {
                    Console.WriteLine("Converted '{0}' to {1}.", value,
                                      Convert.ToBoolean(value));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '{0}' to a Boolean.", value);
                }
            }
        }

        static void Main(string[] args)
        {
            test();
            Console.WriteLine("Hello World!");
            /*
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("LastName", "Chen");
            data.Add("FirstName", "Roger");
            data.Add("Department", "R&D");

            var propertiesInfors = typeof(Person).GetProperties();

            var person = new Person();

            foreach(var property in propertiesInfors)
            {
                property.SetValue(person, data[property.Name]);
            }

            Console.Write($"This is the information of the person {person.FirstName} {person.LastName} in {person.Department}");

            Test1(typeof(Person));
            */

            //Test2();

            //TaskTest.TestAsync().Wait();

            Console.WriteLine("Test completed! Press any key to complete the test.");
            Console.ReadKey();
        }

        static void Test1(Type y)
        {

            var obj = Activator.CreateInstance(y);

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("LastName", "Chen");
            data.Add("FirstName", "Roger");
            data.Add("Department", "R&D");

            var propertiesInfors = obj.GetType().GetProperties();

            foreach (var property in propertiesInfors)
            {
                property.SetValue(obj, data[property.Name]);
            }
        }

        static void Test2()
        {
            var data = new List<Person>();
            data.Add(new Person { country = "AU", State = "NSW", City = "Sydney", LastName = "Chen", FirstName = "Roger", Department = "Marketing" });
            data.Add(new Person { country = "AU", State = "NSW", City = "Sydney", LastName = "Chen", FirstName = "Julia", Department = "Design" });
            data.Add(new Person { country = "AU", State = "VIC", City = "Mel", LastName = "Chen", FirstName = "Eric", Department = "Sale" });
            data.Add(new Person { country = "AU", State = "VIC", City = "Mel", LastName = "Zhang", FirstName = "Eric", Department = "R&D" });
            data.Add(new Person { country = "AU", State = "SA", City = "Adelaid", LastName = "Zhang", FirstName = "Eric", Department = "R&D" });

            var test = data.GroupBy(t => new { t.country, t.State, t.City }, (key, group) => new { key = key, Settings = group.ToList() });

            var result = test.Where(t => t.Settings.Count() > 1); //.Select(t=>t.Setting) ;

            foreach (var item in result)
            {
                //Console.WriteLine($"{item.key.country} {item.key.State} {item.key.City}");
                foreach (var setting in item.Settings)
                {
                    Console.WriteLine("{setting.FirstName} {setting.LastName} {setting.Department}");
                }
            }

            var result2 = test.Where(t => t.Settings.Count() == 1).SelectMany(t => t.Settings).ToList();

            foreach (var item in result2)
            {
                Console.WriteLine("{item.country} {item.State} {item.City} {item.FirstName} {item.LastName}");
            }
            /*
            foreach (var item in result2)
            {
                Console.WriteLine($"{item.key.country} {item.key.State} {item.key.City}");
                foreach (var setting in item.Settings)
                {
                    Console.WriteLine($"{setting.FirstName} {setting.LastName} {setting.Department}");
                }
            }*/


        }

    }

    public class Person
    {
        public string country { get; set; }

        public string State { get; set; }

        public string City { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Department { get; set; }
    }
}

