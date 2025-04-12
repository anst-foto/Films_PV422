using System.Collections.Generic;
using System.Linq;
using Films.DAL;
using Films.Model;

namespace Films.BLL;

public class Service : IService
{
    private ICrud _crud;

    public Service(ICrud crud)
    {
        _crud = crud;
    }

    public bool Create(Film film)
    {
        return _crud.Create(film);
    }

    public IEnumerable<Film> GetAll()
    {
        return _crud.GetAll();
    }

    public IEnumerable<Film> GetByTitle(string title)
    {
        var films = _crud.GetAll();
        return films.Where(f => f.Title.Contains(title, System.StringComparison.CurrentCultureIgnoreCase));
    }
}
