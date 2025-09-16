using AimBotConquer.Guna;
using Client;
using Guna.UI2.WinForms;
using ImGuiNET;


using Loader;
using Newtonsoft.Json;
using Newtonsoft.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms;
using static AotForms.ESP;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using JsonException = System.Text.Json.JsonException;



namespace AotForms
{
    public partial class Form1 : Form
    {




        private static readonly HttpClient client = new HttpClient();

        string selectedMode = "safe";


        IntPtr mainHandle;
        private object guna2Button25;

        public Form1(IntPtr handle)
        {


            InitializeComponent();
            mainHandle = handle;
            UpdateModeSelection();

        }


        private string GetHwid()
        {
            var components = new System.Collections.Generic.List<string>();

            string cpuBrand = null;
            try
            {
                using var searcher = new ManagementObjectSearcher("select Name from Win32_Processor");
                foreach (var item in searcher.Get())
                    cpuBrand = item["Name"]?.ToString();
            }
            catch { }
            if (!string.IsNullOrEmpty(cpuBrand))
                components.Add($"CPU:{cpuBrand}");

            string mac = GetMacAddress();
            if (!string.IsNullOrEmpty(mac))
                components.Add($"MAC:{mac}");

            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                foreach (var item in searcher.Get())
                {
                    var serial = item["SerialNumber"]?.ToString();
                    if (!string.IsNullOrEmpty(serial))
                        components.Add($"MB:{serial}");
                }
            }
            catch { }

            components.Sort();
            var joined = string.Join("|", components);
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(joined));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private string GetMacAddress()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up &&
                    !nic.Description.ToLower().Contains("virtual") &&
                    !nic.Description.ToLower().Contains("vpn"))
                {
                    var mac = nic.GetPhysicalAddress().ToString();
                    if (!string.IsNullOrEmpty(mac))
                        return mac;
                }
            }
            return null;
        }

        private void UpdateModeSelection()
        {
            if (selectedMode == "risk")
            {
                btnRisk.FillColor = Color.FromArgb(188, 205, 246);


                btnSafe.FillColor = Color.FromArgb(188, 200, 231);

            }
            else
            {
                btnRisk.FillColor = Color.FromArgb(188, 200, 231);


                btnSafe.FillColor = Color.FromArgb(188, 205, 246);



            }
        }
        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {


            selectedMode = "safe";
            UpdateModeSelection();






        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void thongtinpvn_Paint(object sender, PaintEventArgs e)
        {

        }


        private void login_Paint(object sender, PaintEventArgs e)
        {

        }

        private void abd_Paint(object sender, PaintEventArgs e)
        {

        }

        public class KeyCheckRequest
        {
            [JsonProperty("key")]
            public string Key { get; set; }

            [JsonProperty("hwid")]
            public string Hwid { get; set; }

            [JsonProperty("machine_name")]
            public string MachineName { get; set; }
        }

        private static readonly HttpClient http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(15)
        };

        private void guna2Button25_Click(object sender, EventArgs e)
        {

        }




        private void RunProtectedCode()
        {
            // ✅ Thêm code cần chạy khi key đúng ở đây
            MessageBox.Show("Đã xác thực thành công. Tiếp tục chạy code...");
        }

        private string GetHWID()
        {
            // Bạn có thể đổi sang cách lấy HWID khác nếu hệ thống yêu cầu
            return System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
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
        private async void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //dwdawdawdawwa   selectedMode = "safe";
            //  UpdateModeSelection();
            //    MessageBox.Show("Mod Menu : Safe");
            MessageBox.Show("Đang Bảo Trì !");
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2vSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void US_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://discord.gg/Eb4Upkfcmz",
                UseShellExecute = true
            });
        }

        private void btnRisk_Click(object sender, EventArgs e)
        {
            selectedMode = "risk";
            UpdateModeSelection();
            MessageBox.Show("Mod Menu : Risk");

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void richTextBoxLog_TextChanged(object sender, EventArgs e)
        {

        }

        private async void guna2Button1_Click_1(object sender, EventArgs e)
        {


        }
        // Define them outside the method if you want to access them globally
        ImFontPtr smallFont;
        ImFontPtr bigFont;





        private async Task LaunchCheat()
        {



            var processes = Process.GetProcessesByName("HD-Player");

            if (processes.Length != 1)
            {
                MessageBox.Show("Open emulator.");
                return;
            }

            var process = processes[0];
            string mainModulePath = Path.GetDirectoryName(process.MainModule?.FileName);

            if (string.IsNullOrEmpty(mainModulePath))
            {
                MessageBox.Show("Reinstall emulator.");
                return;
            }

            var adbPath = Path.Combine(mainModulePath, "HD-Adb.exe");
            if (!File.Exists(adbPath))
            {
                MessageBox.Show("Adb not Found. Reinstall emulator.");
                return;
            }
            MessageBox.Show("Connecting..");


            var adb = new Adb(adbPath);
            await adb.Kill();
            //await Task.Delay(500);
            if (!await adb.Start())
            {
                MessageBox.Show("Adb Error");
                Environment.Exit(0);
                return;
            }
            //await Task.Delay(500);
            var moduleAddr = await adb.FindModule("com.dts.freefiremax", "libil2cpp.so");

            if (moduleAddr == 0)
            {
                MessageBox.Show("Go to Lobby then Logout then try to apply.");
                Environment.Exit(0);
                return;
            }

            MessageBox.Show("Connected done");
            Offsets.Il2Cpp = (uint)moduleAddr;
            Core.Handle = FindRenderWindow(mainHandle);



        }



        private async void guna2Button1_Click_2(object sender, EventArgs e)
        {






            // Create ImGui context
            ImGui.CreateContext();
            ImGuiIOPtr io = ImGui.GetIO();



            //MessageBox.Show("Fonts loaded from embedded resource!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LaunchCheat();
        }



        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private async void guna2Button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            var esp = new ESP();
            await esp.Start();
            new Thread(Data.Work) { IsBackground = true }.Start();
            new Thread(Aimbot.Work) { IsBackground = true }.Start();
            new Thread(AimFOV.Work) { IsBackground = true }.Start();
            new Thread(() => Silent.Work()) { IsBackground = true }.Start();
            new Thread(() => AimSilent.Work()) { IsBackground = true }.Start();
            new Thread(() => AimbotSystem.Work()) { IsBackground = true }.Start();
            new Thread(() => Telekill.Start()) { IsBackground = true }.Start();
            new Thread(UpPlayer.Work) { IsBackground = true }.Start();
            new Thread(ProxTelekill.Work) { IsBackground = true }.Start();
            new Thread(FlyMe.Work) { IsBackground = true }.Start();
            new Thread(Downplayer.Work) { IsBackground = true }.Start();

            Console.Beep(500, 500);
        }

        private void guna2Panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
