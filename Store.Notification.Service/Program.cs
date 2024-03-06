using Store.Notification.Service.Consumers;
 
var builder = WebApplication.CreateBuilder(args);  
builder.Services.Configure<HostOptions>(hostOptions =>
{
    hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});
builder.Services.AddHostedService<Worker>(); 
var app = builder.Build(); 
 
app.Run();