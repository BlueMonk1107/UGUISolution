using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Language
{
    public struct TextData
    {
        public int ID;
        public string Style;
        public string Content;
    }

    public struct StyleData
    {
        public string Font;
        public string FontStyle;
        public int Size;
        public bool RichText;
        public byte r;
        public byte g;
        public byte b;
        public byte a;
    }
}
