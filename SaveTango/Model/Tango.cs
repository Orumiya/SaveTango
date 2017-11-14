﻿using System;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="Tango"/> class.
        /// Megadja Tango kezdőpozícióit a táblán, ill a hozzá kapcsolódó képet.
        /// </summary>
        public Tango()
        {
            this.BlockImage = this.GetMeTheBestTangoPicture();
            this.InitialCanvasTop = 200;
            this.InitialCanvasLeft = 0;
        }

        /// <summary>
        /// Tango vízszintesen áll a táblán
        /// </summary>
        public override bool Vertical { get => false; }

        /// <summary>
        /// Tango 2 hosszúságú
        /// </summary>
        public override int BlockLength { get => 2; }

        /// <summary>
        /// a property Tango képfile-ját tartalmazza
        /// </summary>
        public override Image BlockImage { get => base.BlockImage; }

        public override int InitialCanvasTop { get => base.InitialCanvasTop; set => base.InitialCanvasTop = value; }

        public override int InitialCanvasLeft { get => base.InitialCanvasLeft; set => base.InitialCanvasLeft = value; }

        /// <summary>
        /// Tangohoz hozzárendeli a megfelelő képet
        /// </summary>
        /// <returns>Image (Tango)</returns>
        public Image GetMeTheBestTangoPicture()
        {
            Image img = new Image();
            Uri uri = new Uri("pack://application:,,,/res/Tango.PNG", UriKind.RelativeOrAbsolute);
            img.Source = new BitmapImage(uri);
            return img;
        }
    }
}