using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Model
{
    class TestModel_1 : INotifyPropertyChanged
    {
        
            //自定义字段
            private string _fHeight = "620";
            public string fHeight
            {
                get { return _fHeight; }
                set { _fHeight = value; OnPropertyChanged("fHeight"); }
            }
            #region even
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            #endregion
    }
}
