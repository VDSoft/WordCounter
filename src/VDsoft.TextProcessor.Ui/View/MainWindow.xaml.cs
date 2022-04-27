// <copyright company="VDSoft" file="MainWindow.xaml.cs">
//    Copyright (C) VDSoft. All rights reserved. Confidential.
// </copyright>

using Microsoft.Win32;
using System.Windows;

namespace VDsoft.TextProcessor.Ui.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ButtonBrowse_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog()
        {
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Multiselect = false
        };

        if (dialog.ShowDialog() == true)
        {
            TextBoxFilePath.Text = dialog.FileName;
        }
    }
}
