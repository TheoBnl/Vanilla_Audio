using Business;
using Business.Data;
using Business.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    /// <summary>
    /// Classe ViewModel de la MainWindow
    /// </summary>
    public class MainWindowVM : INotifyPropertyChanged
    {
        private IFolderPathManager folderPathManager;
        private ISongManager songManager;
        private IPlayer player;
        private ObservableCollection<Song> songs;

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Constructor, dependencies injection of the folderPathManager and songManager
        /// </summary>
        /// <param name="folderPathManager">manager of dolderPath</param>
        /// <param name="songManager">manager of songs</param>
        public MainWindowVM(IFolderPathManager folderPathManager, ISongManager songManager, IPlayer player)
        {
            this.folderPathManager = folderPathManager;
            this.songManager = songManager;
            this.player = player;

            this.ReloadSongs();
        }

        /// <summary>
        /// Property, managing observable collection containing all songs
        /// </summary>
        public ObservableCollection<Song> Songs
        {
            get => this.songs;
            set
            {
                this.songs = value;
                OnPropertyChanged(nameof(Songs));
            }
        }

        /// <summary>
        /// Reload Songs when loading or changing the song's directory
        /// </summary>
        public void ReloadSongs()
        {
            string folderPath = this.folderPathManager.LoadFolderPath();
            this.Songs = new ObservableCollection<Song>(this.songManager.CreateSongListFromDirectory(folderPath));
        }

        /// <summary>
        /// Property, notify when there is a change in the called property
        /// </summary>
        /// <param name="propertyName">name of the property that notify</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
