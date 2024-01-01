using MovieApp.DataAccess.Data.Entities;

namespace MovieApp.UI.Menu;

public interface IMenu<T> where T : class, IEntity
{
    void LoadMenu();
}
