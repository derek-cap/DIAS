using Autofac;
using DIAS;
using DIASCoreConsole.RabbitMQ;
using Dicom;
using Dicom.IO.Writer;
using Dicom.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIASCoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
                //Trace.AutoFlush = true;
                //Trace.Indent();

                //string path = @"C:\FAWKESBASE\Release\RunTime64";
                //path = Path.Combine(path, @"..\RunTime");
                //Console.WriteLine(Path.GetFullPath(path));

                //ILoggerFactory loggerFactory = new LoggerFactory()
                //    .AddConsole();


                //ILogger logger = loggerFactory.CreateLogger<Program>();
                //logger.LogDebug("Debug");
                //logger.LogInformation("Information");
                //logger.LogTrace("Trace");
                //logger.LogError("Error");

                //Trace.Unindent();
                //    TestJson();
                //ServiceCollection collection = new ServiceCollection();

                //collection.AddSingleton(sp =>
                //{
                //    return loggerFactory.CreateLogger<Program>();
                //});

                //var provider = collection.BuildServiceProvider();

                //var logger = provider.GetRequiredService<ILogger<Program>>();
                //if (logger == null)
                //{
                //    Console.WriteLine("Logger is null");
                //}
                //else
                //{
                //    logger.LogInformation("Try an information.");
                //}
                string message = "ABCD";
                byte[] mb = Encoding.UTF8.GetBytes(message);
                ReadOnlyMemory<byte> memory = new ReadOnlyMemory<byte>(mb);
                Console.WriteLine(memory.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static IEnumerable<DicomDataset> ReadDicomCollection(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            int i = 0;
            foreach (var item in info.GetFiles())
            {
                DicomFile file = DicomFile.Open(item.FullName);
                yield return file.Dataset;
                if (i++ > 10)
                    break;
            }
        }

        static string DicomToJson(DicomDataset dicom)
        {
            return JsonConvert.SerializeObject(dicom, new JsonDicomConverter());
        }

        static DicomDataset DicomFromJson(string dicomString)
        {
            return JsonConvert.DeserializeObject<DicomDataset>(dicomString, new JsonDicomConverter());
        }


        static void TestJson()
        {
            DicomStation station = new DicomStation(StationCategories.Server)
            {
                AE = "a",
                IPAddress = null,
            };

            //string output = JsonConvert.SerializeObject(station);
            //Console.WriteLine(output);

            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter fs = new StreamWriter(@"D:\dias.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(fs))
                {
                    serializer.Serialize(writer, station);
                }
            }

            using (StreamReader sr = File.OpenText(@"C:\Users\fmisx\Desktop\AadProvider.Configuration.json"))
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(sr));
                Parallel.ForEach(o.AsJEnumerable(), item =>
                {
                    Console.WriteLine($"{item}");
                });
            }
        }
    }
}
