using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private int canvasRowNumber;

        public int CanvasRowNumber
        {
            get { return canvasRowNumber; }
            set { canvasRowNumber = value; }
        }

        private int canvasColumnNummer;

        public int CanvasColumnNummer
        {
            get { return canvasColumnNummer; }
            set { canvasColumnNummer = value; }
        }

    }
}
