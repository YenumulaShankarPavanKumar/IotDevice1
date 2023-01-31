using System;
using DotNetIot.Repository.DeviceProperties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices.Shared;
using PropertiesDto;

namespace MyCoreApi.PropertiesController;

[ApiController]
[Route("[controller]")]
public class DevicePropertiesController:ControllerBase
{
   [HttpPut("UpdateDeviceReportedProperties")]
   
    public async Task<string?> UpdateDeviceReportedProperties(string deviceName,ReportedProperties properties)
    {
        await Properties.AddReportedPropertiesAsync(deviceName,properties);
        return null;
    }
   [HttpPut("UpdateDeviceDesiredProperties")]
   public async Task<string?> UpdateDeviceDesiredProperties(string deviceName)
   {
     await Properties.DesiredPropertiesUpdate(deviceName);
     return null;
   }
   [HttpPut("UpdateIotDeviceTagProperties")]
   public async Task<string?> UpdateIotDeviceTagProperties(string deviceName)
   {
     await Properties.UpdateDeviceTagProperties(deviceName);
     return null;
   }
   [HttpGet("GetIotDeviceProperties")]
   public async Task<Twin> GetIotDevice(string deviceName)
   {
     var device=await Properties.GetDevicePropertiesAsync(deviceName);
     return device;
   }
}