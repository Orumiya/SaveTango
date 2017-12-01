using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SaveTango.Model;
using SaveTango.ViewModel;
using System.Windows.Threading;

namespace SaveTango
{
    /// <summary>
    /// Interaction logic for BoardPage.xaml
    /// </summary>
    public partial class BoardPage : Page
    {
        /// <summary>
        /// hivatkozást tárol a hozzá tartozó ViewModelre
        /// </summary>
        private BoardWindowViewModel bwVM;

        /// <summary>
        /// le van-e nyomva az egér gombja?
        /// </summary>
        private bool pressed = false;

        /// <summary>
        /// egy aktuálisan megfogott és mozgatott Block esetében eltárolja, 
        /// hogyha 2 mező között elengedték valahol, mennyi pixel hiányzik,
        /// hogy elérje a megfelelő pozícióját a mezőben
        /// </summary>
        private int missingX = 0;

        /// <summary>
        /// egy aktuálisan megfogott és mozgatott Block esetében eltárolja, 
        /// hogyha 2 mező között elengedték valahol, mennyi pixel hiányzik,
        /// hogy elérje a megfelelő pozícióját a mezőben
        /// </summary>
        private int missingY = 0;

        /// <summary>
        /// hol kattintottam le az egeret az Image-n belül? mindig lekérhető 
        /// az x és y propertyje --> pl Image-n belül 7 pixellel lejjebb
        /// </summary>
        private Point clickedPoint;

        /// <summary>
        /// timer a Blockkok automatikus csúsztatásához
        /// </summary>
        private DispatcherTimer timer;

        public int level;
        /// <summary>
        /// hivatkozást tárol a MainWindow mainFrame-jére
        /// </summary>
        private Frame mainFrame;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardPage"/> class.
        /// </summary>
        public BoardPage(Frame mainFrame, int level)
        {
            this.InitializeComponent();
            this.level = level;
            this.Gameplay = new GamePlay(level);
            this.mainFrame = mainFrame;
        }

        /// <summary>
        /// hivatkozást tárol a Gameplay osztályra
        /// </summary>
        private GamePlay Gameplay { get; set; }

        /// <summary>
        /// hivatkozást tárol az épp aktuálisan megfogott elemre
        /// </summary>
        private Block ActualBlock { get; set; }

        private int MinLimitLeftPixel { get; set; }

        private int MaxLimitRightPixel { get; set; }

        private int MinLimitTopPixel { get; set; }

        private int MaxLimitBottomPixel { get; set; }

        private Direction Direction { get; set; }

        private Image actualImage;

        private Tango tango;

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
            if (sender is Image)
            {
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
            if (this.pressed && sender is Image)
            {
                double directionHorizontal = this.DirectionHorizontalPixelCounter(sender, e);
                double directionVertical = this.DirectionVerticalPixelCounter(sender, e, directionHorizontal);

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
            }
        }

        /// <summary>
        /// az egér elengedése által indított metódus:
        /// az egér gombja fel van engedve,
        /// elengedi a fogott Image-t,
        /// ha az elengedett Block 2 mező között van, akkor Timer-t indít, hogy a helyére csúsztassa
        /// a táblában átírja a foglalt és szabad helyeket
        /// megnézi, hogy Tango megmenekült-e
        /// </summary>
        /// <param name="sender">az OnMouseUp metódus object típusú paramétere (automatikus)</param>
        /// <param name="e">az OnMouseUp metódus MouseButtonEventArgs típusú paramétere (automatikus)</param>
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 20)
            };
            this.timer.Tick += this.Timer_Tick;
            this.pressed = false;
            if (sender is Image)
            {
                this.actualImage = (Image)sender;
                int gx = (int)Canvas.GetTop((Image)sender);
                int gy = (int)Canvas.GetLeft((Image)sender);
                int getX = 0;
                int getY = 0;
                if (gx % 100 > 50)
                {
                    getX = ((int)Canvas.GetTop((Image)sender) / 100) + 1;
                    this.missingX = 100 - (gx % 100);
                }
                else
                {
                    getX = (int)Canvas.GetTop((Image)sender) / 100;
                    this.missingX = (gx % 100) * (-1);
                }

                if (gy % 100 > 50)
                {
                    getY = ((int)Canvas.GetLeft((Image)sender) / 100) + 1;
                    this.missingY = 100 - (gy % 100);
                }
                else
                {
                    getY = (int)Canvas.GetLeft((Image)sender) / 100;
                    this.missingY = (gy % 100) * (-1);
                }

                // itt mindig a Block InitialX- és InitialY elemét kell kapnia, különben exceptiont dob
                if (this.ActualBlock != null)
                {
                    this.timer.Start();
                    if (getX != this.ActualBlock.OnTableX || getY != this.ActualBlock.OnTableY)
                    {
                        this.Gameplay.ActualSetupUpdater(this.ActualBlock, getX, getY, this.Direction);
                        this.bwVM.MoveCounter();

                    }
                }
            }

            Mouse.Capture(null);
            this.ActualBlock = null;

            this.GameEnd();
        }

        /// <summary>
        /// A metódus végzi a 2 mező között hagyott Blockkok helyére csúsztatását
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            // lefelé
            if (this.missingX > 0)
            {
                for (int i = 0; i < this.missingX; i++)
                {
                    double top = Canvas.GetTop(this.actualImage) + 1;
                    Canvas.SetTop(this.actualImage, top);
                }

                this.missingX = 0;
                this.missingY = 0;
                this.timer.Stop();
            }

            // felfelé
            else if (this.missingX < 0)
            {
                for (int i = 0; i > this.missingX; i--)
                {
                    double top = Canvas.GetTop(this.actualImage) - 1;
                    Canvas.SetTop(this.actualImage, top);
                }

                this.missingX = 0;
                this.missingY = 0;
                this.timer.Stop();
            }

            // jobbra
            else if (this.missingY > 0)
            {
                for (int i = 0; i < this.missingY; i++)
                {
                    double left = Canvas.GetLeft(this.actualImage) + 1;
                    Canvas.SetLeft(this.actualImage, left);
                }

                this.missingX = 0;
                this.missingY = 0;
                this.timer.Stop();
            }

            // balra
            else if (this.missingY < 0)
            {
                for (int i = 0; i > this.missingY; i--)
                {
                    double left = Canvas.GetLeft(this.actualImage) - 1;
                    Canvas.SetLeft(this.actualImage, left);
                }

                this.missingX = 0;
                this.missingY = 0;
                this.timer.Stop();
            }
            else
            {
                this.timer.Stop();
            }
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

        /// <summary>
        /// A page betöltődésekor lefutó metódus, ami felteszi a grafikus felületre a setupnak megfelelően a Blockkokat, ill.
        /// feliratkoztat a Mouse metódusokra
        /// </summary>
        /// <param name="sender"> a Page_Loaded metódus object típusú paramétere </param>
        /// <param name="e">a Page_Loaded metódus RoutedEventArgs típusú paramétere</param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.bwVM = new BoardWindowViewModel(this.level);
            this.DataContext = this.bwVM;
            for (int i = 0; i < this.bwVM.GamePlay.LevelSetup.Count; i++)
            {
                Image img = this.bwVM.GamePlay.LevelSetup[i].BlockImage;
                img.Name = "blockimage" + i.ToString();
                //this.RegisterName(img.Name, img);
                Canvas.SetTop(img, this.bwVM.GamePlay.LevelSetup[i].OnTableX * 100);
                Canvas.SetLeft(img, this.bwVM.GamePlay.LevelSetup[i].OnTableY * 100);
                img.MouseDown += this.OnMouseDown;
                img.MouseUp += this.OnMouseUp;
                img.MouseMove += this.OnMouseMove;
                this.boardCanvas.Children.Add(img);
            }
        }

        /// <summary>
        /// a játék nyerési feltétele az, hogy Tango elérje a kijáratot
        /// </summary>
        private void GameEnd()
        {
            this.Gameplay.DoesWeHaveAWinner();
            if (this.Gameplay.IsTangoSaved)
            {
                this.bwVM.StopTimer();
                this.timer.Stop();
                EndPage endPage = new EndPage(this.mainFrame,this.level, this.bwVM.TimeElapsed,this.bwVM.MovesSum);
                this.mainFrame.Content = endPage;
            }
        }

        /// <summary>
        /// a vissza gombbal a LevelSelector oldalra lehet visszalépni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToLS_Click(object sender, RoutedEventArgs e)
        {
            this.Gameplay = null;
            this.bwVM.StopTimer();
            this.DataContext = null;
            this.bwVM = null;
            LevelSelector levelSel = new LevelSelector(this.mainFrame);
            this.mainFrame.Content = levelSel;
        }

       /// <summary>
       /// a replay gombbal újra lehet kezdeni a játékot
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void Replay_Click(object sender, RoutedEventArgs e)
        {
            BoardPage boardPage = new BoardPage(this.mainFrame, this.level);
            this.mainFrame.Content = boardPage;
        }
    }
}
