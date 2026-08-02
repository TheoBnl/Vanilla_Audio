using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Data
{
    /// <summary>
    /// Interface defining A song manager
    /// </summary>
    public interface ISongManager
    {
        ///// <summary>
        ///// Create Song objects based on the files in the song's directory
        ///// </summary>
        ///// <returns>List containing all songs</returns>
        public List<Song> CreateSongListFromDirectory(string directoryPath);
    }
}
