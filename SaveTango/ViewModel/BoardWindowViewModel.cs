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
    public class BoardWindowViewModel
    {
        

        public GamePlay GamePlay { get; set; }

        

        public BoardWindowViewModel()
        {
            this.GamePlay = new GamePlay();
        }


    }
}
