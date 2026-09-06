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
using System.Windows.Threading;

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

        private DispatcherTimer timerRefreshProgressionSlider; //used to refresh song playback duration

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

            timerRefreshProgressionSlider = new DispatcherTimer();
            timerRefreshProgressionSlider.Tick += TimerRefreshProgressionSlider_Tick;
            timerRefreshProgressionSlider.Interval = TimeSpan.FromMilliseconds(200);
            timerRefreshProgressionSlider.Start();
        }

        /// <summary>
        /// Method called at each timer's ticks, used to refresh and display the song progression time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerRefreshProgressionSlider_Tick(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(CurrentSongProgression));
            OnPropertyChanged(nameof(CurrentSongProgressionInSeconds));
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
        /// Property, expose the song artist to display it in the mainWindow
        /// </summary>
        public string? CurrentSongArtist => this.player.CurrentSong?.Artist;

        /// <summary>
        /// Property, expose the song current time, used to display the progression of the song
        /// </summary>
        public string CurrentSongProgression
        {
            get
            {
                TimeSpan currentSongProgression = this.player.CurrentPosition;

                return $"{currentSongProgression.Minutes}:{currentSongProgression.Seconds:D2}";
            }
        }

        /// <summary>
        /// Property, expose the song current time progression, used by the slider to display the progression or set it by moving it
        /// </summary>
        public double CurrentSongProgressionInSeconds
        {
            get
            {
                TimeSpan currentSongProgression = TimeSpan.Zero;
                if (this.player.CurrentSong != null)
                {
                   currentSongProgression = this.player.CurrentPosition;
                }

                return currentSongProgression.TotalSeconds;
            }

            set => this.player.CurrentPosition = TimeSpan.FromSeconds(value);
        }

        /// <summary>
        /// Property, expose the current played song total duration
        /// </summary>
        public string CurrentSongTotalDuration
        {
            get
            {
                TimeSpan currentSongTotalDuration = TimeSpan.Zero;

                if (this.player.CurrentSong != null)
                {
                    currentSongTotalDuration = player.CurrentSong.Duration;
                }

                return $"{currentSongTotalDuration.Minutes}:{currentSongTotalDuration.Seconds:D2}";
            }
        }

        /// <summary>
        /// Property, expose the current played song total duration in seconds
        /// </summary>
        public double CurrentSongTotalDurationInSeconds
        {
            get
            {
                TimeSpan currentSongTotalDuration = new TimeSpan(0,0,1); //default value to set the position to 0 and maximum = 1

                if (this.player.CurrentSong != null)
                {
                    currentSongTotalDuration = player.CurrentSong.Duration;
                }

                return currentSongTotalDuration.TotalSeconds;
            }
        }

        /// <summary>
        /// Property, expose if a song is currently playing
        /// </summary>
        public bool IsPlaying => this.player.IsPlaying;

        /// <summary>
        /// Notify and update all UI component when song changed
        /// </summary>
        private void NotifyCurrentSongChanged()
        {
            OnPropertyChanged(nameof(CurrentSongCover));
            OnPropertyChanged(nameof(CurrentSongTitle));
            OnPropertyChanged(nameof(CurrentSongArtist));
            OnPropertyChanged(nameof(CurrentSongTotalDuration));
            OnPropertyChanged(nameof(CurrentSongTotalDurationInSeconds));
            OnPropertyChanged(nameof(CurrentSongProgression));
            OnPropertyChanged(nameof(CurrentSongProgressionInSeconds));
        }

        /// <summary>
        /// Method using passing a song from the ListView to play using the player,
        /// notify to refresh the song cover
        /// </summary>
        /// <param name="song">song to play</param>
        public void PlaySelectedSong(Song song)
        {
            this.player.Play(song);

            this.NotifyCurrentSongChanged();
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

                this.NotifyCurrentSongChanged();
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

                this.NotifyCurrentSongChanged();
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
