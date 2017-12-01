using SaveTango.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveTango.ViewModel
{
    public class HighScoreViewModel : Bindable
    {
        private ObservableCollection<string> highScores;

        public ObservableCollection<string> HighScores
        {
            get { return highScores; }
            private set { highScores = value; }
        }

        private ObservableCollection<Score> scores;

        public ObservableCollection<Score> Scores
        {
            get { return this.scores; }
            set { this.scores = value; }
        }

        public HighScoreViewModel()
        {
            this.highScores = new ObservableCollection<string>();
            this.scores = new ObservableCollection<Score>();
            this.ReadingTheHighScoresFromFile();
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
            catch (Exception ex)
            { }
            string[] highscoreArray = line.Split('\n');
            for (int i = 0; i < highscoreArray.Length; i++)
            {
                if (highscoreArray[i] != null)
                {
                    string levelString = string.Empty;
                    int j = 0;
                    while (j < highscoreArray[i].Length-1 && highscoreArray[i][j] != '_')
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
                
                HighScores.Add(item);
            }
        }

    }
}
