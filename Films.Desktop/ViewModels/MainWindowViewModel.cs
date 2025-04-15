﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Films.BLL;
using Films.Model;
using Microsoft.Extensions.Configuration;

namespace Films.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private IService? _service;

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

    public MainWindowViewModel()
    {
        Loaded();
        LoadData();

        CommandClearSearch = new LambdaCommand(
            execute: _ => ClearSearch(),
            canExecute: _ => _service is not null &&
                             !string.IsNullOrEmpty(Search));
        CommandSearch = new LambdaCommand(
            execute: _ => Find(),
            canExecute: _ => _service is not null &&
                             !string.IsNullOrEmpty(Search));

        CommandSave = new LambdaCommand(
            execute: _ => Save(),
            canExecute: _ => _service is not null &&
                             !string.IsNullOrEmpty(Title) &&
                             !string.IsNullOrEmpty(Description) &&
                             !string.IsNullOrEmpty(ReleaseDate.ToString()));
        CommandDelete = new LambdaCommand(
            execute: _ => Delete(),
            canExecute: _ => _service is not null &&
                             SelectedFilm is not null);
        CommandClear = new LambdaCommand(
            execute: _ => Clear(),
            canExecute: _ => _service is not null &&
                             (!string.IsNullOrEmpty(Title) ||
                             !string.IsNullOrEmpty(ReleaseDate.ToString())));
    }

    #region Methods

    private void Loaded()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            MessageBox.Show(
                messageBoxText:"Не удалось получить строку подключения к базе данных",
                caption: "Ошибка",
                MessageBoxButton.OK,
                icon:MessageBoxImage.Error);

            _service = null;
        }
        else
        {
            _service = new Service(connectionString);
        }
    }

    private void LoadData()
    {
        if (_service is null) return;

        var films = _service.GetAll();

        FillFilms(films);
    }

    private void FillFilms(IEnumerable<Film> films)
    {
        Films.Clear();
        foreach (var film in films)
        {
            Films.Add(film);
        }
    }

    private void Find()
    {
        var films = _service!.GetByTitle(Search!);
        FillFilms(films);

        Clear();
    }

    private void ClearSearch()
    {
        Search = null;
    }

    private void Save()
    {
        _service!.Create(new Film()
        {
            Id = Guid.NewGuid(),
            Title = Title!,
            Description = Description,
            ReleaseDate = ReleaseDate!.Value
        });

        Clear();
    }

    private void Delete()
    {
        throw new NotImplementedException();
    }

    private void Clear()
    {
        ID = null;
        Title = null;
        Description = null;
        ReleaseDate = null;

        SelectedFilm = null;
    }

    #endregion
}
