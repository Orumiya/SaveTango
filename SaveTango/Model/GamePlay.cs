namespace SaveTango.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading;
    using System.Windows.Controls;

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public class GamePlay : Bindable
    {
        public GamePlay(int level)
        {
            this.Board = new Board();
            this.ReadTheBoardSetup(level);
            this.MakeADictionary();
            this.TheCollisionTableCreator();
            this.IsTangoSaved = false;
        }

        public bool IsTangoSaved { get; set; }

        public Board Board { get; set; }

        /// <summary>
        /// a kiválasztott játék felállását tartalmazó gyűjtemény
        /// </summary>
        private ObservableCollection<Block> levelSetup;

        public ObservableCollection<Block> LevelSetup
        {
            get { return this.levelSetup; }

            set { this.levelSetup = value;
                OnPropertyChanged("LevelSetup");
            }
        }

        public int MinLimitLeft { get; set; }

        public int MaxLimitRight { get; set; }

        public int MinLimitTop { get; set; }

        public int MaxLimitBottom { get; set; }

        private Dictionary<Image, Block> imageBlockDictionary;

        public Block WhichBlockIsThis(Image image)
        {
            Block block = this.imageBlockDictionary[image];
            return block;
        }

        public void DoesWeHaveAWinner()
        {
            if (this.LevelSetup[this.LevelSetup.Count - 1] is Tango)
            {
                Tango tango = (Tango)this.LevelSetup[this.LevelSetup.Count - 1];
                this.IsTangoSaved = tango.IsTangoSaved();
            }
        }

        /// <summary>
        /// Az aktuálisan kijelölt vízszintesen fekvő Blocktól balra mennyi üres mező van?
        /// </summary>
        /// <param name="actualBlock">vizsgált vízszintes Block</param>
        /// <returns>a szabad mezők száma balra</returns>
        public int HorizontalBlockIsMovableToLeft(Block actualBlock)
        {
            int count = 0;
            if (!actualBlock.Vertical)
            {
                int i = actualBlock.OnTableY - 1;
                while (i >= 0 && this.Board.Table[actualBlock.OnTableX, i] == FieldType.Empty)
                {
                    count++;
                    i--;
                }
            }

            return count;
        }

        /// <summary>
        /// Az aktuálisan kijelölt vízszintesen fekvő Blocktól jobbra mennyi üres mező van?
        /// </summary>
        /// <param name="actualBlock">vizsgált vízszintes Block</param>
        /// <returns>a szabad mezők száma jobbra</returns>
        public int HorizontalBlockIsMovableToRight(Block actualBlock)
        {
            int count = 0;
            if (!actualBlock.Vertical)
            {
                if (actualBlock.BlockLength == 2)
                {
                    int i = actualBlock.OnTableY + 2;
                    while (i < this.Board.Table.GetLength(1) && this.Board.Table[actualBlock.OnTableX, i] == FieldType.Empty)
                    {
                        count++;
                        i++;
                    }
                }
                else if (actualBlock.BlockLength == 3)
                {
                    int i = actualBlock.OnTableY + 3;
                    while (i < this.Board.Table.GetLength(1) && this.Board.Table[actualBlock.OnTableX, i] == FieldType.Empty)
                    {
                        count++;
                        i++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Az aktuálisan kijelölt függőlegesen fekvő Blocktól felfelé mennyi üres mező van?
        /// </summary>
        /// <param name="actualBlock">vizsgált függőleges Block</param>
        /// <returns>a szabad mezők száma felfelé</returns>
        public int VerticalBlockIsMovableToTop(Block actualBlock)
        {
            int count = 0;
            if (actualBlock.Vertical)
            {
                int i = actualBlock.OnTableX - 1;
                while (i >= 0 && this.Board.Table[i, actualBlock.OnTableY] == FieldType.Empty)
                {
                    count++;
                    i--;
                }
            }

            return count;
        }

        /// <summary>
        /// Az aktuálisan kijelölt függőlegesen fekvő Blocktól lefelé mennyi üres mező van?
        /// </summary>
        /// <param name="actualBlock">vizsgált függőleges Block</param>
        /// <returns>a szabad mezők száma lefelé</returns>
        public int VerticalBlockIsMovableToBottom(Block actualBlock)
        {
            int count = 0;
            if (actualBlock.Vertical)
            {
                if (actualBlock.BlockLength == 2)
                {
                    int i = actualBlock.OnTableX + 2;
                    while (i < this.Board.Table.GetLength(0) && this.Board.Table[i, actualBlock.OnTableY] == FieldType.Empty)
                    {
                        count++;
                        i++;
                    }
                }
                else if (actualBlock.BlockLength == 3)
                {
                    int i = actualBlock.OnTableX + 3;
                    while (i < this.Board.Table.GetLength(0) && this.Board.Table[i, actualBlock.OnTableY] == FieldType.Empty)
                    {
                        count++;
                        i++;
                    }
                }
            }

            return count;
        }

        public void ActualSetupUpdater(Block actualBlock, int stepX, int stepY, Direction direction)
        {
            if (actualBlock.Vertical)
            {
                this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY] = FieldType.Empty;
                if (actualBlock.BlockLength == 2)
                {
                    this.Board.Table[actualBlock.OnTableX + 1, actualBlock.OnTableY] = FieldType.Empty;
                }
                else
                {
                    this.Board.Table[actualBlock.OnTableX + 1, actualBlock.OnTableY] = FieldType.Empty;
                    this.Board.Table[actualBlock.OnTableX + 2, actualBlock.OnTableY] = FieldType.Empty;
                }

                actualBlock.OnTableX = stepX;
                if (actualBlock is Tango)
                {
                    this.LevelSetup[this.LevelSetup.Count - 1].OnTableX = stepX;
                }

                this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY] = FieldType.Taken;

                if (direction == Direction.Up || direction == Direction.Down)
                {
                   if (actualBlock.BlockLength == 2)
                    {
                        this.Board.Table[actualBlock.OnTableX + 1, actualBlock.OnTableY] = FieldType.Taken;
                    }
                    else
                    {
                        this.Board.Table[actualBlock.OnTableX + 1, actualBlock.OnTableY] = FieldType.Taken;
                        this.Board.Table[actualBlock.OnTableX + 2, actualBlock.OnTableY] = FieldType.Taken;
                    }
                }
            }
            else
            {
                if (direction == Direction.Left || direction == Direction.Right)
                {
                    this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY] = FieldType.Empty;
                    if (actualBlock.BlockLength == 2)
                    {
                        this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY + 1] = FieldType.Empty;
                    }
                    else
                    {
                        this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY + 1] = FieldType.Empty;
                        this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY + 2] = FieldType.Empty;
                    }

                    actualBlock.OnTableY = stepY;
                    if (actualBlock is Tango)
                    {
                        this.LevelSetup[this.LevelSetup.Count - 1].OnTableY = stepY;
                    }
                    this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY] = FieldType.Taken;
                    if (actualBlock.BlockLength == 2)
                    {
                        this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY + 1] = FieldType.Taken;
                    }
                    else
                    {
                        this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY + 1] = FieldType.Taken;
                        this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY + 2] = FieldType.Taken;
                    }
                }
            }
        }

        /// <summary>
        /// összepárosítja az Image-t a Block-kal
        /// </summary>
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
            int positionX; // sor
            int positionY; // oszlop
            for (int i = 0; i < this.levelSetup.Count; i++)
            {
                positionX = this.levelSetup[i].OnTableX; // sor
                positionY = this.levelSetup[i].OnTableY; // oszlop
                this.Board.Table[positionX, positionY] = FieldType.Taken;
                if (this.levelSetup[i].Vertical)
                {
                    positionX = this.levelSetup[i].OnTableX + 1;
                    positionY = this.levelSetup[i].OnTableY;
                    this.Board.Table[positionX, positionY] = FieldType.Taken;
                    if (this.levelSetup[i].BlockLength == 3)
                    {
                        positionX = this.levelSetup[i].OnTableX + 2;
                        positionY = this.levelSetup[i].OnTableY;
                        this.Board.Table[positionX, positionY] = FieldType.Taken;
                    }
                }
                else
                {
                    positionY = this.levelSetup[i].OnTableY + 1;
                    positionX = this.levelSetup[i].OnTableX;
                    this.Board.Table[positionX, positionY] = FieldType.Taken;

                    if (this.levelSetup[i].BlockLength == 3)
                    {
                        positionY = this.levelSetup[i].OnTableY + 2;
                        positionX = this.levelSetup[i].OnTableX;
                        this.Board.Table[positionX, positionY] = FieldType.Taken;
                    }
                }
            }
        }

        /// <summary>
        /// beolvassa a különböző pályákhoz tartozó setupokat
        /// </summary>
        /// <param name="level"></param>
        private void ReadTheBoardSetup(int level)
        {
            string line = string.Empty;
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            _filePath += @"\levelsetup.txt";

            try
            {
                using (StreamReader sr = new StreamReader(_filePath))
                {
                    line += sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            { }
            string[] levelsetuptext = line.Split('\n');
            int i = 0;
            while (i < levelsetuptext.Length)
            {
                if (levelsetuptext[i][0] == '*')
                {
                    string szam = levelsetuptext[i][1].ToString() + levelsetuptext[i][2].ToString();
                    int szamLev = int.Parse(szam);
                    if (level == szamLev)
                    {
                        break;
                    }
                }

                i++;
            }

            // el kell érni az első sort a level fejléc után
            i++;
            this.levelSetup = new ObservableCollection<Block>();
            while (i < levelsetuptext.Length && levelsetuptext[i][0] != '*')
            {
                if (!levelsetuptext[i][0].Equals('T'))
                {
                    this.levelSetup.Add(new Bar(this.CharToBool(levelsetuptext[i][0]), int.Parse(levelsetuptext[i][2].ToString()), int.Parse(levelsetuptext[i][4].ToString()), int.Parse(levelsetuptext[i][6].ToString())));
                }
                else
                {
                    this.levelSetup.Add(new Tango());
                }

                i++;
            }
        }

        private bool CharToBool(char fort)
        {
            switch (fort)
            {
                case 't': return true;
                    break;
                case 'f': return false;
                    break;
                default: return false;
                    break;
            }
        }
    }
}
