using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CustomUserControlLibrary.Converter
{
    public class TestConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null && value.ToString() == "1")
                {
                    string str = "转换成功" + value;
                    return str;
                }
                else
                {
                    value = "不在转换器范围内";
                    return value;
                }
            }
            catch (Exception e)
            {              
                return "未知";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
