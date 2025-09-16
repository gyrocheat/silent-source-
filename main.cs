using AntiInjection_KhoaVu;
using Guna.UI2.WinForms;
using KhoaVuxMem;
using Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Gma.System.MouseKeyHook;
using AotForms;

using SmartMewwxSeww;
using Client;
using AimBotConquer.Guna;
using System.Net;
using Guna.UI2.AnimatorNS;
using System.Reflection.Emit;
using ImGuiNET;
using System.Numerics;
using TrackbarFloat;
using System.Configuration;




namespace AotForms

{
    public partial class main : Form
    {
        private bool isSpeedEnabled = false;
        private bool isWallEnabled = false;

        private List<long> SpeedAddresses = new();
        private List<long> WallAddresses = new();
        private bool waitingForHidehackKey = false;
        // Patterns
        private const string SearchSpeed = "01 00 00 00 02 2B 07 3D";
        private const string ReplaceSpeed = "01 00 00 00 02 2B 70 3D";

        private const string SearchWall = "c";
        private const string ReplaceWall = "bf ae 47 81 3f 00 1a b7 ee dc 3a 9f ed 30";

        private Keys HideKey;
        private Keys TelePortHotKey;


        static SmartxSewww Gay = new SmartxSewww();



        private bool isListening = false;

        private bool isListening1 = false;

        private Keys assignedKey = Keys.None;
        private IKeyboardMouseEvents globalHook;
        private System.Windows.Forms.Timer processCheckTimer;
        private static KhoaVu m = new KhoaVu();

        private string ATB;
        private static MemThanh skygod = new MemThanh();

        Mem q = new Mem();

        public main()
        {



            InitializeComponent();


            processCheckTimer = new System.Windows.Forms.Timer();
            processCheckTimer.Interval = 4000; // Kiểm tra mỗi 5 giây
            processCheckTimer.Tick += new EventHandler(ProcessCheckTimer1_Tick);
            processCheckTimer.Start();





            trackBarFloatClient2.Event_0 += TrackBarFloat2_ValueChanged;

            // Sự kiện khi cuộn (scroll)
            trackBarFloatClient2.Event_1 += TrackBarFloat2_Scroll;




            trackBarFloatClient1.Event_0 += TrackBarFloat1_ValueChanged;

            // Sự kiện khi cuộn (scroll)
            trackBarFloatClient1.Event_1 += TrackBarFloat1_Scroll;


            hookCallback = new LowLevelKeyboardProc(HookCallback);
            hookID = SetHook(hookCallback);
            Application.ApplicationExit += Application_ApplicationExit;


        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static IntPtr hookID = IntPtr.Zero;
        private LowLevelKeyboardProc hookCallback;
        private bool waitPressKeyForBind = false;
        private bool waitPressKeyForSpd = false;
        private bool waitPressKeyForSpeed = false;
        private bool waitPressKeyForcLft = false;
        private bool waitPressKeyForSpx = false;
        private bool waitPressKeyForFly = false;
        private bool waitPressKeyForUp = false;


        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);


        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            UnhookWindowsHookEx(hookID);
        }
        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                IntPtr moduleHandle = GetModuleHandle(null);
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, moduleHandle, 0);
            }
        }

        // --- Hook callback ---
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                Keys keyPressed = (Keys)Marshal.ReadInt32(lParam);

                if (waitPressKeyForBind)
                {
                    guna2Button1.Text = keyPressed == Keys.Escape ? "None" : keyPressed.ToString();
                    waitPressKeyForBind = false;
                }
                else if (waitPressKeyForSpd)
                {
                    guna2Button2.Text = keyPressed == Keys.Escape ? "None" : keyPressed.ToString();
                    waitPressKeyForSpd = false;
                }
                else if (waitPressKeyForSpeed)
                {
                    guna2Button3.Text = keyPressed == Keys.Escape ? "None" : keyPressed.ToString();
                    waitPressKeyForSpeed = false;
                }
                else if (waitPressKeyForUp)
                {
                    guna2Button15.Text = keyPressed == Keys.Escape ? "None" : keyPressed.ToString();
                    waitPressKeyForUp = false;
                }
                else if (waitPressKeyForFly)
                {
                    guna2Button14.Text = keyPressed == Keys.Escape ? "None" : keyPressed.ToString();
                    waitPressKeyForFly = false;
                }
                else if (waitPressKeyForSpx)
                {
                    guna2Button10.Text = keyPressed == Keys.Escape ? "None" : keyPressed.ToString();
                    waitPressKeyForSpx = false;
                }
                else
                {
                    if (Keys.TryParse(guna2Button1.Text.Replace("...", ""), out Keys bindingForBind) && keyPressed == bindingForBind)
                    {
                        guna2CustomCheckBox1.Checked = !guna2CustomCheckBox1.Checked;
                        // Không cần gọi lại thủ công nếu đã dùng CheckedChanged
                    }
                    if (Keys.TryParse(guna2Button3.Text.Replace("...", ""), out Keys bindingForSpeed) && keyPressed == bindingForSpeed)
                    {
                        guna2CustomCheckBox8.Checked = !guna2CustomCheckBox8.Checked;
                        // Không cần gọi lại thủ công nếu đã dùng CheckedChanged
                    }
                    if (Keys.TryParse(guna2Button2.Text.Replace("...", ""), out Keys bindingForSpd) && keyPressed == bindingForSpd)
                    {
                        guna2CustomCheckBox4.Checked = !guna2CustomCheckBox4.Checked;
                        // Không cần gọi lại thủ công nếu đã dùng CheckedChanged
                    }
                    if (Keys.TryParse(guna2Button15.Text.Replace("...", ""), out Keys bindingForUp) && keyPressed == bindingForUp)
                    {
                        guna2CustomCheckBox20.Checked = !guna2CustomCheckBox20.Checked;
                        // Không cần gọi lại thủ công nếu đã dùng CheckedChanged
                    }
                    if (Keys.TryParse(guna2Button14.Text.Replace("...", ""), out Keys bindingForFly) && keyPressed == bindingForFly)
                    {
                        guna2CustomCheckBox21.Checked = !guna2CustomCheckBox21.Checked;
                        // Không cần gọi lại thủ công nếu đã dùng CheckedChanged
                    }
                    if (Keys.TryParse(guna2Button10.Text.Replace("...", ""), out Keys bindingForSpx) && keyPressed == bindingForSpx)
                    {
                        guna2CustomCheckBox6.Checked = !guna2CustomCheckBox6.Checked;
                        // Không cần gọi lại thủ công nếu đã dùng CheckedChanged
                    }
                }
            }

            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        private void guna2CustomCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            label13.ForeColor = guna2CustomCheckBox1.Checked
                ? Color.FromArgb(20, 20, 20)
                : Color.DimGray;

            Config.telekill = guna2CustomCheckBox1.Checked;
        }


        private void guna2CustomCheckBox21_CheckedChanged(object sender, EventArgs e)
        {
            label18.ForeColor = guna2CustomCheckBox21.Checked
                    ? Color.FromArgb(20, 20, 20)
                    : Color.DimGray;
            Config.flyme = guna2CustomCheckBox21.Checked;
        }


        private void guna2CustomCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            label11.ForeColor = guna2CustomCheckBox6.Checked
                    ? Color.FromArgb(20, 20, 20)
                    : Color.DimGray;
            Config.speedx = guna2CustomCheckBox6.Checked;
        }

        private void guna2CustomCheckBox20_CheckedChanged(object sender, EventArgs e)
        {
            label17.ForeColor = guna2CustomCheckBox20.Checked
             ? Color.FromArgb(20, 20, 20)
             : Color.DimGray;
            Config.UpPlayer = guna2CustomCheckBox20.Checked;
        }

        private void guna2CustomCheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            label9.ForeColor = guna2CustomCheckBox8.Checked
                ? Color.FromArgb(20, 20, 20)
                : Color.DimGray;

            Config.speed = guna2CustomCheckBox8.Checked;


        }

        private void guna2CustomCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            label14.ForeColor = guna2CustomCheckBox4.Checked
                ? Color.FromArgb(20, 20, 20)
                : Color.DimGray;

            Config.proxtelekill = guna2CustomCheckBox4.Checked;
        }


        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        public static bool Streaming;
        private bool hotkeyExecuted;

        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);


        private void TrackBarFloat3_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }


        private void TrackBarFloat7_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }




        private void TrackBarFloat4_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }


        private void TrackBarFloat2_ValueChanged(object sender, EventArgs e)
        {
            var cossp = trackBarFloatClient2.Value;
            Config.CrosshairRotationSpeed = cossp;

            label77.Text = $"{cossp}";




        }

        private void TrackBarFloat2_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }



        private void TrackBarFloat1_ValueChanged(object sender, EventArgs e)
        {
            var cossize = trackBarFloatClient1.Value;
            Config.CrosshairSize = cossize;

            label71.Text = $"{cossize}";
        }
        private void TrackBarFloat1_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }

        private void TrackBarFloat5_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }
        private void ProcessCheckTimer1_Tick(object sender, EventArgs e)
        {
            // Tên của Cheat Engine có thể khác nhau tùy phiên bản, kiểm tra một vài tên phổ biến
            string[] cheatEngineProcessNames = {
                "cheatengine-x86_64",
                "cheatengine-i386",
                "cheatengine",
                "lunarengine-x86_64",
                "lunarengine-i386",
                "lunarengine",
                "The Wireshark Network Analyzer",
                "Progress Telerik Fiddler Web Debugger",
                "Fiddler",
                "HTTP Debugger",
                "x64dbg",
                "dnSpy",
                "FolderChangesView",
                "BinaryNinja",
                "HxD",
                "Cheat Engine 7.2",
                "Cheat Engine 7.1",
                "Cheat Engine 7.4",
                "Cheat Engine 7.5",
                "Cheat Engine 7.0",
                "Cheat Engine 6.9",
                "Cheat Engine 6.8",
                "GameFuqr 0.0.2",
                "GameFuqr",
                "gamerfuqr-x86_64",
                "gamerfuqr-i386",
                "gamefuqr-x86_64-SSE4-AVX2",
                "Ida",
                "Ida Pro",
                "Ida Freeware",
                "HTTP Debugger Pro",
                "Process Hacker",
                "Process Hacker 2",
                "OllyDbg",
                "ollydbg.exe",
                "ProcessHacker.exe",
                "Dump-Fixer.exe",
                "kdstinker.exe",
                "tcpview.exe",
                "autoruns.exe",
                "autorunsc.exe",
                "filemon.exe",
                "procmon.exe",
                "regmon.exe",
                "procexp.exe",
                "ImmunityDebugger.exe",
                "Wireshark.exe",
                "dumpcap.exe",
                "HookExplorer.exe",
                "ImportREC.exe",
                "PETools.exe",
                "LordPE.exe",
                "SysInspector.exe",
                "proc_analyzer.exe",
                "sysAnalyzer.exe",
                "sniff_hit.exe",
                "windbg.exe",
                "joeboxcontrol.exe",
                "Fiddler.exe",
                "joeboxserver.exe",
                "ida64.exe",
                "ida.exe",
                "idaq64.exe"
            };

            foreach (string processName in cheatEngineProcessNames)
            {
                Process[] processes = Process.GetProcessesByName(processName);
                foreach (Process process in processes)
                {
                    try
                    {
                        process.Kill(); // Thử kết thúc tiến trình
                        //SendMessage("Cheat Engine Process Killed", "Đã đóng tiến trình Cheat Engine: " + process.ProcessName);
                    }
                    catch (Exception ex)
                    {
                        //SendMessage("Error Killing Process", "Lỗi khi đóng tiến trình: " + process.ProcessName + " - " + ex.Message);
                    }
                }
            }
            fixesp();
            updateEsp();
        }









        private void guna2CustomCheckBox7_Click(object sender, EventArgs e)
        {

        }

        private void treeViewEntities_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        private void ResetESPForEntity(Entity entity)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }


        public enum AimMode
        {
            Nearest,
            ClosestToCrosshair
        }


        private void dataTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Core.Entities = new();
            InternalMemory.Cache = new();
            Thread.Sleep(5);
        }
        private void label60_Click(object sender, EventArgs e)
        {

        }
        private void label56_Click(object sender, EventArgs e)
        {

        }
        private void label23_Click_2(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            fixesp();
            updateEsp();
        }
        private void fixesp()
        {
            Core.LocalPlayer = new();
            Core.Entities = new();
            InternalMemory.Cache = new();
            Core.Entities = new();
        }
        private void updateEsp() //
        {
            foreach (var entity in Core.Entities.Values)
            {
                if (entity.IsTeam != Bool3.False) continue;
                TreeNode entityNode = new TreeNode(entity.Name);
                entityNode.Nodes.Add(new TreeNode($"IsKnown: {entity.IsKnown}"));
                entityNode.Nodes.Add(new TreeNode($"IsTeam: {entity.IsTeam}"));
                entityNode.Nodes.Add(new TreeNode($"Head: {entity.Head}"));
                entityNode.Nodes.Add(new TreeNode($"Root: {entity.Root}"));
                entityNode.Nodes.Add(new TreeNode($"Health: {entity.Health}"));
                entityNode.Nodes.Add(new TreeNode($"IsDead: {entity.IsDead}"));
                entityNode.Nodes.Add(new TreeNode($"IsKnocked: {entity.IsKnocked}"));
            }
        }

        private void guna2CustomCheckBox3_Click(object sender, EventArgs e)
        {

        }


        private void guna2PictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void Form2_Load(object sender, EventArgs e)
        {
            setting.Location = new Point(39, 38);
            guna2CustomCheckBox1.CheckedChanged += guna2CustomCheckBox1_CheckedChanged;
            guna2CustomCheckBox4.CheckedChanged += guna2CustomCheckBox4_CheckedChanged;
            guna2CustomCheckBox8.CheckedChanged += guna2CustomCheckBox8_CheckedChanged;
            guna2CustomCheckBox20.CheckedChanged += guna2CustomCheckBox20_CheckedChanged;
            guna2CustomCheckBox21.CheckedChanged += guna2CustomCheckBox21_CheckedChanged;
            guna2CustomCheckBox6.CheckedChanged += guna2CustomCheckBox6_CheckedChanged;














        }








        private void guna2Panel5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomCheckBox10_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }



        private void guna2CustomCheckBox11_Click(object sender, EventArgs e)
        {

        }








        private void guna2PictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox13_Click(object sender, EventArgs e)
        {




        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        static IntPtr FindRenderWindow(IntPtr parent)
        {
            IntPtr renderWindow = IntPtr.Zero;
            StringBuilder sb = new StringBuilder(256);

            WinAPI.EnumChildWindows(parent, (hWnd, lParam) =>
            {
                WinAPI.GetWindowText(hWnd, sb, sb.Capacity);
                string windowName = sb.ToString();

                if (!string.IsNullOrEmpty(windowName) && windowName != "HD-Player")
                {
                    renderWindow = hWnd;
                    return false; // Dừng enum nếu đã tìm thấy
                }
                return true; // Tiếp tục tìm kiếm
            }, IntPtr.Zero);

            return renderWindow;
        }



        private void guna2Panel17_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ShowPanel(Panel activePanel)
        {



            activePanel.Visible = true;
        }
        private void guna2Panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomCheckBox15_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }



        private void guna2CustomCheckBox16_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }




        private async void guna2Button1_Click_1(object sender, EventArgs e)
        {


            string search1 = "00 48 2D E9 0D B0 A0 E1 20 D0 4D E2 FC 00 9F E5";
            string replace1 = "00 00 00 00";

            string search2 = "B0 B5 02 AF 88 B0 0C 4C 7C 44 24 68 25 68 07 95";
            string replace2 = "00 00 00 00";

            string search3 = "00 48 2D E9 0D B0 A0 E1 30 D0 4D E2 C8 20 9F E5";
            string replace3 = "00 00 00 00";


            string search4 = "F0 4B 2D E9 18 B0 8D E2 5A DF 4D E2 08 C0 9B E5";
            string replace4 = "00 00 00 00";

            string search5 = "F0 B5 03 AF 2D E9 00 07 8A B0 F8 4D 81 46 4C F2";
            string replace5 = "30 a0 f0 e3 00 f0";

            string search6 = "50 46 52 F2 AA FB 00 28 00 F0 CA 80 45 F2 EF 70";
            string replace6 = "30 a0 f0 e3 00 f0";


            string search7 = "04 46 01 A8 46 F2 62 F8 20 46 95 F2 A6 FE 60 08";
            string replace7 = "30 a0 f0 e3 00 f0";


            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                MessageBox.Show("Process not detected!");
                return;
            }

            int proc = Process.GetProcessesByName("HD-Player")[0].Id;
            m.OpenProcess(proc);

            var searchPatterns = new[] { search1, search2, search3, search4, search5, search6, search7 };
            var replacePatterns = new[] { replace1, replace2, replace3, replace4, replace5, replace6, replace7 };

            foreach (var search in searchPatterns)
            {
                IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                if (!wl.Any())
                {
                    MessageBox.Show($"Không tìm thấy chuỗi: {search}");
                    continue;
                }

                foreach (var address in wl)
                {
                    int index = Array.IndexOf(searchPatterns, search);
                    if (index == -1) continue;

                    m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[index]);
                }
                k = true;
            }

            if (k)
            {

                Console.Beep(1000, 500);
            }
            else
            {

            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_2(object sender, EventArgs e)
        {



        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {


        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }






        private void guna2ToggleSwitch11_CheckedChanged(object sender, EventArgs e)
        {

        }



        private void guna2ToggleSwitch10_CheckedChanged(object sender, EventArgs e)
        {

        }








        private void guna2Button5_Click(object sender, EventArgs e)
        {

        }


        private void guna2PictureBox6_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2ToggleSwitch13_CheckedChanged(object sender, EventArgs e)
        {

            fixesp();
            updateEsp();
        }

        private void esp1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {


        }

        private void guna2CustomCheckBox6_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox5_Click(object sender, EventArgs e)
        {


        }

        private void guna2CustomCheckBox7_Click_1(object sender, EventArgs e)
        {


        }

        private void guna2CustomCheckBox8_Click(object sender, EventArgs e)
        {

        }



        private void guna2CustomCheckBox9_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox9_Click_2(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox10_Click_1(object sender, EventArgs e)
        {


        }

        private void guna2CustomCheckBox11_Click_1(object sender, EventArgs e)
        {

            fixesp();
        }

        private void guna2CustomCheckBox12_Click(object sender, EventArgs e)
        {


            updateEsp();
        }

        private void guna2hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void guna2hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {

        }


        private void guna2CustomCheckBox18_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private async void guna2CustomCheckBox17_Click(object sender, EventArgs e)
        {

            string search2 = "a0 e3 6d 00 00 eb 00 0a b7 ee";
            string replace2 = "ff 02 44 e3 00 0a b7 ee 10 0a";





            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                // error
                MessageBox.Show("địt mẹ th ngu mở gl");


            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);
                // Applying

                int i2 = 22000000;

                var searchPatterns = new[] { search2,
     };
                var replacePatterns = new[] { replace2, };

                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            int index = Array.IndexOf(searchPatterns, search);
                            if (index >= 0 && index < replacePatterns.Length)
                            {
                                m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[index]);
                            }
                        }
                        k = true;
                    }
                }

                if (k)
                {
                    // Applied


                    Console.Beep(1000, 500);
                }
                else
                {

                }
            }
        }

        private void guna2CustomCheckBox20_Click(object sender, EventArgs e)
        {
            // stream mode
        }

        private void guna2hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void aimbot_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomCheckBox13_Click_1(object sender, EventArgs e)
        {


        }

        private void guna2CustomCheckBox8_Click_1(object sender, EventArgs e)
        {


        }

        private void guna2CustomCheckBox19_Click(object sender, EventArgs e)
        {


        }

        private void guna2CustomCheckBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox9_Click_3(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox14_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox15_Click_1(object sender, EventArgs e)
        {

        }


        private void guna2CustomCheckBox6_Click_1(object sender, EventArgs e)
        {

        }


        private void guna2CustomCheckBox4_Click_1(object sender, EventArgs e)
        {


        }

        private void guna2CustomCheckBox3_Click_2(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox2_Click_1(object sender, EventArgs e)
        {


        }


        private void guna2Panel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox14_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox1_Click_2(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox9_Click(object sender, EventArgs e)
        {

        }


        private void guna2CustomCheckBox11_Click_2(object sender, EventArgs e)
        {

        }

        private void guna2vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void guna2CustomCheckBox1_Click_3(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox15_Click(object sender, EventArgs e)
        {

        }


        private void guna2TrackBar2_Scroll(object sender, ScrollEventArgs e)
        {

        }



        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
















        private void guna2ToggleSwitch14_CheckedChanged(object sender, EventArgs e)
        {

            fixesp();
            updateEsp();
        }




        private async void guna2ToggleSwitch19_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void guna2Panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ToggleSwitch21_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {

        }

        private void guna2TrackBar2_Scroll_1(object sender, ScrollEventArgs e)
        {
            var fov1slient = guna2TrackBar2.Value;

            label2.Text = $"Fov : {fov1slient}";

            Config.AimbotFOV = fov1slient;
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }



        private void guna2Button32_Click(object sender, EventArgs e)
        {
            // TODO: Thêm hành động bạn muốn ở đây.
            MessageBox.Show("guna2Button32 Clicked!");
        }





        private void guna2Button33_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button26_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button34_Click(object sender, EventArgs e)
        {

        }

        private void EnabledFeature()// viet 2 bien 
        {
            speedbtn1.PerformClick(); // cai nay la cai bat




        }

        private void DisableFeature()
        {
            speedbtn2.PerformClick();//cai nay la cai tat

        }

        private void EnabledFeature1()// viet 2 bien 
        {
            wallbtn1.PerformClick(); // cai nay la cai bat




        }

        private void DisableFeature1()
        {
            wallbtn2.PerformClick();//cai nay la cai tat

        }



        private void esppvn_Paint(object sender, PaintEventArgs e)
        {

        }

        private async Task<List<long>> ScanAddresses(string pattern)
        {
            var process = Process.GetProcessesByName("HD-Player").FirstOrDefault();
            if (process == null || !m.OpenProcess(process.Id))
            {
                MessageBox.Show("Không tìm thấy hoặc không mở được tiến trình HD-Player.");
                return new List<long>();
            }

            try
            {
                var result = await Task.Run(() => m.AoBScan2(pattern, true, true));
                return result?.ToList() ?? new List<long>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi quét bộ nhớ: {ex.Message}");
                return new List<long>();
            }
        }

        private void PatchMemory(List<long> addresses, string patchPattern, string name)
        {
            if (addresses == null || addresses.Count == 0)
            {
                MessageBox.Show($"Vui lòng quét địa chỉ {name} trước.");
                return;
            }

            var errors = new StringBuilder();
            lock (m)
            {
                foreach (var addr in addresses)
                {
                    try
                    {
                        bool success = m.WriteMemory(
                            addr.ToString("X"),
                            "bytes",
                            patchPattern,
                            "",
                            Encoding.UTF8,
                            false
                        );

                        if (!success)
                            errors.AppendLine($"Không ghi được vào địa chỉ: 0x{addr:X}");
                    }
                    catch (Exception ex)
                    {
                        errors.AppendLine($"Lỗi tại 0x{addr:X}: {ex.Message}");
                    }
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), $"Lỗi ghi {name}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void guna2Button26_Click_1(object sender, EventArgs e)
        {
            string hex = "A0 E3 14 00 8D E5 09 00 A0 E1 18  8D E5 1C 10 8D E5 10 17 02 E3 35 FF 2F E1"; string replace = "A0 E3 14 00 8D E5 09 00 A0 E1 18 8D E5 1C 10 8D E5 10 17 02 E3 35 FF 2F E1";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }

            Console.Beep(800, 400);
        }


        private void guna2ToggleSwitch16_CheckedChanged_1(object sender, EventArgs e)
        {
            this.Close();
            Close();
        }






        private void guna2ToggleSwitch20_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void pvnsetting_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ToggleSwitch21_CheckedChanged_2(object sender, EventArgs e)
        {

        }

        private void guna2ToggleSwitch18_CheckedChanged_2(object sender, EventArgs e)
        {

            Core.Entities = new();
            InternalMemory.Cache = new();
        }

        private async void guna2ToggleSwitch18_CheckedChanged_3(object sender, EventArgs e)
        {
            string search1 = "ED 00 00 EB 24 00 9D E5 10 10 1B E5 0C 00 8D E5 01 00 A0 E1 60 00 00 EB 20 10 1B E5 08 00 8D E5 01 00 A0 E1 61 00 00 EB 0C 10 9D E5 04 00 8D E5 01 00 A0 E1 08 10 9D E5 04 20 9D E5 38 00 00 EB FF FF FF EA 10 00 1B E5 04 00 80 E2 10 00 0B E5 18 10 4B E2 18 00 9D E5 16 01 00 EB";
            string replace1 = "00 00 A0 E3 1E FF 2F E1 10 10 1B E5 0C 00 8D E5 01 00 A0 E1 60 00 00 EB 20 10 1B E5 08 00 8D E5 01 00 A0 E1 61 00 00 EB 0C 10 9D E5 04 00 8D E5 01 00 A0 E1 08 10 9D E5 04 20 9D E5 38 00 00 EB FF FF FF EA 10 00 1B E5 04 00 80 E2 10 00 0B E5 18 10 4B E2 18 00 9D E5 16 01 00 EB";

            string search2 = "88 D0 4D E2 74 0A 9F ED D0 C1 9F E5 0C C0 9F E7 00 C0 9C E5 04 C0 0B E5 38 00 8D E5 34 10 8D E5 30 20 8D E5 2C 30 8D E5 0A 0A 8D ED 38 00 9D E5 24 00 80 E2 00 10 00 E3 0B F6 FD EB 00 00 90 E5 24 00 8D E5 00 00 00 E3 20 00 8D E5 24 00 9D E5 00 00 90 E5 01 00 00 E2 00 00 50 E3";
            string replace2 = "00 00 A0 E3 1E FF 2F E1 D0 C1 9F E5 0C C0 9F E7 00 00 A0 E3 1E FF 2F E1 38 00 8D E5 34 10 8D E5 00 00 A0 E3 1E FF 2F E1 0A 0A 8D ED 38 00 9D E5 24 00 80 E2 00 10 00 E3 0B F6 FD EB 00 00 90 E5 24 00 8D E5 00 00 00 E3 20 00 8D E5 24 00 9D E5 00 00 90 E5 01 00 00 E2 00 00 50 E3";

            string search3 = "30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 00 50 A0 E3 B2 2F BF E6 31 1F BF E6 0C 40 9B E5 00 50 8C E5 B2 21 CD E1 02 20 A0 E3 B0 21 CD E1 10 20 A0 E3 1C 50 8D E5 18 50 8D E5 14 10 8D E5 10 10 8D E2 08 E0 9B E5 00 E0 8D E5 10 10";
            string replace3 = "00 00 A0 E3 1E FF 2F E1 20 D0 4D E2 10 C0 9B E5 00 50 A0 E3 B2 2F BF E6 31 1F BF E6 0C 40 9B E5 00 50 8C E5 B2 21 CD E1 02 20 A0 E3 B0 21 CD E1 10 20 A0 E3 1C 50 8D E5 18 50 8D E5 14 10 8D E5 10 10 8D E2 08 E0 9B E5 00 E0 8D E5 10 10 8D E9 BD FF FF EB 08 D0 4B E2 30 88 BD E8";

            string search4 = "30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5 10 10 8D E5 01 00 02 E2 0F 00 CD E5 08 30 8D E5 14 00 9D E5 10 10 9D E5 01 00 51 E3 04 00 8D E5 05 00 00 1A 49 DD FF EB 0C 10 9B E5 0F 20 DD E5 01 20 02 E2 A6 E2 FF EB 02 00 00 EA 0C 10 9B E5 10 00 4B E2 BC 62 03 EB 0F 00 DD E5 01 00 10 E3 09";
            string replace4 = "00 00 A0 E3 1E FF 2F E1 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 00 00 A0 E3 1E FF 2F E1 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5 10 10 8D E5 01 00 02 E2 0F 00 CD E5 08 30 8D E5 14 00 9D E5 10 10 9D E5 01 00 51 E3 04 00 8D E5 05 00 00 1A 49 DD FF EB 0C 10 9B E5 0F 20 DD E5 01 20 02 E2 A6 E2 FF EB 02 00 00 EA 0C 10 9B E5 10 00 4B E2 BC 62 03 EB 0F 00 DD E5 01 00 10 E3 09";

            string search5 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 00 10 00 E3 15 10 4B E5 24 00 8D E5 10 00 1B E5 0C 00 90 E5 B4 00 D0 E1 14 10 1B E5 2C 10 91 E5 01 10 D1 E5 01 00 50 E1 00 00 00 0A EF 00 00 EA 00 00 00 E3 16 00 4B E5 14 00 1B E5 2C 00 90 E5 01 00 D0 E5 01 00 80 E2 17 00 4B E5 16 00 5B E5 17 10 5B E5 01 00 50 E1 DD 00 00 AA 14 00 1B E5 2C";
            string replace5 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 00 10 00 E3 15 10 4B E5 24 00 8D E5 10 00 1B E5 0C 00 90 E5 B4 00 D0 E1 14 10 1B E5 2C 10 91 E5 01 10 D1 E5 01 00 50 E1 00 00 00 0A EF 00 00 EA 00 00 00 E3 16 00 4B E5 14 00 1B E5 2C 00 90 E5 01 00 D0 E5 01 00 80 E2 17 00 4B E5 16 00 5B E5 17 10 5B E5 01 00 50 E1 DD 00 00 AA 14 00 1B E5 2C";

            string search6 = "30 48 2D E9 08 B0 8D E2 78 D0 4D E2 01 DA 4D E2 C0 13 9F E5 01 10 8F E0 BC 23 9F E5 02 20 9F E7 00 20 92 E5 0C 20 0B E5 5C 00 8D E5 5C 00 9D E5 6C 20 8D E2 30 00 8D E5 02 00 A0 E1 6F 15 00 EB 30 10 9D E5 1C 20 81 E2 2C 00 8D E5 02 00 A0 E1 E3 00 00 EB 01 00 10 E3 02 00 00 0A 01 00 00 E3 58 00 8D E5 CE 00 00 EA 02 EB 4B E2";
            string replace6 = "00 00 A0 E3 1E FF 2F E1 78 D0 4D E2 01 DA 4D E2 C0 13 9F E5 01 10 8F E0 BC 23 9F E5 02 20 9F E7 00 20 92 E5 0C 20 0B E5 5C 00 8D E5 5C 00 9D E5 6C 20 8D E2 30 00 8D E5 02 00 A0 E1 6F 15 00 EB 30 10 9D E5 1C 20 81 E2 2C 00 8D E5 02 00 A0 E1 E3 00 00 EB 01 00 10 E3 02 00 00 0A 01 00 00 E3 58 00 8D E5 CE 00 00 EA 02 EB 4B E2";

            string search7 = "30 48 2D E9 08 B0 8D E2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3 2F 00 00 8A FF FF FF EA 10 00 9D E5 EC 00 90 E5 04 00 90 E5 00 00 50 E3 00 00 00 1A 29 00 00 EA 93 D0 FF EB 10 10 9D E5 B2 10 D1 E1";
            string replace7 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3 2F 00 00 8A FF FF FF EA 10 00 9D E5 EC 00 90 E5 04 00 90 E5 00 00 50 E3 00 00 00 1A 29 00 00 EA 93 D0 FF EB 10 10 9D E5 B2 10 D1 E1";

            string search8 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 8C 31 9F E5 03 30 9F E7 00 30 93 E5 0C 30 0B E5 10 10 0B E5 28 00 0B E5 11 20 4B E5 28 00 1B E5 18 00 8D E5 5C 00 00 EB 18 00 0B E5 10 00 4B E2 18 10 4B E2 85 00 00 EB 2C 00 8D E5 18 00 9D E5 D0 A9 FC EB 28 00 8D E5 18 00 9D E5 F9 33 FD EB";
            string replace8 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 8C 31 9F E5 03 30 9F E7 00 30 93 E5 0C 30 0B E5 10 10 0B E5 28 00 0B E5 11 20 4B E5 28 00 1B E5 18 00 8D E5 5C 00 00 EB 18 00 0B E5 10 00 4B E2 18 10 4B E2 85 00 00 EB 2C 00 8D E5 18 00 9D E5 D0 A9 FC EB 28 00 8D E5 18 00 9D E5 F9 33 FD EB";

            string search9 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 0C C0 9B E5 08 E0 9B E5 10 40 9B E5 10 00 0B E5 14 10 0B E5 18 20 0B E5 1C 30 0B E5 24 C0 0B E5 28 E0 0B E5 10 00 1B E5 00 10 00 E3 2C 10 8D E5 28 10 8D E5 14 20 1B E5 01 00 52 E1 08 00 8D E5 09 00 00 0A 1C 00 1B E5 00 10 00 E3 01 00 50 E1";
            string replace9 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 0C C0 9B E5 08 E0 9B E5 10 40 9B E5 10 00 0B E5 14 10 0B E5 18 20 0B E5 1C 30 0B E5 24 C0 0B E5 28 E0 0B E5 10 00 1B E5 00 10 00 E3 2C 10 8D E5 28 10 8D E5 14 20 1B E5 01 00 52 E1 08 00 8D E5 09 00 00 0A 1C 00 1B E5 00 10 00 E3 01 00 50 E1 05 00 00 0A 18 00 1B E5 00 00 50 E3 02 00 00 BA 10 00 9B E5 00 00 50 E3 05 00 00";

            string search10 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 30 00 0B E5 34 10 0B E5 30 00 1B E5 28 00 8D E5 CC 04 00 EB 00 00 50 E3 02 00 00 0A 00 00 E0 E3 2C 00 0B E5 A7 00 00 EA 34 00 1B E5 02 00 D0 E5 01 00 10 E3 05 00 00 0A 42 2F 03 EB 34 10 1B E5 10 10";
            string replace10 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 30 00 0B E5 34 10 0B E5 30 00 1B E5 28 00 8D E5 CC 04 00 EB 00 00 50 E3 02 00 00 0A 00 00 E0 E3 2C 00 0B E5 A7 00 00 EA 34 00 1B E5 02 00 D0 E5 01 00 10 E3 05 00 00 0A 42 2F 03 EB 34 10 1B E5 10 10";

            string search11 = "00 48 2D E9 0D B0 A0 E1 E8 D0 4D E2 08 C0 9B E5 CC E6 9F E5 0E E0 9F E7 00 E0 9E E5 04 E0 0B E5 64 00 0B E5 68 10 0B E5 6C 20 0B E5 3C 30 0B E5 3C 00 1B E5 08 10 9B E5 01 00 50 E1 02 00 00 3A 00 00 00 E3 60 00 0B E5 96 01 00 EA FF FF FF EA 3C 00 1B E5 08 10 9B E5 01 00 50 E1 8F 01";
            string replace11 = "00 00 A0 E3 1E FF 2F E1 E8 D0 4D E2 08 C0 9B E5 CC E6 9F E5 0E E0 9F E7 00 E0 9E E5 04 E0 0B E5 64 00 0B E5 68 10 0B E5 6C 20 0B E5 3C 30 0B E5 3C 00 1B E5 08 10 9B E5 01 00 50 E1 02 00 00 3A 00 00 00 E3 60 00 0B E5 96 01 00 EA FF FF FF EA 3C 00 1B E5 08 10 9B E5 01 00 50 E1 8F 01";

            string search12 = "00 48 2D E9 0D B0 A0 E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04 00 00 1A 08 10 1B E5 0C 20 9D E5 08 00 9D E5 4F 00 00 EB 03 00 00 EA 08 10 1B E5 0C 20 9D E5 08 00 9D E5 11 01";
            string replace12 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04 00 00 1A 08 10 1B E5 0C 20 9D E5 08 00 9D E5 4F 00 00 EB 03 00 00 EA 08 10 1B E5 0C 20 9D E5 08 00 9D E5 11 01";

            string search13 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 01 10 00 E3 15 10 4B E5 00 10 00 E3 1C 10 0B E5 38 00 8D E5 1C 00 1B E5 10 10 1B E5 DC 10 81 E2 34 00 8D E5 01 00 A0 E1 3B 76 FF EB 34 10 9D E5 00 00";
            string replace13 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 01 10 00 E3 15 10 4B E5 00 10 00 E3 1C 10 0B E5 38 00 8D E5 1C 00 1B E5 10 10 1B E5 DC 10 81 E2 34 00 8D E5 01 00 A0 E1 3B 76 FF EB 34 10 9D E5 00 00";

            string search14 = "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 74 0A 9F ED D0 C1 9F E5 0C C0 9F E7 00 C0 9C E5 04 C0 0B E5 38 00 8D E5 34 10 8D E5 30 20 8D E5 2C 30 8D E5 0A 0A 8D ED 38 00 9D E5 24 00 80 E2 00 10 00 E3 0B F6 FD EB 00 00 90 E5 24 00 8D E5 00 00 00 E3 20 00 8D E5 24 00 9D E5 00 00 90 E5 01 00 00 E2";
            string replace14 = "00 00 A0 E3 1E FF 2F E1";

            string search15 = "00 48 2D E9 0D B0 A0 E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00 31 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 10 00 8D E5 01 00 A0 E1 0C 10 8D E5 02 10 A0 E1 16 20 00 E3 B7 2D FA EB 0C 00 9D E5 15 10 00 E3 18 20 00 E3 FF 20 02 E2 C6 FF FC EB 17 00 8D E2 B8 10 9F E5 01 10";
            string replace15 = "00 00 A0 E3 1E FF 2F E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00 31 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 10 00 8D E5 01 00 A0 E1 0C 10 8D E5 02 10 A0 E1 16 20 00 E3 B7 2D FA EB 0C 00 9D E5 15 10 00 E3 18 20 00 E3 FF 20 02 E2 C6 FF FC EB 17 00 8D E2 B8 10 9F E5 01 10";

            string search16 = "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 E0 22 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 34 00 0B E5 38 10 0B E5 00 00 A0 E3 3C 00 0B E5 38 10 1B E5 20 00 4B E2 2C 00 8D E5 9D 0C FE EB 2C 00 9D E5 7B FB FF EB 28 00 8D E5 FF FF FF EA 20 00 4B E2 E5 A3 FC EB 28 10 9D E5 40 10 0B E5 40 20";
            string replace16 = "00 00 A0 E3 1E FF 2F E1 88 D0 4D E2 E0 22 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 34 00 0B E5 38 10 0B E5 00 00 A0 E3 3C 00 0B E5 38 10 1B E5 20 00 4B E2 2C 00 8D E5 9D 0C FE EB 2C 00 9D E5 7B FB FF EB 28 00 8D E5 FF FF FF EA 20 00 4B E2 E5 A3 FC EB 28 10 9D E5 40 10 0B E5 40 20";



            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;


                var searchPatterns = new[] { search1, search2, search3, search4, search5 ,search6,search7,search8,search9,search10,search11,search12,search13,search14,search15,search16
            };
                var replacePatterns = new[] { replace1, replace2, replace3, replace4, replace5,replace6,replace7,replace8,replace9,replace10,replace11,replace12,replace13,replace14,replace15,replace16
            };

                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {

                    Console.Beep(1000, 500);
                }
                else
                {

                }
            }
        }

        private async void guna2ToggleSwitch19_CheckedChanged_2(object sender, EventArgs e)
        {


            string search7 = "30 48 2D E9 08 B0 8D E2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3 2F 00 00 8A FF FF FF EA 10 00 9D E5 EC 00 90 E5 04 00 90 E5 00 00 50 E3 00 00 00 1A 29 00 00 EA 93 D0 FF EB 10 10 9D E5 B2 10 D1 E1";
            string replace7 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3 2F 00 00 8A FF FF FF EA 10 00 9D E5 EC 00 90 E5 04 00 90 E5 00 00 50 E3 00 00 00 1A 29 00 00 EA 93 D0 FF EB 10 10 9D E5 B2 10 D1 E1";

            string search8 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 8C 31 9F E5 03 30 9F E7 00 30 93 E5 0C 30 0B E5 10 10 0B E5 28 00 0B E5 11 20 4B E5 28 00 1B E5 18 00 8D E5 5C 00 00 EB 18 00 0B E5 10 00 4B E2 18 10 4B E2 85 00 00 EB 2C 00 8D E5 18 00 9D E5 D0 A9 FC EB 28 00 8D E5 18 00 9D E5 F9 33 FD EB";
            string replace8 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 8C 31 9F E5 03 30 9F E7 00 30 93 E5 0C 30 0B E5 10 10 0B E5 28 00 0B E5 11 20 4B E5 28 00 1B E5 18 00 8D E5 5C 00 00 EB 18 00 0B E5 10 00 4B E2 18 10 4B E2 85 00 00 EB 2C 00 8D E5 18 00 9D E5 D0 A9 FC EB 28 00 8D E5 18 00 9D E5 F9 33 FD EB";

            string search9 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 0C C0 9B E5 08 E0 9B E5 10 40 9B E5 10 00 0B E5 14 10 0B E5 18 20 0B E5 1C 30 0B E5 24 C0 0B E5 28 E0 0B E5 10 00 1B E5 00 10 00 E3 2C 10 8D E5 28 10 8D E5 14 20 1B E5 01 00 52 E1 08 00 8D E5 09 00 00 0A 1C 00 1B E5 00 10 00 E3 01 00 50 E1";
            string replace9 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 0C C0 9B E5 08 E0 9B E5 10 40 9B E5 10 00 0B E5 14 10 0B E5 18 20 0B E5 1C 30 0B E5 24 C0 0B E5 28 E0 0B E5 10 00 1B E5 00 10 00 E3 2C 10 8D E5 28 10 8D E5 14 20 1B E5 01 00 52 E1 08 00 8D E5 09 00 00 0A 1C 00 1B E5 00 10 00 E3 01 00 50 E1 05 00 00 0A 18 00 1B E5 00 00 50 E3 02 00 00 BA 10 00 9B E5 00 00 50 E3 05 00 00";

            string search10 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 30 00 0B E5 34 10 0B E5 30 00 1B E5 28 00 8D E5 CC 04 00 EB 00 00 50 E3 02 00 00 0A 00 00 E0 E3 2C 00 0B E5 A7 00 00 EA 34 00 1B E5 02 00 D0 E5 01 00 10 E3 05 00 00 0A 42 2F 03 EB 34 10 1B E5 10 10";
            string replace10 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 30 00 0B E5 34 10 0B E5 30 00 1B E5 28 00 8D E5 CC 04 00 EB 00 00 50 E3 02 00 00 0A 00 00 E0 E3 2C 00 0B E5 A7 00 00 EA 34 00 1B E5 02 00 D0 E5 01 00 10 E3 05 00 00 0A 42 2F 03 EB 34 10 1B E5 10 10";

            string search11 = "00 48 2D E9 0D B0 A0 E1 E8 D0 4D E2 08 C0 9B E5 CC E6 9F E5 0E E0 9F E7 00 E0 9E E5 04 E0 0B E5 64 00 0B E5 68 10 0B E5 6C 20 0B E5 3C 30 0B E5 3C 00 1B E5 08 10 9B E5 01 00 50 E1 02 00 00 3A 00 00 00 E3 60 00 0B E5 96 01 00 EA FF FF FF EA 3C 00 1B E5 08 10 9B E5 01 00 50 E1 8F 01";
            string replace11 = "00 00 A0 E3 1E FF 2F E1 E8 D0 4D E2 08 C0 9B E5 CC E6 9F E5 0E E0 9F E7 00 E0 9E E5 04 E0 0B E5 64 00 0B E5 68 10 0B E5 6C 20 0B E5 3C 30 0B E5 3C 00 1B E5 08 10 9B E5 01 00 50 E1 02 00 00 3A 00 00 00 E3 60 00 0B E5 96 01 00 EA FF FF FF EA 3C 00 1B E5 08 10 9B E5 01 00 50 E1 8F 01";



            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] {search7,search8,search9,search10,search11
            };
                var replacePatterns = new[] { replace7,replace8,replace9,replace10,replace11
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }

        private async void guna2ToggleSwitch18_CheckedChanged_4(object sender, EventArgs e)
        {


            string search12 = "00 48 2D E9 0D B0 A0 E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04 00 00 1A 08 10 1B E5 0C 20 9D E5 08 00 9D E5 4F 00 00 EB 03 00 00 EA 08 10 1B E5 0C 20 9D E5 08 00 9D E5 11 01";
            string replace12 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04 00 00 1A 08 10 1B E5 0C 20 9D E5 08 00 9D E5 4F 00 00 EB 03 00 00 EA 08 10 1B E5 0C 20 9D E5 08 00 9D E5 11 01";

            string search13 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 01 10 00 E3 15 10 4B E5 00 10 00 E3 1C 10 0B E5 38 00 8D E5 1C 00 1B E5 10 10 1B E5 DC 10 81 E2 34 00 8D E5 01 00 A0 E1 3B 76 FF EB 34 10 9D E5 00 00";
            string replace13 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 01 10 00 E3 15 10 4B E5 00 10 00 E3 1C 10 0B E5 38 00 8D E5 1C 00 1B E5 10 10 1B E5 DC 10 81 E2 34 00 8D E5 01 00 A0 E1 3B 76 FF EB 34 10 9D E5 00 00";

            string search14 = "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 74 0A 9F ED D0 C1 9F E5 0C C0 9F E7 00 C0 9C E5 04 C0 0B E5 38 00 8D E5 34 10 8D E5 30 20 8D E5 2C 30 8D E5 0A 0A 8D ED 38 00 9D E5 24 00 80 E2 00 10 00 E3 0B F6 FD EB 00 00 90 E5 24 00 8D E5 00 00 00 E3 20 00 8D E5 24 00 9D E5 00 00 90 E5 01 00 00 E2";
            string replace14 = "00 00 A0 E3 1E FF 2F E1";

            string search15 = "00 48 2D E9 0D B0 A0 E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00 31 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 10 00 8D E5 01 00 A0 E1 0C 10 8D E5 02 10 A0 E1 16 20 00 E3 B7 2D FA EB 0C 00 9D E5 15 10 00 E3 18 20 00 E3 FF 20 02 E2 C6 FF FC EB 17 00 8D E2 B8 10 9F E5 01 10";
            string replace15 = "00 00 A0 E3 1E FF 2F E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00 31 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 10 00 8D E5 01 00 A0 E1 0C 10 8D E5 02 10 A0 E1 16 20 00 E3 B7 2D FA EB 0C 00 9D E5 15 10 00 E3 18 20 00 E3 FF 20 02 E2 C6 FF FC EB 17 00 8D E2 B8 10 9F E5 01 10";

            string search16 = "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 E0 22 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 34 00 0B E5 38 10 0B E5 00 00 A0 E3 3C 00 0B E5 38 10 1B E5 20 00 4B E2 2C 00 8D E5 9D 0C FE EB 2C 00 9D E5 7B FB FF EB 28 00 8D E5 FF FF FF EA 20 00 4B E2 E5 A3 FC EB 28 10 9D E5 40 10 0B E5 40 20";
            string replace16 = "00 00 A0 E3 1E FF 2F E1 88 D0 4D E2 E0 22 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 34 00 0B E5 38 10 0B E5 00 00 A0 E3 3C 00 0B E5 38 10 1B E5 20 00 4B E2 2C 00 8D E5 9D 0C FE EB 2C 00 9D E5 7B FB FF EB 28 00 8D E5 FF FF FF EA 20 00 4B E2 E5 A3 FC EB 28 10 9D E5 40 10 0B E5 40 20";





            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] { search12,search13,search14,search15,search16
            };
                var replacePatterns = new[] { replace12,replace13,replace14,replace15,replace16
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }


        private async void guna2ToggleSwitch24_CheckedChanged(object sender, EventArgs e)
        {
            string search1 = "30 48 2d e9 08 b0 8d e2 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5 10 10 8D E5 01 00 02 E2 0F 00 CD E5 08 30 8D E5 14 00 9D E5";
            string replace1 = "00 00 A0 E3 1E FF 2F E1 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5 10 10 8D E5 01 00 02 E2 0F 00 CD E5 08 30 8D E5 14 00 9D E5";

            string search2 = "30 48 2d e9 08 b0 8d e2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3 2F 00 00 8A FF FF FF EA 10 00 9D E5 EC 00 90 E5";
            string replace2 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3 2F 00 00 8A FF FF FF EA 10 00 9D E5 EC 00 90 E5";

            string search3 = "00 48 2d e9 0d b0 a0 e1 10 D0 4D E2 04 00 0B E5 08 10 8D E5 04 00 1B E5 0C 13 90 E5 00 20 00 E3 02 00 51 E1 04 00 8D E5 04 00 00 0A D0 8B 01 EB 04 10 9D E5 0C 13 91 E5 08 20 9D E5 17 8C 01 EB";
            string replace3 = "00 00 A0 E3 1E FF 2F E1 10 D0 4D E2 04 00 0B E5 08 10 8D E5 04 00 1B E5 0C 13 90 E5 00 20 00 E3 02 00 51 E1 04 00 8D E5 04 00 00 0A D0 8B 01 EB 04 10 9D E5 0C 13 91 E5 08 20 9D E5 17 8C 01 EB";

            string search4 = "34 FF 2F E1 09 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 0F 9F E5";
            string replace4 = "E5 30 FF 00 09 00 A0 E1 1E 89 FE EB 00 60 A0 E1 00 00 56 E3 BD 00 00 0A 54 0F 9F E5";

            string search5 = "00 48 2D E9 0D B0 A0 E1 10 D0 4D E2 04 00 0B E5 08 10 8D E5 04 00 1B E5 0C 13 90 E5 00 20 00 E3 02 00 51 E1 04 00 8D E5 04 00 00 0A D0 8B 01 EB";
            string replace5 = "00 00 A0 E3 1E FF 2F E1 10 D0 4D E2 04 00 0B E5 08 10 8D E5 04 00 1B E5 0C 13 90 E5 00 20 00 E3 02 00 51 E1 04 00 8D E5 04 00 00 0A D0 8B 01 EB";

            string search6 = "30 48 2d e9 08 b0 8d e2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3";
            string replace6 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3";




            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] { search1, search2, search3, search4, search5 ,search6
            };
                var replacePatterns = new[] { replace1, replace2, replace3, replace4, replace5,replace6
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {

                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }

        private async void guna2ToggleSwitch23_CheckedChanged(object sender, EventArgs e)
        {
            string search1 = "30 48 2D E9 08 B0 8D E2 41 DD 4D E2 74 31 9F E5 03 30 9F E7 00 30 93 E5 0C 30 0B E5 2C 00 8D E5 28 10 8D E5 24 20 8D E5 2C 00 9D E5 3C 10 8D E2 18 00 8D E5 01 00 A0 E1";
            string replace1 = "1E FF 2F E1 08 B0 8D E2 41 DD 4D E2 74 31 9F E5 03 30 9F E7 00 30 93 E5 0C 30 0B E5 2C 00 8D E5 28 10 8D E5 24 20 8D E5 2C 00 9D E5 3C 10 8D E2 18 00 8D E5 01 00 A0 E1";

            string search2 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2";
            string replace2 = "00 00 A1 E3 1E FF 2F E1";

            string search3 = "00 48 2D E9 0D B0 A0 E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3";
            string replace3 = "1E FF 2F E1 0D B0 A0 E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3";

            string search4 = "7F 45 4C 46 01 01 01 00 00 00 00 00 00 00 00 00 03 00 28 00 01 00 00 00 00 00 00 00 34 00 00 00 1C 56 E0 07 00 02 00 05 34 00 20 00 0A 00 28 00 1E 00 1D 00 06 00 00 00";
            string replace4 = "1E FF 2F E1 01 01 01 00 00 00 00 00 00 00 00 00 03 00 28 00 01 00 00 00 00 00 00 00 34 00 00 00 1C 56 E0 07 00 02 00 05 34 00 20 00 0A 00 28 00 1E 00 1D 00 06 00 00 00";





            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] { search1, search2, search3, search4,
            };
                var replacePatterns = new[] { replace1, replace2, replace3, replace4,
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {

                }
            }
        }

        private void usn2_Click(object sender, EventArgs e)
        {

        }

        private async void guna2ToggleSwitch28_CheckedChanged(object sender, EventArgs e)
        {
            string hex = "A0 E3 6D 00 00 EB 00 0A B7 EE"; string replace = "FF 02 44 E3 00 0A B7 EE 10 0A";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }
            Console.Beep(800, 400);
        }

        private async void guna2ToggleSwitch27_CheckedChanged(object sender, EventArgs e)
        {
            string search1 = "C3 00 00 00 AB AB AB AB AB AB AB AB";
            string replace1 = "00 00 00 3E 0A D7 23 3D D2 A5 F9 BC";

            string search2 = "00 00 80 3F 00 00 80 3F 00 00 80 3F";
            string replace2 = "00 00 00 3E 0A D7 23 3D D2 A5 F9 BC";

            string search3 = "63 71 B0 BD 90 98 74 BB 00 00 80 B3";
            string replace3 = "CD DC 79 44 90 98 74 BB 00 00 80 B3";

            string search4 = "00 00 00 00 00 98 74 BB 00 00 80 B3";
            string replace4 = "CD DC 79 44 90 98 74 BB 00 00 80 B3";

            string search5 = "7B F9 6C BD 58 34 09 BB B0 60 BE BA";
            string replace5 = "CD DC 79 44 58 34 09 BB B0 60 BE BA";

            string search6 = "54 1B 87 BD 90 C6 D7 BA 80 54 99 B9";
            string replace6 = "CD DC 79 44 90 C6 D7 BA 80 54 99 B9";



            string search7 = "71 02 87 BD 90 FD D7 BA 40 18 98 39";
            string replace7 = "CD DC 79 44 90 FD D7 BA 40 18 98 39";

            string search8 = "CC F8 6C BD 40 D2 CE B9 58 64 BE 3A";
            string replace8 = "CD DC 79 44 40 D2 CE B9 58 64 BE 3A";


            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] { search1, search2, search3, search4, search5 ,search6, search7 ,search8
            };
                var replacePatterns = new[] { replace1, replace2, replace3, replace4, replace5,replace6,replace7,replace8
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }

        private async void guna2ToggleSwitch26_CheckedChanged(object sender, EventArgs e)
        {
            string hex = "E8 00 00 00 00 F0 4D 2D E9 18 B0 8D E2 04 8B 2D ED 00 40 A0 E1 60 03 9F E5 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 50 03 9F E5 00 00 9F E7 00 00 90 E5"; string replace = "E8 00 00 00 00 F0 00 A0 E3 1E B0 2F E1 04 8B 2D ED 00 40 A0 E1 60 03 9F E5 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 50 03 9F E5 00 00 9F E7 00 00 90 E5";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }
            Console.Beep(800, 400);

        }
        private void guna2CircleButton11_Click(object sender, EventArgs e)
        {
            // TODO: Thêm chức năng bạn muốn khi nhấn nút ở đây
        }

        private async void guna2ToggleSwitch25_CheckedChanged(object sender, EventArgs e)
        {
            string hex = "DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE"; string replace = "DB 0F a9 40 10 2A 00 EE 00 10 80 E5 10 3a 01";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }
            Console.Beep(800, 400);

        }

        private async void guna2ToggleSwitch21_CheckedChanged_3(object sender, EventArgs e)
        {
            string hex = "01 3F 01 01 01 01"; string replace = "01 3A";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }
            Console.Beep(800, 400);

        }

        private async void guna2ToggleSwitch20_CheckedChanged_2(object sender, EventArgs e)
        {
            string hex = "00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 80 7F 00 00 80 7F 00 00 80 7F 00 00 80 FF"; string replace = "00 00 00 00 00 80 40 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 80 7F 00 00 80 7F 00 00 80 7F 00 00 80 FF";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }
            Console.Beep(800, 400);

        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void thongtinpvn_Paint(object sender, PaintEventArgs e)
        {

        }

        private void keyup_Click(object sender, EventArgs e)
        {


        }

        private void keytele_Click(object sender, EventArgs e)
        {


        }

        private void guna2ToggleSwitch21_CheckedChanged_4(object sender, EventArgs e)
        {

        }

        private void guna2ToggleSwitch20_CheckedChanged_3(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton12_Click(object sender, EventArgs e)
        {

        }

        private async void guna2ToggleSwitch30_CheckedChanged(object sender, EventArgs e)
        {
            string search1 = "C0 3F 00 00 00 3F 00 00 80 3F 00 00 00 40";
            string replace1 = "C0 0F 00 00 00 3F 00 00 80 3F 00 00 00 40 CD CC CC 3D 01 00 00 00 CD CC CC 3D 01 00 00 00 E0";

            string search2 = "10 0A 18 EE 04 8B BD EC F0 88 BD E8 00 00 55 E3 01 00 00 1A";
            string replace2 = "FF 0E 43 E3 04 8B BD EC F0 88 BD E8 00 00 55 E3 01 00 00 1A";

            string search3 = "DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30 EE 2C 10 80 E5 50 00 C0 F2 04 10 80 E2 00 20 A0 E3 30 20 80 E5 34 20 80 E5 01 1A 22 EE 3C 20 80 E5 8F 0A 41 F4 18 10 80 E2 03 0A 80 EE 03 1A 81 EE 8F 0A 41 F4 0A 0A 80 ED";
            string replace3 = "DB 0F A9 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30 EE 2C 10 80 E5 50 00 C0 F2 04 10 80 E2 00 20 A0 E3 30 20 80 E5 34 20 80 E5 01 1A 22 EE 3C 20 80 E5 8F 0A 41 F4 18 10 80 E2 03 0A 80 EE 03 1A 81 EE 8F 0A 41 F4 0A 0A 80 ED";


            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] { search1, search2, search3,
            };
                var replacePatterns = new[] { replace1, replace2, replace3,
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }

        private async void guna2ToggleSwitch29_CheckedChanged(object sender, EventArgs e)
        {
            string hex = "c0 3f 0a d7 a3 3b 0a d7 a3 3b 8f c2 75 3d ae 47 e1 3d 9a 99 19 3e cd cc 4c 3e a4 70 fd 3e"; string replace = "80 4f 0a d7 a3 3b 0a d7 a3 3b 8f c2 75 3d ae 47 e1 3d 9a 99 19 3e cd cc 4c 3e a4 70 fd 3e";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }
            Console.Beep(800, 400);

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton5_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox1_Click_4(object sender, EventArgs e)
        {
        }

        private void guna2Button17_Click(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {
        }

        private void guna2Panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomCheckBox21_Click(object sender, EventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {
        }

        private void label23_Click_1(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox29_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {

        }


        private async void guna2CustomCheckBox9_Click_4(object sender, EventArgs e)
        {
            string search1 = "00 A0 E3 9F 7E D7 EB 3C 51 87 E5 00 70 94 E5 CC 50 96 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 98 7E D7 EB";
            string replace1 = "00 A0 E3 9F 7E D7 EB 3C 51 87 05";





            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);


                int i2 = 22000000;


                var searchPatterns = new[] { search1,
         };
                var replacePatterns = new[] { replace1,
   };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {

                    Console.Beep(1000, 500);

                }
                else
                {


                }

            }
        }


        private void menuaimbot_Paint(object sender, PaintEventArgs e)
        {
            // TODO: Add your code here
        }



        private async void guna2CustomCheckBox11_Click_3(object sender, EventArgs e)
        {
            string search1 = "ED 00 00 EB 24 00 9D E5 10 10 1B E5 0C 00 8D E5 01 00 A0 E1 60 00 00 EB 20 10 1B E5 08 00 8D E5 01 00 A0 E1 61 00 00 EB 0C 10 9D E5 04 00 8D E5 01 00 A0 E1 08 10 9D E5 04 20 9D E5 38 00 00 EB FF FF FF EA 10 00 1B E5 04 00 80 E2 10 00 0B E5 18 10 4B E2 18 00 9D E5 16 01 00 EB";
            string replace1 = "00 00 A0 E3 1E FF 2F E1 10 10 1B E5 0C 00 8D E5 01 00 A0 E1 60 00 00 EB 20 10 1B E5 08 00 8D E5 01 00 A0 E1 61 00 00 EB 0C 10 9D E5 04 00 8D E5 01 00 A0 E1 08 10 9D E5 04 20 9D E5 38 00 00 EB FF FF FF EA 10 00 1B E5 04 00 80 E2 10 00 0B E5 18 10 4B E2 18 00 9D E5 16 01 00 EB";

            string search2 = "88 D0 4D E2 74 0A 9F ED D0 C1 9F E5 0C C0 9F E7 00 C0 9C E5 04 C0 0B E5 38 00 8D E5 34 10 8D E5 30 20 8D E5 2C 30 8D E5 0A 0A 8D ED 38 00 9D E5 24 00 80 E2 00 10 00 E3 0B F6 FD EB 00 00 90 E5 24 00 8D E5 00 00 00 E3 20 00 8D E5 24 00 9D E5 00 00 90 E5 01 00 00 E2 00 00 50 E3";
            string replace2 = "00 00 A0 E3 1E FF 2F E1 D0 C1 9F E5 0C C0 9F E7 00 00 A0 E3 1E FF 2F E1 38 00 8D E5 34 10 8D E5 00 00 A0 E3 1E FF 2F E1 0A 0A 8D ED 38 00 9D E5 24 00 80 E2 00 10 00 E3 0B F6 FD EB 00 00 90 E5 24 00 8D E5 00 00 00 E3 20 00 8D E5 24 00 9D E5 00 00 90 E5 01 00 00 E2 00 00 50 E3";

            string search3 = "30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 00 50 A0 E3 B2 2F BF E6 31 1F BF E6 0C 40 9B E5 00 50 8C E5 B2 21 CD E1 02 20 A0 E3 B0 21 CD E1 10 20 A0 E3 1C 50 8D E5 18 50 8D E5 14 10 8D E5 10 10 8D E2 08 E0 9B E5 00 E0 8D E5 10 10";
            string replace3 = "00 00 A0 E3 1E FF 2F E1 20 D0 4D E2 10 C0 9B E5 00 50 A0 E3 B2 2F BF E6 31 1F BF E6 0C 40 9B E5 00 50 8C E5 B2 21 CD E1 02 20 A0 E3 B0 21 CD E1 10 20 A0 E3 1C 50 8D E5 18 50 8D E5 14 10 8D E5 10 10 8D E2 08 E0 9B E5 00 E0 8D E5 10 10 8D E9 BD FF FF EB 08 D0 4B E2 30 88 BD E8";

            string search4 = "30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5 10 10 8D E5 01 00 02 E2 0F 00 CD E5 08 30 8D E5 14 00 9D E5 10 10 9D E5 01 00 51 E3 04 00 8D E5 05 00 00 1A 49 DD FF EB 0C 10 9B E5 0F 20 DD E5 01 20 02 E2 A6 E2 FF EB 02 00 00 EA 0C 10 9B E5 10 00 4B E2 BC 62 03 EB 0F 00 DD E5 01 00 10 E3 09";
            string replace4 = "00 00 A0 E3 1E FF 2F E1 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 00 00 A0 E3 1E FF 2F E1 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5 10 10 8D E5 01 00 02 E2 0F 00 CD E5 08 30 8D E5 14 00 9D E5 10 10 9D E5 01 00 51 E3 04 00 8D E5 05 00 00 1A 49 DD FF EB 0C 10 9B E5 0F 20 DD E5 01 20 02 E2 A6 E2 FF EB 02 00 00 EA 0C 10 9B E5 10 00 4B E2 BC 62 03 EB 0F 00 DD E5 01 00 10 E3 09";

            string search5 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 00 10 00 E3 15 10 4B E5 24 00 8D E5 10 00 1B E5 0C 00 90 E5 B4 00 D0 E1 14 10 1B E5 2C 10 91 E5 01 10 D1 E5 01 00 50 E1 00 00 00 0A EF 00 00 EA 00 00 00 E3 16 00 4B E5 14 00 1B E5 2C 00 90 E5 01 00 D0 E5 01 00 80 E2 17 00 4B E5 16 00 5B E5 17 10 5B E5 01 00 50 E1 DD 00 00 AA 14 00 1B E5 2C";
            string replace5 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 00 10 00 E3 15 10 4B E5 24 00 8D E5 10 00 1B E5 0C 00 90 E5 B4 00 D0 E1 14 10 1B E5 2C 10 91 E5 01 10 D1 E5 01 00 50 E1 00 00 00 0A EF 00 00 EA 00 00 00 E3 16 00 4B E5 14 00 1B E5 2C 00 90 E5 01 00 D0 E5 01 00 80 E2 17 00 4B E5 16 00 5B E5 17 10 5B E5 01 00 50 E1 DD 00 00 AA 14 00 1B E5 2C";

            string search6 = "30 48 2D E9 08 B0 8D E2 78 D0 4D E2 01 DA 4D E2 C0 13 9F E5 01 10 8F E0 BC 23 9F E5 02 20 9F E7 00 20 92 E5 0C 20 0B E5 5C 00 8D E5 5C 00 9D E5 6C 20 8D E2 30 00 8D E5 02 00 A0 E1 6F 15 00 EB 30 10 9D E5 1C 20 81 E2 2C 00 8D E5 02 00 A0 E1 E3 00 00 EB 01 00 10 E3 02 00 00 0A 01 00 00 E3 58 00 8D E5 CE 00 00 EA 02 EB 4B E2";
            string replace6 = "00 00 A0 E3 1E FF 2F E1 78 D0 4D E2 01 DA 4D E2 C0 13 9F E5 01 10 8F E0 BC 23 9F E5 02 20 9F E7 00 20 92 E5 0C 20 0B E5 5C 00 8D E5 5C 00 9D E5 6C 20 8D E2 30 00 8D E5 02 00 A0 E1 6F 15 00 EB 30 10 9D E5 1C 20 81 E2 2C 00 8D E5 02 00 A0 E1 E3 00 00 EB 01 00 10 E3 02 00 00 0A 01 00 00 E3 58 00 8D E5 CE 00 00 EA 02 EB 4B E2";




            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] { search1, search2, search3, search4, search5 ,search6
            };
                var replacePatterns = new[] { replace1, replace2, replace3, replace4, replace5,replace6
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {

                }
            }
        }

        private async void guna2CustomCheckBox8_Click_2(object sender, EventArgs e)
        {
            string search7 = "30 48 2D E9 08 B0 8D E2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3 2F 00 00 8A FF FF FF EA 10 00 9D E5 EC 00 90 E5 04 00 90 E5 00 00 50 E3 00 00 00 1A 29 00 00 EA 93 D0 FF EB 10 10 9D E5 B2 10 D1 E1";
            string replace7 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3 2F 00 00 8A FF FF FF EA 10 00 9D E5 EC 00 90 E5 04 00 90 E5 00 00 50 E3 00 00 00 1A 29 00 00 EA 93 D0 FF EB 10 10 9D E5 B2 10 D1 E1";

            string search8 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 8C 31 9F E5 03 30 9F E7 00 30 93 E5 0C 30 0B E5 10 10 0B E5 28 00 0B E5 11 20 4B E5 28 00 1B E5 18 00 8D E5 5C 00 00 EB 18 00 0B E5 10 00 4B E2 18 10 4B E2 85 00 00 EB 2C 00 8D E5 18 00 9D E5 D0 A9 FC EB 28 00 8D E5 18 00 9D E5 F9 33 FD EB";
            string replace8 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 8C 31 9F E5 03 30 9F E7 00 30 93 E5 0C 30 0B E5 10 10 0B E5 28 00 0B E5 11 20 4B E5 28 00 1B E5 18 00 8D E5 5C 00 00 EB 18 00 0B E5 10 00 4B E2 18 10 4B E2 85 00 00 EB 2C 00 8D E5 18 00 9D E5 D0 A9 FC EB 28 00 8D E5 18 00 9D E5 F9 33 FD EB";

            string search9 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 0C C0 9B E5 08 E0 9B E5 10 40 9B E5 10 00 0B E5 14 10 0B E5 18 20 0B E5 1C 30 0B E5 24 C0 0B E5 28 E0 0B E5 10 00 1B E5 00 10 00 E3 2C 10 8D E5 28 10 8D E5 14 20 1B E5 01 00 52 E1 08 00 8D E5 09 00 00 0A 1C 00 1B E5 00 10 00 E3 01 00 50 E1";
            string replace9 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 0C C0 9B E5 08 E0 9B E5 10 40 9B E5 10 00 0B E5 14 10 0B E5 18 20 0B E5 1C 30 0B E5 24 C0 0B E5 28 E0 0B E5 10 00 1B E5 00 10 00 E3 2C 10 8D E5 28 10 8D E5 14 20 1B E5 01 00 52 E1 08 00 8D E5 09 00 00 0A 1C 00 1B E5 00 10 00 E3 01 00 50 E1 05 00 00 0A 18 00 1B E5 00 00 50 E3 02 00 00 BA 10 00 9B E5 00 00 50 E3 05 00 00";

            string search10 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 30 00 0B E5 34 10 0B E5 30 00 1B E5 28 00 8D E5 CC 04 00 EB 00 00 50 E3 02 00 00 0A 00 00 E0 E3 2C 00 0B E5 A7 00 00 EA 34 00 1B E5 02 00 D0 E5 01 00 10 E3 05 00 00 0A 42 2F 03 EB 34 10 1B E5 10 10";
            string replace10 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 30 00 0B E5 34 10 0B E5 30 00 1B E5 28 00 8D E5 CC 04 00 EB 00 00 50 E3 02 00 00 0A 00 00 E0 E3 2C 00 0B E5 A7 00 00 EA 34 00 1B E5 02 00 D0 E5 01 00 10 E3 05 00 00 0A 42 2F 03 EB 34 10 1B E5 10 10";

            string search11 = "00 48 2D E9 0D B0 A0 E1 E8 D0 4D E2 08 C0 9B E5 CC E6 9F E5 0E E0 9F E7 00 E0 9E E5 04 E0 0B E5 64 00 0B E5 68 10 0B E5 6C 20 0B E5 3C 30 0B E5 3C 00 1B E5 08 10 9B E5 01 00 50 E1 02 00 00 3A 00 00 00 E3 60 00 0B E5 96 01 00 EA FF FF FF EA 3C 00 1B E5 08 10 9B E5 01 00 50 E1 8F 01";
            string replace11 = "00 00 A0 E3 1E FF 2F E1 E8 D0 4D E2 08 C0 9B E5 CC E6 9F E5 0E E0 9F E7 00 E0 9E E5 04 E0 0B E5 64 00 0B E5 68 10 0B E5 6C 20 0B E5 3C 30 0B E5 3C 00 1B E5 08 10 9B E5 01 00 50 E1 02 00 00 3A 00 00 00 E3 60 00 0B E5 96 01 00 EA FF FF FF EA 3C 00 1B E5 08 10 9B E5 01 00 50 E1 8F 01";



            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] {search7,search8,search9,search10,search11
            };
                var replacePatterns = new[] { replace7,replace8,replace9,replace10,replace11
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }

        private async void guna2CustomCheckBox10_Click_2(object sender, EventArgs e)
        {

            string search12 = "00 48 2D E9 0D B0 A0 E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04 00 00 1A 08 10 1B E5 0C 20 9D E5 08 00 9D E5 4F 00 00 EB 03 00 00 EA 08 10 1B E5 0C 20 9D E5 08 00 9D E5 11 01";
            string replace12 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04 00 00 1A 08 10 1B E5 0C 20 9D E5 08 00 9D E5 4F 00 00 EB 03 00 00 EA 08 10 1B E5 0C 20 9D E5 08 00 9D E5 11 01";

            string search13 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 01 10 00 E3 15 10 4B E5 00 10 00 E3 1C 10 0B E5 38 00 8D E5 1C 00 1B E5 10 10 1B E5 DC 10 81 E2 34 00 8D E5 01 00 A0 E1 3B 76 FF EB 34 10 9D E5 00 00";
            string replace13 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 01 10 00 E3 15 10 4B E5 00 10 00 E3 1C 10 0B E5 38 00 8D E5 1C 00 1B E5 10 10 1B E5 DC 10 81 E2 34 00 8D E5 01 00 A0 E1 3B 76 FF EB 34 10 9D E5 00 00";

            string search14 = "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 74 0A 9F ED D0 C1 9F E5 0C C0 9F E7 00 C0 9C E5 04 C0 0B E5 38 00 8D E5 34 10 8D E5 30 20 8D E5 2C 30 8D E5 0A 0A 8D ED 38 00 9D E5 24 00 80 E2 00 10 00 E3 0B F6 FD EB 00 00 90 E5 24 00 8D E5 00 00 00 E3 20 00 8D E5 24 00 9D E5 00 00 90 E5 01 00 00 E2";
            string replace14 = "00 00 A0 E3 1E FF 2F E1";

            string search15 = "00 48 2D E9 0D B0 A0 E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00 31 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 10 00 8D E5 01 00 A0 E1 0C 10 8D E5 02 10 A0 E1 16 20 00 E3 B7 2D FA EB 0C 00 9D E5 15 10 00 E3 18 20 00 E3 FF 20 02 E2 C6 FF FC EB 17 00 8D E2 B8 10 9F E5 01 10";
            string replace15 = "00 00 A0 E3 1E FF 2F E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00 31 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 10 00 8D E5 01 00 A0 E1 0C 10 8D E5 02 10 A0 E1 16 20 00 E3 B7 2D FA EB 0C 00 9D E5 15 10 00 E3 18 20 00 E3 FF 20 02 E2 C6 FF FC EB 17 00 8D E2 B8 10 9F E5 01 10";

            string search16 = "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 E0 22 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 34 00 0B E5 38 10 0B E5 00 00 A0 E3 3C 00 0B E5 38 10 1B E5 20 00 4B E2 2C 00 8D E5 9D 0C FE EB 2C 00 9D E5 7B FB FF EB 28 00 8D E5 FF FF FF EA 20 00 4B E2 E5 A3 FC EB 28 10 9D E5 40 10 0B E5 40 20";
            string replace16 = "00 00 A0 E3 1E FF 2F E1 88 D0 4D E2 E0 22 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 34 00 0B E5 38 10 0B E5 00 00 A0 E3 3C 00 0B E5 38 10 1B E5 20 00 4B E2 2C 00 8D E5 9D 0C FE EB 2C 00 9D E5 7B FB FF EB 28 00 8D E5 FF FF FF EA 20 00 4B E2 E5 A3 FC EB 28 10 9D E5 40 10 0B E5 40 20";





            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] { search12,search13,search14,search15,search16
            };
                var replacePatterns = new[] { replace12,replace13,replace14,replace15,replace16
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {

                }
            }
        }

        private async void guna2CustomCheckBox4_Click_2(object sender, EventArgs e)
        {
            string hex = "A0 E3 6D 00 00 EB 00 0A B7 EE"; string replace = "FF 02 44 E3 00 0A B7 EE 10 0A";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }
            Console.Beep(800, 400);

        }

        private async void guna2CustomCheckBox7_Click_2(object sender, EventArgs e)
        {
            string hex = "DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE"; string replace = "DB 0F a9 40 10 2A 00 EE 00 10 80 E5 10 3a 01";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }
            Console.Beep(800, 400);

        }

        private async void guna2CustomCheckBox3_Click_3(object sender, EventArgs e)
        {
            string hex = "E8 00 00 00 00 F0 4D 2D E9 18 B0 8D E2 04 8B 2D ED 00 40 A0 E1 60 03 9F E5 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 50 03 9F E5 00 00 9F E7 00 00 90 E5"; string replace = "E8 00 00 00 00 F0 00 A0 E3 1E B0 2F E1 04 8B 2D ED 00 40 A0 E1 60 03 9F E5 00 00 8F E0 00 00 D0 E5 00 00 50 E3 06 00 00 1A 50 03 9F E5 00 00 9F E7 00 00 90 E5";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }
            Console.Beep(800, 400);


        }

        private async void guna2CustomCheckBox5_Click_1(object sender, EventArgs e)
        {
            string hex = "c0 3f 0a d7 a3 3b 0a d7 a3 3b 8f c2 75 3d ae 47 e1 3d 9a 99 19 3e cd cc 4c 3e a4 70 fd 3e"; string replace = "80 4f 0a d7 a3 3b 0a d7 a3 3b 8f c2 75 3d ae 47 e1 3d 9a 99 19 3e cd cc 4c 3e a4 70 fd 3e";
            string[] pocessname = { "HD-Player" };
            bool sucess = Gay.SetProcess(pocessname);

            if (!sucess)
            {
                return;
            }
            IEnumerable<long> result = await Gay.AoBScan(hex);
            if (result.Any())
            {
                await Task.Run(() =>
                {
                    foreach (long num in result)
                    {

                        Gay.AobReplace(num, replace);
                    }
                });
            }
            Console.Beep(800, 400);

        }

        private async void guna2CustomCheckBox1_Click_5(object sender, EventArgs e)
        {
            string search1 = "C0 3F 00 00 00 3F 00 00 80 3F 00 00 00 40";
            string replace1 = "C0 0F 00 00 00 3F 00 00 80 3F 00 00 00 40 CD CC CC 3D 01 00 00 00 CD CC CC 3D 01 00 00 00 E0";

            string search2 = "10 0A 18 EE 04 8B BD EC F0 88 BD E8 00 00 55 E3 01 00 00 1A";
            string replace2 = "FF 0E 43 E3 04 8B BD EC F0 88 BD E8 00 00 55 E3 01 00 00 1A";

            string search3 = "DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30 EE 2C 10 80 E5 50 00 C0 F2 04 10 80 E2 00 20 A0 E3 30 20 80 E5 34 20 80 E5 01 1A 22 EE 3C 20 80 E5 8F 0A 41 F4 18 10 80 E2 03 0A 80 EE 03 1A 81 EE 8F 0A 41 F4 0A 0A 80 ED";
            string replace3 = "DB 0F A9 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30 EE 2C 10 80 E5 50 00 C0 F2 04 10 80 E2 00 20 A0 E3 30 20 80 E5 34 20 80 E5 01 1A 22 EE 3C 20 80 E5 8F 0A 41 F4 18 10 80 E2 03 0A 80 EE 03 1A 81 EE 8F 0A 41 F4 0A 0A 80 ED";


            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] { search1, search2, search3,
            };
                var replacePatterns = new[] { replace1, replace2, replace3,
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }






        private async void guna2CustomCheckBox14_Click_2(object sender, EventArgs e)
        {
            string search7 = "30 48 2D E9 08 B0 8D E2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5";
            string replace7 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5";

            string search8 = "30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5";
            string replace8 = "00 00 A0 E3 1E FF 2F E1 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5";

            string search9 = "00 48 2D E9 0D B0 A0 E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5";
            string replace9 = "42 F9 20 E5 0E EE 0F E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5";

            string search10 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C";
            string replace10 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C";

            string search11 = "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 74 0A 9F ED";
            string replace11 = "00 00 A0 E3 1E FF 2F E1 88 D0 4D E2 74 0A 9F ED";

            string search14 = "30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 00 50 A0 E3 B2 2F BF E6";
            string replace14 = "00 00 A0 E3 1E FF 2F E1 20 D0 4D E2 10 C0 9B E5 00 50 A0 E3 B2 2F BF E6";

            string search15 = "88 D0 4D E2 74 0A 9F ED D0 C1 9F E5 0C C0 9F E7 00 C0 9C E5 04 C0 0B E5";
            string replace15 = "00 00 A0 E3 1E FF 2F E1 D0 C1 9F E5 0C C0 9F E7 00 00 A0 E3 1E FF 2F E1";

            string search16 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5";
            string replace16 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5";

            string search17 = "00 48 2D E9 0D B0 A0 E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04 00 00 1A 08 10 1B E5 0C 20 9D E5 08 00 9D E5";
            string replace17 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04 00 00 1A 08 10 1B E5 0C 20 9D E5 08 00 9D E5";


            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] {search7,search8,search9,search10,search11,search14,search15,search16,search17
            };
                var replacePatterns = new[] { replace7,replace8,replace9,replace10,replace11,replace14,replace15,replace16,replace17
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }

        private async void guna2CustomCheckBox12_Click_1(object sender, EventArgs e)
        {
            string search7 = "30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 D0 50 9F E5";
            string replace7 = "00 00 A0 E3 1E FF 2F E1 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 00 00 A0 E3 1E FF 2F E1";

            string search8 = "08 1E 29 00 04 D0 4D E2 00 00 8D E5 00 00 9D E5 01 10 00 E3 25 10 C0 E5";
            string replace8 = "00 00 A0 E3 1E FF 2F E1 00 00 8D E5 00 00 9D E5 01 10 00 E3 25 10 C0 E5";

            string search9 = "30 48 2D E9 08 B0 8D E2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5";
            string replace9 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5";

            string search10 = "30 48 2D E9 08 B0 8D E2 78 D0 4D E2 01 DA 4D E2 C0 13 9F E5 01 10 8F E0";
            string replace10 = "00 00 A0 E3 1E FF 2F E1 78 D0 4D E2 01 DA 4D E2 C0 13 9F E5 01 10 8F E0";

            string search11 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 0C 00 0B E5 10 10 0B E5 14 20 0B E5";
            string replace11 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 0C 00 0B E5 10 10 0B E5 14 20 0B E5";

            string search12 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 0C C0 9B E5 08 E0 9B E5 10 40 9B E5";
            string replace12 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 0C C0 9B E5 08 E0 9B E5 10 40 9B E5";

            string search13 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 8C 31 9F E5 03 30 9F E7 00 30 93 E5";
            string replace13 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 8C 31 9F E5 03 30 9F E7 00 30 93 E5";


            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] {search7,search8,search9,search10,search11,search12,search13
            };
                var replacePatterns = new[] { replace7,replace8,replace9,replace10,replace11,replace12,replace13
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }

        private async void guna2CustomCheckBox28_Click(object sender, EventArgs e)
        {
            string search7 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 01 10 00 E3 15 10 4B E5 00 10 00 E3 1C 10 0B E5 38 00 8D E5 1C 00 1B E5 10 10 1B E5";
            string replace7 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 01 10 00 E3 15 10 4B E5 00 10 00 E3 1C 10 0B E5 38 00 8D E5 1C 00 1B E5 10 10 1B E5";

            string search8 = "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 E0 22 9F E5 02 20 9F E7 00 20 92 E5";
            string replace8 = "00 00 A0 E3 1E FF 2F E1 88 D0 4D E2 E0 22 9F E5 02 20 9F E7 00 20 92 E5";

            string search9 = "00 48 2D E9 0D B0 A0 E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00";
            string replace9 = "00 00 A0 E3 1E FF 2F E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00";

            string search10 = "08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5";
            string replace10 = "00 00 A0 E3 1E FF 2F E1 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5";

            string search11 = "ED 00 00 EB 24 00 9D E5 10 10 1B E5 0C 00 8D E5 01 00 A0 E1 60 00 00 EB";
            string replace11 = "00 00 A0 E3 1E FF 2F E1 10 10 1B E5 0C 00 8D E5 01 00 A0 E1 60 00 00 EB";

            string search12 = "28 00 1B E5 30 10 9F E5 01 10 9F E7 00 10 91 E5 04 20 1B E5 02 00 51 E1 00 00 8D E5 04 00 00 1A 00 00 9D E5 0B D0 A0 E1 00 88 BD E8 24 00 9D E5 99 29 08 FA 55 32 FD EB AC 1F 29 00 08 1E 29 00 04 D0 4D E2 00 00 8D E5";
            string replace12 = "00 00 A0 E3 1E FF 2F E1 01 10 9F E7 00 10 91 E5 04 20 1B E5 02 00 51 E1 00 00 8D E5 04 00 00 1A 00 00 9D E5 0B D0 A0 E1 00 88 BD E8 24 00 9D E5 99 29 08 FA 55 32 FD EB AC 1F 29 00 00 00 A0 E3 1E FF 2F E1 00 00 8D E5";

            string search13 = "30 20 8D E5 2C 30 8D E5 0A 0A 8D ED 38 00 9D E5 24 00 80 E2 00 10 00 E3 0B F6 FD EB";
            string replace13 = "00 00 A0 E3 1E FF 2F E1 0A 0A 8D ED 38 00 9D E5 24 00 80 E2 00 10 00 E3 0B F6 FD EB";





            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] {search7,search8,search9,search10,search11,search12,search13
            };
                var replacePatterns = new[] { replace7,replace8,replace9,replace10,replace11,replace12,replace13
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }

        private void spm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel6_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private async void guna2Button25_Click(object sender, EventArgs e)
        {
            string hex = "8D E5 10 17 02 E3 35 FF 2F E1 4C A0 9D E5 00 00 59 E3 01 00 00 1A 00 00 A0 E3 8A 58 D3 EB 08 06 D9 E5 00 00 50 E3 01 00 00 0A 08 A0 8A E3 30 00"; string replace = "8D E5 10 17 02 E3 3f f0 3f e3 4C A0 9D E5 00 00 59 E3 01 00 00 1A 00 00 A0 E3 8A 58 D3 EB 08 06 D9 E5 00 00 50 E3 01 00 00 0A 08 A0 8A E3 30 00";
            string search1 = "70 40 2D E9 02 8B 2D ED 44 51 9F E5 00 40 A0 E1 05 50 8F E0 00 00 D5 E5 00 00 50 E3 07 00 00 1A 30 01 9F E5 00 00 9F E7 B0 00 B5 EB 28 01 9F E5 00 00 9F E7 AD 00 B5 EB 01 00 A0 E3"; string replace1 = "00 00 A0 E3 1E FF 2F E1";
            string search2 = "8D E5 10 17 02 E3 35 FF 2F E1 4C A0 9D E5 00 00 59 E3 01 00 00 1A 00 00 A0 E3 8A 58 D3 EB 08 06 D9 E5 00 00 50 E3 01 00 00 0A 08 A0 8A E3 30 00"; string replace2 = "8D E5 10 17 02 E3 3f f0 3f e3 4C A0 9D E5 00 00 59 E3 01 00 00 1A 00 00 A0 E3 8A 58 D3 EB 08 06 D9 E5 00 00 50 E3 01 00 00 0A 08 A0 8A E3 30 00";
            string search3 = "30 48 2D E9 08 B0 8D E2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5"; string replace3 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5";
            string search4 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C"; string replace4 = "8D E5 10 17 02 E3 3f f0 3f e3 4C A0 9D E5 00 00 59 E3 01 00 00 1A 00 00 A0 E3 8A 58 D3 EB 08 06 D9 E5 00 00 50 E3 01 00 00 0A 08 A0 8A E3 30 00";



            string[] pocessname = { "HD-Player" };
            bool success = Gay.SetProcess(pocessname);
            if (!success)
            {
                return;
            }

            // Tạo mảng các cặp search-replace
            var patterns = new[]
            {
    new { Search = hex, Replace = replace },
    new { Search = search1, Replace = replace1 },
    new { Search = search2, Replace = replace2 },
        new { Search = search3, Replace = replace3 },
    new { Search = search4, Replace = replace4 }

};

            // Xử lý tất cả các patterns
            foreach (var pattern in patterns)
            {
                IEnumerable<long> results = await Gay.AoBScan(pattern.Search);
                if (results.Any())
                {
                    await Task.Run(() =>
                    {
                        foreach (long address in results)
                        {
                            Gay.AobReplace(address, pattern.Replace);
                        }
                    });
                }
            }

            Console.Beep(800, 400);

        }

        private async void guna2Button17_Click_1(object sender, EventArgs e)
        {
            string hex = "10 8D E5 00 A0 A0 E3 14 00 8D E5 09 00 A0 E1 18 10 8D E5 1C 10 8D E5 10 17 02 E3 35 FF 2F E1"; string replace = "10 8D E5 00 A0 A0 E3 14 00 8D E5 09 00 A0 E1 18 10 8D E5 1C 10 8D E5 10 17 02 E3 00";




            string[] pocessname = { "HD-Player" };
            bool success = Gay.SetProcess(pocessname);
            if (!success)
            {
                return;
            }

            // Tạo mảng các cặp search-replace
            var patterns = new[]
            {
    new { Search = hex, Replace = replace },

};

            // Xử lý tất cả các patterns
            foreach (var pattern in patterns)
            {
                IEnumerable<long> results = await Gay.AoBScan(pattern.Search);
                if (results.Any())
                {
                    await Task.Run(() =>
                    {
                        foreach (long address in results)
                        {
                            Gay.AobReplace(address, pattern.Replace);
                        }
                    });
                }
            }

            Console.Beep(800, 400);

        }


        private void guna2Button17_Click_2(object sender, EventArgs e)
        {

        }



        private void label12_Click_1(object sender, EventArgs e)
        {

        }
















        private void guna2Button49_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click_2(object sender, EventArgs e)
        {


        }


        private void guna2Button9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel5_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }


        private async void guna2CustomCheckBox43_Click(object sender, EventArgs e)
        {

            string search0 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C", replace0 = "00 00 A0 E3 1E FF 2F E1";
            string search1 = "00 48 2D E9 0D B0 A0 E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 04 00 1B E5 00 10 90 E5", replace1 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 04 00 1B E5 00 10 90 E5";
            string search2 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 04 00 0B E5 04 00 1B E5 1C 10 90 E5 00 00 51 E3 0C 00 0B E5 00 00 00 1A", replace2 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 04 00 0B E5 04 00 1B E5 1C 10 90 E5 00 00 51 E3 0C 00 0B E5 00 00 00 1A";
            string search3 = "30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 D0 50 9F E5", replace3 = "00 00 A0 E3 1E FF 2F E1 00 00 8D E5 00 00 9D E5 01 10 00 E3 25 10 C0 E5";
            string search4 = "08 1E 29 00 04 D0 4D E2 00 00 8D E5 00 00 9D E5 01 10 00 E3 25 10 C0 E5", replace4 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5";
            string search5 = "30 48 2D E9 08 B0 8D E2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5", replace5 = "00 00 A0 E3 1E FF 2F E1 78 D0 4D E2 01 DA 4D E2 C0 13 9F E5 01 10 8F E0";
            string search6 = "30 48 2D E9 08 B0 8D E2 78 D0 4D E2 01 DA 4D E2 C0 13 9F E5 01 10 8F E0", replace6 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 0C 00 0B E5 10 10 0B E5 14 20 0B E5";
            string search7 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 0C 00 0B E5 10 10 0B E5 14 20 0B E5", replace7 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 0C C0 9B E5 08 E0 9B E5 10 40 9B E5";
            string search8 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 0C C0 9B E5 08 E0 9B E5 10 40 9B E5", replace8 = "00 00 A0 E3 1E FF 2F E1 50 D0 4D E2 8C 31 9F E5 03 30 9F E7 00 30 93 E5";
            string search9 = "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 8C 31 9F E5 03 30 9F E7 00 30 93 E5", replace9 = "00 00 A0 E3 1E FF 2F E1 20 D0 4D E2 10 C0 9B E5 00 50 A0 E3 B2 2F BF E6";



            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] {search0 ,search1,search2,search3 ,search4 ,search5 ,search6,search7,search8,search9
            };
                var replacePatterns = new[] { replace0,replace1,replace2,replace3,replace4,replace5 ,replace6 ,replace7,replace8,replace9
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }

        }

        private async void guna2CustomCheckBox42_Click(object sender, EventArgs e)
        {

            string search0 = "88 D0 4D E2 74 0A 9F ED D0 C1 9F E5 0C C0 9F E7 00 C0 9C E5 04 C0 0B E5", replace0 = "00 00 A0 E3 1E FF 2F E1 01 10";
            string search1 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5", replace1 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 01 10 00 E3 15 10 4B E5 00 10 00 E3 1C 10 0B E5 38 00 8D E5 1C 00 1B E5 10 10 1B E5";
            string search2 = "00 48 2D E9 0D B0 A0 E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04 00 00 1A 08 10 1B E5 0C 20 9D E5 08 00 9D E5", replace2 = "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5";
            string search3 = "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C 00 1B E5 01 10 00 E3 15 10 4B E5 00 10 00 E3 1C 10 0B E5 38 00 8D E5 1C 00 1B E5 10 10 1B E5", replace3 = "00 00 A0 E3 1E FF 2F E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04 00 00 1A 08 10 1B E5 0C 20 9D E5 08 00 9D E5";
            string search4 = "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 E0 22 9F E5 02 20 9F E7 00 20 92 E5", replace4 = "00 00 A0 E3 1E FF 2F E1 D0 C1 9F E5 0C C0 9F E7 00 00 A0 E3 1E FF 2F E1";
            string search5 = "00 48 2D E9 0D B0 A0 E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00", replace5 = "00 00 A0 E3 1E FF 2F E1 88 D0 4D E2 E0 22 9F E5 02 20 9F E7 00 20 92 E5";
            string search6 = "08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5", replace6 = "00 00 A0 E3 1E FF 2F E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00";
            string search7 = "ED 00 00 EB 24 00 9D E5 10 10 1B E5 0C 00 8D E5 01 00 A0 E1 60 00 00 EB", replace7 = "00 00 A0 E3 1E FF 2F E1 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5";
            string search8 = "28 00 1B E5 30 10 9F E5 01 10 9F E7 00 10 91 E5 04 20 1B E5 02 00 51 E1 00 00 8D E5 04 00 00 1A 00 00 9D E5 0B D0 A0 E1 00 88 BD E8 24 00 9D E5 99 29 08 FA 55 32 FD EB AC 1F 29 00 08 1E 29 00 04 D0 4D E2 00 00 8D E5", replace8 = "00 00 A0 E3 1E FF 2F E1 10 10 1B E5 0C 00 8D E5 01 00 A0 E1 60 00 00 EB";
            string search9 = "00 C0 9C E5 04 C0 0B E5 38 00 8D E5 34 10 8D E5 30 20 8D E5 2C 30 8D E5 0A 0A 8D ED", replace9 = "00 00 A0 E3 1E FF 2F E1 38 00 8D E5 34 10 8D E5 00 00 A0 E3 1E FF 2F E1 0A 0A 8D ED";
            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] {search0 ,search1,search2,search3 ,search4 ,search5 ,search6,search7,search8,search9
            };
                var replacePatterns = new[] { replace0,replace1,replace2,replace3,replace4,replace5 ,replace6 ,replace7,replace8,replace9
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }

        private async void guna2CustomCheckBox40_Click(object sender, EventArgs e)
        {



            string search0 = "00 A0 E3 9F 7E D7 EB 3C 51 87 E5 00 70 94 E5 CC 50 96 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 98 7E D7 EB 44 51 87 E5 00 70 94 E5 98 50 D6 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 91 7E D7 EB 48 51 C7 E5 00 40 94 E5 99 50 D6 E5 00 00 54 E3 01 00 00 1A 00 00 A0 E3 8A 7E D7 EB 03 00 A0 E3 00 10 A0 E3 49 51 C4 E5 D1 87 5E EB 18 D0 4B E2 F0 8B BD E8 00 00 A0 E3 0F E0 A0", replace0 = "00 A0 E3 9F 7E D7 EB 3C 50 80";
            string search1 = "05 00 00 0A 00 70 94 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 9F 7E D7 EB 3C 51 87 E5 00 70 94 E5", replace1 = "00 A0 E3 9F 7E D7";
            string search2 = "00 A0 E3 9F 7E D7 EB 3C 51 87 E5 00 70 94 E5 CC 50 96 E5 00 00 57 E3 01 00 00 1A 00 00 A0 E3 98 7E D7 EB", replace2 = "00 A0 E3 9F 7E D7 EB 3C 50 87 05";

            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {

                Console.Beep(240, 300);
            }
            else
            {
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                int i2 = 22000000;

                var searchPatterns = new[] {search0 ,search1,search2
            };
                var replacePatterns = new[] { replace0,replace1,replace2
            };
                foreach (var search in searchPatterns)
                {
                    IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                    if (wl.Any())
                    {
                        foreach (var address in wl)
                        {
                            ++i2;
                            m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                        }
                        k = true;
                    }
                }

                if (k)
                {


                    Console.Beep(1000, 500);
                }
                else
                {


                }
            }
        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {
        }

        private void label37_Click_1(object sender, EventArgs e)
        {
        }

        private void label47_Click(object sender, EventArgs e)
        {
        }

        private void label59_Click_1(object sender, EventArgs e)
        {
        }

        private void guna2Button48_Click(object sender, EventArgs e)
        {

        }
        static void DeleteFirewallRule(string programPath)
        {
            ExecuteCommand("netsh advfirewall firewall delete rule name=all program=\"" + programPath + "\"");
        }
        static void ExecuteCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c " + command,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();
        }



        private void guna2Button23_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button47_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label96_Click(object sender, EventArgs e)
        {

        }




        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click_1(object sender, EventArgs e)
        {

        }
        private void label53_Click(object sender, EventArgs e)
        {
        }

        private void guna2Panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label79_Click(object sender, EventArgs e)
        {

        }

        private void label78_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button41_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void label62_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Button28_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel17_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);




        private void guna2CustomCheckBox13_Click_2(object sender, EventArgs e)
        {
            Config.Aimkill = guna2CustomCheckBox13.Checked;
        }

        private void label108_Click(object sender, EventArgs e)
        {

        }


        private void guna2CustomCheckBox26_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox26.Checked)
            {
                label107.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label107.ForeColor = Color.DimGray;
            }
            Config.enableAimBot2 = guna2CustomCheckBox26.Checked;
        }

        private void guna2CustomCheckBox24_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox24.Checked)
            {
                label106.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label106.ForeColor = Color.DimGray;
            }
            Config.NoRecoil = guna2CustomCheckBox24.Checked;
        }

        private void guna2CustomCheckBox15_Click_2(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox15.Checked)
            {
                label105.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label105.ForeColor = Color.DimGray;
            }
            Config.IgnoreKnocked = guna2CustomCheckBox15.Checked;
        }

        private void guna2CustomCheckBox1_Click_6(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox1.Checked)
            {
                label13.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label13.ForeColor = Color.DimGray;
            }
            Config.telekill = guna2CustomCheckBox1.Checked;
        }

        private void guna2CustomCheckBox4_Click_3(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox4.Checked)
            {
                label14.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label14.ForeColor = Color.DimGray;
            }
            Config.proxtelekill = guna2CustomCheckBox4.Checked;
        }

        private void guna2Button1_Click_3(object sender, EventArgs e)
        {
            guna2Button1.Text = "...";
            waitPressKeyForBind = true;
            waitPressKeyForSpd = false;
            waitPressKeyForcLft = false;
            waitPressKeyForSpeed = false;
            waitPressKeyForUp = false;
            waitPressKeyForFly = false;

            waitPressKeyForSpx = false;
        }
        private bool isProcessing = false;
        private float progress = 0.0f;

        private CX memoryfast = new CX();
        private bool anticheat = false;
        private async Task
AntiCheat()
        {
            if (!anticheat)
            {
                List<string> searchList = new List<string>
{
                "30 48 2D E9 08 B0 8D E2 78 D0 4D E2 01 DA 4D E2 C0 13 9F E5 01 10 8F E0 BC 23 9F E5 02 20 9F E7 00 20 92 E5 0C 20 0B E5 5C 00 8D E5 5C 00 9D E5 6C 20 8D E2 30 00 8D E5 02 00",
    "00 48 2D E9 0D B0 A0 E1 18 D0 4D E2 04 00 0B E5 08 10 0B E5 0C 20 8D E5 04 00 1B E5 08 10 1B E5 08 00 8D E5 16 00 00 EB 08 10 1B E5 28 10 D1 E5 01 10 01 E2 01 00 51 E3 04",
    "30 48 2D E9 08 B0 8D E2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5 0F 00 DD E5 01 00 40 E2 01 00 50 E3 2F 00 00 8A FF FF FF EA 10 00",
    "00 48 2D E9 0D B0 A0 E1 48 D0 4D E2 1A 10 4B E2 04 21 9F E5 02 20 8F E0 00 31 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 10 00 8D E5 01 00 A0 E1 0C 10 8D E5 02 10 A0 E1 16",
    "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 08 23 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 30 00 0B E5 34 10 0B E5 30 00 1B E5 28 00 8D E5 CC 04 00 EB 00 00 50 E3 02 00 00 0A 00",
    "00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B0 35 9F E5 03 30 9F E7 00 30 93 E5 04 30 0B E5 0C 00 0B E5 10 10 0B E5 14 20 0B E5 0C",
    "30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5 14 00 8D E5 10 10 8D E5 01 00 02 E2 0F 00 CD E5 08 30",
    "ED 00 00 EB 24 00 9D E5 10 10 1B E5 0C 00 8D E5",
    "08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5",
    "88 D0 4D E2 74 0A 9F ED D0 C1 9F E5 0C C0 9F E7",
    "00 C0 9C E5 04 C0 0B E5 38 00 8D E5 34 10 8D E5",
    "30 20 8D E5 2C 30 8D E5 0A 0A 8D ED 38 00 9D E5",
    "30 48 2d e9 08 b0 8d e2 20 D0 4D E2 10 C0 9B E5",
    "10 4C 2D E9 08 B0 8D E2 50 D0 4D E2 ?? ?? ?? E5 ?? ?? ?? ??", // S1
    "00 48 2D E9 0D B0 A0 E1 E8 D0 4D E2 08 C0 9B E5 CC",           // S2             // S3
    "00 48 2D E9 0D B0 A0 E1 88 D0 4D E2 E0 22 9F E5",              // S4
    "28 00 1B E5 30 10 9F E5 01 10 9F E7 00 10 91 E5",
    "08 1E 29 00 04 D0 4D E2 00 00 8D E5 00 00 9D E5",


};

                List<string> replaceList = new List<string>
{
                "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1 20 D0 4D E2 10 C0 9B E5",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1 70 D0 4D E2 B0 35 9F E5",
    "00 00 A0 E3 1E FF 2F E1 88 D0 4D E2 E0 22 9F E5",
    "00 00 A0 E3 1E FF 2F E1",
    "00 00 A0 E3 1E FF 2F E1"

};

                bool k = false;
                memoryfast.SetProcess(new string[] { "HD-Player" });
                int i2 = 22000000;

                int totalSteps = searchList.Count;
                int currentStep = 0;

                for (int j = 0; j < searchList.Count; j++)
                {
                    progress = (float)currentStep / totalSteps;
                    IEnumerable<long> cu = await memoryfast.AoBScan(searchList[j]);
                    string u = "0x" + cu.FirstOrDefault().ToString("X");

                    if (cu.Count() != 0)
                    {
                        for (int i = 0; i < cu.Count(); i++)
                        {
                            i2++;
                            memoryfast.AobReplace(cu.ElementAt(i), replaceList[j]);
                        }
                        k = true;
                    }
                    currentStep++;
                    progress = (float)currentStep / totalSteps;
                }

                if (k == true)
                {
                    anticheat = true;
                    progress = 1.0f;
                    Console.Beep(2000, 600);
                }
                isProcessing = false;
            }
        }
        private async void guna2CustomCheckBox2_Click_2(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox2.Checked)
            {
                label3.ForeColor = Color.FromArgb(20, 20, 20);

            }
            else
            {
                label3.ForeColor = Color.DimGray;
            }
        }


        private void guna2Button8_Click_2(object sender, EventArgs e)
        {
            ttpsd.Visible = true;
            aimbotpvn.Visible = false;
            esppsd.Visible = false;
            setting.Location = new Point(39, 38);
        }

        private void guna2Button7_Click_1(object sender, EventArgs e)
        {
            ttpsd.Visible = false;
            aimbotpvn.Visible = true;
            esppsd.Visible = false;
            setting.Location = new Point(139, 38);
        }

        private void guna2Button5_Click_1(object sender, EventArgs e)
        {
            ttpsd.Visible = false;
            aimbotpvn.Visible = false;
            esppsd.Visible = true;
            setting.Location = new Point(240, 38);
        }
        // Event khi thay đổi giá trị trackbar, cập nhật label hiển thị

        // Hàm fixesp - làm mới dữ liệu ESP (ví dụ)
        // Biến CancellationTokenSource toàn cục
        private CancellationTokenSource espResetToken = new CancellationTokenSource();
        private Task espResetTask = null;
        private int espResetDelay = 1000; // mặc định 5s

        private void UpdateEntities()
        {

            foreach (var entity in Core.Entities.Values)
            {
                if (entity.IsTeam != Bool3.False) continue;

                TreeNode entityNode = new TreeNode(entity.Name);

                entityNode.Nodes.Add(new TreeNode($"IsKnown: {entity.IsKnown}"));
                entityNode.Nodes.Add(new TreeNode($"IsTeam: {entity.IsTeam}"));
                entityNode.Nodes.Add(new TreeNode($"Head: {entity.Head}"));
                entityNode.Nodes.Add(new TreeNode($"Root: {entity.Root}"));
                entityNode.Nodes.Add(new TreeNode($"Health: {entity.Health}"));
                entityNode.Nodes.Add(new TreeNode($"IsDead: {entity.IsDead}"));
                entityNode.Nodes.Add(new TreeNode($"IsKnocked: {entity.IsKnocked}"));


            }
            Thread.Sleep(1000);
        }
        private void NoCache()
        {

            InternalMemory.Cache = new();
            Core.Entities = new();
            Thread.Sleep(1000);
        }
        // Hàm start auto reset ESP, có delay truyền vào
        private void StartEspAutoReset()
        {
            // Hủy task cũ nếu có
            espResetToken.Cancel();
            espResetToken = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (!espResetToken.Token.IsCancellationRequested)
                {
                    try
                    {
                        fixesp();       // Làm mới dữ liệu ESP
                        updateEsp(); // Xử lý, log hoặc render ESP
                        NoCache();
                        UpdateEntities();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ESP error: " + ex.Message);
                    }

                    await Task.Delay(1000); // Delay động từ tham số
                }
            }, espResetToken.Token);
        }


        // Khi đóng form dừng task
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            espResetToken.Cancel();
            base.OnFormClosing(e);
        }

        private void guna2CustomCheckBox14_Click_3(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox14.Checked)
            {
                label74.ForeColor = Color.FromArgb(20, 20, 20);
                StartEspAutoReset();
            }
            else
            {
                espResetToken.Cancel();
                label74.ForeColor = Color.DimGray;
            }

        }

        private void guna2CustomCheckBox13_Click_3(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox13.Checked)
            {
                label74.ForeColor = Color.FromArgb(20, 20, 20);
                StartEspAutoReset();
            }
            else
            {
                espResetToken.Cancel();
                label74.ForeColor = Color.DimGray;
            }
        }



        private void guna2Button11_Click_1(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPLineColor = picker.Color;
            }
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPFillBoxColor = picker.Color;
            }
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPBonesColor = picker.Color;
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPNameColor = picker.Color;
            }
        }

        private void guna2Button9_Click_1(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPLineColor = picker.Color;
            }
        }

        private async void guna2Button3_Click_1(object sender, EventArgs e)
        {



        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2Button2.Text = "...";
            waitPressKeyForSpd = true;
            waitPressKeyForBind = false;
            waitPressKeyForSpeed = false;
            waitPressKeyForcLft = false;
            waitPressKeyForUp = false;
            waitPressKeyForFly = false;
            waitPressKeyForSpx = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }



        private void label107_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox3_Click_4(object sender, EventArgs e)
        {
            Config.FastReload = guna2CustomCheckBox3.Checked;
        }

        private void guna2CustomCheckBox5_Click_2(object sender, EventArgs e)
        {
            Config.ESPWeapon = guna2CustomCheckBox5.Checked;
        }



        private void guna2CustomCheckBox5_Click_3(object sender, EventArgs e)
        {

            if (guna2CustomCheckBox5.Checked)
            {
                label5.ForeColor = Color.FromArgb(20, 20, 20);

            }
            else
            {

                label5.ForeColor = Color.DimGray;
            }
            Config.FastReload = guna2CustomCheckBox5.Checked;
        }

        private async void guna2CustomCheckBox6_Click_2(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox6.Checked)
            {
                label8.ForeColor = Color.FromArgb(20, 20, 20);



                string search1 = "30 48 2D E9 08 B0 8D E2 68 D0 4D E2 70 13 9F E5";
                string replace1 = "00 00 A0 E3 1E FF 2F E1";

                string search2 = "F0 4F 2D E9 1C B0 8D E2 44 D0 4D E2 18 C0 9B E5";
                string replace2 = "00 00 A0 E3 1E FF 2F E1";

                string search3 = "00 48 2D E9 0D B0 A0 E1 48 D0 4D E2 FC 10 9F E5";
                string replace3 = "00 00 A0 E3 1E FF 2F E1";


                bool k = false;

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {

                }
                else
                {
                    Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                    m.OpenProcess(proc);

                    int i2 = 22000000;

                    var searchPatterns = new[] { search1, search2, search3,
            };
                    var replacePatterns = new[] { replace1, replace2, replace3,
            };
                    foreach (var search in searchPatterns)
                    {
                        IEnumerable<long> wl = await m.AoBScan2(search, writable: true);

                        if (wl.Any())
                        {
                            foreach (var address in wl)
                            {
                                ++i2;
                                m.WriteMemory(address.ToString("X"), "bytes", replacePatterns[Array.IndexOf(searchPatterns, search)]);
                            }
                            k = true;
                        }
                    }

                    if (k)
                    {


                        Console.Beep(1000, 500);
                    }
                    else
                    {


                    }
                }
            }
            else
            {
                label8.ForeColor = Color.DimGray;
            }
        }

        private void guna2CustomCheckBox7_Click_3(object sender, EventArgs e)
        {
            Config.CameraHackEnabled = guna2CustomCheckBox7.Checked;
        }

        private void guna2CustomCheckBox8_Click_3(object sender, EventArgs e)
        {
            label9.ForeColor = guna2CustomCheckBox8.Checked
             ? Color.FromArgb(20, 20, 20)
             : Color.DimGray;
            Config.speed = guna2CustomCheckBox8.Checked;
        }

        private void guna2Button3_Click_2(object sender, EventArgs e)
        {
            guna2Button3.Text = "...";
            waitPressKeyForSpd = false;
            waitPressKeyForBind = false;
            waitPressKeyForSpeed = true;
            waitPressKeyForcLft = false;
            waitPressKeyForUp = false;
            waitPressKeyForFly = false;
            waitPressKeyForSpx = false;
        }

        private void guna2CustomCheckBox9_Click_5(object sender, EventArgs e)
        {
            fixesp();
            updateEsp();

        }

        private void guna2Panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomCheckBox10_Click_3(object sender, EventArgs e)
        {
            Config.telekill2 = guna2CustomCheckBox10.Checked;
        }

        private void guna2CustomCheckBox11_Click_4(object sender, EventArgs e)
        {
            label10.ForeColor = guna2CustomCheckBox11.Checked
            ? Color.FromArgb(20, 20, 20)
            : Color.DimGray;
            if (guna2CustomCheckBox11.Checked)
            {
                // form thong tin
                // VIE
                label66.Text = "Thông Tin Cập Nhật:";
                label58.Text = "Cập Nhật Form Panel";
                label57.Text = "Sửa lỗi Esp Cao";
                label46.Text = "Cập Nhật Anticheat High";
                label3.Text = "Chống Gian Lận Mới";
                label8.Text = "Bảo mật - Kiểm tra";
                label70.Text = "Thông Tin";
                label45.Text = "Phát Triển";
                label42.Text = "Được Làm Bởi : Prlix#0000";
                label41.Text = "Được Phát Hành Từ : Seww";
                label40.Text = "Quản Lí Tất Cả : Kewm";
                label39.Text = "Phát Hành Vào Ngày : 3/6/2025";
                label38.Text = "Cập Nhật Sau : 3/6/2025";
                label1.Text = "Trạng Thái Panel : Online";
            }
            else
            {
                // FIRN THONG TIN
                // ENG
                label66.Text = "Update Information:";
                label58.Text = "Update Form Panel";
                label57.Text = "Fix Esp High";
                label46.Text = "High Anticheat Update";
                label3.Text = "Anticheat New";
                label8.Text = "Security - Test";
                label70.Text = "Informatin";
                label45.Text = "Development";
                label42.Text = "Made By : Prlix#0000";
                label41.Text = "Published By: Seww";
                label40.Text = "Manage All : Kewm";
                label39.Text = "Release Date: 6/3/2025";
                label38.Text = "Last Updated: 6/3/2025";
                label1.Text = "Panel Status: Online";

            }
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            guna2Button15.Text = "...";
            waitPressKeyForSpd = false;
            waitPressKeyForBind = false;
            waitPressKeyForSpeed = false;
            waitPressKeyForcLft = false;
            waitPressKeyForUp = true;
            waitPressKeyForFly = false;
            waitPressKeyForSpx = false;
        }

        private void guna2CustomCheckBox20_Click_1(object sender, EventArgs e)
        {
            label17.ForeColor = guna2CustomCheckBox20.Checked
            ? Color.FromArgb(20, 20, 20)
            : Color.DimGray;
            Config.UpPlayer = guna2CustomCheckBox20.Checked;
        }

        private void guna2CustomCheckBox21_Click_1(object sender, EventArgs e)
        {
            label18.ForeColor = guna2CustomCheckBox21.Checked
            ? Color.FromArgb(20, 20, 20)
            : Color.DimGray;
            Config.flyme = guna2CustomCheckBox21.Checked;
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            guna2Button14.Text = "...";
            waitPressKeyForSpd = false;
            waitPressKeyForBind = false;
            waitPressKeyForSpeed = false;
            waitPressKeyForcLft = false;
            waitPressKeyForUp = false;
            waitPressKeyForFly = true;
            waitPressKeyForSpx = false;
        }

        private void guna2CustomCheckBox6_Click_3(object sender, EventArgs e)
        {
            label11.ForeColor = guna2CustomCheckBox6.Checked
           ? Color.FromArgb(20, 20, 20)
           : Color.DimGray;
            Config.speedx = guna2CustomCheckBox6.Checked;
        }

        private void guna2Button10_Click_1(object sender, EventArgs e)
        {
            guna2Button10.Text = "...";
            waitPressKeyForSpd = false;
            waitPressKeyForBind = false;
            waitPressKeyForSpeed = false;
            waitPressKeyForcLft = false;
            waitPressKeyForUp = false;
            waitPressKeyForFly = false;
            waitPressKeyForSpx = true;
        }

        private void guna2CustomCheckBox28_Click_1(object sender, EventArgs e)
        {
            label22.ForeColor = guna2CustomCheckBox28.Checked
         ? Color.FromArgb(20, 20, 20)
         : Color.DimGray;
            Config.ESPLine = guna2CustomCheckBox28.Checked;
        }

        private void guna2CustomCheckBox27_Click(object sender, EventArgs e)
        {

            label21.ForeColor = guna2CustomCheckBox27.Checked
         ? Color.FromArgb(20, 20, 20)
         : Color.DimGray;
            Config.ESPBox = guna2CustomCheckBox27.Checked;
        }

        private void guna2CustomCheckBox25_Click(object sender, EventArgs e)
        {
            label20.ForeColor = guna2CustomCheckBox25.Checked
        ? Color.FromArgb(20, 20, 20)
        : Color.DimGray;
            Config.ESPFillBox = guna2CustomCheckBox25.Checked;
        }

        private void guna2CustomCheckBox23_Click(object sender, EventArgs e)
        {
            label19.ForeColor = guna2CustomCheckBox23.Checked
      ? Color.FromArgb(20, 20, 20)
      : Color.DimGray;
            Config.CrosshairEnabled = guna2CustomCheckBox23.Checked;
        }

        private void guna2CustomCheckBox22_Click(object sender, EventArgs e)
        {
            label16.ForeColor = guna2CustomCheckBox22.Checked
     ? Color.FromArgb(20, 20, 20)
     : Color.DimGray;

            Config.ESPName = guna2CustomCheckBox22.Checked;
            Config.ESPHealth = guna2CustomCheckBox22.Checked;
        }

        private void guna2CustomCheckBox11_Click_5(object sender, EventArgs e)
        {
            label10.ForeColor = guna2CustomCheckBox11.Checked
    ? Color.FromArgb(20, 20, 20)
    : Color.DimGray;

            Config.ESPBones = guna2CustomCheckBox11.Checked;
        }

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPBoxColor = picker.Color;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPFillBoxColor = picker.Color;
            }
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.CrosshairColor = picker.Color;
            }
        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                //     Config.ESPHealthColor = picker.Color;
                Config.ESPNameColor = picker.Color;
            }
        }

        private void guna2Button17_Click_3(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                //     Config.ESPHealthColor = picker.Color;
                Config.ESPBonesColor = picker.Color;
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Kiểm tra null và parse giá trị từ ComboBox
            if (guna2ComboBox1.SelectedItem == null ||
                !Enum.TryParse(guna2ComboBox1.SelectedItem.ToString(), out AimBotType selectedType))
            {
                MessageBox.Show("Loại AimBot không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cập nhật cấu hình
            Config.AimBotType = selectedType;

            // Thiết lập các thuộc tính liên quan
            Config.SilentAim2 = selectedType == AimBotType.Silent;

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox2.SelectedItem != null &&
                Enum.TryParse(guna2ComboBox2.SelectedItem.ToString(), out TargetingMode mode))
            {
                Config.TargetingMode = mode;
            }
        }

        private void guna2Panel4_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void aimbotpvn_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

