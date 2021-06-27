using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelDefense.Controls
{
    public class FormOption
    {
        protected string name;
        protected object value;

        public FormOption(string NAME, object VALUE)
        {
            this.name = NAME;
            this.value = VALUE;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetValue(object Value)
        {
            this.value = Value;
        }

        public object GetValue()
        {
            return this.value;
        }
    }
}
