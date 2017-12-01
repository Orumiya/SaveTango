using System.Windows;
using System.Windows.Controls;

namespace SaveTango
{
    /// <summary>
    /// Interaction logic for LevelSelector.xaml
    /// </summary>
    public partial class LevelSelector : Page
    {
        /// <summary>
        /// hivatkozást tárol a MainWindow mainFrame-jére
        /// </summary>
        private Frame mainFrame;

        public int level;
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelSelector"/> class.
        /// </summary>
        /// <param name="mainFrame">a mainFrame-t megkapja paraméterként</param>
        public LevelSelector(Frame mainFrame)
        {
            this.InitializeComponent();
            this.mainFrame = mainFrame;
        }

        /// <summary>
        /// az adott pályát indító metódus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectLevelClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            this.level = int.Parse(btn.Content.ToString());
            BoardPage boardPage = new BoardPage(mainFrame, this.level);
            this.mainFrame.Content = boardPage;

        }

        private void BackToMS_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}
