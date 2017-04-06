using CD.ABM.Logic.PDF;

namespace CD.ABM.Logic.Blocks
{
    abstract class BasicPDFBlock : IBlock
    {
        protected UIConfig uiConfig = null;
        protected PDFDoc doc = null;
        protected PDFConfig config = null;
        static public int gap = 10;
        public PDFDoc Doc
        {
            get
            {
                return doc;
            }
        }

        public abstract float Draw();
        public abstract float Draw(float curY);
    }
}
