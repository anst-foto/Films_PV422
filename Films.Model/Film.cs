using System;

namespace Films.Model;

public class Film
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required DateTime ReleaseDate { get; set; }

    public string ReleaseDateAsString => ReleaseDate.ToString("d");
}
