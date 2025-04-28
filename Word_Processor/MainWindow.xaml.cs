using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Word_Processor.ViewModels;
using System.IO;

namespace Word_Processor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const string SettingsFilePath = "AppSettings.txt";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DocumentViewModel(); //where the bindings happen

            // Load Saved Theme choice on start
            LoadThemePreference();

            this.Closing += MainWindow_Closing;
        }

        private void MainWindowClosnig(object sender, RoutedEventArgs e)
        {
            if (DataContext is DocumentViewModel)
            {
                if (viewModel.HasUnsavedChanges)
                {
                    var result = MessageBox.Show(
                        "You have unsaved changes. Do you want to save before exiting?",
                        "Unsaved Changes",
                        MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Warning,);

                    if (result == MessageBoxResult.Yes)
                    {
                        viewModel.SaveCommand.Execute(null);
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    //If No then the window closes without saving
            }
        }

        private void DarkModeClick(object sender, RoutedEventArgs e)
        {
            ChangeTheme("Themes/DarkTheme.xaml");
            SaveThemePreference("Themes/DarkTheme.xaml");
        }
        
        private void LightModeClick(object sender, RoutedEventArgs e)
        {
            ChangeTheme("Themes/LightTheme.xaml");
            SaveThemePreference("Themes/LightTheme.xaml");
        }

        private void ChangeTheme(string themePath)
        {
            // Clear current dictionaries
            Application.Current.Resources.MergedDictionaries.Clear();

            // Load the selected theme
            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }

        private void SaveThemePreference(string themePath)
        {
            try
            {
                File.WriteAllText(SettingsFilePath, themePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured when Saving Theme: " + ex.ToString());
            }
        }

        private void LoadThemePreference()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string themePath = File.ReadAllText(SettingsFilePath.Trim());

                    if (!string.IsNullOrEmpty(themePath) && File.Exists(themePath))
                    {
                        ChangeTheme(themePath);
                        return;
                    }
                }
                else
                {
                    // Theme path inside settings is missing or invalid
                    throw new FileNotFoundException("Saved theme file not found.");
                }

                // If settings file doesn't exist, fall back to default (Dark Mode)
                ChangeTheme("Themes/DarkTheme.xaml");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured when Loading Theme: " + ex.ToString());

                ChangeTheme("Themes/DarkTheme.xaml");

                // Notify the user something went wrong
                MessageBox.Show(
                    "There was a problem loading your theme.\nThe application will use Dark Mode instead.\n\nDetails: " + ex.Message,
                    "Theme Loading Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );

            }

        }

    }
}