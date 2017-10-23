using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SaveTango.Model
{
    class Block 
    {
        /// <summary>
        /// a vertical property azt mondja meg, hogy a blokk horizontálisan vagy vertikálisan helyezkedik-e el
        /// vertical igaz, ha vertikális
        /// vertical hamis, ha horizontális
        /// </summary>
        private bool vertical;

        public bool Vertical
        {
            get { return vertical; }
            set { vertical = value; }
        }

        /// <summary>
        /// a blockLength property beállítja a blokk méretét
        /// lehetséges értékek a játékban: 2 vagy 3
        /// </summary>
        private int blockLength;

        public int BlockLenght
        {
            get { return blockLength; }
            set { blockLength = value; }
        }

        /// <summary>
        /// a Block-nak a vizuális megjelenési formája a táblán
        /// </summary>
        private Image blockImage;

        public Image BlockImage
        {
            get { return blockImage; }
            set { blockImage = value; }
        }

        private int initialCanvasTop;

        public int InitialCanvasTop
        {
            get { return initialCanvasTop; }
            set { initialCanvasTop = value; }
        }


        private int initialCanvasLeft;

        public int InitialCanvasLeft
        {
            get { return initialCanvasLeft; }
            set { initialCanvasLeft = value; }
        }



        /// <summary>
        /// A Block osztály konstruktora három paraméterrel
        /// </summary>
        /// <param name="vertical">függőlegesen áll-e a blokk?</param>
        /// <param name="blockLength">milyen hosszú a blokk?</param>
        /// <param name="blockImage">a blokkhoz tartozó kép</param>
        public Block(bool vertical, int blockLength, int initialCanvasTop, int initialCanvasLeft)
        {
            this.vertical = vertical;
            this.blockLength = blockLength;
            this.blockImage = this.WhichImageGoesToWhichBlock();
            this.initialCanvasTop = initialCanvasTop;
            this.initialCanvasLeft = initialCanvasLeft;
        }

        private Image WhichImageGoesToWhichBlock()
        {
            Image img = new Image();
            Uri uri;
            if (vertical & blockLength ==2)
            {
                uri = new Uri("res/2vertical.PNG");
                img.Source = new BitmapImage(uri);
                return img;

            }
            else if (vertical & blockLength == 3)
            {
                uri = new Uri("res/3vertical.PNG");
                img.Source = new BitmapImage(uri);
                return img;
            }
            else if (!vertical & blockLength == 2)
            {
                uri = new Uri("res/2horizontal.PNG");
                img.Source = new BitmapImage(uri);
                return img;
            }
            else
            {
                uri = new Uri("res/3horizontal.PNG");
                img.Source = new BitmapImage(uri);
                return img;
            }
        }
    }
}
