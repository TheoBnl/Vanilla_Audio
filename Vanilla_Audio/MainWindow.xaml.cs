using Business.Models;
using Data;
using System.Windows;
using System.Windows.Controls;
using ViewModels;

namespace Vanilla_Audio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IFolderPathManager folderPathManager;

        /// <summary>
        /// Constructor of the mainWindow, init the file saving the folder path
        /// create the VM and init its DataContext
        /// </summary>
        public MainWindow(MainWindowVM mainWindowVM, IFolderPathManager folderPathManager)
        {
            InitializeComponent();
            this.DataContext = mainWindowVM;
            this.folderPathManager = folderPathManager;

            listViewSongs.SelectionChanged += PlaySelectedSong;
        }

        /// <summary>
        /// Event opening the settings menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingWindow settingWindow = new SettingWindow();
                settingWindow.DataContext = new SettingWindowVM(this.folderPathManager);

                bool? result = settingWindow.ShowDialog();

                if (result == true)
                {
                    MainWindowVM vm = (MainWindowVM)this.DataContext;
                    vm.ReloadSongs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Event Playing the selected song when clicking on an item from the ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaySelectedSong(object sender, SelectionChangedEventArgs e)
        {
            Song selectedSong = (Song)((ListView)sender).SelectedItem;

            MainWindowVM vm = (MainWindowVM)this.DataContext;
            vm.PlaySelectedSong(selectedSong);
        }

        /// <summary>
        /// Event Playing or Pausing the current selected song
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayPause_Click(object sender, RoutedEventArgs e)
        {
            MainWindowVM vm = (MainWindowVM)this.DataContext;
            vm.PauseOrResumeCurrentSong();
        }

        private void SkipForward_Click(object sender, RoutedEventArgs e)
        {
            MainWindowVM vm = (MainWindowVM)this.DataContext;
            vm.SkipForward();
        }
    }
}