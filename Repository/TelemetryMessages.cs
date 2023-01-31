
using Microsoft.Azure.Devices.Shared;
using PropertiesDto;
using Microsoft.Azure.Devices.Client;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Azure.Devices;

namespace DotNetIot.Repository.SendTelemetryMessages
{
    public class TelemetryMessages
    {
        private static string connectionString="";
        public static RegistryManager? registryManager;
        public static DeviceClient? client=null;
        public static string myDeviceConnection="";
        public static async Task SendMessage(string deviceName)
        {
            try
            {
                registryManager=RegistryManager.CreateFromConnectionString(connectionString);
                var device=await registryManager.GetTwinAsync(deviceName);
                ReportedProperties properties=new ReportedProperties();
                TwinCollection reportedProp;
                reportedProp=device.Properties.Reported;
                client=DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                while(true)
                {
                    var telemetry=new{
                        temperature=reportedProp["pressure"],
                        pressure=reportedProp["temperature"],
                        drift=reportedProp["batteryvoltage"],
                        accuracy=reportedProp["accuracy"]
                    };
                    var telemetryString=JsonConvert.SerializeObject(telemetry);
                    var message=new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(telemetryString));
                    await client.SendEventAsync(message);
                    Console.WriteLine("{0}>Sending message:{1}",DateTime.Now,telemetryString);
                    await Task.Delay(1000);
                } 
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}