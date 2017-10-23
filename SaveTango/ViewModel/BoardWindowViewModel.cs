using SaveTango.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SaveTango.ViewModel
{
    class BoardWindowViewModel
    {
        private ObservableCollection<Block> gameLevelSetup;

        public BoardWindowViewModel()
        {
            this.gameLevelSetup = new ObservableCollection<Block> {
                new Block(true, 2,0,0),
                new Block(true, 2,0,300),
                new Block(true, 2,300,0),
                new Block(true, 3,0,200),
                new Block(true, 3,200,500),
                new Block(false, 2,0,400),
                new Block(false, 2,100,400),
                new Block(false, 3,300,100),
                new Block(false, 2,500,100)

            };
        }

    }
}
