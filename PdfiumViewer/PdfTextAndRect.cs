using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PdfiumViewer
{
    /// <summary>
    /// The text and rect.
    /// </summary>
    public class PdfTextAndRect
    {
        /// <summary>
        /// The text
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// The rectangle which surrounds text
        /// </summary>
        public RectangleF Rect { get; }

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="text">The text</param>
        /// <param name="rect">The rectangle which surrounds text</param>
        public PdfTextAndRect(string text, RectangleF rect)
        {
            Text = text;
            Rect = rect;
        }
    }
}
