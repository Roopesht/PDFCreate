using CD.ABM.Logic.PDF;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //Try this: send the parameter (bool simulation), and check the YLine towards the end
            float curY= _curY, curX;

            Rectangle rect = Doc.AddRectange(10, curY - 15, doc.PageSize.Width - 10, curY, BaseColor.GREEN);
            curY = Doc.AddText(rect, config.MainQuestion, BaseColor.WHITE) + 10;

            rect = new Rectangle(rect.Left, curY -15, 300, curY);
            curY = doc.AddText(rect.OffSetRectByYAxis( 15), config.SubQuestions, BaseColor.LIGHT_GRAY);
            //Input input = (Input)config.Inputs.Select(item => item.Identifer == "overall");
            //if (input != null)
            {
                rect = doc.AddRectange(rect.Left, curY - 25, 300, curY - 10,BaseColor.GREEN); 
                Rectangle rRect = new Rectangle(255, rect.Top, rect.Left + 15, rect.Bottom);
                
                curY = doc.AddText(rect, "Overall", BaseColor.WHITE);
                doc.AddRadioGroup("grp" + rect.Top.ToString(), rRect, new List<String> { "NA", "1", "2", "3", "4", "5" },25 );
                //curY = doc.AddRadioGroup(input.IDRef, );
            }

            List<POCO.Input> radios = new List<POCO.Input>();
            Rectangle radioRect = new Rectangle(rect.Left, rect.Top, rect.Left + 15, rect.Bottom  );
            curX = 100;
            curY = doc.AddText(rect.OffSetRectByXAxis(curX), "NA", BaseColor.WHITE);
            radios.Add(config.Inputs[0].AddRectangle(radioRect.OffSetRectByXAxis(curX + 20)));
            curX += 35;
            curY = doc.AddText(rect.OffSetRectByXAxis(curX), "1", BaseColor.WHITE);
            radios.Add(config.Inputs[1].AddRectangle(radioRect.OffSetRectByXAxis(curX + 15)));
            curX += 25;
            curY = doc.AddText(rect.OffSetRectByXAxis(curX), "2", BaseColor.WHITE);
            radios.Add(config.Inputs[2].AddRectangle(radioRect.OffSetRectByXAxis(curX + 15)));
            curX += 25;
            curY = doc.AddText(rect.OffSetRectByXAxis(curX), "3", BaseColor.WHITE);
            radios.Add(config.Inputs[3].AddRectangle(radioRect.OffSetRectByXAxis(curX + 15)));
            curX += 25;
            curY = doc.AddText(rect.OffSetRectByXAxis(curX), "4", BaseColor.WHITE);
            radios.Add(config.Inputs[4].AddRectangle(radioRect.OffSetRectByXAxis(curX + 15)));
            curX += 25;
            curY = doc.AddText(rect.OffSetRectByXAxis(curX), "5", BaseColor.WHITE);
            radios.Add(config.Inputs[5].AddRectangle(radioRect.OffSetRectByXAxis(curX + 15)));
            //doc.AddRadio(radios);
            Rectangle bigTextRect = new Rectangle(300, _curY - 15, doc.PageSize.Width - 10, curY+5);
            Input input = new Input(InputTypes.TextBox, "bigtext", "text",bigTextRect);
            doc.AddTextField(input, bigTextRect);
            return curY;
        }
    }
}
