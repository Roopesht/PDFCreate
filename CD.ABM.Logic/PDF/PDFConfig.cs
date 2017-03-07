using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CD.ABM.Logic.PDF
{
    public class PDFConfig
    {
        private String mainQuestion;
        private List<String> subQuestions;
        private List<POCO.Input> inputs;

        public List<POCO.Input>  Inputs
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
        public PDFConfig (String xml)
        {
            if (xml=="1")
            {
                mainQuestion = "Value Creation and Impact";
                subQuestions = new List<string>() {
            "Depth of expertise contributes to the overall success of the project",
            "Develops clear recommendations with an action bias",
            "Networks within client organization to understand agenda",
            "Is able to assess implementation challenges",
            "Applies expertise to generate superior and sustainable results for client; is committed to making change happen",
            "Effectively transfers capabilities to client teams"};
            }
            inputs = new List<POCO.Input>()
            {
                new POCO.Input(POCO.InputTypes.RadioButton, "RAD1_1", "Val1"),
                new POCO.Input(POCO.InputTypes.RadioButton, "RAD1_2", "Val2"),
                new POCO.Input(POCO.InputTypes.RadioButton, "RAD1_3", "Val3"),
                new POCO.Input(POCO.InputTypes.RadioButton, "RAD1_4", "Val4"),
                new POCO.Input(POCO.InputTypes.RadioButton, "RAD1_5", "Val5")
            };
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
