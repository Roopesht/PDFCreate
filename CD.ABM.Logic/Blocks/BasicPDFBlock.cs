using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD.ABM.Logic.PDF;

namespace CD.ABM.Logic.Blocks
{
    
    abstract class BasicPDFBlock : IBlock
    {
        protected PDFDoc doc = null;
        protected PDFConfig config = null;
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
