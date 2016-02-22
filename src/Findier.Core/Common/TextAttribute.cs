using System;

namespace Findier.Core.Common
{
    public class TextAttribute : Attribute
    {
        public TextAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}