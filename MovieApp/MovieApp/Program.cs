using Microsoft.Extensions.DependencyInjection;
using MovieApp.AplicationServices.Components.DataGenerator;
using MovieApp.AplicationServices.Components.FileCreator.CsvFile;
using MovieApp.AplicationServices.Components.FileCreator.XmlFile;
using MovieApp.DataAccess;
using MovieApp.DataAccess.Data.Entities;
using MovieApp.DataAccess.Data.Repositories;
using MovieApp.UI;
using MovieApp.UI.Menu;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IDataGenerator, DataGenerator>();
services.AddDbContext<MovieAppDbContext>();
services.AddSingleton<IRepository<Movie>, ListRepository<Movie>>();
services.AddSingleton<IRepository<Movie>, SqlRepository<Movie>>();
services.AddSingleton<ICsvCreator, CsvCreator>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlCreator, XmlCreator>();
services.AddSingleton<IXmlReader, XmlReader>();
services.AddSingleton<IMainMenu, MainMenu>();
services.AddSingleton<IMenu<Movie>, MoviesMenu>();
services.AddSingleton<IMenu<Artist>, ArtistsMenu>();
services.AddSingleton<IRepository<Artist>, ListRepository<Artist>>();
services.AddSingleton<IRepository<Artist>, SqlRepository<Artist>>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();