using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;

namespace Word_Processor.ViewModels
{
    public class DocumentViewModel : INotifyPropertyChanged
    {
        private DispatcherTimer _autoSaveTimer;
        private string _documentText = string.Empty;
        private DateTime _lastSaved;
        private bool _isAutoSaveEnabled = true;
        private string _saveStatusIcon = "🟢"; // Default to saved
        private string _saveStatusMessage = "Auto-save is on.";
        public string LastSavedTooltip => $"Document was last saved at {LastSaved:T}";

        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }

        public string DocumentText
        {
            get => _documentText;
            set
            {
                if (_documentText != value)
                {
                    _documentText = value;
                    OnPropertyChanged(nameof(DocumentText));
                    OnPropertyChanged(nameof(WordCount));
                }
            }
        }

        public DateTime LastSaved
        {
            get => _lastSaved;
            private set
            {
                if (_lastSaved != value)
                {
                    _lastSaved = value;
                    OnPropertyChanged(nameof(LastSaved));
                }
            }
        }

        public bool IsAutoSaveEnabled
        {
            get => _isAutoSaveEnabled;
            set
            {
                if (_isAutoSaveEnabled != value)
                {
                    _isAutoSaveEnabled = value;
                    OnPropertyChanged(nameof(IsAutoSaveEnabled));

                    if (_isAutoSaveEnabled)
                    {
                        StartAutoSave();
                        SaveStatusIcon = "🟢";
                        SaveStatusMessage = "Auto-save is on.";
                    }
                    else
                    {
                        StopAutoSave();
                        SaveStatusIcon = "🔴";
                        SaveStatusMessage = "Auto-save is off.";
                    }
                        
                }
            }
        }

        public string SaveStatusIcon
        {
            get => _saveStatusIcon;
            set
            {
                if (_saveStatusIcon != value)
                {
                    _saveStatusIcon = value;
                    OnPropertyChanged(nameof(SaveStatusIcon));
                }
            }
        }

        public string SaveStatusMessage
        {
            get => _saveStatusMessage;
            set
            {
                if (_saveStatusMessage != value)
                {
                    _saveStatusMessage = value;
                    OnPropertyChanged(nameof(SaveStatusMessage));
                }
            }
        }


        public int WordCount =>
            string.IsNullOrWhiteSpace(DocumentText)
                ? 0
                : DocumentText.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;

        public event PropertyChangedEventHandler? PropertyChanged;


        //-------------------------FUNCTIONS--------------------------

        //Document View Constructor
        public DocumentViewModel()
        {
            SaveCommand = new RelayCommand(_ => SaveDocument());
            LoadCommand = new RelayCommand(_ => LoadDocument());

            SetupAutoSave();
        }

        private void LoadDocument()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                DocumentText = File.ReadAllText(dialog.FileName);
            }
        }

        private void SaveDocument()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, DocumentText);
            }
        }


        /*
         * These functions are for the auto-save feature, which will use a timer to call
         * the UpdateLastSavedUI function to update the UI, and then creates a separate thread that saves the file.
         */

        private void SetupAutoSave()
        {
            _autoSaveTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(30)
            };
            _autoSaveTimer.Tick += (s, e) => HandleAutoSave();
            if (IsAutoSaveEnabled) _autoSaveTimer.Start();
        }


        private void StartAutoSave() => _autoSaveTimer?.Start();
        private void StopAutoSave() => _autoSaveTimer?.Stop();

        private void HandleAutoSave()
        {
            SaveStatusIcon = "🟡";
            SaveStatusMessage = "Saving...";

            LastSaved = DateTime.Now;

            Task.Run(() =>
            {
                SaveToFile();

                // Back on UI thread to safely update UI
                App.Current.Dispatcher.Invoke(() =>
                {
                    SaveStatusIcon = "🟢";
                    SaveStatusMessage = $"Last saved at {LastSaved:T}";
                });
            });
        }

        private void SaveToFile()
        {
            try
            {
                string path = "autosave.txt"; // TODO: customize later
                File.WriteAllText(path, DocumentText);
            }
            catch (Exception ex)
            {
                // Optionally log the error
            }
        }

    protected void OnPropertyChanged(string propertyName) =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
