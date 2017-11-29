namespace SaveTango
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// A főmenü page-t indítja
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            Menu page1 = new Menu(this.mainFrame);
            this.mainFrame.Content = page1;
        }
    }
}
