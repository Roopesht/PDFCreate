using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.ABM.Logic.POCO
{
    public class Input
    {
        private InputTypes inputType;
        private String iDRef;
        private String identifer;
        private String defaultValue;
        private Rectangle rect; 

        public Input AddRectangle (Rectangle _rect)
        {
            this.rect = _rect;
            return this;
        }
        public Rectangle Rect
        {
            get { return this.rect; }
        }

        public InputTypes InputType
        {
            get { return inputType; }
        }
        public String IDRef
        {
            get { return iDRef; }
        }

        public String Identifer
        {
            get { return this.identifer; }
        }

        public Input(InputTypes _inputType, String _identifier, String _idRef)
        {
            this.iDRef = _idRef;
            this.inputType = _inputType;
            this.identifer = _identifier;
        }
        public Input (InputTypes _inputType, String _identifier, String _idRef, Rectangle _rect)
        {
            this.iDRef = _idRef;
            this.inputType = _inputType;
            this.identifer = _identifier;
            this.rect = _rect;
        }
        public String DefaultValue
        {
            get { return defaultValue; }
        }
    }
    public enum InputTypes
    {
        RadioButton=1,
        TextBox = 2,
        Checkbox = 3
    }
}
