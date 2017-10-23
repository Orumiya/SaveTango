using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveTango.Model
{
    class GamePlay : Bindable
    {   /// <summary>
    /// játékos idejét tároló változó
    /// </summary>
        private DateTime dateTime;

        public DateTime PlayerTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

    }
}
