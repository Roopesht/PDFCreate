using System;
using iTextSharp.text;

namespace CD.ABM.Logic.POCO
{
    public class ItemRef
    {
        public ItemRef(String inputType, string formsGenId, string question, String uniqueId)
        {
            if (inputType.ToLower() == "textbox") 
                this.InputType = InputTypes.TextBox;
            this.FormGenId = formsGenId;
            this.Question = question;
            this.UniqueId = uniqueId;
        }

        public ItemRef(InputTypes inputType, string formsGenId, string question, String uniqueId)
        {
            this.InputType = inputType;
            this.FormGenId = formsGenId;
            this.Question = question;
            this.UniqueId = uniqueId;
        }

        /// <summary>
        /// To identify the item, and apply the business logic, and PDF rendering logic, with in the the block
        /// </summary>
        public String FormGenId { get; set; }

        /// <summary>
        /// Unique identifier to apply any Javascript
        /// </summary>
        public String UniqueId { get; set; }

        public InputTypes InputType { get; set; }

        public String Question { get; set; }

        public bool IsMandatory { get; set; }

        public String DefaultValue { get; set; }

        public Rectangle rect { get; set; }

    }
}
