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
    class BoardWindowViewModel
    {
        private Dictionary<Image, Block> imageBlockDictionary;

        /// <summary>
        /// a játék kezdőpozícióit tároló gyűjtemény
        /// </summary>
        private ObservableCollection<Block> gameLevelSetup;

        public ObservableCollection<Block> GameLevelSetup
        {
            get { return this.gameLevelSetup; }
            set { this.gameLevelSetup = value; }
        }

        public BoardWindowViewModel()
        {
            this.gameLevelSetup = new ObservableCollection<Block> {
                new Bar(true, 2, 0, 0),
                new Bar(true, 2, 0, 300),
                new Bar(true, 2, 300, 0),
                new Bar(true, 3, 0, 200),
                new Bar(true, 3, 200, 500),
                new Bar(false, 2, 0, 400),
                new Bar(false, 2, 100, 400),
                new Bar(false, 3, 300, 100),
                new Bar(false, 2, 500, 100),
                new Tango()
            };
            this.MakeADictionary();
        }

        private void MakeADictionary()
        {
            this.imageBlockDictionary = new Dictionary<Image, Block>();
            foreach (Block item in this.gameLevelSetup)
            {
                this.imageBlockDictionary.Add(item.BlockImage, item);
            }
        }

        public Block WhichBlockIsThis(Image image)
        {
            Block block = imageBlockDictionary[image];
            return block;
        }

    }
}
