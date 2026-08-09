using Business.Models;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TagLib.Mpeg;

namespace Business
{
    /// <summary>
    /// Song player using NAudio library
    /// </summary>
    public class NAudioPlayer : IPlayer
    {
        private Song? currentSong;
        public bool IsPlaying { get; private set; }
        public float Volume { get; set; }
        private bool isChangingSong;

        private WaveOut? outputDevice;
        private AudioFileReader? audioFileReader;
        private bool closing = false;

        public event EventHandler SongEnded;

        public NAudioPlayer()
        {
            this.currentSong = null;
            this.IsPlaying = false;
            this.Volume = 1.0f;
            this.isChangingSong = false;
            this.outputDevice = null;
            this.audioFileReader = null;
        }

        public Song? CurrentSong => currentSong;

        public void Play(Song song)
        {
            this.isChangingSong = true;

            if (outputDevice != null) //Destroy the previous player to avoid multiple song playing at the same time
            {
                outputDevice.PlaybackStopped -= OnPlaybackStopped; 
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
            }

            audioFileReader?.Dispose();
            audioFileReader = new AudioFileReader(song.Path);
            audioFileReader.Volume = this.Volume;

            outputDevice = new WaveOut();
            outputDevice.PlaybackStopped += OnPlaybackStopped;
            outputDevice.Init(audioFileReader);
            outputDevice.Play();

            this.currentSong = song;
            this.IsPlaying = true;
            this.isChangingSong = false;
        }

        public void PauseOrResume()
        {
            if(this.IsPlaying)
            {
                this.IsPlaying = false;
                //TODO
            }
            else
            {
                //TODO
            }
        }

        public void Randomize()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finally, we need to clean up, and the best place to do that is in the PlaybackStopped event handler. 
        /// Playback can stop for three reasons:
        /// 1) you requested it to stop with Stop()
        /// 2) you reached the end of the input file
        /// 3) there was an error(e.g.you removed the USB headphones you were listening on)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            if(!this.isChangingSong)
            {
                this.IsPlaying = false;
            } 
        }
    }
}
