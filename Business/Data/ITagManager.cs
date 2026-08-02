using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Data
{
    /// <summary>
    /// Interface, Tag manager fetching tags from songs
    /// </summary>
    public interface ITagManager
    {
        /// <summary>
        /// Fetch tags from a song file path and add them to the song
        /// </summary>
        /// <param name="song">song object we want to fetch tags from path</param>
        public void FetchTagsAndAddToSong(Song song);
    }
}
