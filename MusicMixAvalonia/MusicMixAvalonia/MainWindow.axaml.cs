using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MusicMixAvalonia
{
    public partial class MainWindow : Window
    {
        MusicClass _musicClass;
        bool _isChoiseFolderMix;
        string _mixFolder;
        ObservableCollection<string> _folders;
        string? _choiseFolder;

        public MainWindow()
        {
            InitializeComponent();

            ChoiseFolders.Click += ChoiseFoders_Click;
            ChoiseFolderMix.Click += ChoiseFolderMix_Click;
            Delete.Click += Delete_Click;
            Upload.Click += Upload_Click;

            ListBox.SelectionChanged += ListBox_SelectionChanged;

            _folders = new ObservableCollection<string>();
        }

        private void Upload_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if ((_mixFolder != null && _mixFolder != "") && (_folders.Count != 0)) 
            {
                _musicClass = new MusicClass(_folders, _mixFolder);
                _musicClass.Mix();
            }
        }

        private void Delete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (_choiseFolder != null && _choiseFolder != "") 
            {
                _folders.Remove(_choiseFolder);
                //ListBox.Items.IndexOf(_choiseFolder);
                ListBox.Items.Remove(_choiseFolder);
            }

            _choiseFolder = null;
        }

        private void ListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (ListBox.SelectedItem != null) 
            {
                _choiseFolder = ListBox.SelectedItem as string;
            }
        }

        [System.Obsolete]
        private async void ChoiseFolderMix_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _isChoiseFolderMix = true;
            await openFold();
        }

        [System.Obsolete]
        private async void ChoiseFoders_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _isChoiseFolderMix = false;
            await openFold();
        }

        [System.Obsolete]
        public async Task openFold()
        {
            var dialog = new OpenFolderDialog();
            var result = await dialog.ShowAsync(this);

            if (result != null)
            {
                if (_isChoiseFolderMix)
                {
                    _mixFolder = result;
                    NameMixFolder.Content = result;
                }
                else 
                {
                    if (!(_folders.Contains(result)) && result != _mixFolder) 
                    {
                        _folders.Add(result);
                        ListBox.Items.Add(result);
                    }
                }
            }
        }
    }
}