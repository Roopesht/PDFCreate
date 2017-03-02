using CD.ABM.Logic.PDF;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD.ABM.Logic.Drawing;

namespace CD.ABM.Logic.Blocks
{
    class BlockR1GrpRText : BasicPDFBlock
    {
        public BlockR1GrpRText(PDFDoc _doc, PDFConfig _config)
        {
            doc = _doc;
            config = _config;
        }
        public override float Draw()
        {
            doc.AddNewPage();
            float curY = doc.PageSize.Height - 50;
            return Draw(curY);
        }

        public override float Draw(float curY)
        {
            float start = curY;

            Rectangle rect = Doc.AddRectange(10, curY - 15, doc.PageSize.Width - 10, curY, BaseColor.GREEN);
            curY = Doc.AddText(rect, config.MainQuestion, BaseColor.WHITE) + 10;

            rect = new Rectangle(rect.Left, curY -15, 270, curY);
            curY = doc.AddText(rect.OffSetRectByYAxis( 15), config.SubQuestions, BaseColor.LIGHT_GRAY);

            rect = new Rectangle(rect.Left, curY - 25, 270, curY - 10);
            doc.AddRectange(rect, BaseColor.GREEN);
            curY = doc.AddText(rect, "Overall", BaseColor.WHITE);

            curY = doc.AddText(rect.OffSetRectByXAxis(100), "NA", BaseColor.WHITE);
            curY = doc.AddText(rect.OffSetRectByXAxis(125), "1", BaseColor.WHITE);
            curY = doc.AddText(rect.OffSetRectByXAxis(150), "2", BaseColor.WHITE);
            curY = doc.AddText(rect.OffSetRectByXAxis(175), "3", BaseColor.WHITE);
            curY = doc.AddText(rect.OffSetRectByXAxis(200), "4", BaseColor.WHITE);
            curY = doc.AddText(rect.OffSetRectByXAxis(225), "5", BaseColor.WHITE);

            Doc.Writer.Flush();
            return curY;
        }
    }
}
