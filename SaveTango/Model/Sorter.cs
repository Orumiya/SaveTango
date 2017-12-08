// <copyright file="Sorter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SaveTango.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class Sorter
    {

        public static void Sort<T>(this ObservableCollection<T> scores)
            where T : IComparable
        {
            List<T> sorted = scores.OrderBy(x => x).ToList();
            for (int i = 0; i < sorted.Count(); i++)
            {
              scores.Move(scores.IndexOf(sorted[i]), i);
            }
        }
    }
}
