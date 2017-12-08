// <copyright file="EndPage.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SaveTango
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using SaveTango.ViewModel;

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
            this.WriteTheScoresToFile();
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

        private void ScoresPage_Click(object sender, RoutedEventArgs e)
        {
            Scores scores = new Scores(this.mainFrame);
            this.mainFrame.Content = scores;
        }

        private void Levelselector_Click(object sender, RoutedEventArgs e)
        {
            LevelSelector levelSel = new LevelSelector(this.mainFrame);
            this.mainFrame.Content = levelSel;
        }

        private void WriteTheScoresToFile()
        {
            // Set a variable to the My Documents path.
            string filePath = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += @"\scores.txt";

            string text = this.Level.ToString() + "_" + this.Moves.ToString() + "_" + this.Gametime;

            // Create a string array with the additional lines of text
            string[] lines = { text };

            // Append new lines of text to the file
            File.AppendAllLines(filePath, lines);
        }
    }
}
