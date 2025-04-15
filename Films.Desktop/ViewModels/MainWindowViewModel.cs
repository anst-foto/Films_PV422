using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Films.Model;

namespace Films.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    #region Fields

    private string? _search;
    public string? Search
    {
        get => _search;
        set => SetField(ref _search, value);
    }

    private Guid? _id;
    public Guid? ID
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    private string? _title;
    public string? Title
    {
        get => _title;
        set => SetField(ref _title, value);
    }

    private string? _description;
    public string? Description
    {
        get => _description;
        set => SetField(ref _description, value);
    }

    private DateTime? _releaseDate;
    public DateTime? ReleaseDate
    {
        get => _releaseDate;
        set => SetField(ref _releaseDate, value);
    }

    public ObservableCollection<Film> Films { get; } = [];

    private Film? _selectedFilm;
    public Film? SelectedFilm
    {
        get => _selectedFilm;
        set => SetField(ref _selectedFilm, value);
    }

    #endregion

    #region Commands

    public ICommand CommandSearch { get; }
    public ICommand CommandClearSearch { get; }

    public ICommand CommandSave { get; }
    public ICommand CommandDelete { get; }
    public ICommand CommandClear { get; }

    #endregion

}
