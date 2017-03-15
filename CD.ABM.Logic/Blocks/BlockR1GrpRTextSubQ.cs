using CD.ABM.Logic.PDF;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using CD.ABM.Logic.Drawing;
using CD.ABM.Logic.POCO;

namespace CD.ABM.Logic.Blocks
{
    class BlockR1GrpRTextSubQ : BasicPDFBlock
    {
        public BlockR1GrpRTextSubQ(PDFDoc _doc, PDFConfig _config)
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

        private List<ItemRef> getSubQuestions()
        {
            List<ItemRef> items = new List<ItemRef>();
            for (int counter = 0; counter < 15; counter++)
            {
                Random rand = new Random();
                if (counter >= config.SubQuestions.Count) break;
                ItemRef item = new ItemRef(
                    inputType: InputTypes.RadioButton,
                    formsGenId: "SubQue" + counter.ToString() ,
                    question: config.SubQuestions[counter],
                    uniqueId: "UniqueId" + counter.ToString() + rand.Next());
                items.Add(item);
            }
            return items;
        }

        public override float Draw(float _curY)
        {
            float curY= _curY, curX=200;
            Rectangle rect = Doc.AddRectange(10, curY - 15, doc.PageSize.Width - 10, curY, PDFColor.BCGGreen);
            curY = Doc.AddText(rect, config.MainQuestion, BaseColor.WHITE) + 10;
            {
                curY = doc.AddText(rect.OffSetRectByXAxis(curX-rect.Left), "NA", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX += 25), "1", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX += 25), "2", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX += 25), "3", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX += 25), "4", BaseColor.WHITE);
                curY = doc.AddText(rect.OffSetRectByXAxis(curX += 25), "5", BaseColor.WHITE);
            }
            curX = 200;
            foreach (ItemRef item in getSubQuestions())
            {
                Rectangle rRect = new Rectangle(curX, curY , curX + 15, curY - 15);
                doc.AddRadioGroup(item, rRect, new List<String> { "NA", "1", "2", "3", "4", "5" }, 25);
                rect = new Rectangle(10, curY - 15, 200, curY);
                curY = doc.AddText(rect, item.Question, PDFColor.BCGBlack);
            }
            if (config.OverAll != null)
            {
                rect = doc.AddRectange(rect.Left, curY - 25, 350, curY - 10, PDFColor.BCGGreen);
                curY = doc.AddText(rect, "Overall", BaseColor.WHITE);
                Rectangle rRect = new Rectangle(curX , rect.Top, curX+25, rect.Bottom);
                doc.AddRadioGroup(config.OverAll, rRect, new List<String> { "NA", "1", "2", "3", "4", "5" }, 25);
            }

            if (config.Comments != null)
            {
                doc.AddTextField(config.Comments,
                    new Rectangle(350, _curY - 15, doc.PageSize.Width - 10, curY + 5));
            }
            return curY;
        }
    }
}
