using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace Doan1
{
    public class Config : INotifyPropertyChanged
    {
        public int PageNow { get; set; }
        public int PageTotal { get; set; }
        public int CountView { get; set; }
        public string pre { get; set; }
        public string next { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
