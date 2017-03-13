using CD.ABM.Logic.PDF;
using CD.ABM.Logic;
namespace CD.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            //TestCreatePDF pdf = new TestCreatePDF();

            CreatePDF();
        }

        private static void CreatePDF()
        {
            string filename = @"D:\abc.pdf";
            PDFDoc doc = new PDFDoc(filename, "Expert");
            doc.ConstructPDF();
            doc.Close();
            SystemUtil.OpenFile(filename);
        }
    }
}
