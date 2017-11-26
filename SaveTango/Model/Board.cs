namespace SaveTango.Model
{
    public enum FieldType
    {
        Empty,
        Taken
    }

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
            this.Table = new FieldType[6, 6];
            this.TableWidth = 600;
            this.TableHeight = 600;
            this.TableInitializer();
        }

        private FieldType[,] table;

        public FieldType[,] Table
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
                    this.Table[i, j] = FieldType.Empty;
                }
            }
        }
    }
}
