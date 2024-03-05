using Store.Notification.Service;
using Store.Notification.Service.Consumers; 

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSignalR();
var app = builder.Build();
 
app.MapHub<NotificationHub>("/notifications");

app.Run();
