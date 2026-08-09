using Business.Data;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TagLibSharpManagerTest
    {
        [Fact]
        public void TestFetchTagsAndAddToSong()
        {
            string testFilePath = Path.Combine("TestAssets", "test.mp3");

            TagLibSharpManager tagLibSharpManager = new TagLibSharpManager();
            Song song = new Song { Path = testFilePath };

            tagLibSharpManager.FetchTagsAndAddToSong(song);

            Assert.Equal(Path.GetFileNameWithoutExtension(testFilePath), song.Title);
        }

        [Fact]
        public void FetchTagsAndAddToSong_NoPathException()
        {
            TagLibSharpManager tagLibSharpManager = new TagLibSharpManager();
            Song song = new Song { Path = "does_not_exist.mp3" };

            Assert.Throws<Exception>(() => tagLibSharpManager.FetchTagsAndAddToSong(song));
        }
    }
}
