using Business;
using Business.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    public class MockTagManager : ITagManager
    {
        public int CallCount { get; private set; }

        public void FetchTagsAndAddToSong(Song song)
        {
            CallCount++;
            song.Title = "Fake Title";
        }
    }
}
