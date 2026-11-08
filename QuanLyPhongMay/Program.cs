using System;
using System.Windows.Forms;

namespace QuanLyPhongMay
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Tối ưu hóa hiển thị sắc nét trên màn hình độ phân giải cao (High-DPI 125%, 150%, 4K)
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            Application.Run(new Views.FormSoDo());
        }
    }
}
