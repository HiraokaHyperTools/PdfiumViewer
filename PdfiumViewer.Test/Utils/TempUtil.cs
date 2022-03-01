using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PdfiumViewer.Test.Utils
{
    internal static class TempUtil
    {
        internal static string ForPdf(string name)
        {
            return Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                name
            );
        }
    }
}
