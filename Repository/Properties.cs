using System;
using Microsoft.Azure.Devices.Shared;
using PropertiesDto;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;

namespace DotNetIot.Repository.DeviceProperties
{
    public class Properties
    {
        private static string connectionString="";
        public static RegistryManager registryManager=RegistryManager.CreateFromConnectionString(connectionString);
        public static DeviceClient? client =null;
        public static string myDeviceConnection="";
        public static async Task AddReportedPropertiesAsync(string deviceName,ReportedProperties properties)
        {
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Please enter a valid device name!");
            }
            else
            {
                client=DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection reportedProperties,connectivity;
                reportedProperties=new TwinCollection();
                connectivity=new TwinCollection();
                connectivity["type"]="cellular";
                reportedProperties["connectivity"]="connectivity";
                reportedProperties["pressure"]=properties.pressure;
                reportedProperties["temperature"]=properties.temperature;
                reportedProperties["batteryvoltage"]=properties.batteryvoltage;
                reportedProperties["accuracy"]=properties.accuracy;
                
                await client.UpdateReportedPropertiesAsync(reportedProperties);
                return;
            
            }
        }
        public static async Task DesiredPropertiesUpdate(string deviceName)
        {
            client=DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device=await registryManager.GetTwinAsync(deviceName);
            TwinCollection desiredproperties, telemetryconfig;
            desiredproperties=new TwinCollection();
            telemetryconfig=new TwinCollection();
            telemetryconfig["frequency"]="50Hz";
            desiredproperties["telemetryconfig"]=telemetryconfig;
            device.Properties.Desired["telemetryconfig"]=telemetryconfig;
            await registryManager.UpdateTwinAsync(device.DeviceId,device,device.ETag);
            return;
        }
        public static async Task<Twin> GetDevicePropertiesAsync(string deviceName)
        {
            var device=await registryManager.GetTwinAsync(deviceName);
            return device;
        }
        public static async Task UpdateDeviceTagProperties(string deviceName)
        {
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Please enter a valid device name!");
            }
            else
            {
                var twin=await registryManager.GetTwinAsync(deviceName);
                var patchData=
                @"{
                    tags:{
                        location:{
                            city:'Paris',
                            plant:'MyIot'
                            }
                        }
                    }";
                client=DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection connectivity;
                connectivity=new TwinCollection();
                connectivity["type"]="cellular";
                await registryManager.UpdateTwinAsync(twin.DeviceId,patchData,twin.ETag);
                return;
            }
        }
    }
}
