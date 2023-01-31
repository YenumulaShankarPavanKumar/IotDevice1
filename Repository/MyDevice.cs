using System;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;


namespace ProjectIot.Repository
{
    public class MyDevice
    {
        public static RegistryManager? registryManager;
        private static string connectionString="";
        //static Device device;
        public static async Task AddDeviceAsync(string deviceName)
        {
            Device device;
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("enter a device name");
            }
            registryManager=RegistryManager.CreateFromConnectionString(connectionString);
            device=await registryManager.AddDeviceAsync(new Device(deviceName));
            return ;
        }

        public static async Task<Device> GetDeviceAsync(string deviceId)
        {
            Device device;
            registryManager=RegistryManager.CreateFromConnectionString(connectionString);
            device=await registryManager.GetDeviceAsync(deviceId);
            return device;
        }
         public static async Task DeleteDeviceAsync(string deviceId)
        {
            registryManager=RegistryManager.CreateFromConnectionString(connectionString);
            await registryManager.RemoveDeviceAsync(deviceId);
        }
        public static async Task<Device> UpdateDeviceAsync(string deviceId)
        {
            if(string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("submit device id");
            }
            Device device=new Device(deviceId);
            registryManager=RegistryManager.CreateFromConnectionString(connectionString);
            device=await registryManager.GetDeviceAsync(deviceId);
            device.StatusReason="Updated";
            device=await registryManager.UpdateDeviceAsync(device);
            return device;
        }
    }
}