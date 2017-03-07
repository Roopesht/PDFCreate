using CD.ABM.Logic.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace CD.ABM.Logic.PDF
{
    public class PDFDoc
    {
        iTextSharp.text.Document doc = new Document();
        private PdfWriter writer;
        PdfContentByte cb =null;
        private PDFPageSize pageSize;
        List<PDFConfig> config;
        private PdfStamper stamper;
        private String filename;
        private PdfReader reader;
        private string filenameBlank;

        public PdfStamper Stamper
        {
            get
            {

                return stamper;
            }
        }
        
        private PDFDoc ()
        {
            //Declared a empty constructor to make it private
        }
        public void GetReader()
        {
            reader = new PdfReader(filenameBlank);
            stamper = new PdfStamper(reader, new FileStream(filename, FileMode.Create));
            writer = stamper.Writer;
        }

        public PDFDoc(String _filename, String formName)
        {
            filename = _filename;
            filenameBlank = _filename.Replace(".", "_Blank.");
            FileStream fs = new System.IO.FileStream(filenameBlank, System.IO.FileMode.Create);
            writer = PdfWriter.GetInstance(doc, fs);
            SetPageProperties();
            doc.Open();
            cb = writer.DirectContent;
            //Based on the formName, populate the Config
            config = new List<PDFConfig>();
            config.Add(new PDFConfig("1"));
            config.Add(new PDFConfig("2"));

        }

        public void ConstructPDF()
        {
            float curY=0f;
            //Read the config and
            Blocks.BlockR1GrpRText block = new Blocks.BlockR1GrpRText(this, new PDFConfig("1"));
            curY = block.Draw();

            //block = new Blocks.BlockR1GrpRText(this, new PDFConfig("2"));
            //block.Draw(curY-20);


            //TODO: Loop through PDFConfig List, and instantiate the BlockR1GrpRText
            //TODO: Execute the instantiated blocks in sequence
            //TODO: Based on the page number, either pass the Current Y position
        }




        public PDFPageSize PageSize
        {
            get { return pageSize; }
        }
            
        public PdfWriter Writer
        {
            get { return stamper.Writer; }
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

        public void AddTextField(POCO.Input input, Rectangle rect)
        {
            TextField tf = new TextField(Writer, rect, input.IDRef);
            tf.BorderColor = BaseColor.BLACK;
            tf.BorderStyle = PdfBorderDictionary.STYLE_SOLID;
            tf.Text = input.DefaultValue;
            stamper.AddAnnotation(tf.GetTextField(), 1);
            return;
        }

        public void AddRadio(List<POCO.Input > inputs )
        {
            PdfFormField group = PdfFormField.CreateRadioButton(writer, true);
            RadioCheckField tf=null;
            int i = 0;
            foreach (POCO.Input input in inputs)
            {
                Rectangle rect = new Rectangle(40, 806 - i * 40, 60, 600 - i * 40);
                tf = new RadioCheckField(Writer, input.Rect, null, input.IDRef);
                tf.BackgroundColor = new GrayColor(0.8f);
                tf.BorderColor = GrayColor.BLACK;
                tf.CheckType = RadioCheckField.TYPE_CIRCLE;
                tf.BorderStyle = PdfBorderDictionary.STYLE_SOLID;
                group.AddKid(tf.RadioField);                
                stamper.AddAnnotation(tf.RadioField, 1);
                i++;
            }
        }

        public float AddText(Rectangle rec, String text)
        {
            return AddText(rec, text, BaseColor.BLACK);
        }
        public bool Close()
        {
            if (this.doc.IsOpen())
                this.doc.Close();
            try
            {
                stamper.Close();
                reader.Close();
            }
            catch(Exception e)
            {}
            try
            {
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {}
            return true;
        }
        public void AddNewPage ()
        {
            doc.NewPage();
        }
    }
}
