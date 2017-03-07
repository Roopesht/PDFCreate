using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Console
{
    class SystemUtil
    {
        public static void OpenFile(String cmd)
        {
            Process proc = null;
            string batDir = string.Format(@"C:\");
            proc = new Process();
            proc.StartInfo.WorkingDirectory = batDir;
            proc.StartInfo.FileName = cmd;
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();
            //proc.WaitForExit();
        }
    }
}
