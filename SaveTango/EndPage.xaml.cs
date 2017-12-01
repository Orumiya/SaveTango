using SaveTango.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for EndPage.xaml
    /// </summary>
    public partial class EndPage : Page
    {
        private EndPageViewModel epvm;
        private Frame mainFrame;

        public EndPage(Frame mainFrame, int level, string gametime, int moves)
        {
            this.InitializeComponent();
            this.Level = level;
            this.Gametime = gametime;
            this.Moves = moves;
            this.mainFrame = mainFrame;
            this.epvm = new EndPageViewModel(level, gametime, moves);
            this.DataContext = this.epvm;
        }

        public int Level { get; set; }

        public string Gametime { get; set; }

        public int Moves { get; set; }

        private void NextLevel_Click(object sender, RoutedEventArgs e)
        {
            BoardPage boardPage = new BoardPage(this.mainFrame, this.Level + 1);
            this.mainFrame.Content = boardPage;
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Menu page1 = new Menu(this.mainFrame);
            this.mainFrame.Content = page1;
        }

        private void Replay_Click(object sender, RoutedEventArgs e)
        {
            BoardPage boardPage = new BoardPage(this.mainFrame, this.Level);
            this.mainFrame.Content = boardPage;
        }
    }
}
