using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Doan1
{
    class RelativetoAbsoluteConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imagFile = (string)value;
            var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
            return currentFolder +"Img\\"+ imagFile;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
