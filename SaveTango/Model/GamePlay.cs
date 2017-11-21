namespace SaveTango.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Controls;

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
            set { this.moves = value; }
        }

        private Dictionary<Image, Block> imageBlockDictionary;
        public Block WhichBlockIsThis(Image image)
        {
            Block block = this.imageBlockDictionary[image];
            return block;
        }

        /// <summary>
        /// Az aktuálisan kijelölt Blockot lehet-e 1-el elmozgatni, amennyiben a Block vízszintesen fekszik
        /// </summary>
        /// <param name="actualBlock"></param>
        /// <param name="vectorX"> értéke csak -1,0,1 lehet</param>
        /// <param name="vectorY"> értéke csak -1,0,1 lehet</param>
        /// <returns></returns>
        public bool HorizontalBlockIsMovableOrNot(Block actualBlock, int vectorX, int vectorY)
        {
            // ha vízszintesen irányba mozgatjuk - ilyenkor balra -1 értékünk érkezik, jobbra +1
            if (vectorX != 0)
            {
                // balra mozgatva van-e helye (-1 értékkel dolgozunk)
                if (vectorX < 0)
                {
                    if (actualBlock.OnTableX + vectorX >= 0 && !this.Board.Table[actualBlock.OnTableX + vectorX, actualBlock.OnTableY])
                    {
                        return true;
                    }
                }

                // jobbra mozgatva van-e helye, de itt figyelembe kell venni már a Block hosszát is
                else
                {
                    if (actualBlock.BlockLength == 2)
                    {
                        if (actualBlock.OnTableX + 1 + vectorX < 6 && !this.Board.Table[actualBlock.OnTableX +1 + vectorX, actualBlock.OnTableY])
                        {
                            return true;
                        }
                    }
                    else if (actualBlock.BlockLength == 3)
                    {
                        if (actualBlock.OnTableX + 2 + vectorX < 6 && !this.Board.Table[actualBlock.OnTableX + 2 + vectorX, actualBlock.OnTableY])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool VerticalBlockIsMovableOrNot(Block actualBlock, int vectorX, int vectorY)
        {
            // függőleges irányba mozgatjuk - ilyenkor felfelé -1 értékünk érkezik, lefelé +1
            if (vectorY != 0)
            {
                // felfelé mozgatva van-e helye
                if (vectorY < 0)
                {
                    if (actualBlock.OnTableY + vectorY >= 0 && !this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY - vectorY])
                    {
                        return true;
                    }
                }

                // lefelé mozgásnál +1-et kaptunk, itt figyelembe kell venni már a Block hosszát is
                else
                {
                    if (actualBlock.BlockLength == 2)
                    {
                        if (actualBlock.OnTableY + 1 + vectorY < 6 && !this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY + 1 + vectorY])
                        {
                            return true;
                        }
                    }
                    else if (actualBlock.BlockLength == 3)
                    {
                        if (actualBlock.OnTableY + 2 + vectorY < 6 && !this.Board.Table[actualBlock.OnTableX, actualBlock.OnTableY + 2 + vectorY])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void ActualSetupUpdater(Block actualBlock)
        {

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
            int positionX;
            int positionY;
            for (int i = 0; i < this.levelSetup.Count; i++)
            {
                positionX = this.levelSetup[i].InitialCanvasLeft / 100;
                positionY = this.levelSetup[i].InitialCanvasTop / 100;
                this.Board.Table[positionY, positionX] = true;
                if (this.levelSetup[i].Vertical)
                {
                    positionY = (this.levelSetup[i].InitialCanvasTop + 100) / 100;
                    positionX = this.levelSetup[i].InitialCanvasLeft / 100;
                    this.Board.Table[positionY, positionX] = true;
                    if (this.levelSetup[i].BlockLength == 3)
                    {
                        positionY = (this.levelSetup[i].InitialCanvasTop + 200) / 100;
                        positionX = this.levelSetup[i].InitialCanvasLeft / 100;
                        this.Board.Table[positionY, positionX] = true;
                    }
                }
                else
                {
                    positionX = (this.levelSetup[i].InitialCanvasLeft + 100) / 100;
                    positionY = this.levelSetup[i].InitialCanvasTop / 100;
                    this.Board.Table[positionY, positionX] = true;

                    if (this.levelSetup[i].BlockLength == 3)
                    {
                        positionX = (this.levelSetup[i].InitialCanvasLeft + 200) / 100;
                        positionY = this.levelSetup[i].InitialCanvasTop / 100;
                        this.Board.Table[positionY, positionX] = true;
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
