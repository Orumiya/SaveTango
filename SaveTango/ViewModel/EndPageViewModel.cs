using SaveTango.Model;

namespace SaveTango.ViewModel
{
    public class EndPageViewModel : Bindable
    {
        public EndPageViewModel(int level, string gametime, int moves)
        {
            this.Level = level;
            this.MovesSum = moves;
            this.EndElapsedTime = gametime;
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
    }
}
