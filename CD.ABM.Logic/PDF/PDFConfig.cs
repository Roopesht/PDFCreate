using CD.ABM.Logic.POCO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CD.ABM.Logic.PDF
{
    public class PDFConfig
    {
        private String mainQuestion;
        private List<String> subQuestions;
        private List<ItemRef> inputs;

        public ItemRef Comments
        {
            get
            {
                return inputs.Find(item => item.FormGenId == "Comments");
            }
        }

        public ItemRef OverAll
        {
            get
            {
                return inputs.Find(item => item.FormGenId == "OverallRadio");
            }
        }


        private void extractMainQuestion(List<ItemRef> items)
        {
            ItemRef it = items.SingleOrDefault(item => item.FormGenId == "MainText");
            mainQuestion = it.Question;
            items.Remove(it);
        }

        private void ExtractSubQuestions(List<ItemRef> items)
        {
            List<ItemRef> its = items.FindAll(item => item.FormGenId.Substring(0, 6) == "SubQue");
            subQuestions = its.Select(item => item.Question).ToList();
            for(int counter=its.Count-1; counter>=0; counter--)
            {
                items.Remove(its[counter]);
            }
        }

        public PDFConfig(List<ItemRef> items)
        {
            extractMainQuestion(items);
            ExtractSubQuestions(items);
            inputs = items;
        }


        public List<POCO.ItemRef>  Inputs
        {
            get { return inputs; }
        }
        public String MainQuestion
        {
            get {return mainQuestion; }
        }
        public List<String> SubQuestions
        {
            get { return subQuestions; }
        }
        public PDFConfig ()
        {

        }
        Random rand = new Random(100);
        public PDFConfig (String xml)
        {
            if (xml=="1")
            {
                mainQuestion = "Value Creation and Impact new TExt";
                subQuestions = new List<string>() {
            "Depth of expertise contributes to the overall success of the project",
            "Develops clear recommendations with an action bias",
            "Networks within client organization to understand agenda",
            "Is able to assess implementation challenges",
            "Applies expertise to generate superior and sustainable results for client; is committed to making change happen",
            "Effectively transfers capabilities to client teams",
            "Networks within client organization to understand agenda",
            "Is able to assess implementation challenges",
            "Applies expertise to generate superior and sustainable results for client; is committed to making change happen",
            "Effectively transfers capabilities to client teams"};
            }
            inputs.Add(new ItemRef(InputTypes.TextBox, "Comments", "", rand.Next().ToString())); 
            /*
             * 
            */
            if (xml =="2")
            {
                mainQuestion = "Insight and Intellectual Leadership in Area of Expertise";
                subQuestions = new List<string>() {
            "Is recognized by team as an expert",
            "Defines the client's overall problem; accurately and creatively frames the real issue(s)",
            "Can handle complex problems; delivers integrated solutions",
            "Goes beyond the obvious",
            "Motivates team creativity and uses team insight effectively",
            "Mobilizes the best expert support; quickly adapts and deploys BCG ideas",
            "Builds and contributes to BCG's intellectual capital by developing and sharing new ideas"};

            }
        }


    }
}
