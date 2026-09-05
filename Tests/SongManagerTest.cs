using Business.Models;
using Data;
using Tests.Mocks;

namespace Tests
{
    public class SongManagerTest
    {
        [Fact]
        public void TestCreateSongListFromDirectory()
        {
            string testDirectory = "test_songs";
            Directory.CreateDirectory(testDirectory);
            File.WriteAllText(Path.Combine(testDirectory, "song1.mp3"), string.Empty);
            File.WriteAllText(Path.Combine(testDirectory, "song2.flac"), string.Empty);

            MockTagManager mockTagManager = new MockTagManager();
            SongManager songManager = new SongManager(mockTagManager);

            List<Song> songs = songManager.CreateSongListFromDirectory(testDirectory);

            Assert.Equal(2, songs.Count);
            Assert.Equal(2, mockTagManager.CallCount);

            Directory.Delete(testDirectory, true);
        }
    }
}
