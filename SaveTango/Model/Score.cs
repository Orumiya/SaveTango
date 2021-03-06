﻿// <copyright file="Score.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SaveTango.Model
{
    using System;

    /// <summary>
    /// Score class a letárolt eredmények feldolgozásához
    /// </summary>
    public class Score : IComparable
    {
        public Score(int level, int moves, string time)
        {
            this.Level = level;
            this.Moves = moves;
            this.Time = time;
        }

        public int Level { get; set; }

        public int Moves { get; set; }

        public string Time { get; set; }

        public int CompareTo(object obj)
        {
            Score a = this;
            Score b = (Score)obj;
            if (a.Moves < b.Moves)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
