using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Data
{
    /// <summary>
    /// SongManager using TagLibSharp to handle tags
    /// </summary>
    public class TagLibSharpManager : ITagManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TagLibSharpManager() { }

        public void FetchTagsAndAddToSong(Song song)
        {
            try
            {
                var tfile = TagLib.File.Create(song.Path);

                song.Title = tfile.Tag.Title;
                
                if(string.IsNullOrEmpty(song.Title)) //If no Title tag, we use the file name instead
                {
                    song.Title = Path.GetFileNameWithoutExtension(song.Path);
                }

                song.Artist = tfile.Tag.FirstAlbumArtist;
                song.Duration = tfile.Properties.Duration;
                if(tfile.Tag.Pictures.Length > 0)
                {
                    song.Cover = tfile.Tag.Pictures[0].Data.Data; //0 correspond to the cover of the song
                }          
            }
            catch(Exception ex) 
            {
                throw new Exception("Error when fetching song's Tag", ex);
            }        
        }
    }
}
