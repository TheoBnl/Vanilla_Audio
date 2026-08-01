using Business.Models;
using System;
using System.IO;
using System.Text.Json;

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

        public SongFolder LoadFolderPath()
        {
            SongFolder songFolder = null;
            try
            {
                if (File.Exists(this.fileName))
                {
                    string jsonString = File.ReadAllText(this.fileName);
                    songFolder = JsonSerializer.Deserialize<SongFolder>(jsonString);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load song folder", ex);
            }
            return songFolder;
        }

        public void SaveFolderPath(SongFolder songFolder)
        {
            try
            {
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