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
    /// VM of the settings window
    /// </summary>
    public class SettingWindowVM : INotifyPropertyChanged
    {
        private IFolderPathManager folderPathManager;
        private string folderPath;

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Constructor, get the current FolderPath if it exist
        /// </summary>
        /// <param name="manager">Folder path manager to use</param>
        public SettingWindowVM(IFolderPathManager manager) 
        {
            this.folderPathManager = manager;
            this.folderPath = this.folderPathManager.LoadFolderPath();
        }

        /// <summary>
        /// Method using the folderPathManager to save the path of the new selected directory
        /// </summary>
        public void SaveNewFolderPath(string path)
        {
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
        /// Property, notify when there is a change in the called property
        /// </summary>
        /// <param name="propertyName">name of the property that notify</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
