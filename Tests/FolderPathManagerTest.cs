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

            string folderPath = "C:/test/songs/";
            manager.SaveFolderPath(folderPath);
            string loadedFolderPath = manager.LoadFolderPath();

            Assert.Equal(folderPath, loadedFolderPath);
        }

        [Fact]
        public void TestLoadFolderPath_FileDoesNotExist_ReturnsEmptyString()
        {
            if (File.Exists(testJson)) File.Delete(testJson);
            FolderPathManager manager = new FolderPathManager(testJson);

            string loadedFolderPath = manager.LoadFolderPath();

            Assert.Equal(string.Empty, loadedFolderPath);
        }
    }
}
