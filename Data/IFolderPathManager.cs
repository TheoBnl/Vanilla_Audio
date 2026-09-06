using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    ///Interface defining method of the FolderPathManager
    ///The Manager can save and load the path of the directory containing audio files
    /// </summary>
    public interface IFolderPathManager
    {
        /// <summary>
        /// Method used to load the directory path
        /// </summary>
        /// <returns>path of the songs folder</returns>
        public string LoadFolderPath();

        /// <summary>
        /// Method used to save the path of the directory containing songs
        /// </summary>
        /// <param name="songFolder">path of the song folder</param>
        public void SaveFolderPath(string songFolder);
    }
}
