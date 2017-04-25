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
            
            BaseFont bfHelvetica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, false);
            Font helvetica12 = new Font(bfHelvetica, 12, Font.NORMAL, BaseColor.BLACK);

            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 570f;
            table.LockedWidth = true;
            //relative col widths in proportions - 1/3 and 2/3
            float[] widths = new float[] { 2f, 3f };
            table.SetWidths(widths);
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            

            PdfPCell cell1 = new PdfPCell(new Phrase("Value Creation and Impact", new Font(Font.NORMAL, 10f, Font.NORMAL, BaseColor.WHITE)));
            cell1.PaddingLeft = 10;
            cell1.BackgroundColor = PDFColor.BCGGreen;
            cell1.Colspan = 3;
            cell1.Border = PdfPCell.NO_BORDER;
            cell1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell1);

            PdfPTable nested = new PdfPTable(1);
            //nested.TotalWidth = 270f;
            //nested.LockedWidth = true;
            //float[] widthforcol = new float[] { 100f, 170f};
            //nested.SetWidths(widthforcol);

            PdfPCell cell2 = new PdfPCell(new Phrase("Develops clear recommendations with an action bias", new Font(Font.NORMAL, 10f, Font.NORMAL, BaseColor.BLACK)));
            cell2.PaddingLeft = 10;
            //cell2.Colspan = 2;
            cell2.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell2.Border = PdfPCell.NO_BORDER;
            cell2.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            nested.AddCell(cell2);

            PdfPCell cell3 = new PdfPCell(new Phrase("Networks within client organization to understand agenda", new Font(Font.NORMAL, 10f, Font.NORMAL, BaseColor.BLACK)));
            cell3.PaddingLeft = 10;
            //cell3.Colspan = 2;
            cell3.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell3.Border = PdfPCell.NO_BORDER;
            cell3.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            nested.AddCell(cell3);

            PdfPCell cell4 = new PdfPCell(new Phrase("Is able to assess implementation challenges", new Font(Font.NORMAL, 10f, Font.NORMAL, BaseColor.BLACK)));
            cell4.PaddingLeft = 10;
            //cell4.Colspan = 2;
            cell4.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell4.Border = PdfPCell.NO_BORDER;
            cell4.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            nested.AddCell(cell4);

            PdfPCell cell5 = new PdfPCell(new Phrase("Applies expertise to generate superior and sustainable results for client; is committed to making change happen", new Font(Font.NORMAL, 10f, Font.NORMAL, BaseColor.BLACK)));
            cell5.PaddingLeft = 10;
            //cell5.Colspan = 2;
            cell5.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell5.Border = PdfPCell.NO_BORDER;
            cell5.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            nested.AddCell(cell5);

            PdfPCell cell6 = new PdfPCell(new Phrase("Effectively transfers capabilities to client teams", new Font(Font.NORMAL, 10f, Font.NORMAL, BaseColor.BLACK)));
            cell6.PaddingLeft = 10;
            //cell6.Colspan = 2;
            cell6.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell6.Border = PdfPCell.NO_BORDER;
            cell6.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            nested.AddCell(cell6);

            //Rectangle rect = new iTextSharp.text.Rectangle(10, 20, 30, 40);
            //var rf1 = new RadioCheckField(writer, rect, "cellRadioBox", "on");
            //rf1.CheckType = RadioCheckField.TYPE_CHECK;
            //PdfFormField field = rf1.CheckField;

            string[] labels = { "NA", "1", "2", "3", "4", "5" };
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            // create a radio field spanning different pages
            PdfFormField radiogroup = PdfFormField.CreateRadioButton(writer, true);
            radiogroup.FieldName = "language";
            //Rectangle rect = new Rectangle(40, 606, 60, 588);
            RadioCheckField radio;
            PdfFormField radiofield;
            Rectangle rect;
            for (int i = 0; i < labels.Length; ++i)
            {
                rect = new Rectangle(50 + i * 30, 705, 50 + (i + 1) * 30, 695);//PDFDocPageSize.RIGHT
                //rect = new Rectangle(40, 606, 60, 588);
                radio = new RadioCheckField(writer, rect, null, labels[i]);
                radio.BackgroundColor = new GrayColor(0.8f);
                radiofield = radio.RadioField;
                radiofield.PlaceInPage = 1;
                //radiofield.PlaceInPage= ++page;
                radiogroup.AddKid(radiofield);
            }


            var radioEvents = new iTextSharp.text.pdf.events.FieldPositioningEvents(writer, radiogroup);
            PdfPCell radioCell = new PdfPCell();
            radioCell.CellEvent = radioEvents;
            nested.AddCell(radioCell);

            PdfPCell nesthousing = new PdfPCell(nested);
            nesthousing.Padding = 0f;
            table.AddCell(nesthousing);
            TextField textfield = new TextField(writer, new iTextSharp.text.Rectangle(10, 20, 30, 40), "cellTextBox");
            PdfPCell tbCell = new PdfPCell(new Phrase(" ", helvetica12));
            iTextSharp.text.pdf.events.FieldPositioningEvents events =
                new iTextSharp.text.pdf.events.FieldPositioningEvents(writer, textfield.GetTextField());
            tbCell.CellEvent = events;
            table.AddCell(tbCell);

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