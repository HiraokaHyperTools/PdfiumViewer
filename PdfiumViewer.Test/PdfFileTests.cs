using NUnit.Framework;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfiumViewer.Test
{
    public class PdfFileTests
    {
        [Test]
        public void CreateEmpty()
        {
            var file = PdfFile.CreateEmpty();

            file.Dispose();
        }
    }
}
