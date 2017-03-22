using CD.ABM.Logic.PDF;
using CD.ABM.Logic;
namespace CD.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCreate();
            //CreatePDF();
            
        }
        private static void TestCreate ()
        {
            string filename = @"D:\a1.pdf";
            TestCreatePDF pdf = new TestCreatePDF(filename);
            SystemUtil.OpenFile(filename);
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
