using System;
using System.Windows.Forms;

namespace FallingPictureWindowsForms
{
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
            MyForm m = new MyForm();
            m.showDialog_();
        }
    }
}