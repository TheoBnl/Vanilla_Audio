using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// Interface defining a music player
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Property, true if a song is currently playing else false
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Property, song that is currently played by the player, can be null
        /// at player creation
        /// </summary>
        Song? CurrentSong { get; }

        /// <summary>
        /// Property, volume of the player
        /// </summary>
        float Volume { get; set; }

        /// <summary>
        /// Event, triggered when the song ends
        /// </summary>
        event EventHandler SongEnded;

        /// <summary>
        /// Method used to play the given song
        /// </summary>
        /// <param name="song">song to play</param>
        public void Play(Song song);

        /// <summary>
        /// Pause or resume the current selected song
        /// </summary>
        public void PauseOrResume();
        
        /// <summary>
        /// Pause the current song
        /// </summary>
        public void Pause();

        /// <summary>
        /// Resume the current selected song if its currently paused
        /// </summary>
        public void Resume();

        public void Randomize();
        
        /// <summary>
        /// Property used to get the current progression of the song or set it if you move the slider
        /// </summary>
        public TimeSpan CurrentPosition { get; set; }

        /// <summary>
        /// Loop the current selected Song
        /// </summary>
        //public bool Loop { get; set; } TODO

    }
}
