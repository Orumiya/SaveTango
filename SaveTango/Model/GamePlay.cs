namespace SaveTango.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
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
        public GamePlay()
        {
            this.Board = new Board();
            this.levelSetup = new ObservableCollection<Block> {
                new Bar(true, 2, 0, 0),
                new Bar(true, 2, 3, 0),
                new Bar(true, 2, 0, 3),
                new Bar(true, 3, 0, 2),
                new Bar(true, 3, 2, 5),
                new Bar(false, 2, 0, 4),
                new Bar(false, 2, 1, 4),
                new Bar(false, 3, 3, 1),
                new Bar(false, 2, 5, 1),
                new Tango()
            };
            this.MakeADictionary();
            this.TheCollisionTableCreator();
            //this.StopwatchTimer();
            
        }

        public Board Board { get; set; }
        
        /// <summary>
        /// játékos idejét tároló változó
        /// </summary>
        private TimeSpan playerTime;

        public TimeSpan PlayerTime
        {
            get { return playerTime; }
            set { playerTime = value;
                  OnPropertyChanged("PlayerTime");
            }
        }

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

        /// <summary>
        /// a játékos által elhúzott blokkok száma
        /// </summary>
        private int moves;

        public int Moves
        {
            get { return this.moves; }

            set { this.moves = value;
                OnPropertyChanged("Moves");
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
                this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY] = FieldType.Taken;

                if (direction == Direction.Up)
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
                else if (direction == Direction.Down)
                {
                    if (actualBlock.BlockLength == 2)
                    {
                        this.Board.Table[actualBlock.OnTableX - 1, actualBlock.OnTableY] = FieldType.Taken;
                    }
                    else
                    {
                        this.Board.Table[actualBlock.OnTableX - 1, actualBlock.OnTableY] = FieldType.Taken;
                        this.Board.Table[actualBlock.OnTableX - 2, actualBlock.OnTableY] = FieldType.Taken;
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
        /// 
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

        //public void StopwatchTimer() {
        //    Stopwatch stopWatch = new Stopwatch();
        //    stopWatch.Start();
        //    Thread.Sleep(1000);
        //    // Get the elapsed time as a TimeSpan value.
        //    this.playerTime = stopWatch.Elapsed;

        //    // Format and display the TimeSpan value.
        //    string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        //        playerTime.Hours, playerTime.Minutes, playerTime.Seconds,
        //        playerTime.Milliseconds / 10);  
        //}
    }
}
