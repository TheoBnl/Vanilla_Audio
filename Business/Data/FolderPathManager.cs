using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Classe permettant de sauvegarder et de lire le chemin du dossier de musiques enregistré
    /// </summary>
    public class FolderPathManager : IFolderPathManager
    {
        private string fileName;

        /// <summary>
        /// Constructeur, initialise le nom du fichier de sauvegarde
        /// </summary>
        /// <param name="jsonFileName">nom du fichier de sauvegarde</param>
        public FolderPathManager(string fileName) 
        { 
            this.fileName = fileName;
        }

        public string LoadFolderPath()
        {
            string folderPath = string.Empty;

            try
            {
                if(File.Exists(this.fileName))
                {
                    string jsonString = File.ReadAllText(this.fileName);
                    folderPath = JsonSerializer.Deserialize<string>(jsonString);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to load song folder path", ex);
            }

            return folderPath;
        }

        public void SaveFolderPath(string folderPath)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(folderPath);
                File.WriteAllText(this.fileName, jsonString);
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to save song folder path", ex);
            }
        }
    }
}
