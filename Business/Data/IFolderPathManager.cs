using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    ///Interface permettant de définir les opération du FolderPathManager,
    ///Il permet de sauvegarder et de charger le path vers le dossier de musique
    /// </summary>
    public interface IFolderPathManager
    {
        /// <summary>
        /// Méthode permettant de charger le path du dossier sauvegardé
        /// </summary>
        /// <returns>chemin vers le dossier contenant les musiques</returns>
        public SongFolder LoadFolderPath();

        /// <summary>
        /// Méthode permettant de sauvegarder le path du dossier de musiques
        /// </summary>
        public void SaveFolderPath(SongFolder songFolder);
    }
}
