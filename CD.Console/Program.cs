﻿using CD.ABM.Logic.PDF;
using CD.ABM.Logic.Blocks;

namespace CD.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            //TestCreatePDF pdf = new TestCreatePDF();
            //SystemUtil.OpenFile(@"D:\a1.pdf");
            ValueCreationBlock pdf = new ValueCreationBlock();
            SystemUtil.OpenFile(@"D:\a1.pdf");
            //CreatePDF();
            
        }

        private static void CreatePDF()
        {
            //this line is added in Dev
            string filename = @"D:\abc.pdf";
            PDFDoc doc = new PDFDoc(filename, "105");
            doc.ConstructPDF();
            SystemUtil.OpenFile(filename);
        }
    }
}
