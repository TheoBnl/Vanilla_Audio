using System.Text.Json;
using Business.Models;

namespace Data
{
    /// <summary>
    /// Classe permettant de sauvegarder et de lire le dossier de musiques enregistré
    /// </summary>
    public class FolderPathManager : IFolderPathManager
    {
        private string fileName;

        /// <summary>
        /// Constructeur, initialise le nom du fichier de sauvegarde
        /// </summary>
        /// <param name="fileName">nom du fichier de sauvegarde</param>
        public FolderPathManager(string fileName)
        {
            this.fileName = fileName;
        }

        public string LoadFolderPath()
        {
            string folderPath = string.Empty;
            try
            {
                if (File.Exists(this.fileName))
                {
                    string jsonString = File.ReadAllText(this.fileName);
                    if(!string.IsNullOrWhiteSpace(jsonString))
                    {
                        SongFolder songFolder = JsonSerializer.Deserialize<SongFolder>(jsonString);
                        folderPath = songFolder.FolderPath ?? string.Empty;
                    } 
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load song folder path", ex);
            }
            return folderPath;
        }

        public void SaveFolderPath(string songFolderPath)
        {
            try
            {
                SongFolder songFolder = new SongFolder(songFolderPath);
                string jsonString = JsonSerializer.Serialize(songFolder);
                File.WriteAllText(this.fileName, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save song folder", ex);
            }
        }
    }
}