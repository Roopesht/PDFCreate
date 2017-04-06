﻿using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace CD.ABM.Logic
{
    public class TestCreatePDF : IDisposable
    {
        iTextSharp.text.Document doc = new Document();
        private PdfWriter writer;
        PdfContentByte cb = null;
        private String filename;
        private PdfReader reader;
        private string filenameBlank;
        private PdfStamper stamper;


        public TestCreatePDF(String _filename)
        {
            filename = _filename;
            filenameBlank = filename.Replace(".", "_Blank.");

            FileStream fs = new System.IO.FileStream(filenameBlank, System.IO.FileMode.Create);
            writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            cb = writer.DirectContent;
            Rectangle rect = new Rectangle(50, 700, 300, 600)
            {
                BorderWidth = .25f,
                Border = 255
            };
            cb.Rectangle(rect);
            cb.Stroke();

            
            //Add text
            Font font3 = new Font(FontFactory.GetFont(FontFactory.HELVETICA, 1000, Font.NORMAL, BaseColor.BLACK));
            Chunk chunk = new Chunk("Roopesh", new Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLUE)));
            Phrase phrase = new Phrase(chunk);

            ColumnText ctext = new ColumnText(cb);
            ctext.SetSimpleColumn(rect.Left + 5, rect.Top, rect.Right, rect.Bottom);
            ctext.SetText(phrase);
            ctext.Go();

            //Add HTML
            rect = new Rectangle(rect.Left, rect.Top - 100, rect.Right, rect.Bottom - 100);
            ColumnText ct1 = new ColumnText(cb);
            ct1.SetSimpleColumn(rect.Left, rect.Top, rect.Right, rect.Bottom);
            String html = "<p> <font color='red'>Hello red</font><p>" + 
                "<table border='0'><tr><td><p>Hello <b>Bold Now I am <I> Italic</I>, or <u>underline</u></B></p>" +
                "<ol><li>this is the first item </li><li>this is the second item</li><li>this is really very long text so that it will run for 2 lines for demo purpose, and prove that the rectangles wont get adjusted automatically</li></ol>" +
                "</td></tr></table>";
            AddHTMLContent (html, ct1);
            ct1.Go();
            
            this.doc.Close();
            GetReader();

            //Add TextBox
            rect = new Rectangle(rect.Left, rect.Top - 100, rect.Right, rect.Bottom - 100);
            TextField tf = new TextField(writer, rect, "text1")
            {
                Alignment = Element.ALIGN_LEFT | Element.ALIGN_TOP,
                BorderColor = BaseColor.BLACK,
                BorderStyle = PdfBorderDictionary.STYLE_SOLID,
                Text = "TextField"
            };
            stamper.AddAnnotation(tf.GetTextField(), 1);

            //Add Radio
            rect = new Rectangle(rect.Left, rect.Top - 100, rect.Right, rect.Bottom - 25);
            PdfFormField group = PdfFormField.CreateRadioButton(writer, true);
            group.FieldName = "grp1";
            for (int i = 0; i < 5; i++)
            {
                Rectangle radioRect = new Rectangle(rect.Left + i * 25, rect.Top, rect.Left + (i + 1) * 25, rect.Bottom);
                RadioCheckField radioField = new RadioCheckField(writer, radioRect, "chk" + i.ToString(), i.ToString())
                {
                    BorderColor = GrayColor.BLACK,
                    CheckType = RadioCheckField.TYPE_CIRCLE,
                    BorderStyle = PdfBorderDictionary.STYLE_SOLID
                };
                group.AddKid(radioField.RadioField);
            }
            //group.SetAdditionalActions(PdfName.E, PdfAction.JavaScript("app.alert(validate);",writer));
            stamper.AddAnnotation(group, 1);

            //Add submit button
            rect = new Rectangle(rect.Left, rect.Top - 100, rect.Right, rect.Bottom - 25);
            PushbuttonField button = new PushbuttonField(writer, rect, "postSubmit")
            {
                FontSize = 8,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                BorderColor = GrayColor.BLACK,
                BorderWidth = 1f,
                BorderStyle = PdfBorderDictionary.STYLE_BEVELED,
                TextColor = GrayColor.GREEN,
                Text = "Submit",
                Visibility = PushbuttonField.VISIBLE_BUT_DOES_NOT_PRINT
            };
            PdfFormField field = button.Field;
            String javascript = "validate();";
            field.Action = PdfAction.JavaScript(javascript, writer);

            //field.Action = PdfAction.CreateSubmitForm( @"rtayaloor@sapient.com", null, PdfAction.SUBMIT_HTML_FORMAT | PdfAction.SUBMIT_INCLUDE_NO_VALUE_FIELDS);
            stamper.AddAnnotation(field, 1);
            PdfAcroForm f = new PdfAcroForm(writer);

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

        public void AddHTMLContent(String HTMLContent, ColumnText rect)

        {
            var parsedHtmlElements =  HTMLWorker.ParseToList(new StringReader(HTMLContent), new StyleSheet());
            foreach (var htmlElement in parsedHtmlElements)
                rect.AddElement(htmlElement as IElement);
        }
        protected virtual void Dispose(bool flag)
        {
            doc.Dispose();
            stamper.Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
