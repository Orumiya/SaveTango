namespace SaveTango.Model
{
    public class Board : Bindable
    {
        private double tableWidth;

        public double TableWidth
        {
            get { return tableWidth; }
            set { tableWidth = value; }
        }
        private double tableHeight;

        public double TableHeight
        {
            get { return tableHeight; }
            set { tableHeight = value; }
        }

        public Board()
        {
            this.Table = new bool[6, 6];
            this.TableWidth = 600;
            this.TableHeight = 600;
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

        //private string Kiir()
        //{
        //    string szo = "";
        //    for (int i = 0; i < this.Table.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < this.Table.GetLength(1); j++)
        //        {
        //            szo += this.Table[i, j];
        //        }
        //    }
        //    return szo;
        //}

    }
}
