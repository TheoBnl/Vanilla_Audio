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
    }
}
