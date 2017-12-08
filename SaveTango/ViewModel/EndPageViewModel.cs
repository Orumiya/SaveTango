// <copyright file="EndPageViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SaveTango.ViewModel
{
    using SaveTango.Model;

    public class EndPageViewModel : Bindable
    {
        private int movesSum;

        /// <summary>
        /// eltelt játékidő
        /// </summary>
        private string endElapsedTime;
        private int level;

        public EndPageViewModel(int level, string gametime, int moves)
        {
            this.Level = level;
            this.MovesSum = moves;
            this.EndElapsedTime = gametime;
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
