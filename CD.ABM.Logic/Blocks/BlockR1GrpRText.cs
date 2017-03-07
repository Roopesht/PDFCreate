﻿using CD.ABM.Logic.PDF;
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
            List<POCO.Input> radios = new List<POCO.Input>();
            curY = doc.AddText(rect.OffSetRectByXAxis(100), "NA", BaseColor.WHITE);
            radios.Add(config.Inputs[0].AddRectangle(rect.OffSetRectByXAxis(100).OffSetRectByYAxis(100)));
            curY = doc.AddText(rect.OffSetRectByXAxis(125), "1", BaseColor.WHITE);
            radios.Add(config.Inputs[1].AddRectangle(rect.OffSetRectByXAxis(125).OffSetRectByYAxis(100)));
            curY = doc.AddText(rect.OffSetRectByXAxis(150), "2", BaseColor.WHITE);
            radios.Add(config.Inputs[2].AddRectangle(rect.OffSetRectByXAxis(150).OffSetRectByYAxis(100)));
            curY = doc.AddText(rect.OffSetRectByXAxis(175), "3", BaseColor.WHITE);
            radios.Add(config.Inputs[3].AddRectangle(rect.OffSetRectByXAxis(175).OffSetRectByYAxis(100)));
            curY = doc.AddText(rect.OffSetRectByXAxis(200), "4", BaseColor.WHITE);
            radios.Add(config.Inputs[4].AddRectangle(rect.OffSetRectByXAxis(200).OffSetRectByYAxis(100)));
            curY = doc.AddText(rect.OffSetRectByXAxis(225), "5", BaseColor.WHITE);
            doc.Close();
            doc.GetReader();
            doc.AddRadio(radios);
            doc.AddTextField(new POCO.Input (POCO.InputTypes.TextBox, "abc","abc" ), rect.OffSetRectByXAxis(225));
            return curY;
        }
    }
}
