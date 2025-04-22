using System;
using System.Collections.Generic;
using System.Linq;
using Films.Model;
using Microsoft.Extensions.Caching.Memory;

namespace Films.BLL;

public class ServiceWithCache : IService
{
    private readonly IService _service;
    private readonly IMemoryCache _cache;

    public ServiceWithCache(IService service)
    {
        _service = service;
        _cache = new MemoryCache(new MemoryCacheOptions());
    }

    public bool Create(Film film)
    {
        var result = _service.Create(film);

        if (result)
        {
            _cache.Set(film.Id, film); //FIXME: Не имеет смысла
        }

        return result;
    }

    public IEnumerable<Film> GetAll()
    {
        if (_cache.TryGetValue<IEnumerable<Film>>("films", out var _films))
            return _films!;

        var films = _service.GetAll();
        _cache.Set("films", films, TimeSpan.FromMinutes(5));

        return films;
    }

    public IEnumerable<Film> GetByTitle(string title)
    {
        return _cache.TryGetValue<IEnumerable<Film>>("films", out var films)
            ? films!.Where(f => f.Title.Contains(title, System.StringComparison.CurrentCultureIgnoreCase))
            : _service.GetByTitle(title);
    }
}
