using System.Windows;
using System.Windows.Controls;

namespace SaveTango
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        /// <summary>
        /// hivatkozást tárol a MainWindow mainFrame-jére
        /// </summary>
        private Frame mainFrame;

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="mainFrame">a mainFrame-t megkapja paraméterként</param>
        public Menu(Frame mainFrame)
        {
            this.InitializeComponent();
            this.mainFrame = mainFrame;
        }

        /// <summary>
        /// a játékválasztó page-t indítja a gomb metódusa
        /// </summary>
        /// <param name="sender"> sender paraméter</param>
        /// <param name="e">RoutedEventArgs típusú paraméter</param>
        private void ButtonClick_main_newgame(object sender, RoutedEventArgs e)
        {
            LevelSelector levelSel = new LevelSelector(this.mainFrame);
            this.mainFrame.Content = levelSel;
        }

        /// <summary>
        /// az Exit gomb metódusa bezárja a programot
        /// </summary>
        /// <param name="sender">sender paraméter</param>
        /// <param name="e">RoutedEventArgs típusú paraméter</param>
        private void Button_main_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            Application.Current.Shutdown();
        }

        private void button_main_highscores_Click(object sender, RoutedEventArgs e)
        {
            Scores scores = new Scores(this.mainFrame);
            this.mainFrame.Content = scores;
        }
    }
}
