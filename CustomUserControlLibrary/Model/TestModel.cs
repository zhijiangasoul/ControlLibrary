using CustomUserControlLibrary.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Model
{
    public class TestModel : BaseBind
    {

        public string TestText = "1";
        public string testText
        {
            get { return TestText; }
            set
            {
                SetProperty(ref TestText, value);
            }
        }


    }
}
