using iTextSharp.text;

namespace CD.ABM.Logic.PDF
{
    public static class PDFFont
    {
        public static readonly Font NormalFont = new Font(FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.NORMAL, BaseColor.BLACK));
        public static readonly Font SmallFont = new Font(FontFactory.GetFont(FontFactory.HELVETICA, 1000, Font.NORMAL, BaseColor.BLACK));

        public static Font GetNormalFont(BaseColor color)
        {
            return new Font(PDFFont.NormalFont) { Color= color};//Color = color
        }
    }
}
