using System;
using System.Collections.Generic;
using System.Linq;
using Films.Model;

namespace Films.DAL;

public class Db : ICrud
{
    private readonly DataBaseContext _context;

    public Db(string connectionString)
    {
        _context = new DataBaseContext(connectionString);
    }

    public bool Create(Film film)
    {
        _context.Films.Add(film);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Update(Film film)
    {
        _context.Films.Update(film);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Delete(Guid id)
    {
        var film = _context.Films.SingleOrDefault(x => x.Id == id);

        if (film is null) return false;

        _context.Films.Remove(film);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public IEnumerable<Film> GetAll()
    {
        return _context.Films;
    }
}
