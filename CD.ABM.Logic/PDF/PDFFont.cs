using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.ABM.Logic.PDF
{
    public static class PDFFont
    {
        public static readonly Font NormalFont = new Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK));
        public static readonly Font SmallFont = new Font(FontFactory.GetFont(FontFactory.HELVETICA, 1000, Font.NORMAL, BaseColor.BLACK));

        public static Font GetNormalFont(BaseColor color)
        {
            return new Font(PDFFont.NormalFont) { Color = color };
        }
    }
}
