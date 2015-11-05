using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace server
{
    static class Program
    {
        /// <summary>
        /// The frmServer entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmServer());
        }
    }
}
