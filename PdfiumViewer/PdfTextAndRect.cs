using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PdfiumViewer
{
    public class PdfTextAndRect
    {
        public string Text { get; }
        public RectangleF Rect { get; }

        public PdfTextAndRect(string text, RectangleF rect)
        {
            Text = text;
            Rect = rect;
        }
    }
}
