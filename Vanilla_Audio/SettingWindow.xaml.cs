using Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModels;

namespace Vanilla_Audio
{
    /// <summary>
    /// Logique d'interaction pour SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evenement, ouverture d'un folder dialog pour sélectionner le dossier des chansons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseSongFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog ofd = new OpenFolderDialog();
            bool? result = ofd.ShowDialog();
            
            if(result == true)
            {
                SettingWindowVM vm = (SettingWindowVM)this.DataContext; //Récupérer le VM via le DataContext, fourni à la création de window
                vm.SaveNewFolderPath(ofd.FolderName); //On récupère le path et sauvegarde
                textBoxDisplayFolderPath.Text = ofd.FolderName;
            }
        }
    }
}
