using Business.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tests
{
    public class FolderPathManagerTest
    {
        private string testJson = "test_folderPath.json";

        [Fact]
        public void TestSaveAndLoadFolderPath()
        {
            if (File.Exists(testJson)) File.Delete(testJson);
            FolderPathManager manager = new FolderPathManager(testJson);
            Assert.NotNull(manager);

            SongFolder songFolder = new SongFolder("C:/test/songs/");
            manager.SaveFolderPath(songFolder);
            SongFolder loadedFolder = manager.LoadFolderPath();

            Assert.Equal(songFolder.FolderPath, loadedFolder.FolderPath);
        }
    }
}
