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
        public Block()
        {

        }

        /// <summary>
        /// a vertical property azt mondja meg, hogy a blokk horizontálisan vagy vertikálisan helyezkedik-e el
        /// vertical igaz, ha vertikális
        /// vertical hamis, ha horizontális
        /// </summary>
        public virtual bool Vertical
        {
            get;
            set;
        }

        /// <summary>
        /// a blockLength property beállítja a blokk méretét
        /// lehetséges értékek a játékban: 2 vagy 3
        /// </summary>
        public virtual int BlockLength
        {
            get;
            set;
        }

        /// <summary>
        /// a Block-nak a vizuális megjelenési formája a táblán
        /// </summary>
        public virtual Image BlockImage
        {
            get;
            set;
        }

        /// <summary>
        /// a blokk kezdő pozíciója a tábla tetejétől számítva
        /// </summary>
        public virtual int InitialCanvasTop
        {
            get;
            set;
        }

        /// <summary>
        /// a blokk kezdő pozíciója a tábla bal oldalától számítva
        /// </summary>
        public virtual int InitialCanvasLeft
        {
            get;
            set;
        }

    }
}
