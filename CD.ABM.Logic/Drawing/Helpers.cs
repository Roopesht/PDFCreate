using iTextSharp.text;

namespace CD.ABM.Logic.Drawing
{
    public static class Helpers
    {
        public static Rectangle OffSetRectByYAxis(this Rectangle rec, float distance)
        {
            Rectangle rect = new Rectangle(rec.Left, rec.Bottom - distance, rec.Right, rec.Top - distance);
            return rect;
        }
        public static Rectangle OffSetRectByXAxis(this Rectangle rec, float distance)
        {
            Rectangle rect = new Rectangle(rec.Left + distance, rec.Bottom, rec.Right + distance, rec.Top);
            return rect;
        }
    }
}
