using Microsoft.Extensions.DependencyInjection;
using MovieApp.UI;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();