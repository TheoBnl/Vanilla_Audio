using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    /// <summary>
    /// Class representing the song folder
    /// </summary>
    public class SongFolder
    {
        private string folderPath;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SongFolder() { }

        /// <summary>
        /// Constructor, initizalize the folder's path
        /// </summary>
        /// <param name="path">folder's path</param>
        public SongFolder(string path)
        {
            this.FolderPath = path;
        }

        /// <summary>
        /// Property, manage the folder's path
        /// </summary>
        public string FolderPath { get => folderPath; set => folderPath = value; }
    }
}
