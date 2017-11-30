using SaveTango.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Diagnostics;

namespace SaveTango.ViewModel
{
    public class BoardWindowViewModel : Bindable
    {
        public GamePlay GamePlay { get; set; }

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

        /// <summary>
        /// játékidő
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

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardWindowViewModel"/> class.
        /// </summary>
        public BoardWindowViewModel(int level)
        {
            this.GamePlay = new GamePlay(level);
            this.Moves = 0;
            this.StartTimer();
            this.Level = level;
        }

        private int level;

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
            this.stopWatch.Stop();
            this.timer.Stop();
        }

        private void DispatcherTimerTick_(object sender, EventArgs e)
        {
            TimeSpan timeSpan = this.stopWatch.Elapsed;
            this.TimeElapsed = timeSpan.Minutes + ":" + timeSpan.Seconds;
        }
    }
}
