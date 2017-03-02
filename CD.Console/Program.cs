using CD.ABM.Logic.PDF;
namespace CD.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = @"D:\abc.pdf";
            PDFDoc doc = new PDFDoc(filename, new PDFConfig(""));
            doc.ConstructPDF();
            doc.Close();
            SystemUtil.Execute(filename);
        }
    }
}
