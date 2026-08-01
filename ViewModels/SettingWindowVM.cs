using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    /// <summary>
    /// VM de la fenêtre de paramètre
    /// </summary>
    public class SettingWindowVM : INotifyPropertyChanged
    {
        private IFolderPathManager folderPathManager;
        private string folderPath;

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Constructeur, injection du folderPathManager
        /// </summary>
        /// <param name="manager"></param>
        public SettingWindowVM(IFolderPathManager manager) 
        {
            this.folderPathManager = manager;
            this.folderPath = this.folderPathManager.LoadFolderPath();
        }

        /// <summary>
        /// Méthode permettant de sauvegarder le chemin du nouveau dossier de chanson
        /// </summary>
        public void SaveNewFolderPath(string path)
        {
            this.FolderPath = path;
            this.folderPathManager.SaveFolderPath(this.folderPath);
        }

        /// <summary>
        /// Property, manage the Folder's Path
        /// </summary>
        public string FolderPath
        {
            get => folderPath;
            set
            {
                folderPath = value;
                OnPropertyChanged(nameof(FolderPath));
            }
        }

        /// <summary>
        /// Property changed observer
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
