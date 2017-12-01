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
        /// játékidő string formátumban
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
        /// játékidő
        /// </summary>
        private TimeSpan timeSpan;

        public TimeSpan TimeSpan
        {
            get { return timeSpan; }
            set
            {
                timeSpan = value;
                OnPropertyChanged("timeSpan");
            }
        }

        /// <summary>
        /// eltelt játékidő
        /// </summary>
        private string endElapsedTime;

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

        private int movesSum;
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
            this.TimeElapsed = timeSpan.Minutes + ":" + timeSpan.Seconds;
        }
    }
}
