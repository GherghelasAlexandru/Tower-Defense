using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Controls
{
    public class FormOption
    {
        public string name;
        public object value;

        public FormOption(string NAME, object VALUE)
        {
            name = NAME;
            value = VALUE;
        }
    }
}
