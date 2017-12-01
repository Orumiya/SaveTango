using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveTango.Model
{
    public static class Sorter
    {

        public static void Sort<T>(this ObservableCollection<T> Scores) where T : IComparable
        {
            List<T> sorted = Scores.OrderBy(x => x).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                Scores.Move(Scores.IndexOf(sorted[i]), i);
        }
    }
}
