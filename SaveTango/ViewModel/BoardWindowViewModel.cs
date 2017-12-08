// <copyright file="BoardWindowViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SaveTango.ViewModel
{
    using System;
    using System.Diagnostics;
    using System.Windows.Threading;
    using SaveTango.Model;

    public class BoardWindowViewModel : Bindable
    {
        /// <summary>
        /// a játékos által elhúzott blokkok száma
        /// </summary>
        private int moves;

        /// <summary>
        /// a játékidő számlálására
        /// </summary>
        private DispatcherTimer timer;

        /// <summary>
        /// a játékidő számlálására
        /// </summary>
        private Stopwatch stopWatch;
        private string timeElapsed;
        private int level;
        private int movesSum;

        /// <summary>
        /// játékidő
        /// </summary>
        private TimeSpan timeSpan;

        /// <summary>
        /// eltelt játékidő
        /// </summary>
        private string endElapsedTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardWindowViewModel"/> class.
        /// </summary>
        /// <param name="level">kiválasztott pálya szintje</param>
        public BoardWindowViewModel(int level)
        {
            this.GamePlay = new GamePlay(level);
            this.Moves = 0;
            this.StartTimer();
            this.Level = level;
        }

        public TimeSpan TimeSpan
        {
            get
            {
                return this.timeSpan;
            }

            set
            {
                this.timeSpan = value;
                this.OnPropertyChanged("timeSpan");
            }
        }

        public GamePlay GamePlay { get; set; }

        public string EndElapsedTime
        {
            get
            {
                return this.endElapsedTime;
            }

            set
            {
                this.endElapsedTime = value;
                this.OnPropertyChanged("endElapsedTime");
            }
        }

        /// <summary>
        /// Gets or sets játékidő string formátumban
        /// </summary>
        public string TimeElapsed
        {
            get
            {
                return this.timeElapsed;
            }

            set
            {
                this.timeElapsed = value;
                this.OnPropertyChanged("timeElapsed");
            }
        }

        public int Level
        {
            get
            {
                return this.level;
            }

            set
            {
                this.level = value;
                this.OnPropertyChanged("Level");
            }
        }

        public int Moves
        {
            get
            {
                return this.moves;
            }

            set
            {
                this.moves = value;
                this.OnPropertyChanged("moves");
            }
        }

        public int MovesSum
        {
            get
            {
                return this.movesSum;
            }

            private set
            {
                this.movesSum = value;
                this.OnPropertyChanged("movesSum");
            }
        }

        public void MoveCounter()
        {
            this.Moves++;
        }

        public void StartTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.DispatcherTimerTick_;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            this.stopWatch = new Stopwatch();
            this.stopWatch.Start();
            this.timer.Start();
        }

        public void StopTimer()
        {
            this.MovesSum = this.Moves;
            this.EndElapsedTime = this.TimeElapsed;
            this.stopWatch.Stop();
            this.timer.Stop();
        }

        private void DispatcherTimerTick_(object sender, EventArgs e)
        {
            this.timeSpan = this.stopWatch.Elapsed;
            this.TimeElapsed = this.timeSpan.Minutes + ":" + this.timeSpan.Seconds;
        }
    }
}
