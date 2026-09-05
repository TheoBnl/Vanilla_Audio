using Business.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Data
{
    /// <summary>
    /// Class managing Song object creation
    /// </summary>
    public class SongManager : ISongManager
    {
        private string[] extensions = { "*.mp3", "*.flac" };
        private ITagManager tagManager;

        /// <summary>
        /// Constructor, injection of the tags manager
        /// </summary>
        /// <param name="tagManager">tag's manager</param>
        public SongManager(ITagManager tagManager)
        {
            this.tagManager = tagManager;
        }

        public List<Song> CreateSongListFromDirectory(string directoryPath)
        {
            List<Song> songsList = new List<Song>();

            if (!string.IsNullOrWhiteSpace(directoryPath) && Directory.Exists(directoryPath))
            {
                List<string> filesPath = new List<string>();

                foreach (string extension in this.extensions)
                {
                    filesPath.AddRange(Directory.GetFiles(directoryPath, extension));
                }

                foreach (string path in filesPath)
                {
                    Song song = new Song();
                    song.Path = path;

                    this.tagManager.FetchTagsAndAddToSong(song);

                    songsList.Add(song);
                }
            }

            return songsList;
        }
    }
}
