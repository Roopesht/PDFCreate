using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.ABM.Logic.Blocks
{
    interface IBlock
    {
        float Draw();
        float Draw(float curY);
    }
}
