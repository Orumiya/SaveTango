// <copyright file="HighScoreViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SaveTango.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using SaveTango.Model;

    public class HighScoreViewModel : Bindable
    {
        private ObservableCollection<string> highScores;
        private ObservableCollection<Score> scores;
        private ObservableCollection<Score> sortedScores;

        public HighScoreViewModel()
        {
            this.highScores = new ObservableCollection<string>();
            this.scores = new ObservableCollection<Score>();
            this.ReadingTheHighScoresFromFile();
            this.scores.Sort();
        }

        public ObservableCollection<string> HighScores
        {
            get { return this.highScores; }
            private set { this.highScores = value; }
        }

        public ObservableCollection<Score> Scores
        {
            get
            {
                return this.scores;
            }

            set
            {
                this.scores = value;
                this.OnPropertyChanged("Scores");
            }
        }

        private void ReadingTheHighScoresFromFile()
        {
            string line = string.Empty;
            string filePath = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += @"\scores.txt";

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    line += sr.ReadToEnd();
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
            }

            string[] highscoreArray = line.Split('\n');
            for (int i = 0; i < highscoreArray.Length; i++)
            {
                if (highscoreArray[i] != string.Empty)
                {
                    string levelString = string.Empty;
                    int j = 0;
                    while (j < highscoreArray[i].Length - 1 && highscoreArray[i][j] != '_')
                    {
                        levelString += highscoreArray[i][j].ToString();
                        j++;
                    }

                    j++;
                    int level = int.Parse(levelString);
                    string movesString = string.Empty;
                    while (j < highscoreArray[i].Length && highscoreArray[i][j] != '_')
                    {
                        movesString += highscoreArray[i][j].ToString();
                        j++;
                    }
                    j++;
                    int moves = int.Parse(movesString);
                    string time = string.Empty;
                    while (j < highscoreArray[i].Length && highscoreArray[i][j] != '\r')
                    {
                        time += highscoreArray[i][j].ToString();
                        j++;
                    }

                    this.Scores.Add(new Score(level, moves, time));
                    }
            }

            foreach (string item in highscoreArray)
            {
                this.HighScores.Add(item);
            }
        }
    }
}
