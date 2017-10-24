using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SaveTango.Model
{
    public class Block
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// A Block osztály konstruktora
        /// </summary>
        /// <param name="vertical">függőlegesen áll-e a blokk?</param>
        /// <param name="blockLength">milyen hosszú a blokk?</param>
        /// <param name="blockImage">a blokkhoz tartozó kép</param>
        /// <param name="initialCanvasTop">a blokk kezdő pozíciója a tábla tetejétől számítva</param>
        /// <param name="initialCanvasLeft">a blokk kezdő pozíciója a tábla bal oldalától számítva</param>
        public Block(bool vertical, int blockLength, int initialCanvasTop, int initialCanvasLeft)
        {
            this.vertical = vertical;
            this.blockLength = blockLength;
            this.blockImage = this.WhichImageGoesToWhichBlock();
            this.initialCanvasTop = initialCanvasTop;
            this.initialCanvasLeft = initialCanvasLeft;
        }

        /// <summary>
        /// a vertical property azt mondja meg, hogy a blokk horizontálisan vagy vertikálisan helyezkedik-e el
        /// vertical igaz, ha vertikális
        /// vertical hamis, ha horizontális
        /// </summary>
        private bool vertical;

        public bool Vertical
        {
            get { return this.vertical; }
            set { this.vertical = value; }
        }

        /// <summary>
        /// a blockLength property beállítja a blokk méretét
        /// lehetséges értékek a játékban: 2 vagy 3
        /// </summary>
        private int blockLength;

        public int BlockLenght
        {
            get { return this.blockLength; }
            set { this.blockLength = value; }
        }

        /// <summary>
        /// a Block-nak a vizuális megjelenési formája a táblán
        /// </summary>
        private Image blockImage;

        public Image BlockImage
        {
            get { return this.blockImage; }
            set { this.blockImage = value; }
        }

        /// <summary>
        /// a blokk kezdő pozíciója a tábla tetejétől számítva
        /// </summary>
        private int initialCanvasTop;

        public int InitialCanvasTop
        {
            get { return this.initialCanvasTop; }
            set { this.initialCanvasTop = value; }
        }

        /// <summary>
        /// a blokk kezdő pozíciója a tábla bal oldalától számítva
        /// </summary>
        private int initialCanvasLeft;

        public int InitialCanvasLeft
        {
            get { return this.initialCanvasLeft; }
            set { this.initialCanvasLeft = value; }
        }

        private Image WhichImageGoesToWhichBlock()
        {
            Image img = new Image();
            Uri uri;
            if (this.vertical & this.blockLength == 2)
            {
                uri = new Uri("pack://application:,,,/res/2vertical.PNG", UriKind.RelativeOrAbsolute);
                img.Source = new BitmapImage(uri);
                return img;

            }
            else if (this.vertical & this.blockLength == 3)
            {
                uri = new Uri("pack://application:,,,/res/3vertical.PNG", UriKind.RelativeOrAbsolute);
                img.Source = new BitmapImage(uri);
                return img;
            }
            else if (!this.vertical & this.blockLength == 2)
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
