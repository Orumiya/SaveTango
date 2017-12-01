using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveTango.Model
{
    /// <summary>
    /// Score class a letárolt eredmények feldolgozásához
    /// </summary>
    public class Score
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
    }
}
