using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace McsUsbExample
{
    using MeaExampleNet;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MeaForm());
            // Application.Run(new Form1());
        }
    }
}
