using Business;
using Business.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data;

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

        /// <summary>
        /// Property, expose the song cover to display it in the mainWindow
        /// </summary>
        public byte[]? CurrentSongCover => this.player.CurrentSong?.Cover;

        /// <summary>
        /// Property, expose if a song is currently playing
        /// </summary>
        public bool IsPlaying => this.player.IsPlaying;

        /// <summary>
        /// Method using passing a song from the ListView to play using the player,
        /// notify to refresh the song cover
        /// </summary>
        /// <param name="song">song to play</param>
        public void PlaySelectedSong(Song song)
        {
            this.player.Play(song);
            OnPropertyChanged(nameof(CurrentSongCover));
        }
    }
}
