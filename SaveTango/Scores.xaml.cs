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
    /// Interaction logic for Scores.xaml
    /// </summary>
    public partial class Scores : Page
    {
        Frame mainFrame;
        public HighScoreViewModel hsvm;
        public Scores(Frame mainFrame)
        {
            this.InitializeComponent();
            this.mainFrame = mainFrame;
            hsvm = new HighScoreViewModel();
            this.DataContext = this.hsvm;
        }
    }
}
