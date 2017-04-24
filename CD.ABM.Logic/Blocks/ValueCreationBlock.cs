using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using CD.ABM.Logic.PDF;
using CD.ABM.Logic.Blocks;
using System.Collections.Generic;

namespace CD.ABM.Logic.Blocks
{
    public class ValueCreationBlock
    {
        iTextSharp.text.Document doc = new Document();
        private PdfWriter writer;
        PdfContentByte cb = null;
        private String filename = @"D:\a1.pdf";
        private PdfReader reader;
        private string filenameBlank;
        private PdfStamper stamper;


        public ValueCreationBlock()
        {
            filenameBlank = filename.Replace(".", "_Blank.");

            FileStream fs = new System.IO.FileStream(filenameBlank, System.IO.FileMode.Create);
            writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            cb = writer.DirectContent;
            Rectangle rect = new Rectangle(50, 700, 300, 600);
            
            BaseFont bfHelvetica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, false);
            Font helvetica12 = new Font(bfHelvetica, 12, Font.NORMAL, BaseColor.BLACK);
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 570f;
            table.LockedWidth = true;
            PdfPCell cell = new PdfPCell(new Phrase("Header spanning 3 columns"));
            cell.Border = PdfBorderDictionary.STYLE_SOLID;
            cell.BorderWidth = .25f;
            cell.BorderColor = BaseColor.BLACK;
            cell.BackgroundColor = PDFColor.BCGGreen;
            cell.Colspan = 3;

            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);
            TextField textfield = new TextField(writer, new iTextSharp.text.Rectangle(67, 585, 140, 800), "cellTextBox");
            PdfPCell tbCell = new PdfPCell(new Phrase(" ", helvetica12));
            iTextSharp.text.pdf.events.FieldPositioningEvents events =
                new iTextSharp.text.pdf.events.FieldPositioningEvents(writer, textfield.GetTextField());
            tbCell.CellEvent = events;
            table.AddCell(tbCell);
            table.AddCell("Col 2 Row 1fjwfwefnwegnejwkggjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj");
            table.AddCell("Col 1 Row 2");
            table.AddCell("Col 2 Row 2");
            table.AddCell("Col 1 Row 3");
            table.AddCell("Col 2 Row 3");
            table.AddCell("Col 1 Row 4");
            table.AddCell("Col 2 Row 4");
            table.AddCell("Col 1 Row 5");
            table.AddCell("Col 2 Row 5");

            PdfFormField _radioGroup = PdfFormField.CreateRadioButton(writer, true);
            _radioGroup.FieldName = "Overall";
            string[] genders = { "1", "2", "3", "4", "5" };
            RadioCheckField genderRadioCheckField = null;
            PdfFormField radioGField;
            for (int i = 0; i < genders.Length; i++)
            {
                Rectangle radioRect = new Rectangle(rect.Left + i * 25, rect.Top, rect.Left + (i + 1) * 25, rect.Bottom);
                genderRadioCheckField = new RadioCheckField(writer, new Rectangle(40, 806 - i * 40, 60, 788 - i * 40), "chk" + i.ToString(), i.ToString())
                {
                    BorderColor = GrayColor.BLACK,
                    CheckType = RadioCheckField.TYPE_CIRCLE,
                    BorderStyle = PdfBorderDictionary.STYLE_SOLID
                };
                _radioGroup.AddKid(genderRadioCheckField.RadioField);
            }

            //pdfWriter.AddAnnotation(_radioGroup);
            iTextSharp.text.pdf.events.FieldPositioningEvents genderEvents
                = new iTextSharp.text.pdf.events.FieldPositioningEvents(writer, _radioGroup);
            PdfPCell genderFieldCell = new PdfPCell();
            genderFieldCell.FixedHeight = 75f;

            genderFieldCell.CellEvent = genderEvents;
            table.AddCell(genderFieldCell);
            table.AddCell("Col 2 Row 6");
            doc.Add(table);

            this.doc.Close();

            GetReader();

            
            //Add common Javascript code
            writer.AddJavaScript("var requiredFields = ['text1', 'grp1'];");
            string validateFunction = "function validate () { " +
                " for (i=0; i<requiredFields.length; i++) {" +
                " var grp = this.getField(requiredFields[i]);  if (!grp || grp.value === null || grp.value == ''|| grp.value=='Off') { " +
                " app.alert('please select this '+ requiredFields[i]); return false; }}" +
            "return true}";
            writer.AddJavaScript(validateFunction);


            //Close all the streams
            stamper.Close();
            reader.Close();
            doc.Close();
        }
        public void GetReader()
        {
            reader = new PdfReader(filenameBlank);
            stamper = new PdfStamper(reader, new FileStream(filename, FileMode.Create));
            writer = stamper.Writer;
        }

    }

}