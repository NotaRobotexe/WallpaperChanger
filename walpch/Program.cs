using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace walpch
{
    class Program
    {
        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam,string pvParam, uint fWinIni);
        [DllImport("User32", CharSet = CharSet.Auto)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();

            ShowWindow(handle, 0);

            Class1 cls = new Class1();
            cls.createfolderAndCopyImg();
            cls.getpicturesAndSpeed();

            while (true)
            {
                foreach (string pc in cls.pictures)
                {
                    if (!pc.Contains("conf"))
                    {
                        SystemParametersInfo(0x0014, 0, pc, 0x0001);
                        Thread.Sleep(int.Parse(cls.speed));
                    }
                }
            }
            
        }
    }
}
