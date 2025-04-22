using System;
using System.Collections.Generic;
using System.Linq;
using Films.Model;
using NLog;

namespace Films.DAL;

public class Db : ICrud
{
    private readonly Logger _logger;
    private readonly DataBaseContext _context;

    public Db(string connectionString)
    {
        _logger = LogManager.GetCurrentClassLogger();
        _context = new DataBaseContext(connectionString);
    }

    public bool Create(Film film)
    {
        _context.Films.Add(film);
        var result = _context.SaveChanges();
        if (result > 0)
        {
            _logger.Info($"Добавлен фильм {film.Id}");
            return true;
        }
        else
        {
            _logger.Error($"Не удалось добавить фильм {film.Id}");
            return false;
        }
    }

    public bool Update(Film film)
    {
        _context.Films.Update(film);
        var result = _context.SaveChanges();
        if (result > 0)
        {
            _logger.Info($"Обновлен фильм {film.Id}");
            return true;
        }
        else
        {
            _logger.Error($"Не удалось обновить фильм {film.Id}");
            return false;
        }
    }

    public bool Delete(Guid id)
    {
        var film = _context.Films.SingleOrDefault(x => x.Id == id);

        if (film is null)
        {
            _logger.Error($"Не удалось удалить фильм {id}");
            return false;
        }

        _context.Films.Remove(film);
        var result = _context.SaveChanges();
        if (result > 0)
        {
            _logger.Info($"Удален фильм {film.Id}");
            return true;
        }
        else
        {
            _logger.Error($"Не удалось удалить фильм {film.Id}");
            return false;
        }
    }

    public IEnumerable<Film> GetAll()
    {
        _logger.Trace("Получение всех фильмов");
        return _context.Films;
    }
}
