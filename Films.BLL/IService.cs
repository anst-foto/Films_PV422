using System.Collections.Generic;
using Films.Model;

namespace Films.BLL;

public interface IService
{
    public bool Create(Film film);
    public IEnumerable<Film> GetAll();
    public IEnumerable<Film> GetByTitle(string title);
}
