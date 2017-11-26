namespace SaveTango
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using SaveTango.Model;
    using SaveTango.ViewModel;

    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        public BoardWindow()
        {
            this.InitializeComponent();
            this.Gameplay = new GamePlay();
            TextboxTest.Text = Gameplay.LevelSetup[0].OnTableX.ToString();
        }

        private BoardWindowViewModel bwVM;

        /// <summary>
        /// le van-e nyomva az egér gombja?
        /// </summary>
        private bool pressed = false;


        /// <summary>
        /// hol kattintottam le az egeret az Image-n belül? mindig lekérhető az x és y propertyje --> pl Image-n belül 7 pixellel lejjebb
        /// </summary>
        private Point clickedPoint;

        private GamePlay Gameplay { get; set; }

        private Block ActualBlock { get; set; }

        private int MinLimitLeftPixel { get; set; }

        private int MaxLimitRightPixel { get; set; }

        private int MinLimitTopPixel { get; set; }

        private int MaxLimitBottomPixel { get; set; }

        private Direction Direction { get; set; }

        /// <summary>
        /// Az ablak betöltődésekor lefutó metódus, ami felteszi a grafikus felületre a setupnak megfelelően a Blockkokat, ill.
        /// feliratkoztat a Mouse metódusokra
        /// </summary>
        /// <param name="sender"> az OnWindowLoaded metódus object típusú paramétere </param>
        /// <param name="e">az OnWindowLoaded metódus RoutedEventArgs típusú paramétere</param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.bwVM = new BoardWindowViewModel();
            this.DataContext = this.bwVM;
            for (int i = 0; i < this.bwVM.GamePlay.LevelSetup.Count; i++)
            {
                Image img = this.bwVM.GamePlay.LevelSetup[i].BlockImage;
                img.Name = "blockimage" + i.ToString();
                this.RegisterName(img.Name, img);
                Canvas.SetTop(img, this.bwVM.GamePlay.LevelSetup[i].OnTableX * 100);
                Canvas.SetLeft(img, this.bwVM.GamePlay.LevelSetup[i].OnTableY * 100);
                img.MouseDown += this.OnMouseDown;
                img.MouseUp += this.OnMouseUp;
                img.MouseMove += this.OnMouseMove;
                this.boardCanvas.Children.Add(img);
            }
        }

        /// <summary>
        /// a tábláról érkező egérkattintás metódusa:
        /// az egér gombja le van nyomva,
        /// megkeresi, hogy az Image-n belül hol van az egér,
        /// megfogja az objektumot
        /// meghatározza az aktuálisan elkapott elem környezetét, hogy mennyit lehet vele mozogni és ezeket változókba írja
        /// </summary>
        /// <param name="sender">az OnMouseDown metódus object típusú paramétere (automatikus)</param>
        /// <param name="e">az OnMouseDown metódus MouseButtonEventArgs típusú paramétere (automatikus)</param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.pressed = true;
            this.clickedPoint = Mouse.GetPosition((Image)sender); // a clickedpoint azt jelenti, hogy a Blockon belül hova kattint a user (ezzel kell korrigálni a vertical és horizontal mozgatásban használt koordinátákat)
            Mouse.Capture((Image)sender);
            this.ActualBlock = this.bwVM.GamePlay.WhichBlockIsThis((Image)sender);
            this.Gameplay.MinLimitLeft = this.Gameplay.HorizontalBlockIsMovableToLeft(this.ActualBlock);
            this.Gameplay.MaxLimitRight = this.Gameplay.HorizontalBlockIsMovableToRight(this.ActualBlock);
            this.Gameplay.MinLimitTop = this.Gameplay.VerticalBlockIsMovableToTop(this.ActualBlock);
            this.Gameplay.MaxLimitBottom = this.Gameplay.VerticalBlockIsMovableToBottom(this.ActualBlock);
            this.MinLimitLeftPixel = this.Gameplay.MinLimitLeft * 100;
            this.MinLimitTopPixel = this.Gameplay.MinLimitTop * 100;
            this.MaxLimitBottomPixel = this.Gameplay.MaxLimitBottom * 100;
            this.MaxLimitRightPixel = this.Gameplay.MaxLimitRight * 100;
        }

        /// <summary>
        /// az egér mozgatásakor
        /// ha meg van nyomva az egér gombja,
        /// megnézi, hogy a kattintott blokk horizontális vagy vertikális-e, és
        /// ennek függvényében engedi mozgatni a képet.
        /// </summary>
        /// <param name="sender">az OnMouseMove metódus object típusú paramétere (automatikus)</param>
        /// <param name="e">az OnMouseMove metódus MouseButtonEventArgs típusú paramétere (automatikus)</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.pressed)
            {
                double directionHorizontal = this.DirectionHorizontalPixelCounter(sender, e);
                double directionVertical = this.DirectionVerticalPixelCounter(sender, e, directionHorizontal);
                //string tesztszoveg = "directionHorizontal: " + directionHorizontal;

                //tesztszoveg += "directionVertical: " + directionVertical;
                //TextboxTest.Text = tesztszoveg;

                if (this.ActualBlock.Vertical)
                {
                    if (this.Direction == Direction.Down)
                    {
                        if (directionVertical <= this.MaxLimitBottomPixel)
                        {
                            double top = Canvas.GetTop((Image)sender) + Mouse.GetPosition((Image)sender).Y - this.clickedPoint.Y;
                            Canvas.SetTop((Image)sender, top);
                        }
                        else if (directionVertical > this.MaxLimitBottomPixel)
                        {
                            double top = (this.ActualBlock.OnTableX * 100) + this.MaxLimitBottomPixel;
                            Canvas.SetTop((Image)sender, top);
                        }
                    }
                    else if (this.Direction == Direction.Up)
                    {
                        if (directionVertical <= this.MinLimitTopPixel)
                        {
                            double top = Canvas.GetTop((Image)sender) + Mouse.GetPosition((Image)sender).Y - this.clickedPoint.Y;
                            Canvas.SetTop((Image)sender, top);
                        }
                        else if (directionVertical > this.MinLimitTopPixel)
                        {
                            double top = (this.ActualBlock.OnTableX * 100) - this.MinLimitTopPixel;
                            Canvas.SetTop((Image)sender, top);
                        }
                    }
                }
                else
                {
                    if (this.Direction == Direction.Right)
                    {
                        if (directionHorizontal <= this.MaxLimitRightPixel)
                        {
                            double left = Canvas.GetLeft((Image)sender) + Mouse.GetPosition((Image)sender).X - this.clickedPoint.X;
                            Canvas.SetLeft((Image)sender, left);
                        }
                        else if (directionHorizontal > this.MaxLimitRightPixel)
                        {
                            double left = (this.ActualBlock.OnTableY * 100) + this.MaxLimitRightPixel;
                            Canvas.SetLeft((Image)sender, left);
                        }
                    }
                    else if (this.Direction == Direction.Left)
                    {
                        if (directionHorizontal <= this.MinLimitLeftPixel)
                        {
                            double left = Canvas.GetLeft((Image)sender) + Mouse.GetPosition((Image)sender).X - this.clickedPoint.X;
                            Canvas.SetLeft((Image)sender, left);
                        }
                        else if (directionHorizontal > this.MinLimitLeftPixel)
                        {
                            double left = (this.ActualBlock.OnTableY * 100) - this.MinLimitLeftPixel;
                            Canvas.SetLeft((Image)sender, left);
                        }
                    }

                }
                //TextboxTest.Text = tesztszoveg + verticalX;
            }
        }

        /// <summary>
        /// az egér elengedése által indított metódus:
        /// az egér gombja fel van engedve,
        /// elengedi a fogott Image-t
        /// </summary>
        /// <param name="sender">az OnMouseUp metódus object típusú paramétere (automatikus)</param>
        /// <param name="e">az OnMouseUp metódus MouseButtonEventArgs típusú paramétere (automatikus)</param>
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.pressed = false;
            // itt mindig a Block InitialX- és InitialY elemét kell kapnia, különben exceptiont dob
            this.Gameplay.ActualSetupUpdater(this.ActualBlock, 5, 4, this.Direction);
            Mouse.Capture(null);
            ActualBlock = null;
            BoolTombotKiir();
        }

        /// <summary>
        /// A metódus megadja azt a távolságot pixelben, amennyivel elmozgattuk a Blockot az egér nyomva tartásával vízszintes irányba
        /// Illetve beállítja a Direction propertyt annak függvényében, hogy az egérrel jobbra vagy balra mozdítottuk el a Blockot
        /// </summary>
        /// <param name="sender"> megkapja az OnMouseMove object paraméterét</param>
        /// <param name="e"> megkapja az OnMouseMove MouseEventArgs paraméterét</param>
        /// <returns>visszaadja a kiszámított double értéket</returns>
        private double DirectionHorizontalPixelCounter(object sender, MouseEventArgs e)
        {
            // egér távolsága a canvas kezdő pontjától (horizontálisan)
            double posEX = e.GetPosition(this.boardCanvas).X - this.clickedPoint.X;

            // ez jobbra mínusz irányba megy
            // egér távolsága a megnyomott Block kezdő pontjától (horizontálisan)
            double directionHorizontal = this.ActualBlock.InitialCanvasLeft - posEX + (this.ActualBlock.OnTableY * 100);

            if (directionHorizontal < 0)
            {
                this.Direction = Direction.Right;
                directionHorizontal *= -1;
            }
            else
            {
                this.Direction = Direction.Left;
            }

            return directionHorizontal;
        }

        /// <summary>
        /// A metódus megadja azt a távolságot pixelben, amennyivel elmozgattuk a Blockot az egér nyomva tartásával függőleges irányba
        /// Illetve beállítja a Direction propertyt annak függvényében, hogy az egérrel felfelé vagy lefelé mozdítottuk el a Blockot
        /// Ennek azonban csak akkor szabad megtörténnie, ha függőleges irányba többet mozdítottuk el az egeret, mint vízszintes irányba,
        /// különben felülírná a Direction propertyt
        /// </summary>
        /// <param name="sender"> megkapja az OnMouseMove object paraméterét</param>
        /// <param name="e"> megkapja az OnMouseMove MouseEventArgs paraméterét</param>
        /// <param name="directionHorizontal"> megkapja a horizontálisan elmozgatott pixelek számát</param>
        /// <returns>visszaadja a kiszámított double értéket</returns>
        private double DirectionVerticalPixelCounter(object sender, MouseEventArgs e, double directionHorizontal)
        {
            double posY = e.GetPosition(this.boardCanvas).Y - this.clickedPoint.Y; // egér távolsága a canvas kezdő pontjától (x tengely mentén)
            // ez lefele mínusz irányba megy
            double directionVertical = this.ActualBlock.InitialCanvasTop - posY + (this.ActualBlock.OnTableX * 100); // egér távolsága a megnyomott Block kezdő pontjától (vertikálisan)
            if (directionHorizontal < System.Math.Abs(directionVertical))
            {
                if (directionVertical < 0)
                {
                    this.Direction = Direction.Down;
                    directionVertical *= -1;
                }
                else
                {
                    this.Direction = Direction.Up;
                }
            }

            return directionVertical;
        }

        

        public void BoolTombotKiir()
        {
            string tomb = "";
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    tomb += Gameplay.Board.Table[i, j] + "\t";
                }
                tomb += '\n';
            }
            TextboxTest2.Text = tomb;


        }
    }
}
