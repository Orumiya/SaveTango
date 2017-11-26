namespace SaveTango.Model
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    public class Bar : Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bar"/> class.
        /// A Bar osztály konstruktora
        /// </summary>
        /// <param name="vertical">függőlegesen áll-e a blokk?</param>
        /// <param name="blockLength">milyen hosszú a blokk?</param>
        /// <param name="blockImage">a blokkhoz tartozó kép</param>
       
        public Bar(bool vertical, int blockLength, int onTableX, int onTableY)
        {
            this.Vertical = vertical;
            this.BlockLength = blockLength;
            this.BlockImage = this.WhichImageGoesToWhichBlock();
            this.OnTableX = onTableX;
            this.OnTableY = onTableY;


        }

        public override bool Vertical { get => base.Vertical; set => base.Vertical = value; }

        public override int BlockLength { get => base.BlockLength; set => base.BlockLength = value; }

        public override Image BlockImage { get => base.BlockImage; set => base.BlockImage = value; }

        public override int InitialCanvasTop { get => base.InitialCanvasTop; set => base.InitialCanvasTop = value; }

        public override int InitialCanvasLeft { get => base.InitialCanvasLeft; set => base.InitialCanvasLeft = value; }
        public override int OnTableX { get => base.OnTableX; set => base.OnTableX = value; }
        public override int OnTableY { get => base.OnTableY; set => base.OnTableY = value; }


        /// <summary>
        /// megállapítja, hogy a táblán levő rúd hossza és orientációja
        /// /// alapján milyen képet kell hozzárendelni
        /// </summary>
        /// <returns>visszaadja a megfelelő képet</returns>
        private Image WhichImageGoesToWhichBlock()
        {
            Image img = new Image();
            Uri uri;
            if (this.Vertical & this.BlockLength == 2)
            {
                uri = new Uri("pack://application:,,,/res/2vertical.PNG", UriKind.RelativeOrAbsolute);
                img.Source = new BitmapImage(uri);
                return img;

            }
            else if (this.Vertical & this.BlockLength == 3)
            {
                uri = new Uri("pack://application:,,,/res/3vertical.PNG", UriKind.RelativeOrAbsolute);
                img.Source = new BitmapImage(uri);
                return img;
            }
            else if (!this.Vertical & this.BlockLength == 2)
            {
                uri = new Uri("pack://application:,,,/res/2horizontal.PNG", UriKind.RelativeOrAbsolute);
                img.Source = new BitmapImage(uri);
                return img;
            }
            else
            {
                uri = new Uri("pack://application:,,,/res/3horizontal.PNG", UriKind.RelativeOrAbsolute);
                img.Source = new BitmapImage(uri);
                return img;
            }
        }
    }
}
