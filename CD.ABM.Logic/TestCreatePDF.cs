using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.ABM.Logic
{
    public class TestCreatePDF
    {
        iTextSharp.text.Document doc = new Document();
        private PdfWriter writer;
        PdfContentByte cb = null;
        private String filename=@"D:\a1.pdf";
        private PdfReader reader;
        private string filenameBlank=@"D:\a.pdf";
        private PdfStamper stamper;


        public TestCreatePDF()
        {
            FileStream fs = new System.IO.FileStream(filenameBlank, System.IO.FileMode.Create);
            writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            cb = writer.DirectContent;
            Rectangle rect = new Rectangle(50, 700, 300, 600);
            rect.BorderWidth = .25f;
            rect.Border = 255;
            cb.Rectangle(rect);
            cb.Stroke();


            //Add text
            Font font3 = new Font(FontFactory.GetFont(FontFactory.HELVETICA, 1000, Font.NORMAL, BaseColor.BLACK));
            Chunk chunk = new Chunk("Roopesh", new Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLUE)));
            Phrase phrase = new Phrase(chunk);

            ColumnText ctext = new ColumnText(cb);
            ctext.SetSimpleColumn(rect.Left+5, rect.Top, rect.Right, rect.Bottom);
            ctext.SetText(phrase);
            ctext.Go();

            this.doc.Close();

            GetReader();

            //Add TextBox
            rect = new Rectangle(rect.Left, rect.Top - 100, rect.Right, rect.Bottom-100);
            TextField tf = new TextField(writer, rect, "text1");
            tf.Alignment = Element.ALIGN_LEFT | Element.ALIGN_TOP;
            tf.BorderColor = BaseColor.BLACK;
            tf.BorderStyle = PdfBorderDictionary.STYLE_SOLID;
            tf.Text = "TextField";
            stamper.AddAnnotation(tf.GetTextField(), 1);

            //Add Radio
            rect = new Rectangle(rect.Left, rect.Top - 100, rect.Right, rect.Bottom - 25);
            PdfFormField group = PdfFormField.CreateRadioButton(writer, true);

            group.FieldName = "grp1";
            RadioCheckField radioField = null;
            for (int i =0;i<5;i++)
            {
                Rectangle radioRect = new Rectangle(rect.Left + i * 25, rect.Top, rect.Left + (i + 1) * 25, rect.Bottom);
                radioField = new RadioCheckField(writer, radioRect, "chk"+i.ToString(), i.ToString());
                //radioField.BackgroundColor = new GrayColor(0.8f);
                radioField.BorderColor = GrayColor.BLACK;
                radioField.CheckType = RadioCheckField.TYPE_CIRCLE;
                //radioField.BorderWidth = 1;
                radioField.BorderStyle = PdfBorderDictionary.STYLE_SOLID;
                group.AddKid(radioField.RadioField);

                
            }
            group.SetAdditionalActions(PdfName.E, PdfAction.JavaScript("app.alert(validate);",writer));
            stamper.AddAnnotation(group, 1);


            //Add submit button
            rect = new Rectangle(rect.Left, rect.Top - 100, rect.Right, rect.Bottom - 25);
            PushbuttonField button = new PushbuttonField(writer, rect, "postSubmit");
            button.FontSize =20;
            button.BackgroundColor = BaseColor.LIGHT_GRAY;
            button.BorderColor = GrayColor.BLACK;
            button.BorderWidth = 1f;
            button.BorderStyle = PdfBorderDictionary.STYLE_BEVELED;
            button.TextColor = GrayColor.GREEN;
            button.FontSize = 8f;
            button.Text = "Submit";
            button.Visibility = PushbuttonField.VISIBLE_BUT_DOES_NOT_PRINT;
            PdfFormField field = button.Field;  
            field.Put(PdfName.TU, new PdfString("Save changes and return to the folder."));
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
    }


}
