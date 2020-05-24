using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace ConsoleAppSatelliteAssembly
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create array of supported cultures
                string[] cultures = { "en-CA", "en-US", "fr-FR", "ru-RU" };

                string appName = Assembly.GetExecutingAssembly().FullName.Split(',')[0];
                string path = $"{AppContext.BaseDirectory}{appName}.resources.dll";
                Assembly assembly = Assembly.LoadFrom(path);

                ResourceManager resourceManager = new ResourceManager("ConsoleAppSatelliteAssembly.resources.Greeting", assembly);

                string greeting = string.Format("The current culture is {0}.\n{1}",
                                                Thread.CurrentThread.CurrentUICulture.Name,
                                                resourceManager.GetString("HelloString"));

                Console.WriteLine(greeting);

                foreach (string culture in cultures)
                {
                    CultureInfo newCulture = new CultureInfo(culture);
                    Thread.CurrentThread.CurrentCulture = newCulture;
                    Thread.CurrentThread.CurrentUICulture = newCulture;

                    appName = Assembly.GetExecutingAssembly().FullName.Split(',')[0];
                    path = $"{AppContext.BaseDirectory}{appName}.resources.dll";
                    assembly = Assembly.LoadFrom(path);

                    resourceManager = new ResourceManager("ConsoleAppSatelliteAssembly.resources.Greeting", assembly);

                    greeting = string.Format("The current culture is {0}.\n{1}",
                                                    Thread.CurrentThread.CurrentUICulture.Name,
                                                    resourceManager.GetString("HelloString"));

                    Console.WriteLine(greeting);
                }

                Console.ReadLine();
            }
            catch (CultureNotFoundException e)
            {
                Console.WriteLine("Unable to instantiate culture {0}", e.InvalidCultureName);
            }
        }
    }
}
