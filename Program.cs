using Microsoft.VisualBasic.Logging;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace AotForms
{
    internal static class Program
    {
        [UnmanagedCallersOnly(EntryPoint = "Load")]
        public static void Load(nint pVM)
        {
            InternalMemory.Initialize(pVM);
            var process = Process.GetCurrentProcess();
            ComWrappers.RegisterForTrackerSupport(WinFormsComInterop.WinFormsComWrappers.Instance);
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.Run(new Form1(process.MainWindowHandle));

        }

        //public static void Main(string[] args) {

        //    Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        //    Application.EnableVisualStyles();
        //    Application.Run(new Main(IntPtr.Zero));
        //}
    }
}