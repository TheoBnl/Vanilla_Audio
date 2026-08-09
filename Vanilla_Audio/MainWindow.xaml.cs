using Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels;

namespace Vanilla_Audio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IFolderPathManager folderPathManager;

        /// <summary>
        /// Constructeur de la mainWindow, initialise le fichier de sauv egarde du chemin du dossier
        /// de chansons, créé son VM et l'affecte à son DataContext
        /// </summary>
        public MainWindow(MainWindowVM mainWindowVM, IFolderPathManager folderPathManager)
        {
            InitializeComponent();
            this.DataContext = mainWindowVM;
            this.folderPathManager = folderPathManager;
        }

        /// <summary>
        /// Evenement permettant d'ouvrir le menu de paramètre de l'application (chemin du dossier etc)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingWindow settingWindow = new SettingWindow();
                settingWindow.DataContext = new SettingWindowVM(this.folderPathManager);

                bool? result = settingWindow.ShowDialog();

                if (result == true)
                {
                    MainWindowVM vm = (MainWindowVM)this.DataContext;
                    vm.ReloadSongs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }
    }
}