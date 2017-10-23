using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaveTango
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : Window
    {
        /// <summary>
        /// le van-e nyomva az egér gombja?
        /// </summary>
        private bool pressed = false;

        /// <summary>
        /// hol kattintottam le az egeret az Image-n belül? mindig lekérhető az x és y propertyje --> pl Image-n belül 7 pixellel lejjebb
        /// </summary>
        private Point clickedPoint;

        public Board()
        {
            InitializeComponent();
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
        /// akkor lekéri a xaml-ből a kattintott Image objektum 
        /// </summary>
        /// <param name="sender">az OnMouseMove metódus object típusú paramétere (automatikus)</param>
        /// <param name="e">az OnMouseMove metódus MouseButtonEventArgs típusú paramétere (automatikus)</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (pressed)
            {
                double top = Canvas.GetTop((Image)sender) + Mouse.GetPosition((Image)sender).Y - clickedPoint.Y; //mivel a téglalapba nem mindig a tetején kattintok
                //Canvas.SetTop((Image)sender, top); //állítsd be az objektum felső koordinátáját + beadom neki a top változót
                double left = Canvas.GetLeft((Image)sender) + Mouse.GetPosition((Image)sender).X - clickedPoint.X; //mivel a téglalapba nem mindig a tetején kattintok
                Canvas.SetLeft((Image)sender, left);
            }
        }

        Dictionary<Image, Block> imageBlockDictionary;

        private void WhichBlockIsMyImage()
        {
            imageBlockDictionary = new Dictionary<Image, Block>();
            
        }
    }
}
