using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace K191432_DDR_A1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Application Started");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMazeSolver());
            Console.WriteLine("Application Ended");     
        }
    }
}
