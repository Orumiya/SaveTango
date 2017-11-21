using SaveTango.Model;
using SaveTango.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaveTango
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private BoardWindowViewModel bwVM;

        /// <summary>
        /// le van-e nyomva az egér gombja?
        /// </summary>
        private bool pressed = false;
        Image selectedImage;

        /// <summary>
        /// hol kattintottam le az egeret az Image-n belül? mindig lekérhető az x és y propertyje --> pl Image-n belül 7 pixellel lejjebb
        /// </summary>
        private Point clickedPoint;

        GamePlay gameplay;

        public List<Rect> BoardElements { get; set; }

        public BoardWindow()
        {
            InitializeComponent();
            gameplay = new GamePlay();
            TextboxTest.Text = gameplay.LevelSetup[0].OnTableX.ToString();
        }

        public void RectListFiller(Image image)
        {
            this.BoardElements = new List<Rect>();
            foreach (Image item in boardCanvas.Children)
            {
                if (item != image)
                {
                    BoardElements.Add(new Rect(Canvas.GetLeft(item), Canvas.GetTop(item), item.ActualWidth, item.ActualHeight));
                }
            }
        }

        /// <summary>
        /// a tábláról érkező egérkattintás metódusa:
        /// az egér gombja le van nyomva,
        /// megkeresi, hogy az Image-n belül hol van az egér,
        /// és megfogja az objektumot
        /// </summary>
        /// <param name="sender">az OnMouseDown metódus object típusú paramétere (automatikus)</param>
        /// <param name="e">az OnMouseDown metódus MouseButtonEventArgs típusú paramétere (automatikus)</param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.pressed = true;
            this.clickedPoint = Mouse.GetPosition((Image)sender); //a clickedpoint azt jelenti, hogy a Blockon belül hova kattint a user (ezzel kell korrigálni a vertical és horizontal mozgatásban használt koordinátákat)
            //RectListFiller((Image)sender);
            Mouse.Capture((Image)sender);
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
            Mouse.Capture(null);
            BoolTombotKiir();
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
            Block block = this.bwVM.GamePlay.WhichBlockIsThis((Image)sender);
            //Rect movedRect = new Rect(Canvas.GetLeft((Image)sender), Canvas.GetTop((Image)sender), ((Image)sender).Width, ((Image)sender).Height);
            int verticalX = 0;
            int verticalY = 0;

            if (this.pressed)
            {
                string tesztszoveg = "Clickpoint x: " + clickedPoint.X + " y: " + clickedPoint.Y;
                double posEX = e.GetPosition(this.boardCanvas).X- clickedPoint.X; //egér távolsága a canvas kezdő pontjától (x tengely mentén)
                tesztszoveg += "posEx: "+posEX + "\t"+" Initialleft: "+ block.InitialCanvasLeft;
                double directionX = block.InitialCanvasLeft - posEX ; //egér távolsága a megnyomott Block kezdő pontjától (x tengely mentén)
                tesztszoveg += "DirectionX: "+ directionX;
                
                verticalX = directionX > 0 ? -1 : 1;
                    //currentPositionX = e.GetPosition(this).X;
                
                double directionY = block.InitialCanvasTop - e.GetPosition(this.boardCanvas).Y - clickedPoint.Y;
                verticalY = directionY > 0 ? -1 : 1;
            
                if (block.Vertical)
                {
                    verticalX = 0;
                    if (this.bwVM.GamePlay.VerticalBlockIsMovableOrNot(block, verticalX, verticalY))
                    {
                        tesztszoveg += '\n' + "verticalX: " + verticalX + " verticalY: " + verticalY;
                        double top = Canvas.GetTop((Image)sender) + Mouse.GetPosition((Image)sender).Y - this.clickedPoint.Y;
                        Canvas.SetTop((Image)sender, top);
                    }
                    
                }
                else
                {
                    verticalY = 0;
                    if (this.bwVM.GamePlay.HorizontalBlockIsMovableOrNot(block, verticalX, verticalY))
                    {
                        tesztszoveg += '\n' + "verticalX: " + verticalX + " verticalY: " + verticalY;
                        double left = Canvas.GetLeft((Image)sender) + Mouse.GetPosition((Image)sender).X - this.clickedPoint.X;
                        Canvas.SetLeft((Image)sender, left);
                    }
                   
                }
                TextboxTest.Text = tesztszoveg;
            }
        }
        //public bool Collisions(Rect image)
        //{
        //    string text = "";
        //    for (int i = 0; i < BoardElements.Count; i++)
        //    {
        //        text += image.IntersectsWith(BoardElements[i]);
        //        //textbox.Text = text;
        //        if (image.IntersectsWith(BoardElements[i]))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.bwVM = new BoardWindowViewModel();
            this.DataContext = this.bwVM;
            for (int i = 0; i < this.bwVM.GamePlay.LevelSetup.Count; i++)
            {
                Image img = this.bwVM.GamePlay.LevelSetup[i].BlockImage;
                img.Name = "blockimage" + i.ToString();
                this.RegisterName(img.Name, img);
                Canvas.SetTop(img, this.bwVM.GamePlay.LevelSetup[i].InitialCanvasTop);
                Canvas.SetLeft(img, this.bwVM.GamePlay.LevelSetup[i].InitialCanvasLeft);
                img.MouseDown += this.OnMouseDown;
                img.MouseUp += this.OnMouseUp;
                img.MouseMove += this.OnMouseMove;
                this.boardCanvas.Children.Add(img);
            }

        }
        public void BoolTombotKiir()
        {
            string tomb = "";
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    tomb += gameplay.Board.Table[i, j] + "\t";
                }
                tomb += '\n';
            }
            TextboxTest2.Text = tomb;


        }
    }
}
