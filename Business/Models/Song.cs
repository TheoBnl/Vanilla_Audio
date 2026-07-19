using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Song
    {
        private string name;
        private long duration;
        private Byte[] cover;

        public Song(string name, long duration)
        {
            this.name = name;
            this.duration = duration;
            this.cover = cover;
        }
    }
}
