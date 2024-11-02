using web_app_csharp.Attributes;
using web_app_csharp.Models;

namespace web_app_csharp.Services;


public delegate void WorkerCreatedEventHandler(object sender, Worker worker);


[ScopedService]
public class WorkerLoggingService
{
    public void OnWorkerCreated(object sender, Worker worker)
    {
        Console.WriteLine($"[LOG] New Worker Created: Name = {worker.Name}, Address = {worker.Address}, DeptId = {worker.DeptId}");
    }
}