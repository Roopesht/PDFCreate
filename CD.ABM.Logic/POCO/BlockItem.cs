using System;

namespace CD.ABM.Logic.POCO
{
    public class BlockItem
    {
        private String formGenIdentifer;

        public String FormGenIdentifer
        {
            get { return formGenIdentifer; }
            set { formGenIdentifer = value; }
        }

        private String inputType;
        public String InputType
        {
            get { return inputType; }
            set { inputType = value; }
        }

        private String question;

        public String Question
        {
            get { return question; }
            set { question = value; }
        }




    }
}
