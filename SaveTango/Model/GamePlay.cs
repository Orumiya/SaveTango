using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SaveTango.Model
{
    public class GamePlay : Bindable
    {
        public GamePlay()
        {
            this.Board = new Board();
            this.levelSetup = new ObservableCollection<Block> {
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
            this.TheCollisionTableCreator();
        }

        public Board Board { get; set; }
        
        /// <summary>
        /// játékos idejét tároló változó
        /// </summary>
        private TimeSpan playerTime;

        public TimeSpan PlayerTime
        {
            get { return playerTime; }
            set { playerTime = value; }
        }

        /// <summary>
        /// a kiválasztott játék felállását tartalmazó gyűjtemény
        /// </summary>
        private ObservableCollection<Block> levelSetup;

        public ObservableCollection<Block> LevelSetup
        {
            get { return this.levelSetup; }
            set { this.levelSetup = value; }
        }

        /// <summary>
        /// a játékos által elhúzott blokkok száma
        /// </summary>
        private int moves;

        public int Moves
        {
            get { return this.moves; }
            set { this.moves = value; }
        }

        private Dictionary<Image, Block> imageBlockDictionary;
        public Block WhichBlockIsThis(Image image)
        {
            Block block = this.imageBlockDictionary[image];
            return block;
        }

        private void MakeADictionary()
        {
            this.imageBlockDictionary = new Dictionary<Image, Block>();
            foreach (Block item in this.levelSetup)
            {
                this.imageBlockDictionary.Add(item.BlockImage, item);
            }
        }

        /// <summary>
        /// a pozíciónak a sorrendje: x,y koordináta, azaz
        /// először a CanvasLeft, majd a CanvasTop
        /// </summary>
        private void TheCollisionTableCreator()
        {
            int positionX;
            int positionY;
            for (int i = 0; i < this.levelSetup.Count; i++)
            {
                positionX = this.levelSetup[i].InitialCanvasLeft / 100;
                positionY = this.levelSetup[i].InitialCanvasTop / 100;
                this.Board.Table[positionX, positionY] = true;
                if (this.levelSetup[i].Vertical)
                {
                    positionY = (this.levelSetup[i].InitialCanvasTop + 100) / 100;
                    positionX = this.levelSetup[i].InitialCanvasLeft / 100;
                    this.Board.Table[positionX, positionY] = true;
                    if (this.levelSetup[i].BlockLength == 3)
                    {
                        positionY = (this.levelSetup[i].InitialCanvasTop + 200) / 100;
                        positionX = this.levelSetup[i].InitialCanvasLeft / 100;
                        this.Board.Table[positionX, positionY] = true;
                    }
                }
                else
                {
                    positionX = (this.levelSetup[i].InitialCanvasLeft + 100) / 100;
                    positionY = this.levelSetup[i].InitialCanvasTop / 100;
                    this.Board.Table[positionX, positionY] = true;

                    if (this.levelSetup[i].BlockLength == 3)
                    {
                        positionX = (this.levelSetup[i].InitialCanvasLeft + 200) / 100;
                        positionY = this.levelSetup[i].InitialCanvasTop / 100;
                        this.Board.Table[positionX, positionY] = true;
                    }
                }
            }
        }
    }
}
