// <copyright company="VDSoft" file="MainViewModel.cs">
//    Copyright (C) VDSoft. All rights reserved. Confidential.
// </copyright>

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using VDsoft.TextProcessor.BusinessLogic.Infrastructure;
using VDsoft.TextProcessor.BusinessLogic.Processor;
using VDsoft.TextProcessor.Infrastructure;
using VDsoft.TextProcessor.Ui.Model;

namespace VDsoft.TextProcessor.Ui.ViewModel;

/// <summary>
/// View model for the <see cref="View.MainWindow"/>.
/// </summary>
/// <seealso cref="ObservableObject" />
public class MainViewModel : ObservableObject
{
    private readonly IFileReader fileReader;
    private CancellationTokenSource cancellationTokenSource;

    private ObservableCollection<WordCount> words = new();
    private bool isProcessing = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    public MainViewModel()
    {
        fileReader = new FileReader();
        cancellationTokenSource = new CancellationTokenSource();

        ReadFileAsyncCommand = new AsyncRelayCommand<string>(ReadFileAsync);
        CancelOperationCommand = new AsyncRelayCommand(CancelOperationAsync);
    }

    /// <summary>
    /// Gets the command to read a file asynchronous.
    /// </summary>
    public IAsyncRelayCommand<string> ReadFileAsyncCommand
    {
        get;
    }

    /// <summary>
    /// Gets the command to cancel any operation.
    /// </summary>
    public IAsyncRelayCommand CancelOperationCommand
    {
        get;
    }

    /// <summary>
    /// Gets the collection of read words.
    /// </summary>
    public ObservableCollection<WordCount> Words
    {
        get => words;
        private set => SetProperty(ref words, value);
    }

    /// <summary>
    /// Gets a value indicating whether a file is currently processed or not.
    /// </summary>
    public bool IsProcessing
    {
        get => isProcessing;
        private set => SetProperty(ref isProcessing, value);
    }

    private Task CancelOperationAsync()
    {
        cancellationTokenSource.Cancel();
        return Task.CompletedTask;
    }

    private async Task ReadFileAsync(string? path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        ResetView();

        await ProcessFileAsync(path);
    }

    private async Task ProcessFileAsync(string path)
    {
        IsProcessing = true;

        try
        {
            var fileContent = await fileReader.ReadFileAsync(path, cancellationTokenSource.Token);

            Words = new ObservableCollection<WordCount>(CalculateWords(fileContent));
        }
        finally
        {
            IsProcessing = false;
            ResetCancellationToken();
        }
    }

    private IList<WordCount> CalculateWords(string fileContent) =>
        WordCounter.Count(fileContent, cancellationTokenSource.Token)
            .Select(x => new WordCount(x.Key, x.Value))
            .OrderByDescending(x => x.Count)
            .ToList();

    private void ResetCancellationToken()
    {
        if (!cancellationTokenSource.TryReset())
        {
            cancellationTokenSource.Dispose();
            cancellationTokenSource = new();
        }
    }

    private void ResetView()
    {
        IsProcessing = false;
        Words.Clear();
    }
}
