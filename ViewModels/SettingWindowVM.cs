using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    /// <summary>
    /// VM de la fenêtre de paramètre
    /// </summary>
    public class SettingWindowVM
    {
        private IFolderPathManager folderPathManager;

        /// <summary>
        /// Constructeur, injection du folderPathManager
        /// </summary>
        /// <param name="manager"></param>
        public SettingWindowVM(IFolderPathManager manager) 
        {
            this.folderPathManager = manager;
        }

        /// <summary>
        /// Méthode permettant de sauvegarder le chemin du nouveau dossier de chanson
        /// </summary>
        /// <param name="folderPath">path du dossier de chanson</param>
        public void SaveNewFolderPath(string folderPath)
        {
            this.folderPathManager.SaveFolderPath(folderPath);
        }
    }
}
