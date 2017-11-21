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

namespace SaveTango
{
    /// <summary>
    /// Interaction logic for LevelSelection.xaml
    /// </summary>
    public partial class LevelSelection : Window
    {
        

        public LevelSelection()
        {
            InitializeComponent();
        }

        private void OnSelectLevelClick(object sender, RoutedEventArgs e)
        {
            BoardWindow boardWindow = new BoardWindow();
            boardWindow.Owner = Application.Current.MainWindow;
            boardWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            boardWindow.Show();
        }
    }
}
