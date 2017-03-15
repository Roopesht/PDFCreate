using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.ABM.Logic.PDF
{
    public static class PDFColor
    {
        public static BaseColor BCGGreen
        {
            get
            {
                return new BaseColor(0, 114, 84);
            }
        }
        public static BaseColor BCGBlack
        {
            get
            {
                return new BaseColor(25, 25, 25);
            }
        }
    }
}
