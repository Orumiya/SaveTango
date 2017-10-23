using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveTango.Model
{
    /// <summary>
    /// A class az adatkötésekhez szükséges OnPropertyChanged metódust szolgáltatja.
    /// </summary>
    public class Bindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(propertyname, new PropertyChangedEventArgs("propertyname"));
        }
    }
}
