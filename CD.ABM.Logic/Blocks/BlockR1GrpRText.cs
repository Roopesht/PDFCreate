using CD.ABM.Logic.PDF;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using CD.ABM.Logic.Drawing;
using CD.ABM.Logic.POCO;

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

        public override float Draw(float _curY)
        {
            float curY= _curY, curX=100;
            Rectangle rect = Doc.AddRectange(10, curY - 15, doc.PageSize.Width - 10, curY, BaseColor.GREEN);
            curY = Doc.AddText(rect, config.MainQuestion, BaseColor.WHITE) + 10;

            rect = new Rectangle(rect.Left, curY -15, 300, curY);
            curY = doc.AddText(rect.OffSetRectByYAxis( 15), config.SubQuestions, BaseColor.LIGHT_GRAY);

            //Input input = (Input)config.Inputs.Select(item => item.Identifer == "overall");
            if (config.OverAll != null)
            {
                rect = doc.AddRectange(rect.Left, curY - 25, 300, curY - 10,BaseColor.GREEN); 
                Rectangle rRect = new Rectangle(255, rect.Top, rect.Left + 15, rect.Bottom);
                
                curY = doc.AddText(rect, "Overall", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX), "NA", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX += 35), "1", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX += 25), "2", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX += 25), "3", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX += 25), "4", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX += 25), "5", BaseColor.WHITE);

                doc.AddRadioGroup(config.OverAll.UniqueId, rRect, new List<String> { "NA", "1", "2", "3", "4", "5" }, 25);
            }

            if (config.Comments != null)
            {
                doc.AddTextField(config.Comments,
                    new Rectangle(300, _curY - 15, doc.PageSize.Width - 10, curY + 5));
            }
            return curY;
        }
    }
}
