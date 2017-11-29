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

namespace SaveTango.ViewModel
{
    public class BoardWindowViewModel : Bindable
    {
        

        public GamePlay GamePlay { get; set; }

        /// <summary>
        /// a játékos által elhúzott blokkok száma
        /// </summary>
        private int moves;
        public int Moves
        {
            get { return this.moves; }

            set
            {
                this.moves = value;
                OnPropertyChanged("moves");
            }
        }

        public BoardWindowViewModel()
        {
            this.GamePlay = new GamePlay();
            this.Moves = 0;
        }

        public void MoveCounter()
        {
            this.Moves++;
        }

    }
}
