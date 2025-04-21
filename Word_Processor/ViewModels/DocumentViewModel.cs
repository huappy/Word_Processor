using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;

namespace Word_Processor.ViewModels
{
    public class DocumentViewModel : INotifyPropertyChanged
    {
        private string _documentText = string.Empty;

        private readonly DispatcherTimer _autoSaveTimer;
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

            _autoSaveTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            _autoSaveTimer.Tick += AutoSave_Tick;
            _autoSaveTimer.Start();
        }
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


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
        private void AutoSave_Tick(object? sender, EventArgs e)
        {
            UpdateLastSavedTimeUI(); // Fast, UI-safe update
            _ = SaveDocumentAsync(); // Fire-and-forget async file save
        }

        private void UpdateLastSavedTimeUI()
        {
            LastSaved = DateTime.Now.ToString("T"); // Bind this to your UI
        }

        private async Task SaveDocumentAsync()
        {
            string path = CurrentFilePath ?? "autosave.txt";
            string content = DocumentText;

            try
            {
                // Avoid UI thread blocking here!
                await Task.Run(() =>
                {
                    File.WriteAllText(path, content);
                });
            }
            catch (Exception ex)
            {
                // Log or show a non-blocking error message
            }
        }

    }
}
