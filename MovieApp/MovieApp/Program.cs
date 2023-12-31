using Microsoft.Extensions.DependencyInjection;
using MovieApp.AplicationServices.Components.DataGenerator;
using MovieApp.DataAccess;
using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;
using MovieApp.UI;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IDataGenerator, DataGenerator>();
services.AddDbContext<MovieAppDbContext>();
services.AddSingleton<IRepository<Movie>, ListRepository<Movie>>();
services.AddSingleton<IRepository<Movie>, SqlRepository<Movie>>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();