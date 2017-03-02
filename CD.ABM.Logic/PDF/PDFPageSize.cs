using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.ABM.Logic.PDF
{
    public class PDFPageSize
    {
        public readonly float Height = iTextSharp.text.PageSize.LETTER.Height;
        public readonly float Width = iTextSharp.text.PageSize.LETTER.Width;
        public readonly float LeftMargin = 50;
        public readonly float RightMargin = 50;
        public readonly float TopMargin = 50;
        public readonly float BottomMargin = 50;

    }
}
