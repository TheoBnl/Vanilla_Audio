using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    /// <summary>
    /// Model object Song, contain Song's tags and filepath
    /// </summary>
    public class Song
    {
        public string Title {get; set;}
        public TimeSpan Duration { get; set; }
        public string Artist { get; set; }
        public byte[] Cover { get; set; }
        public string Path { get; set; }
    }
}
