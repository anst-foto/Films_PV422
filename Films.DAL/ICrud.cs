using System;
using System.Collections.Generic;
using Films.Model;

namespace Films.DAL;

public interface ICrud
{
    public bool Create(Film film);
    public bool Update(Film film);
    public bool Delete(Guid id);

    public IEnumerable<Film> GetAll();
}
