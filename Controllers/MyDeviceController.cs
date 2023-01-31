using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using ProjectIot.Repository;
namespace ProjectIot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyDeviceController:ControllerBase
    {
         [HttpPost("AddIotDevice")]
    public async Task<string?> AddDevice(string deviceName)
    {
        await ProjectIot.Repository.MyDevice.AddDeviceAsync(deviceName);
        return null;
    }
    [HttpGet("GetIotDevice")]
    public async Task<Device> GetIotDevice(string deviceName)
    {
        Device device;
        device=await ProjectIot.Repository.MyDevice.GetDeviceAsync(deviceName);
        return device;
    }
    [HttpDelete("DeleteIotDevice")]
    public async Task<string?> DeleteIotDevice(string deviceName)
    {
        await ProjectIot.Repository.MyDevice.DeleteDeviceAsync(deviceName);
        return null;
    }
    [HttpPut("UpdatedIotDevice")]
    public async Task<Device> UpdateDeviceProperties(string deviceName)
    {
        Device device;
        device=await ProjectIot.Repository.MyDevice.UpdateDeviceAsync(deviceName);
        return device;
    }
    }
}