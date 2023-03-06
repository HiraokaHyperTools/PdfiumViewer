using NUnit.Framework;
using PdfiumViewer.Test.Properties;
using PdfiumViewer.Test.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PdfiumViewer.Test
{
    public class PdfDocTests
    {
        private static PdfDocument Ex1() => PdfDocument.Load(new MemoryStream(Resources.Example1, false));
        private static PdfDocument Ex2() => PdfDocument.Load(new MemoryStream(Resources.Example2, false));
        private static PdfDocument Ex123() => PdfDocument.Load(new MemoryStream(Resources.Ex123, false));
        private static PdfDocument ExABC() => PdfDocument.Load(new MemoryStream(Resources.ExABC, false));
        private static PdfDocument ExR0() => PdfDocument.Load(new MemoryStream(Resources.ExR0, false));
        private static PdfDocument ExR90() => PdfDocument.Load(new MemoryStream(Resources.ExR90, false));
        private static PdfDocument ExR180() => PdfDocument.Load(new MemoryStream(Resources.ExR180, false));
        private static PdfDocument ExR270() => PdfDocument.Load(new MemoryStream(Resources.ExR270, false));
        private static PdfDocument ExNoInfo() => PdfDocument.Load(new MemoryStream(Resources.ExNoInfo, false));

        [Test]
        public void LoadFromStream()
        {
            var ex1 = Ex1();
            var ex2 = Ex2();
            var ex123 = Ex123();
            var exABC = ExABC();

            Assert.That(ex1.PageCount, Is.EqualTo(22));
            Assert.That(ex2.PageCount, Is.EqualTo(1));
            Assert.That(ex123.PageCount, Is.EqualTo(3));
            Assert.That(exABC.PageCount, Is.EqualTo(3));

            ex1.Dispose();
            ex2.Dispose();
            ex123.Dispose();
            exABC.Dispose();
        }

        [Test]
        public void LoadAndSaveAndLoad()
        {
            var ex123 = Ex123();

            var temp = new MemoryStream();
            ex123.Save(temp);
            ex123.Dispose();

            temp.Position = 0;

            var ex123New = PdfDocument.Load(temp);
            Assert.That(ex123New.PageCount, Is.EqualTo(3));
            ex123New.Dispose();
        }

        [Test]
        public void DeletePage()
        {
            var ex123 = Ex123();

            Assert.That(ex123.PageCount, Is.EqualTo(3));
            ex123.DeletePage(0);
            Assert.That(ex123.PageCount, Is.EqualTo(2));
            ex123.DeletePage(0);
            Assert.That(ex123.PageCount, Is.EqualTo(1));

            ex123.Dispose();
        }

        [Test]
        public void CopyPages()
        {
            var ex123 = Ex123();

            Assert.That(ex123.PageCount, Is.EqualTo(3));
            ex123.CopyPages("1-3", 0);
            Assert.That(ex123.PageCount, Is.EqualTo(6));

            ex123.Save(TempUtil.ForPdf("123123.pdf"));

            ex123.Dispose();
        }

        [Test]
        public void ImportPages()
        {
            var ex123 = Ex123();
            var exABC = ExABC();

            Assert.That(ex123.PageCount, Is.EqualTo(3));
            ex123.ImportPages(exABC, "1-3", 3);
            Assert.That(ex123.PageCount, Is.EqualTo(6));

            ex123.Save(TempUtil.ForPdf("123ABC.pdf"));

            ex123.Dispose();
        }

        [Test]
        public void ImportPages2()
        {
            var exNoInfo = ExNoInfo();
            var ex1 = Ex1();

            Assert.That(exNoInfo.PageCount, Is.EqualTo(1));
            exNoInfo.ImportPages(ex1, "1", 1);
            Assert.That(exNoInfo.PageCount, Is.EqualTo(2));

            exNoInfo.Save(TempUtil.ForPdf("NoInfo1.pdf"));

            exNoInfo.Dispose();
        }

        [Test]
        public void Rotate1()
        {
            var exR90 = ExR90();
            Assert.That(exR90.GetPageRotation(0), Is.EqualTo(PdfRotation.Rotate90));

            exR90.RotatePage(0, PdfRotation.Rotate180);
            Assert.That(exR90.GetPageRotation(0), Is.EqualTo(PdfRotation.Rotate180));

            exR90.Save(TempUtil.ForPdf("R90_SetR180.pdf"));

            exR90.RotatePage(0, PdfRotation.Rotate270);
            Assert.That(exR90.GetPageRotation(0), Is.EqualTo(PdfRotation.Rotate270));

            exR90.Save(TempUtil.ForPdf("R90_SetR180_SetR270.pdf"));
        }

        [Test]
        public void Rotate2()
        {
            var exR270 = ExR270();
            Assert.That(exR270.GetPageRotation(0), Is.EqualTo(PdfRotation.Rotate270));

            exR270.RotatePage(0, exR270.GetPageRotation(0) + 1);
            Assert.That(exR270.GetPageRotation(0), Is.EqualTo(PdfRotation.Rotate0));

            exR270.Save(TempUtil.ForPdf("R270_Rel90.pdf"));
        }

        [Test]
        public void GetRotation()
        {
            Assert.That(ExR0().GetPageRotation(0), Is.EqualTo(PdfRotation.Rotate0));
            Assert.That(ExR90().GetPageRotation(0), Is.EqualTo(PdfRotation.Rotate90));
            Assert.That(ExR180().GetPageRotation(0), Is.EqualTo(PdfRotation.Rotate180));
            Assert.That(ExR270().GetPageRotation(0), Is.EqualTo(PdfRotation.Rotate270));
        }
    }
}
