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
    public partial class Board : Window
    {
        private BoardWindowViewModel bwVM;
        /// <summary>
        /// le van-e nyomva az egér gombja?
        /// </summary>
        private bool pressed = false;

        /// <summary>
        /// hol kattintottam le az egeret az Image-n belül? mindig lekérhető az x és y propertyje --> pl Image-n belül 7 pixellel lejjebb
        /// </summary>
        private Point clickedPoint;

        GamePlay gameplay;

        public Board()
        {
            InitializeComponent();
            gameplay = new GamePlay();
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
            this.clickedPoint = Mouse.GetPosition((Image)sender);
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
            Block block = this.bwVM.WhichBlockIsThis((Image)sender);
            if (this.pressed)
            {
                if (block.Vertical)
                {
                    double top = Canvas.GetTop((Image)sender) + Mouse.GetPosition((Image)sender).Y - this.clickedPoint.Y; 
                    Canvas.SetTop((Image)sender, top);
                }
                else
                {
                    double left = Canvas.GetLeft((Image)sender) + Mouse.GetPosition((Image)sender).X - this.clickedPoint.X;
                    Canvas.SetLeft((Image)sender, left);
                }
            }
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.bwVM = new BoardWindowViewModel();
            this.DataContext = this.bwVM;
            for (int i = 0; i < this.bwVM.GameLevelSetup.Count; i++)
            {
                Image img = this.bwVM.GameLevelSetup[i].BlockImage;
                img.Name = "blockimage" + i.ToString();
                this.RegisterName(img.Name, img);
                Canvas.SetTop(img, this.bwVM.GameLevelSetup[i].InitialCanvasTop);
                Canvas.SetLeft(img, this.bwVM.GameLevelSetup[i].InitialCanvasLeft);
                img.MouseDown += this.OnMouseDown;
                img.MouseUp += this.OnMouseUp;
                img.MouseMove += this.OnMouseMove;
                this.boardCanvas.Children.Add(img);
            }

        }
    }
}
