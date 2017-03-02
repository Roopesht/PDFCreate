using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD.ABM.Logic.Drawing;

namespace CD.ABM.Logic.PDF
{
    public class PDFDoc
    {
        iTextSharp.text.Document doc = new Document();
        private PdfWriter writer;
        PdfContentByte cb =null;
        private PDFPageSize pageSize;
        PDFConfig config;
        
        private PDFDoc ()
        {
            //Declared a empty constructor to make it private
        }
        public PDFDoc(String filename, PDFConfig _config)
        {
            FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
            writer = PdfWriter.GetInstance(doc, fs);
            SetPageProperties();
            doc.Open();
            cb = writer.DirectContent;
            config = _config;
        }

        public void ConstructPDF()
        {
            float curY=0f;
            //Read the config and
            Blocks.BlockR1GrpRText block = new Blocks.BlockR1GrpRText(this, new PDFConfig("1"));
            curY = block.Draw();
            block = new Blocks.BlockR1GrpRText(this, new PDFConfig("2"));
            block.Draw(curY-20);
        }




        public PDFPageSize PageSize
        {
            get { return pageSize; }
        }
            
        public PdfWriter Writer
        {
            get { return writer; }
        }
        public PdfContentByte PDFcb {
            get { return cb; }
        }
        private void SetPageProperties()
        {
            this.pageSize = new PDFPageSize();
            doc.SetPageSize(new iTextSharp.text.Rectangle(pageSize.Width, pageSize.Height));
            doc.SetMargins(pageSize.LeftMargin, pageSize.RightMargin, pageSize.TopMargin, pageSize.BottomMargin);
            doc.AddTitle("POC - Principal form");
            doc.AddCreator("CD ABM Logic");
        }

        public Rectangle AddRectange(Rectangle rect, BaseColor color)
        {
            rect.Border = PdfBorderDictionary.STYLE_SOLID;
            rect.BorderWidth = .25f;
            rect.Border = 255;
            if (color != null) rect.BackgroundColor = color;
            rect.BorderColor = BaseColor.BLACK;
            cb.Rectangle(rect);
            cb.Stroke();
            return rect;
        }
        public Rectangle AddRectange(float lx, float ly, float ux, float uy, BaseColor color)
        {
            Rectangle rect = new Rectangle(lx, ly, ux, uy);
            return AddRectange(rect, color);

        }
        public void AddParagraph(Document doc, int alignment, iTextSharp.text.Font font, iTextSharp.text.IElement content)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.SetLeading(0f, 1.2f);
            paragraph.Alignment = alignment;
            paragraph.Font = font;
            paragraph.Add(content);
            doc.Add(paragraph);
        }

        public float AddText(Rectangle rec, List<String> texts, BaseColor color)
        {
            float curY=0;
            Rectangle rect = new Rectangle(rec.Left + 5, rec.Bottom, rec.Right, rec.Top);
            foreach (String text in texts)
            {
                curY= AddText(rect, text, color);
                rect = rect.OffSetRectByYAxis(rect.Top - curY );
            }
            return curY;
        }

        public float AddText(Rectangle rec, String text, BaseColor color)
        {
            Rectangle rect = new Rectangle(rec.Left + 5, rec.Bottom+5, rec.Right, rec.Top+5);

            Font font3 = new Font(FontFactory.GetFont(FontFactory.HELVETICA, 1000, Font.NORMAL, color));
            Chunk chunk = new Chunk(text, new Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, color)));
            Phrase text33 = new Phrase(chunk);

            ColumnText columnText4 = new ColumnText(cb);
            columnText4.SetSimpleColumn(rect.GetLeft(0), 0, rect.GetRight(0), rect.GetTop(0));
            columnText4.SetText(text33);
            columnText4.Alignment = Element.ALIGN_LEFT;
            columnText4.Go();
            return columnText4.YLine;
        }

        public float AddText(Rectangle rec, String text)
        {
            return AddText(rec, text, BaseColor.BLACK);
        }
        public bool Close()
        {
            this.doc.Close();
            return true;
        }
        public void AddNewPage ()
        {
            doc.NewPage();
        }
    }
}
