using Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event, open a folder dialog to select the song's folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseSongFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog ofd = new OpenFolderDialog();
            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                SettingWindowVM vm = (SettingWindowVM)this.DataContext;
                vm.FolderPath = ofd.FolderName; // update the property for the display (no saving)
            }
        }

        private void ValidateSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingWindowVM vm = (SettingWindowVM)this.DataContext;//Get VM via DataContext from the construction of the window
            vm.SaveNewFolderPath(vm.FolderPath); //Get path and save it
            this.DialogResult = true;
            this.Close();
        }
    }
}
