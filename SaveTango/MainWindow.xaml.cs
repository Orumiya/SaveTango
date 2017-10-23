using SaveTango.ViewModel;
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

namespace SaveTango
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  

        public MainWindow()
        {
            InitializeComponent();
            
        }
       
        
        /// <summary>
        /// A gomb a szintválasztó ablakot nyitja oly módon, hogy az a main window ablakban nyílik meg.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClick_main_newgame(object sender, RoutedEventArgs e)
        {
            LevelSelection lvlSelWindow = new LevelSelection();
            lvlSelWindow.Owner = Application.Current.MainWindow; 
            lvlSelWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            lvlSelWindow.Show();
            //this.Close();
        }
    }
}
