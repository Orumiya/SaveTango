// <copyright file="Scores.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SaveTango
{
    using System.Windows;
    using System.Windows.Controls;
    using SaveTango.ViewModel;

    /// <summary>
    /// Interaction logic for Scores.xaml
    /// </summary>
    public partial class Scores : Page
    {
        private HighScoreViewModel hsvm;
        private Frame mainFrame;

        public Scores(Frame mainFrame)
        {
            this.InitializeComponent();
            this.mainFrame = mainFrame;
            this.hsvm = new HighScoreViewModel();
            this.DataContext = this.hsvm;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Menu page1 = new Menu(this.mainFrame);
            this.mainFrame.Content = page1;
        }
    }
}
