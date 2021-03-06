﻿// <copyright file="Block.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SaveTango.Model
{
    using System.Windows.Controls;

    public class Block : Bindable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// A Block osztály konstruktora
        /// </summary>
        public Block()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether a vertical property azt mondja meg, hogy a blokk horizontálisan vagy vertikálisan helyezkedik-e el
        /// vertical igaz, ha vertikális
        /// vertical hamis, ha horizontális
        /// </summary>
        public virtual bool Vertical
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a blockLength property beállítja a blokk méretét
        /// lehetséges értékek a játékban: 2 vagy 3
        /// </summary>
        public virtual int BlockLength
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a Block-nak a vizuális megjelenési formája a táblán
        /// </summary>
        public virtual Image BlockImage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a blokk kezdő pozíciója a tábla tetejétől számítva
        /// </summary>
        public virtual int InitialCanvasTop
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a blokk kezdő pozíciója a tábla bal oldalától számítva
        /// </summary>
        public virtual int InitialCanvasLeft
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a blokk kezdő sor koordinátája
        /// </summary>
        public virtual int OnTableX { get; set; }

        /// <summary>
        /// Gets or sets a blokk kezdő oszlop koordinátája
        /// </summary>
        public virtual int OnTableY { get; set; }

    }
}
