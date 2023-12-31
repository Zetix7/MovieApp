using Microsoft.Extensions.DependencyInjection;
using MovieApp.AplicationServices.Components.DataGenerator;
using MovieApp.UI;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IDataGenerator, DataGenerator>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();