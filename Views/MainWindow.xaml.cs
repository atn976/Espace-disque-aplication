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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ratrapage_version_2._0.ViewModels;




namespace ratrapage_version_2._0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // Initialise les composants de l'interface utilisateur.
            int interval = 10; // Vous pouvez changer cela pour utiliser un paramètre de configuration si vous le souhaitez.
            DataContext = new DiskSpaceViewModel(interval); // Définit le DataContext pour le binding avec le ViewModel.
        }
    }
}
