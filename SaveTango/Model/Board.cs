using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveTango.Model
{
    public class Board : Bindable
    {
        public Board()
        {
            this.Table = new bool[6, 6];
            this.TableInitializer();
        }

        private bool[,] table;

        public bool[,] Table
        {
            get
            {
                return this.table;
            }

            set
            {
                this.table = value;
                this.OnPropertyChanged("Table");
            }
        }

        private void TableInitializer()
        {
            for (int i = 0; i < this.Table.GetLength(0); i++)
            {
                for (int j = 0; j < this.Table.GetLength(1); j++)
                {
                    this.Table[i, j] = false;
                }
            }
        }

    }
}
