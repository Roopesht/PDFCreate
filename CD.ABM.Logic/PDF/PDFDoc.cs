using CD.ABM.Logic.Blocks;
using CD.ABM.Logic.Drawing;
using CD.ABM.Logic.POCO;
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
        List<PDFConfig> pdfConfig;
        private PdfStamper stamper;
        private String filename;
        private PdfReader reader;
        private string filenameBlank;
        private List<Action> drawingFuncs = new List<Action>();
        private Random rand = new Random(100);

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
            populatePDFConfig("105");
            //pdfConfig.RemoveAt(1);
        }

        private PDFConfig getPDFConfig(String formId,  String blockId)
        {
            PDFConfig config = new PDFConfig();
            DALC.FormsDAL formsDAL = new DALC.FormsDAL();

            return config;
        }

        private void populatePDFConfig (String formId)
        {
            pdfConfig = new List<PDFConfig>();

            if (1 == 1)
            {
                //Old Code 
                pdfConfig.Add(new PDFConfig("1"));
               // pdfConfig.Add(new PDFConfig("2"));
            }

            if (1 == 1)
            {

                DALC.FormsDAL formsDAL = new DALC.FormsDAL();
                foreach (String blockid in formsDAL.GetBlocks(formId))
                {
                    List<BlockItem> items = formsDAL.getItems(formId, blockid);
                    pdfConfig.Add(new PDFConfig(items));
                }
            }
        }

        public void ConstructPDF()
        {
            float curY = PageSize.Height;
            foreach(PDFConfig config in pdfConfig)
            {
                Blocks.BlockR1GrpRText block = new Blocks.BlockR1GrpRText(this, config);
                curY = block.Draw(curY) - 10;
            }
            Close();
            GetReader();
            foreach (Action action in drawingFuncs)
            {
                action.Invoke();
            }
            Close();
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

        public Rectangle AddRectange(Rectangle rect)
        {
            cb.Rectangle(rect);
            cb.Stroke();
            return rect;
        }
        public Rectangle AddRectange(float lx, float ly, float ux, float uy, BaseColor color)
        {
            Rectangle rect = new Rectangle(lx, ly, ux, uy)
            {
                Border = PdfBorderDictionary.STYLE_SOLID,
                BorderWidth = .25f,
                BorderColor = BaseColor.BLACK,
                BackgroundColor = color
            };
            return AddRectange(rect);
        }

        public Rectangle AddRectangeWithText(float lx, float ly, float ux, float uy, BaseColor color, String text, BaseColor textColor)
        {
            return AddRectange(lx, ly, ux, uy, color);
        }

        public void AddParagraph(Document doc, int alignment, iTextSharp.text.Font font, iTextSharp.text.IElement content)
        {
            Paragraph paragraph = new Paragraph()
            {
                Alignment = alignment,
                Font = font
            };
            paragraph.SetLeading(0f, 1.2f);
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
            Rectangle rect = new Rectangle(rec.Left + 5, rec.Bottom, rec.Right, rec.Top);

            Chunk chunk = new Chunk(text, PDFFont.GetNormalFont(color));
            Phrase phrase = new Phrase(chunk);

            ColumnText ctext = new ColumnText(cb)
            {
                Alignment = Element.ALIGN_BOTTOM | Element.ALIGN_LEFT,
                ExtraParagraphSpace = 10
            };
            ctext.SetSimpleColumn(rect.GetLeft(0), 0, rect.GetRight(0), rect.GetTop(0));
            ctext.SetText(phrase);
            ctext.SetLeading(BasicPDFBlock.gap, 0f);
            ctext.Go();
            return ctext.YLine;
        }

        public void AddTextField(POCO.Input input, Rectangle rect)
        {
           drawingFuncs.Add(() =>
           {
               TextField tf = new TextField(Writer, rect, input.IDRef + rand.Next().ToString())
               {
                   Alignment = Element.ALIGN_LEFT | Element.ALIGN_TOP,
                   BorderColor = BaseColor.BLACK,
                   BorderStyle = PdfBorderDictionary.STYLE_SOLID,
                   Text = input.DefaultValue
               };
               stamper.AddAnnotation(tf.GetTextField(), 1);
           });
            return;
        }


        public float AddRadioGroup(String Id, Rectangle rectStart, List<String> labels, int distance) 
        {
            float curY = rectStart.Top;
            drawingFuncs.Add(() =>
            {
                PdfFormField group = PdfFormField.CreateRadioButton(writer, true);
                group.FieldName = Id;
                for (int i=0; i<labels.Count; i++)
                {
                    Rectangle rect = new Rectangle(rectStart.Left + i * distance, rectStart.Top, rectStart.Right + i * 25, rectStart.Bottom);
                    RadioCheckField tf = new RadioCheckField(Writer, rect, Id + "_chk" + i.ToString(), i.ToString())
                    {
                        BackgroundColor = new GrayColor(0.8f),
                        BorderColor = GrayColor.BLACK,
                        CheckType = RadioCheckField.TYPE_CIRCLE,
                        BorderStyle = PdfBorderDictionary.STYLE_SOLID
                    };
                    group.AddKid(tf.RadioField);
                }
                stamper.AddAnnotation(group, 1);
            });

            return curY;
        }

        public void AddRadio(List<POCO.Input > inputs )
        {
            drawingFuncs.Add(() =>
            {
                PdfFormField group = PdfFormField.CreateRadioButton(writer, true);
                String groupname = "grp" + rand.Next().ToString();
                group.FieldName = groupname;
                RadioCheckField tf = null;
                int i = 0;
                foreach (POCO.Input input in inputs)
                {
                    tf = new RadioCheckField(Writer, input.Rect, groupname + "_chk" + i.ToString(),i.ToString());
                    tf.BackgroundColor = new GrayColor(0.8f);

                    tf.BorderColor = GrayColor.BLACK;
                    tf.CheckType = RadioCheckField.TYPE_CIRCLE;
                    tf.BorderStyle = PdfBorderDictionary.STYLE_SOLID;
                    group.AddKid(tf.RadioField);
                    i++;
                }
                stamper.AddAnnotation(group, 1);
            });
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
