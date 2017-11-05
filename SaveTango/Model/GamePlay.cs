using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveTango.Model
{
    class GamePlay : Bindable
    {
        public GamePlay()
        {
        }

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

        /// <summary>
        /// a változó azt tartja számon, hogy a tábla adott pozíciójá
        /// foglalt-e, van-e már ott elem
        /// true, ha van ott elem
        /// false, ha üres a pozíció
        /// </summary>
        private Dictionary<string, bool> theCollisionDictionary;

        public Dictionary<string, bool> TheCollisionDictionary
        {
            get { return this.theCollisionDictionary; }
            set { this.theCollisionDictionary = value; }
        }

        /// <summary>
        /// a kiválasztott játék felállását létrehozó metódus
        /// </summary>
        /// <param name="selectedLevel">a játékos által kiválasztott pálya száma (1-16-ig)</param>
        private void LevelSetupSelector(int selectedLevel)
        {
            switch (selectedLevel)
            {
                case 1:
                    this.levelSetup = new ObservableCollection<Block>
                {
                new Bar(true, 2, 0, 0),
                new Bar(true, 2, 0, 300),
                new Bar(true, 2, 300, 0),
                new Bar(true, 3, 0, 200),
                new Bar(true, 3, 200, 500),
                new Bar(false, 2, 0, 400),
                new Bar(false, 2, 100, 400),
                new Bar(false, 3, 300, 100),
                new Bar(false, 2, 500, 100)
            };
                    break;
                //case 2:
                //    break;
                //case 3:
                //    break;
                //case 4:
                //    break;
                //case 5:
                //    break;
                //case 6:
                //    break;
                //case 7:
                //    break;
                //case 8:
                //    break;
                //case 9:
                //    break;
                //case 10:
                //    break;
                //case 11:
                //    break;
                //case 12:
                //    break;
                //case 13:
                //    break;
                //case 14:
                //    break;
                //case 15:
                //    break;
                //case 16:
                //    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// a pozíciónak a sorrendje: x,y koordináta, azaz
        /// először a CanvasLeft, majd a CanvasTop
        /// </summary>
        private void TheCollisionDictionaryCreator()
        {
            this.theCollisionDictionary = new Dictionary<string, bool>();
            string position = string.Empty;
            for (int i = 0; i < this.levelSetup.Count; i++)
            {
                position = this.levelSetup[i].InitialCanvasLeft.ToString() + "_" + this.levelSetup[i].InitialCanvasTop.ToString();
                this.theCollisionDictionary.Add(position, true);
                if (this.levelSetup[i].Vertical)
                {
                    int fieldPositionTop = this.levelSetup[i].InitialCanvasTop + 100;
                    position = this.levelSetup[i].InitialCanvasLeft.ToString() + "_" + fieldPositionTop.ToString();
                    this.theCollisionDictionary.Add(position, true);
                    if (this.levelSetup[i].BlockLength == 3)
                    {
                        fieldPositionTop = this.levelSetup[i].InitialCanvasTop + 200;
                        position = this.levelSetup[i].InitialCanvasLeft.ToString() + "_" + fieldPositionTop.ToString();
                        this.theCollisionDictionary.Add(position, true);
                    }
                }
                else
                {
                    int fieldPositionLeft = this.levelSetup[i].InitialCanvasLeft + 100;
                }
            }
        }
    }
}
