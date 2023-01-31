using System;
using Microsoft.AspNetCore.Mvc;
using DotNetIot.Repository.SendTelemetryMessages;
namespace MyCoreApi.MyDeviceController;

[ApiController]
[Route("[controller]")]

public class TelemetryController:ControllerBase
{
    [HttpPost("SendTelemeteryMessage")]
    public async Task<string?> SendMessage(string deviceName)
    {
        await TelemetryMessages.SendMessage(deviceName);
        return null;
    }
}
