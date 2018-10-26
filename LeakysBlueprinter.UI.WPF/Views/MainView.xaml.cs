using LeakysBlueprinter.UI.WPF.EventPayloads;
using LeakysBlueprinter.UI.WPF.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media;

namespace LeakysBlueprinter.UI.WPF.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        public MainView(MainViewModel viewModel)
        {
            DataContext = viewModel;

            // Handle ViewModel's file uri requests
            viewModel.RequestFileUri += (_, message) => message.FileUriSelected = GetFileUri(message.FileNameFilter, message.InitialDirectory);

            // Handle ViewModel's file uri requests
            viewModel.RequestFolderPath += (_, message) => message.FolderSelected = GetFolderPath(message.InitialDirectory);

            // Connect ViewModel's app exit request to closing the window
            viewModel.RequestExitApp += (_, __) => this.Close();

            InitializeComponent();
        }

        /// <summary>
        /// Opens file browser dialog for file selection.
        /// </summary>
        /// <returns>Selected filename or String.Empty</returns>
        private string GetFileUri(string filter = null, string initialDirectory = null)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = filter,
                Multiselect = false,
                InitialDirectory = initialDirectory
            };

            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }

        private string GetFolderPath(string initialDirectory = null)
        {
            // Create a "Save As" dialog for selecting a directory (HACK)
            var dialog = new SaveFileDialog
            {
                InitialDirectory = initialDirectory,
                Title = "Select a Directory", // instead of default "Save As"
                Filter = "Directory|*.directory", // Prevents displaying files
                FileName = "select" // Filename will then be "select.this.directory"
            };

            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                // Remove fake filename from resulting path
                path = path.Replace("\\select.directory", "");
                path = path.Replace(".directory", "");
                // If user has changed the filename, create the new directory
                //if (!System.IO.Directory.Exists(path))
                //{
                //    System.IO.Directory.CreateDirectory(path);
                //}
                // Our final value is in path
                return path;
            }
            else
                return String.Empty;
        }
    }
}
