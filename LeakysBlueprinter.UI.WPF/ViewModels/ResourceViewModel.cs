using Easy.MessageHub;
using LeakysBlueprinter.UI.WPF.EventPayloads;
using LeakysBlueprinter.UI.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeakysBlueprinter.UI.WPF.ViewModels
{
    public class Resource
    {
        public string FileUri;

        public string TargetFileName;
        public string DefaultRelativePath;
        public string DisplayName;
        public string Description;
    }

    /// <summary>
    /// Holds information about a single resource
    /// </summary>
    public class ResourceViewModel : ViewModelBase
    {
        public ICommand SelectFileCommand { get; }

        public string TargetFileName { get; }
        public string RelativePath { get; }
        public string Description { get; }

        public string FileUri
        {
            get => _fileUri;
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    Set(ref _fileUri, value);
                    FileUriValid = IsValid(_fileUri);
                    FileUriAutodetected = false;
                }
            }
        }

        public bool FileUriValid
        {
            get => _fileUriValid;
            protected set => Set(ref _fileUriValid, value);
        }

        public bool FileUriAutodetected
        {
            get => _fileUriAutodetected;
            protected set => Set(ref _fileUriAutodetected, value);
        }

        protected bool _fileUriAutodetected;
        protected string _fileUri;
        protected bool _fileUriValid;

        /// <summary>
        /// Parameterless constructor for Designer functionality
        /// </summary>
        public ResourceViewModel()
        {
            DisplayName = "Example resource";
            TargetFileName = "Example.sbc";
            RelativePath = @"Content\Data";
            Description = "Description of resource.";
            FileUri = @"C:\Folder\AnotherFolder\AnotherFolder\MyFile.file";
        }

        public ResourceViewModel(
            IMessageHub messageHub,
            string targetFileName,
            string relativePath,
            string displayName,
            string description,
            string existingFileUri = "")
            : base(messageHub)
        {
            TargetFileName = targetFileName ?? throw new ArgumentNullException(nameof(targetFileName));
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            Description = description;
            RelativePath = relativePath;

            FileUri = existingFileUri;

            SelectFileCommand = new RelayCommand<object>(SelectFile);
        }

        protected void SelectFile(object obj)
        {
            var message = new SelectFileUriThroughDialog() {
                Sender = this,
                FileNameFilter = DisplayName + "|" + TargetFileName,
                InitialDirectory = ResourceDirectoryBestGuess()
            };
            Publish(message);
            if (!string.IsNullOrEmpty(message.FileUriSelected))
                FileUri = message.FileUriSelected;
        }

        protected string ResourceDirectoryBestGuess()
        {
            if (!string.IsNullOrEmpty(FileUri))
                return FileUri.Substring(0, FileUri.LastIndexOf('\\'));
            else
                return null;
        }

        public bool DetectUri(string baseDirectory)
        {
            string candidateFileUri = MakeUri(baseDirectory);

            if (IsValid(candidateFileUri))
            {
                FileUri = candidateFileUri;
                FileUriAutodetected = true;
                return true;
            }

            return false;
        }

        public string MakeUri(string baseDir)
            => baseDir.TrimEnd('\\') + @"\" + RelativePath.Trim('\\') + @"\" + TargetFileName;

        public static bool IsValid(string uri)
            => File.Exists(uri); //TODO: consider abstracting
    }
}
