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

            this.player.SongEnded += OnSongEnded;

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
        /// Property, expose the song name to display it in the mainWindow
        /// </summary>
        public string? CurrentSongTitle => this.player.CurrentSong?.Title;

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
            OnPropertyChanged(nameof(CurrentSongTitle));
        }

        /// <summary>
        /// Method pausing or resuming the current selected song
        /// </summary>
        public void PauseOrResumeCurrentSong()
        {
            this.player.PauseOrResume();
        }

        /// <summary>
        /// Property, expose the Volume of the player, used to change volume with volumeSlider
        /// </summary>
        public float Volume
        {
            get => this.player.Volume;
            set
            {
                this.player.Volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }

        /// <summary>
        /// Skip to the next song in the list
        /// </summary>
        public void SkipForward()
        {
            if (this.player.CurrentSong != null)
            {
                int currentIndex = this.Songs.IndexOf(this.player.CurrentSong);
                if (currentIndex >= 0 && currentIndex + 1 < this.Songs.Count) //check if its not the last item
                {
                    this.player.Play(this.Songs[currentIndex + 1]);
                }
                else
                {
                    this.player.Play(this.Songs.First());
                }

                OnPropertyChanged(nameof(CurrentSongCover));
                OnPropertyChanged(nameof(CurrentSongTitle));
            }
        }

        /// <summary>
        /// Skip to the previous song in the list
        /// </summary>
        public void SkipBackward()
        {
            if (this.player.CurrentSong != null)
            {
                int currentIndex = this.Songs.IndexOf(this.player.CurrentSong);
                if (currentIndex > 0) //if its not the first song
                {
                    this.player.Play(this.Songs[currentIndex - 1]);
                }
                else //Play the first song again if current song is the first of the list
                {
                    this.player.Play(this.Songs.First());
                }

                OnPropertyChanged(nameof(CurrentSongCover));
                OnPropertyChanged(nameof(CurrentSongTitle));
            }
        }

        /// <summary>
        /// Method called when a song end to skip to the next one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSongEnded(object? sender, EventArgs e)
        {
            this.SkipForward();
        }
    }
}
