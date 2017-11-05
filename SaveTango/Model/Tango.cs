using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SaveTango.Model
{
    public class Tango : Block
    {

        public Tango()
        {
            this.BlockImage = GetMeTheBestTangoPicture();
            this.InitialCanvasTop = 200;
            this.InitialCanvasLeft = 0;
        }

        public override bool Vertical { get => false; }

        public override int BlockLength { get => 2; }

        public override Image BlockImage { get => base.BlockImage; }

        public override int InitialCanvasTop { get => base.InitialCanvasTop; set => base.InitialCanvasTop = value; }

        public override int InitialCanvasLeft { get => base.InitialCanvasLeft; set => base.InitialCanvasLeft = value; }

        public Image GetMeTheBestTangoPicture()
        {
            Image img = new Image();
            Uri uri = new Uri("pack://application:,,,/res/Tango.PNG", UriKind.RelativeOrAbsolute);
            img.Source = new BitmapImage(uri);
            return img;
        }
    }
}
