using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.ABM.Logic.PDF
{
    public class JavaScript
    {
        public static void addValidateFunction(PdfWriter writer)
        {
            writer.AddJavaScript("var reqFields; ");
            string validateFunction = "function validate () { app.alert('This will validate the required fields'); " +
                " for (i=0; i<reqFields.length; i++) { " +
                " var grp = this.getField(reqFields[i]);  app.alert(grp.value); " + 
                " if (!grp || grp.value === null || grp.value == ''|| grp.value=='Off') { " +
                " app.alert('please select this '+ reqFields[i]); return false; }}" +
            "return true}";
            writer.AddJavaScript(validateFunction);

        }
        public static void addRequiredFields (PdfWriter writer, List<String> fieldNames)
        {
            String fieldString = "";
            foreach (string s in fieldNames)
            {
                if (fieldString == null || fieldString.Length == 0)
                    fieldString = fieldString +  "'" + s + "'" ;
                else
                    fieldString = fieldString + ",'" + s + "'";
            }
            writer.AddJavaScript("reqFields = [" + fieldString + "];");
            Console.WriteLine("reqFields = [" + fieldString + "];");
        }
    }
}
