using ClickableTransparentOverlay;
using Client;
using Guna.UI2.WinForms;
using ImGuiNET;
using Reborn;
using KhoaVuxMem;
using Loader;
using Memory;
using Microsoft.VisualBasic.Logging;
using SharpGen.Runtime;
using SmartMewwxSeww;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static AotForms.WinAPI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AotForms
{

    internal partial class ESP : ClickableTransparentOverlay.Overlay
    {
        string fontpath = "C:\\Windows\\Fonts\\taileb.ttf";

        public static api KeyAuth = new api(
       name: "Ken_r7's Application",
       ownerid: "JqPssmND3C",
       secret: "749fa5315f69557f720ea450443e91553586576486e8398784d095d8a373563e",
       version: "1.0"
   );
        // Thêm các hằng số và enum này vào class
        const int GWL_EXSTYLE = -20;
        const int WS_EX_TOOLWINDOW = 0x00000080;
        const int WS_EX_APPWINDOW = 0x00040000;
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        enum WDA : uint
        {
            WDA_NONE = 0x00000000,
            WDA_MONITOR = 0x00000001,
            WDA_EXCLUDEFROMCAPTURE = 0x00000011,
        }

        // Thêm các hàm WinAPI
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
     


     
        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("kernel32.dll")]
        private static extern bool Beep(int frequency, int duration);
        private CX memoryfast = new CX();
        private bool anticheat = false;
        private static KhoaVu m = new KhoaVu();
        Mem sew = new Mem();
        public static int AimAssistRadius = 100;
        private bool isProcessing = false;
        private float progress = 0.0f;
        private ConcurrentDictionary<int, EntityRenderData> processedEntities = new();
        private Task entityProcessingTask;
        private struct EntityRenderData
        {
            public Vector2 headScreenPos;
            public Vector2 bottomScreenPos;
            public float Distance;
            public bool IsValid;
        }
        static SmartxSewww Gay = new SmartxSewww();
        private static Dictionary<string, IntPtr> _weaponIcons = new();
        public static class FontManager
        {
            public static ImFontPtr SmallFont;
            public static ImFontPtr BigFont;
        }
        private CancellationTokenSource _cancellationTokenSource;
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);
        public static bool IsKeyDown(Keys key)
        {
            return (GetAsyncKeyState(key) & 0x8000) != 0;
        }
        // Start a background task to listen for key presses



        private void StartHotkeyThread()
        {
            new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (KeyHelper.IsKeyDown(Config.SpKey))
                        {
                            if (!Config.KeyAlreadyPressed)
                            {
                                Config.speed = !Config.speed;
                                Config.KeyAlreadyPressed = true;
                            }
                        }
                        else Config.KeyAlreadyPressed = false;

                        if (KeyHelper.IsKeyDown(Config.TelePortKey))
                        {
                            if (!Config.KeyAlreadyPressed3)
                            {
                                Config.telekill = !Config.telekill;
                                Config.KeyAlreadyPressed3 = true;
                            }
                        }
                        else Config.KeyAlreadyPressed3 = false;

                        if (KeyHelper.IsKeyDown(Config.TeleKillKey))
                        {
                            if (!Config.KeyAlreadyPressed4)
                            {
                                Config.proxtelekill = !Config.proxtelekill;
                                Config.KeyAlreadyPressed4 = true;
                            }
                        }
                        else Config.KeyAlreadyPressed4 = false;

                        if (KeyHelper.IsKeyDown(Config.UpPlayerKey))
                        {
                            if (!Config.KeyAlreadyPressed6)
                            {
                                Config.UpPlayer = !Config.UpPlayer;
                                Config.KeyAlreadyPressed6 = true;
                            }
                        }
                        else Config.KeyAlreadyPressed6 = false;

                        Thread.Sleep(10);
                    }
                    catch { }
                }
            })
            { IsBackground = true }.Start();
        }

        private void StartHotkeyListener()
        {
            new Thread(() =>
            {
                while (true)
                {
                    // SPEED
                    if (KeyHelper.IsKeyDown(Config.SpKey))
                    {
                        if (!Config.KeyAlreadyPressed)
                        {
                            Config.speed = !Config.speed;
                            Config.KeyAlreadyPressed = true;
                        }
                    }
                    else
                    {
                        Config.KeyAlreadyPressed = false;
                    }

                    // TELEPORT
                    if (KeyHelper.IsKeyDown(Config.TelePortKey))
                    {
                        if (!Config.KeyAlreadyPressed3)
                        {
                            Config.telekill = !Config.telekill;
                            Config.KeyAlreadyPressed3 = true;
                        }
                    }
                    else
                    {
                        Config.KeyAlreadyPressed3 = false;
                    }

                    // TELEKILL
                    if (KeyHelper.IsKeyDown(Config.TeleKillKey))
                    {
                        if (!Config.KeyAlreadyPressed4)
                        {
                            Config.proxtelekill = !Config.proxtelekill;
                            Config.KeyAlreadyPressed4 = true;
                        }
                    }
                    else
                    {
                        Config.KeyAlreadyPressed4 = false;
                    }

                    // UP PLAYER
                    if (KeyHelper.IsKeyDown(Config.UpPlayerKey))
                    {
                        if (!Config.KeyAlreadyPressed6)
                        {
                            Config.UpPlayer = !Config.UpPlayer;
                            Config.KeyAlreadyPressed6 = true;
                        }
                    }
                    else
                    {
                        Config.KeyAlreadyPressed6 = false;
                    }

                    Thread.Sleep(10); // tránh ăn 100% CPU
                }
            })
            { IsBackground = true }.Start();
        }



        int EnemyCount = 0;
        IntPtr hWnd;
        IntPtr HDPlayer;
        private Vector4 lineColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 fovColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 boxColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 fillboxColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 crossColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 skeletonColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private bool isAutoRefreshChecked = false;
        private int selectedBoxIndex = 0;



        private readonly string[] _comboItems2 = { "Closest To Crosshair", "Target 360", "Closest player", "Lowest health" };
        private readonly string[] _comboItems1 = { "Silent Aim", "Aimbot Rage(Risky)" };
        private readonly string[] _comboItems = { "AimBot", "Aim Mouse", "Aim By XynQaw" , "Silent Aim" };
        private readonly string[] _headerItems = { "Aim", "Esp", "Colors", "Extras", "Misc" };
        private int _selectedHeader, _comboBox, _comboBox1, _comboBox2;

        private bool isAutoRefreshActive = false; private async void autorefresh_Tick(object sender, EventArgs e)
        {
            while (isAutoRefreshActive) // Stop the loop if the flag is false
            {
                InternalMemory.Cache = new();
                Core.Entities = new();

                await Task.Delay(1000); // Wait for 1 second before looping again
            }
        }
        private async void AntiCheat()
        {
            var patterns = new (string search, string replace)[]
            {



  ("10 4C 2D E9 08 B0 8D E2 F8 D0 4D E2 60 25 9F E5", "00 00 A0 E3 1E FF 2F E1"),
    ("30 48 2D E9 08 B0 8D E2 92 DF 4D E2 01 DB 4D E2", "00 00 A0 E3 1E FF 2F E1"),
    ("30 48 2D E9 08 B0 8D E2 1D DE 4D E2 C0 C4 9F E5", "00 00 A0 E3 1E FF 2F E1"),
    ("30 48 2D E9 08 B0 8D E2 17 DE 4D E2 30 CE 9F E5", "00 00 A0 E3 1E FF 2F E1"),
    ("00 48 2D E9 0D B0 A0 E1 C0 D0 4D E2 B0 C6 9F E5", "00 00 A0 E3 1E FF 2F E1"),
    ("10 4C 2D E9 08 B0 8D E2 29 DE 4D E2 0C C0 9B E5", "00 00 A0 E3 1E FF 2F E1"),
    ("10 4C 2D E9 08 B0 8D E2 A6 DF 4D E2 0C C0 9B E5", "00 00 A0 E3 1E FF 2F E1"),

            };

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                MessageBox.Show("Process not detected!");
                return;
            }

            int proc = Process.GetProcessesByName("HD-Player")[0].Id;
            m.OpenProcess(proc);

            bool success = false;

            foreach (var (search, replace) in patterns)
            {
                IEnumerable<long> addresses = await m.AoBScan2(search, writable: true);

                if (!addresses.Any())
                {
                    
                    continue;
                }

                foreach (var addr in addresses)
                {
                    m.WriteMemory(addr.ToString("X"), "bytes", replace);
                }

                success = true;
            }

            if (success)
            {
                statusMessage = " Anticheat P64 Done !";
                Console.Beep(1000, 500);
                MessageBox.Show("Done Anticheat");
            }
        }

        private async void AntiCheat2()
        {
            var patterns = new (string search, string replace)[]
            {





            };

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                MessageBox.Show("Process not detected!");
                return;
            }

            int proc = Process.GetProcessesByName("HD-Player")[0].Id;
            m.OpenProcess(proc);

            bool success = false;

            foreach (var (search, replace) in patterns)
            {
                IEnumerable<long> addresses = await m.AoBScan2(search, writable: true);

                if (!addresses.Any())
                {
                    MessageBox.Show($"Không tìm thấy chuỗi: {search}");
                    continue;
                }

                foreach (var addr in addresses)
                {
                    m.WriteMemory(addr.ToString("X"), "bytes", replace);
                }

                success = true;
            }

            if (success)
            {

                Console.Beep(1000, 500);
                MessageBox.Show("Done Anticheat Step 2");
            }
        }



        private async void Brutal()
        {
            string search1 = "DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30 EE 2C 10 80 E5 50 00 C0 F2 04 10 80 E2 00 20 A0 E3 30 20 80 E5 34 20 80 E5 01 1A 22 EE 3C 20 80 E5 8F 0A 41 F4 18 10 80 E2 03 0A 80 EE 03 1A 81 EE 8F 0A 41 F4 0A 0A 80 ED";
            string replace1 = "DB 0F A9 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30 EE 2C 10 80 E5 50 00 C0 F2 04 10 80 E2 00 20 A0 E3 30 20 80 E5 34 20 80 E5 01 1A 22 EE 3C 20 80 E5 8F 0A 41 F4 18 10 80 E2 03 0A 80 EE 03 1A 81 EE 8F 0A 41 F4 0A 0A 80 ED";

            string search2 = "CD CC 4C 3E 00 00 00 00 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            string replace2 = "00 00 80 BF";

         
            bool k = false;

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                MessageBox.Show("Process not detected!");
                return;
            }

            int proc = Process.GetProcessesByName("HD-Player")[0].Id;
            m.OpenProcess(proc);

            var searchPatterns = new[] { search1, search2,
            };
            var replacePatterns = new[] { replace1, replace2,
            };

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
       
    

        private static bool IsKeyPressed(Keys key)
        {
            return (GetAsyncKeyState(key) & 1) != 0;
        }
        protected override unsafe void Render()
        {
            RenderImgui();

            if (!Core.HaveMatrix || Core.CameraMatrix == Matrix4x4.Identity)
                return;

            
                        CreateHandle();

            // ==============================
            // Hiển thị tiêu đề + EnemyCount
            // ==============================
            var windowWidth = Core.Width;
            var windowHeight = Core.Height;

            var tmp = Core.Entities;
            if (tmp == null || tmp.Count == 0)
                return;

            EnemyCount = tmp.Values.Count(entity => !entity.IsDead); // ❌ Không dùng IsKnown

            string text = $"Ken_R7 - {EnemyCount}";
            var textSize = ImGui.CalcTextSize(text);
            var textPosX = (windowWidth - textSize.X) / 2;
            var textPosY = 80;
            uint textColor = ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 1f));
            var drawList = ImGui.GetForegroundDrawList();
            drawList.AddText(new Vector2(textPosX, textPosY), textColor, text);

            // ===========================
            // Font tiêu đề lớn Ken_R7
            // ===========================
            ReplaceFont(fontpath, 15, FontGlyphRangeType.Vietnamese);
            float fontSize = 250.0f;
            ImGui.GetIO().FontGlobalScale = fontSize / ImGui.GetFontSize();
            var screenSize = ImGui.GetIO().DisplaySize;
            var pos = new Vector2((screenSize.X - textSize.X) / 2, 10);
            // drawList.AddText(pos, ImGui.GetColorU32(new Vector4(1f, 0f, 0f, 1f)), " Ken_R7 - Premium");
            // =============================
            // Hiển thị trạng thái các chức năng
            // =============================
            var basePos = new Vector2(10, 10);
            float lineHeight = 20;

            drawList.AddText(basePos,
                Config.UpPlayer ? ImGui.GetColorU32(new Vector4(0, 1, 0, 1)) : ImGui.GetColorU32(new Vector4(1, 0, 0, 1)),
                $"Up Player : {(Config.UpPlayer ? "ON" : "OFF")} - [ {Config.UpPlayerKeyLabel} ]");

            drawList.AddText(basePos + new Vector2(0, lineHeight),
                Config.proxtelekill ? ImGui.GetColorU32(new Vector4(0, 1, 0, 1)) : ImGui.GetColorU32(new Vector4(1, 0, 0, 1)),
                $"Tele Kill : {(Config.proxtelekill ? "ON" : "OFF")} - [ {Config.TeleKillKeyLabel} ]");

            drawList.AddText(basePos + new Vector2(0, lineHeight * 2),
                Config.speed ? ImGui.GetColorU32(new Vector4(0, 1, 0, 1)) : ImGui.GetColorU32(new Vector4(1, 0, 0, 1)),
                $"Speed Internal: {(Config.speed ? "ON" : "OFF")} - [ {Config.SpKeyLabel} ]");

            drawList.AddText(basePos + new Vector2(0, lineHeight * 3),
                Config.telekill ? ImGui.GetColorU32(new Vector4(0, 1, 0, 1)) : ImGui.GetColorU32(new Vector4(1, 0, 0, 1)),
                $"Tele To Enemy: {(Config.telekill ? "ON" : "OFF")} - [ {Config.TelePortKeyLabel} ]");

            // =====================
            // Vẽ ESP Box Enemy
            // =====================


            // if (!Core.HaveMatrix) return;

          //  CreateHandle();

        //    var tmp = Core.Entities;
            EnemyCount = tmp.Values.Count(entity => !entity.IsDead && entity.IsKnown);
            foreach (var entity in tmp.Values)
            {
                if (entity.IsDead || !entity.IsKnown) continue;
                var dist = Vector3.Distance(Core.LocalMainCamera, entity.Head);
                if (dist > 200) continue;
                var headScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
                var bottomScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Root, Core.Width, Core.Height);
                if (headScreenPos.X < 1 || headScreenPos.Y < 1) continue;
                if (bottomScreenPos.X < 1 || bottomScreenPos.Y < 1) continue;
                float CornerHeight = Math.Abs(headScreenPos.Y - bottomScreenPos.Y);
                float CornerWidth = (float)(CornerHeight * 0.65);




                if (Config.ESPLine)
                {
                    // Check if the entity is "Knocked"
                    uint lineColor;

                    if (entity.IsKnocked)
                    {
                        lineColor = ColorToUint32(Color.Red); // Red for "Knocked" state
                    }
                    else
                    {
                        lineColor = ColorToUint32(Config.ESPLineColor); // Normal color
                    }

                    // Draw the line with the appropriate color
                    ImGui.GetBackgroundDrawList().AddLine(
                        new Vector2(Core.Width / 2f, 0f),
                        headScreenPos,
                        lineColor,
                        1f
                    );

                }
                if (Config.ESPWeapon && !string.IsNullOrEmpty(entity.WeaponName))
                {
                    Vector2 fixedNameSize1 = new Vector2(95, 16);
                    if (headScreenPos.X >= 0 && headScreenPos.Y >= 0 &&
                        headScreenPos.X <= Core.Width && headScreenPos.Y <= Core.Height)
                    {
                        Vector2 namePos = new Vector2(
                            headScreenPos.X - fixedNameSize1.X / 2,
                            headScreenPos.Y - fixedNameSize1.Y - 13);

                        try
                        {
                            var weaponFileName = entity.WeaponName.ToLower();
                            var imagePath = $"C:\\weaponff\\{weaponFileName}.png";
                            if (File.Exists(imagePath))
                            {
                                IntPtr imageHandle;
                                AddOrGetImagePointer(imagePath, true, out imageHandle, out var width, out var height);

                                if (imageHandle != IntPtr.Zero)
                                {
                                    Vector2 iconSize = new Vector2(60, 20);
                                    Vector2 iconPos = new Vector2(namePos.X + (fixedNameSize1.X - iconSize.X) / 2, namePos.Y - iconSize.Y - 2);
                                    ImGui.GetForegroundDrawList().AddImage(imageHandle, iconPos, iconPos + iconSize);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ESPWeapon] Image load error: {ex.Message}");
                        }
                    }
                }
                if (Config.ESPLineDuoi)
                {
                    // Check if the entity is "Knocked"
                    uint lineColor;


                    lineColor = ColorToUint32(Config.ESPLineDuoiColor); // Normal color


                    // Draw the line with the appropriate color
                    ImGui.GetBackgroundDrawList().AddLine(
                      new Vector2(Core.Width / 2f, Core.Height / 2f),  // Từ giữa màn hình (Center ESP)
                        headScreenPos,
                        lineColor,
                        1f
                    );

                }
                // Đọc thông tin vũ khí của entity từ bộ nhớ



                if (Config.AimBot)
                {
                    //   var drawList = ImGui.GetBackgroundDrawList();
                    int numSegments = 1000; // Số lượng đoạn, càng cao thì hình tròn càng mịn
                    drawList.AddCircle(new Vector2(Core.Width / 2, Core.Height / 2), Config.AimbotFOV, ColorToUint32(Config.FovColor), numSegments, 1.4f);
                }

                if (Config.minimap)
                {
                    DrawMinimap();
                }

                if (Config.ESPDistance)
                {
                    // Create the distance string
                    string distanceText = $"{MathF.Round(dist)} M";

                    // Estimate text width (average character width = 8px)
                    float estimatedTextWidth = distanceText.Length * 8f;

                    // Center the text based on screen position
                    Vector2 distancePosition = new Vector2(bottomScreenPos.X + 1 - (estimatedTextWidth / 2), bottomScreenPos.Y + 15f);

                    // Draw the distance text directly (yellow or any other configured color)
                    ImGui.GetForegroundDrawList().AddText(distancePosition, ColorToUint32(Config.ESPLineColor), distanceText);
                }
                Vector2 fixedNameSize = new Vector2(95, 16);
                float healthBarHeight = 4;

                if (entity.Name == "")
                    entity.Name = "Seww";
                if (headScreenPos.X >= 0 && headScreenPos.Y >= 0 && headScreenPos.X <= Core.Width && headScreenPos.Y <= Core.Height)
                {
                    Vector2 namePos = new Vector2(headScreenPos.X - fixedNameSize.X / 2, headScreenPos.Y - fixedNameSize.Y - 15);



                    Vector2 textSizeName = ImGui.CalcTextSize(entity.Name);

                    Vector2 textSizeDistance = ImGui.CalcTextSize($" ({MathF.Round(Vector3.Distance(Core.LocalMainCamera, entity.Head))}m)");

                    Vector2 textPosName = new Vector2(namePos.X + 5, namePos.Y + (fixedNameSize.Y - textSizeName.Y) / 2);
                    Vector2 textPosDistance = new Vector2(namePos.X + fixedNameSize.X - textSizeDistance.X + 5, namePos.Y + (fixedNameSize.Y - textSizeDistance.Y) / 2);



                    if (Config.ESPName)
                    {
                        ImGui.GetForegroundDrawList().AddRectFilled(namePos, namePos + new Vector2(fixedNameSize.X, fixedNameSize.Y), ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 0.7f)), 3f);
                        ImGui.GetForegroundDrawList().AddText(textPosName, ColorToUint32(Config.ESPNameColor), entity.Name);
                        ImGui.GetForegroundDrawList().AddText(textPosDistance, ColorToUint32(Config.ESPNameColor), $" {MathF.Round(Vector3.Distance(Core.LocalMainCamera, entity.Head))}m");
                    }
                    if (Config.ESPHealth && Config.ESPName)
                    {
                        Vector2 barPos = new Vector2(namePos.X, namePos.Y + fixedNameSize.Y);
                        var vList = ImGui.GetForegroundDrawList();
                        float healthPercentage = entity.Health > 1000 ? 1f :
                        entity.Health < 0 ? 1f :
                        (float)entity.Health / (entity.Health > 230 ? 500 : 200);
                        float barWidth = fixedNameSize.X * healthPercentage;
                        uint barColor;
                        if (healthPercentage > 0.8f)
                        {
                            barColor = ColorToUint32(Color.GreenYellow);
                        }
                        else if (healthPercentage > 0.4f)
                        {
                            barColor = ColorToUint32(Color.Orange);
                        }
                        else
                        {
                            barColor = ColorToUint32(Color.Red);
                        }

                        vList.AddRectFilled(new Vector2(barPos.X, barPos.Y), new Vector2(barPos.X + fixedNameSize.X, barPos.Y + 2), 0x90000000);

                        barColor = entity.IsKnocked ? ColorToUint32(Color.Red) : barColor;
                        vList.AddRectFilled(new Vector2(barPos.X, barPos.Y), new Vector2(barPos.X + barWidth, barPos.Y + 2), barColor);
                    }


                   

                    

                }

                if (Config.ESPFillBox)
                {
                    float alpha = 1.2f; // Độ trong suốt của filled box, chỉnh sửa theo nhu cầu của bạn
                    uint fillboxColor = ColorToUint32(Color.White);
                    DrawFullBox(headScreenPos.X - (CornerWidth / 2), headScreenPos.Y, CornerWidth, CornerHeight, ColorToUint32(Color.White), alpha);
                    DrawCorneredBox2(headScreenPos.X - (CornerWidth / 2), headScreenPos.Y, CornerWidth, CornerHeight, fillboxColor, 1f);
                }
                if (Config.ESPBox)
                {
                    uint boxColor = ColorToUint32(Config.ESPBoxColor);

                    DrawCorneredBox(headScreenPos.X - (CornerWidth / 2), headScreenPos.Y, CornerWidth, CornerHeight, boxColor, 1f);

                }
                if (Config.ESPSkeleton)
                {
                    uint skeletonColor = ColorToUint32(Config.ESPSkeletonColor);
                    DrawSkeleton(entity, skeletonColor);
                }


            }
           
            
                if (Config.AimbotVisible)
            {
                //   var drawList = ImGui.GetBackgroundDrawList();
                int numSegments = 1000; // Số lượng đoạn, càng cao thì hình tròn càng mịn
                drawList.AddCircle(new Vector2(Core.Width / 2, Core.Height / 2), Config.AimbotFOV, ColorToUint32(Config.FovColor), numSegments, 1.4f);
            }
           
            DisplayEnemyCount(Core.Width, Core.Height);
            DrawShurikenCrosshair();
        }
       
     

        const uint WDA_NONE = 0x00000000;
        const uint WDA_MONITOR = 0x00000001;
        const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;

        bool IsValidTarget(Entity entity)
        {
            // Kiểm tra điều kiện hợp lệ của target, ví dụ:
            return entity != null && !entity.IsDead && entity.IsEnemy;
        }


        void DrawSmallTextWithOutline(Vector2 pos, string text, uint textColor, uint outlineColor)
        {
            var vList = ImGui.GetForegroundDrawList();
            float outlineThickness = 1.2f;  // Smaller outline for smoothness
            float boldOffset = 0.4f;        // Adjusted for smaller text
            float spacing = 2.0f;           // Adds space between characters

            Vector2 adjustedPos = pos;

            foreach (char c in text)
            {
                string character = c.ToString(); // Convert char to string

                // Smooth outline
                for (float x = -outlineThickness; x <= outlineThickness; x += 1.0f)
                {
                    for (float y = -outlineThickness; y <= outlineThickness; y += 1.0f)
                    {
                        if (x == 0 && y == 0) continue;
                        vList.AddText(new Vector2(adjustedPos.X + x, adjustedPos.Y + y), outlineColor, character);
                    }
                }

                // Bold effect
                vList.AddText(new Vector2(adjustedPos.X - boldOffset, adjustedPos.Y), textColor, character);
                vList.AddText(new Vector2(adjustedPos.X + boldOffset, adjustedPos.Y), textColor, character);
                vList.AddText(new Vector2(adjustedPos.X, adjustedPos.Y - boldOffset), textColor, character);
                vList.AddText(new Vector2(adjustedPos.X, adjustedPos.Y + boldOffset), textColor, character);

                // Main text layer
                vList.AddText(adjustedPos, textColor, character);

                // Move position for next character with spacing
                adjustedPos.X += ImGui.CalcTextSize(character).X + spacing;
            }
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
        public void DrawGradientBox(float X, float Y, float W, float H, Color topColor, Color bottomColor)
        {
            var vList = ImGui.GetForegroundDrawList();

            int slices = 50; // Number of slices for gradient
            float sliceHeight = H / slices;

            for (int i = 0; i < slices; i++)
            {
                float t = (float)i / slices; // Interpolation factor
                Color sliceColor = Color.FromArgb(
                    (int)(topColor.A * (1 - t) + bottomColor.A * t), // Interpolating opacity
                    (int)(topColor.R * (1 - t) + bottomColor.R * t), // Interpolating Red
                    (int)(topColor.G * (1 - t) + bottomColor.G * t), // Interpolating Green
                    (int)(topColor.B * (1 - t) + bottomColor.B * t)  // Interpolating Blue
                );

                uint sliceColorUint = ColorToUint32(sliceColor);

                // Draw each slice
                vList.AddRectFilled(
                    new Vector2(X, Y + i * sliceHeight),
                    new Vector2(X + W, Y + (i + 1) * sliceHeight),
                    sliceColorUint
                );
            }
        }
        private bool showMenu = true;

        string user = "";
        string pass = "";
        bool ShowPanelZ = true;
        private static int loginCount = 0;
        //private static string username = "";
        //private static string password = "";
        //private static bool rememberMe = false;
        //private static bool Logins = false;

      
        bool ShowPanelz()
        {
            return ShowPanelZ == true;
        }

        bool Logins = false;
        private int selectedTab = 0;


   
        private bool wasInsertPressed = false; // Theo dõi trạng thái Insert


        bool scopeEnabled = false;
        bool scopeBusy = false;
        bool scopeReady = false;
        List<long> scopeAddresses = new List<long>();
        string statusText = "";
        // Spd
        string[] processName = { "HD-Player" };
        string hex = "00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 80 7F 00 00 80 7F 00 00 80 7F 00 00 80 FF";           // AoB cần quét
        string replace = "00 00 00 00 00 80 40 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 80 7F 00 00 80 7F 00 00 80 7F 00 00 80 FF";       // thay bằng bytes
        string restore = "00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 80 7F 00 00 80 7F 00 00 80 7F 00 00 80 FF";       // khôi phục lại
        bool showWindow = true;

        private string licenseKey = "";
        private bool isLoggedIn = false;
        private string loginMessage = "";
        private bool autoResetEsp = false;
        bool isInitialized = false;

        private string statusMessage = "";

        // Thêm biến toàn cục để chống spam nút
        private DateTime lastToggleTime = DateTime.MinValue;

        private void RenderImgui()
        {
            // Xử lý phím Insert không bị chớp
            if (IsKeyPressed(Keys.Insert) && DateTime.Now - lastToggleTime > TimeSpan.FromMilliseconds(200))
            {
                showMenu = !showMenu;
                lastToggleTime = DateTime.Now;
            }
           
            KeyAuth.init();


            // KHÔNG cần gọi RenderImgui() ở đây nữa



            // ❌ Không return nếu showMenu = false (ESP vẫn cần vẽ!)
            if (!showMenu) return;








            ApplyStyle(); // Gọi style dùng chung cho cả login và main
            ReplaceFont(fontpath, 15, FontGlyphRangeType.Vietnamese); // To hơn một chút

            ImGui.PushFont(FontManager.BigFont);

            if (!isLoggedIn)
            {
                ApplyStyle();
              //  ReplaceFont(fontpath, 14, FontGlyphRangeType.Vietnamese);
                ImGui.PushFont(FontManager.BigFont);

                Vector2 loginSize = new Vector2(360, 180);
                ImGui.SetNextWindowSize(loginSize, ImGuiCond.Always);
                ImGui.Begin("Ken_R7 | Auth Login", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse);

                // --- Tiêu đề ---
                string title = "Enter your License Key";
                float titleWidth = ImGui.CalcTextSize(title).X;
                ImGui.SetCursorPosX((ImGui.GetWindowSize().X - titleWidth) / 2f);
                ImGui.Text(title);

                ImGui.Spacing();

                // --- Kích thước và vị trí chung cho cả textbox và button ---
                float inputWidth = ImGui.GetWindowSize().X - 50;
                float inputX = (ImGui.GetWindowSize().X - inputWidth) / 2f;

                // --- Textbox ---
                ImGui.SetCursorPosX(inputX);
                ImGui.PushItemWidth(inputWidth); // CHỈNH: chiều rộng cố định
                ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 6f);
                ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(12, 8));
                ImGui.InputText("##license", ref licenseKey, 100, ImGuiInputTextFlags.AutoSelectAll);
                ImGui.PopStyleVar(2);
                ImGui.PopItemWidth(); // nhớ pop lại

                ImGui.Spacing();

                // --- Button Login ---
                ImGui.SetCursorPosX(inputX);
                if (ImGui.Button("Login", new Vector2(inputWidth, 36)))
                {
                    try
                    {
                        KeyAuth.license(licenseKey);
                        isLoggedIn = KeyAuth.response.success;
                        loginMessage = isLoggedIn ? "Login successful!" : "Invalid license key!";


                        // 👉 Khởi động Hotkey Thread
                        StartHotkeyThread();
                    }
                    catch (Exception ex)
                    {
                        loginMessage = $" Error: {ex.Message}";
                    }
                }

                // --- Kết quả ---
                if (!string.IsNullOrEmpty(loginMessage))
                {
                    ImGui.Spacing();
                    float msgWidth = ImGui.CalcTextSize(loginMessage).X;
                    ImGui.SetCursorPosX((ImGui.GetWindowSize().X - msgWidth) / 2f);
                    ImGui.TextColored(
                        loginMessage.StartsWith("✅") ? new Vector4(0.2f, 1f, 0.2f, 1f) : new Vector4(1f, 0.2f, 0.2f, 1f),
                        loginMessage
                    );
                }

                // --- Buy Key ---
                ImGui.Spacing();
                string buyText = "Buy Key: Inbox Ken_R7";
                float buyWidth = ImGui.CalcTextSize(buyText).X;
                ImGui.SetCursorPosX((ImGui.GetWindowSize().X - buyWidth) / 2f);
                ImGui.TextColored(new Vector4(0.7f, 0.7f, 0.7f, 1f), buyText);

                ImGui.End();
                ImGui.PopFont();
                return;
            }








            ImGuiStylePtr style = ImGui.GetStyle();
            // ReplaceFont(fontpath, 14, FontGlyphRangeType.Vietnamese);
            // Style
            style.WindowBorderSize = 0f;
            style.WindowRounding = 6f;
            style.FrameBorderSize = 0f;
            style.FrameRounding = 5f;
            style.GrabRounding = 4f;
            style.ScrollbarRounding = 6f;
            style.PopupRounding = 4f;
            style.ItemSpacing = new Vector2(10, 6);
            style.WindowPadding = new Vector2(10, 8);
            style.FramePadding = new Vector2(10, 6);

            // Màu nền đen và xám đậm
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.08f, 0.08f, 0.08f, 1f); // Gần như đen
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.10f, 0.10f, 0.10f, 1f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.12f, 0.12f, 0.12f, 1f);
            style.Colors[(int)ImGuiCol.Border] = new Vector4(0.20f, 0.20f, 0.20f, 1f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.13f, 0.13f, 0.13f, 1f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.18f, 0.18f, 0.18f, 1f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.22f, 0.22f, 0.22f, 1f);

            // Màu chữ trắng
            style.Colors[(int)ImGuiCol.Text] = new Vector4(1f, 1f, 1f, 1f); // Trắng rõ

            // Nút đen đậm, khi hover sáng nhẹ
            style.Colors[(int)ImGuiCol.Button] = new Vector4(0.15f, 0.15f, 0.15f, 1f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.25f, 0.25f, 0.25f, 1f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.20f, 0.20f, 0.20f, 1f);

            // Tab (giống như button)
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.12f, 0.12f, 0.12f, 1f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.20f, 0.20f, 0.20f, 1f);
            style.Colors[(int)ImGuiCol.TabActive] = new Vector4(0.18f, 0.18f, 0.18f, 1f);

            // Drop down, popup
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.12f, 0.12f, 0.12f, 1f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.10f, 0.10f, 0.10f, 1f);

            // Các thành phần khác nếu dùng
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(1f, 1f, 1f, 1f); // Dấu check trắng
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(1f, 1f, 1f, 0.5f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1f, 1f, 1f, 0.8f);



            ImGui.PushFont(FontManager.BigFont);
            ImGui.SetNextWindowSize(new Vector2(420, 320)); // ✅ Nhỏ gọn lại
            ImGui.Begin("Ken_R7 | Silent Aim", ImGuiWindowFlags.NoResize);

            string[] tabs = { "Aim", "Visual", "Settings", "Brutal", "Aimbot"};
            float buttonWidth = 80f;     // 👈 Giảm cho vừa
            float buttonHeight = 30f;
            float buttonSpacing = 8f;


            // Tính vị trí để canh giữa tab buttons
            float totalWidth = (buttonWidth * tabs.Length) + (buttonSpacing * (tabs.Length - 1));
            float windowWidth = ImGui.GetWindowSize().X;
            float offsetX = (windowWidth - totalWidth) / 2.0f;
            ImGui.SetCursorPosX(offsetX);

            for (int i = 0; i < tabs.Length; i++)
            {
                bool isActive = (selectedTab == i);

                // ✅ Màu nút đen đậm, hover nhẹ
                ImGui.PushStyleColor(ImGuiCol.Button, isActive ? new Vector4(0.10f, 0.10f, 0.10f, 1f) : new Vector4(0.06f, 0.06f, 0.06f, 1f));
                ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.15f, 0.15f, 0.15f, 1f));
                ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.12f, 0.12f, 0.12f, 1f));
                ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1f, 1f, 1f, 1f)); // Text trắng rõ

                ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 8f);
                ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(12, 8));
                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(buttonSpacing, 6));

                if (i > 0)
                    ImGui.SameLine();

                if (ImGui.Button(tabs[i], new Vector2(buttonWidth, buttonHeight)))
                    selectedTab = i;

                ImGui.PopStyleVar(3);
                ImGui.PopStyleColor(4);
            }

            ImGui.Separator();
            ImGui.BeginChild("Content", new Vector2(0, 0), ImGuiChildFlags.None); // ✅ BeginChild bọc tất cả tabs


            if (selectedTab == 0)
            {
               



                ImGui.Text("Aim Hacks Panel");

                ImGui.Spacing();
                ImGui.Separator();
                ImGui.Spacing();

                // 🔘 Silent Aim tổng
                ImGui.Checkbox("Enable Silent Aim", ref Config.SilentAimEnabled);

                if (Config.SilentAimEnabled)
                {
                    string[] silentModes = { "Silent Aim 360", "Silent Aim V2" };

                    ImGui.Text("Silent Aim Mode:");
                    if (ImGui.Combo("##silentaimmode", ref Config.SilentAimMode, silentModes, silentModes.Length))
                    {
                        // Đã chọn lại chế độ
                    }

                    ImGui.TextColored(
                        new Vector4(0.2f, 1f, 0.2f, 1f),
                        $"Current: {silentModes[Config.SilentAimMode]}"
                    );
                }
                else
                {
                    ImGui.TextColored(
                        new Vector4(1f, 0.2f, 0.2f, 1f),
                        "Silent Aim is OFF"
                    );
                }

                // 🔁 Logic xử lý bật/tắt chế độ
                if (Config.SilentAimEnabled)
                {
                    Config.Slient = (Config.SilentAimMode == 0);   // Silent 360
                    Config.Slient2 = (Config.SilentAimMode == 1);  // Silent V2
                }
                else
                {
                    Config.Slient = false;
                    Config.Slient2 = false;
                }

          
            



            ImGui.Spacing();





                ImGui.Separator();
                ImGui.Spacing();

                //  ImGui.Checkbox("AimLock", ref Config.AimLock);



                ImGui.SliderFloat("AimFov Size", ref Config.AimbotFOV, 0, 1000);


                //ImGui.Text("Telikill Range Power:");
                //ImGui.SliderFloat("##Range", ref Config.TeleportRange, 0, 100, "%.2f");


                //ImGui.Text("Telikill Range Power:");

                //ImGui.SliderFloat("##Range", ref Config.TeleportRange, 10, 100, "%.2f");



                ImGui.Checkbox("Ignore Knocked", ref Config.IgnoreKnocked);
                ImGui.Checkbox("No Recoil", ref Config.NoRecoil);



                ImGui.Spacing();





                ImGui.Separator();
                ImGui.Spacing();

                ImGui.Text("Misc Hack :");
                ImGui.Checkbox("Fast Reload", ref Config.FastReload);

                //     ImGui.Checkbox("Down Player", ref Config.down);
                //     ImGui.Checkbox("Fly Me", ref Config.flyme);
                ImGui.Checkbox("Teleport", ref Config.telekill);
                ImGui.Checkbox("Speed", ref Config.speed);

                ImGui.Checkbox("TeleKill", ref Config.proxtelekill);

                if (ImGui.CollapsingHeader("Telekill Settings"))
                {
                    ImGui.SliderFloat("Offset X", ref Config.TeleOffsetX, -5f, 5f, "%.2f");
                    ImGui.SliderFloat("Offset Y", ref Config.TeleOffsetY, -5f, 5f, "%.2f");
                    ImGui.SliderFloat("Offset Z", ref Config.TeleOffsetZ, -5f, 5f, "%.2f");
                }

                //     ImGui.Checkbox("Down Player", ref Config.down);
                //     ImGui.Text("Down Player Power :");

                //  ImGui.SliderFloat("##downspeed", ref Config.downSpeed, 0.1f, 5.0f, "%.2f");
                ImGui.Checkbox("Up Player", ref Config.UpPlayer);



            //    ImGui.Checkbox("Down Player", ref Config.down);

                ImGui.Text("Up Player Power :");

                ImGui.SliderFloat("##power", ref Config.test, 30 ,100, "%.3f");

                ImGui.Spacing();





                ImGui.Separator();
                ImGui.Spacing();






                ImGui.Text("Hotkey Hack :");
                ImGui.Spacing();

                // ---------------- SPEED HOTKEY ----------------
                ImGui.Text("Speed Keybind :");
                ImGui.SameLine();
                if (ImGui.Button(Config.WaitingForKeybindSp ? "Press a Key..." : Config.SpKeyLabel))
                {
                    Config.WaitingForKeybindSp = true;
                    Config.SpKeyLabel = "Press a Key...";
                }
                if (Config.WaitingForKeybindSp)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (KeyHelper.IsKeyDown(key))
                        {
                            if (key == Config.TelePortKey || key == Config.TeleKillKey)
                                Config.SpKeyLabel = "Key in use!";
                            else
                            {
                                Config.SpKey = key;
                                Config.SpKeyLabel = key.ToString();
                            }
                            Config.WaitingForKeybindSp = false;
                            Config.KeyAlreadyPressed = false;
                            break;
                        }
                    }
                }
                ImGui.TextColored(Config.speed ? new Vector4(0.2f, 0.8f, 0.3f, 1.0f) : new Vector4(0.8f, 0.2f, 0.2f, 1.0f), $"Speed Status: {(Config.speed ? "ON" : "OFF")}");

                // ---------------- TELEPORT HOTKEY ----------------
                ImGui.Text("Teleport Keybind :");
                ImGui.SameLine();
                if (ImGui.Button(Config.WaitingForKeybindTelePort ? "Press a Key..." : Config.TelePortKeyLabel))
                {
                    Config.WaitingForKeybindTelePort = true;
                    Config.TelePortKeyLabel = "Press a Key...";
                }
                if (Config.WaitingForKeybindTelePort)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (KeyHelper.IsKeyDown(key))
                        {
                            if (key == Config.SpKey || key == Config.TeleKillKey)
                                Config.TelePortKeyLabel = "Key in use!";
                            else
                            {
                                Config.TelePortKey = key;
                                Config.TelePortKeyLabel = key.ToString();
                            }
                            Config.WaitingForKeybindTelePort = false;
                            Config.KeyAlreadyPressed3 = false;
                            break;
                        }
                    }
                }
                ImGui.TextColored(Config.telekill ? new Vector4(0.2f, 0.8f, 0.3f, 1.0f) : new Vector4(0.8f, 0.2f, 0.2f, 1.0f), $"Teleport Status: {(Config.telekill ? "ON" : "OFF")}");

                // ---------------- TELEKILL HOTKEY ----------------
                ImGui.Text("TeleKill Keybind :");
                ImGui.SameLine();
                if (ImGui.Button(Config.WaitingForKeybindTeleKill ? "Press a Key..." : Config.TeleKillKeyLabel))
                {
                    Config.WaitingForKeybindTeleKill = true;
                    Config.TeleKillKeyLabel = "Press a Key...";
                }
                if (Config.WaitingForKeybindTeleKill)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (KeyHelper.IsKeyDown(key))
                        {
                            if (key == Config.SpKey || key == Config.TelePortKey)
                                Config.TeleKillKeyLabel = "Key in use!";
                            else
                            {
                                Config.TeleKillKey = key;
                                Config.TeleKillKeyLabel = key.ToString();
                            }
                            Config.WaitingForKeybindTeleKill = false;
                            Config.KeyAlreadyPressed4 = false;
                            break;
                        }
                    }
                }
                ImGui.TextColored(Config.proxtelekill ? new Vector4(0.2f, 0.8f, 0.3f, 1.0f) : new Vector4(0.8f, 0.2f, 0.2f, 1.0f), $"TeleKill Status: {(Config.proxtelekill ? "ON" : "OFF")}");

                // ---------------- UP PLAYER HOTKEY ----------------
                ImGui.Text("Up Player Keybind :");
                ImGui.SameLine();
                if (ImGui.Button(Config.WaitingForKeybindUpPlayer ? "Press a Key..." : Config.UpPlayerKeyLabel))
                {
                    Config.WaitingForKeybindUpPlayer = true;
                    Config.UpPlayerKeyLabel = "Press a Key...";
                }
                if (Config.WaitingForKeybindUpPlayer)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (KeyHelper.IsKeyDown(key))
                        {
                            if (key == Config.SpKey || key == Config.TelePortKey || key == Config.TeleKillKey)
                                Config.UpPlayerKeyLabel = "Key in use!";
                            else
                            {
                                Config.UpPlayerKey = key;
                                Config.UpPlayerKeyLabel = key.ToString();
                            }
                            Config.WaitingForKeybindUpPlayer = false;
                            Config.KeyAlreadyPressed6 = false;
                            break;
                        }
                    }
                }
                ImGui.TextColored(Config.UpPlayer ? new Vector4(0.2f, 0.8f, 0.3f, 1.0f) : new Vector4(0.8f, 0.2f, 0.2f, 1.0f), $"Up Player Status: {(Config.UpPlayer ? "ON" : "OFF")}");

                ImGui.PopFont();




            }

            else if (selectedTab == 1)
            {
                ImGui.Text("Visuals Panel");
                ImGui.Checkbox("ESP Line", ref Config.ESPLine);
                ImGui.SameLine();
                ImGui.ColorEdit4("ESP Line Color", ref lineColor, ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.NoInputs);
                {
                    Config.ESPLineColor = Color.FromArgb(
                        (int)(lineColor.W * 255),
                        (int)(lineColor.X * 255),
                        (int)(lineColor.Y * 255),
                        (int)(lineColor.Z * 255)
                    );
                }







                ImGui.Checkbox("ESP Box", ref Config.ESPBox);

                ImGui.SameLine();
                // Color picker when active

                ImGui.ColorEdit4("ESP Box Color", ref boxColor, ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.NoInputs);
                Config.ESPBoxColor = Color.FromArgb(
                    (int)(boxColor.W * 255),
                    (int)(boxColor.X * 255),
                    (int)(boxColor.Y * 255),
                    (int)(boxColor.Z * 255)
                );





                // Checkbox to enable/disable selected box


                //  bool espweapion = Config.ESPWeapon;
                //    if (ImGui.Checkbox("ESP Weapon", ref espweapion)) Config.ESPWeapon = espweapion;



                ImGui.Checkbox("ESP Health", ref Config.ESPHealth);
                ImGui.Checkbox("ESP Name", ref Config.ESPName);
                ImGui.Checkbox("ESP Distance", ref Config.ESPDistance);
                ImGui.Checkbox("ESP FillBox", ref Config.ESPFillBox);
                ImGui.SameLine();
                ImGui.ColorEdit4("ESP Box Fill Color", ref fillboxColor, ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.NoInputs);
                {
                    Config.ESPFillBoxColor = Color.FromArgb(
                        (int)(fillboxColor.W * 255),
                        (int)(fillboxColor.X * 255),
                        (int)(fillboxColor.Y * 255),
                        (int)(fillboxColor.Z * 255)
                    );
                }



                ImGui.Checkbox("ESP Weapon", ref Config.ESPWeapon);


                ImGui.Checkbox("ESP Skeleton", ref Config.ESPSkeleton);
                ImGui.ColorEdit4("ESP  Skeleton Color", ref skeletonColor, ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.NoInputs);
                {
                    Config.ESPSkeletonColor = Color.FromArgb(
                        (int)(skeletonColor.W * 255),
                        (int)(skeletonColor.X * 255),
                        (int)(skeletonColor.Y * 255),
                        (int)(skeletonColor.Z * 255)
                    );
                }
                ImGui.Checkbox("ESP Cross Shair", ref Config.CrosshairEnabled);
                ImGui.ColorEdit4("ESP Cross Shair Color", ref crossColor, ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.NoInputs);
                {
                    Config.CrosshairColor = Color.FromArgb(
                        (int)(fillboxColor.W * 255),
                        (int)(fillboxColor.X * 255),
                        (int)(fillboxColor.Y * 255),
                        (int)(fillboxColor.Z * 255)
                    );
                }

                ImGui.SliderFloat("Cross Shair Size", ref Config.CrosshairSize, 0, 100);

                ImGui.SliderFloat("Cross Shair Speed", ref Config.CrosshairRotationSpeed, 0, 100);




                //     ImGui.Checkbox("ESP Distance", ref Config.ESPDistance);

                //     ImGui.Checkbox("ESP Mini Map", ref Config.minimap);
                if (ImGui.Button("Update Entities"))
                {
                    UpdateEntities();
                    Core.Entities = new();
                    InternalMemory.Cache = new();
                }

                ImGui.SameLine(); // Đặt ở đây mới có tác dụng

                if (ImGui.Checkbox("Auto Reset ESP", ref autoResetEsp))
                {
                    if (autoResetEsp)
                    {
                        StartEspAutoReset(); // Hàm của bạn
                    }
                    else
                    {
                        espResetToken.Cancel(); // Hủy reset tự động
                    }
                }


            }


            else if (selectedTab == 2)
            {
                ImGui.Text("Settings Panel");
                ImGui.Checkbox("Stream Mode", ref Config.StreamMode);
                bool close = false;
                ImGui.Checkbox("Close Panel", ref close);
                {
                    if (close)
                    {
                        KillProcess("HD-Adb");
                        Task.Delay(2000);
                        KillProcess("HD-Player");
                        Task.Delay(1000);
                        Environment.Exit(0);
                    }
                }


                ImGui.Spacing();
                ImGui.Separator();
                ImGui.Spacing();





                // 👇 Cấu hình chung
                float buttonWidth1 = 280f;
                float buttonHeight1 = 40f;
                float windowWidth1 = ImGui.GetWindowSize().X;
                float centerX = (windowWidth1 - buttonWidth1) / 2f;

                // 👇 Style nút: xanh dương
                ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.20f, 0.50f, 0.60f, 1.0f));
                ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.25f, 0.60f, 0.70f, 1.0f));
                ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.18f, 0.45f, 0.55f, 1.0f));
                ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 8f);
                ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(12, 10));

                // 👇 Button 1
                ImGui.SetCursorPosX(centerX);
                if (ImGui.Button("RUN ANTICHEAT Ken_R7", new Vector2(buttonWidth1, buttonHeight1)))
                {
                    AntiCheat();
                    statusMessage = " Anticheat Ken_R7 Is Activing  !";
                }

                // 👇 Khoảng cách giữa 2 nút
                ImGui.Dummy(new Vector2(0, 8));


                // 👇 Pop style
                ImGui.PopStyleVar(2);
                ImGui.PopStyleColor(3);





























                // 👇 Hiển thị status dưới nút (nếu có)
                if (!string.IsNullOrEmpty(statusMessage))
                {
                    ImGui.Spacing();
                    float msgWidth = ImGui.CalcTextSize(statusMessage).X;
                    ImGui.SetCursorPosX((windowWidth1 - msgWidth) / 2f);

                    ImGui.TextColored(new Vector4(0.2f, 1f, 0.2f, 1f), statusMessage);
                }

                // Button colors


























                // Dòng 1: Dev Panel
                string devText = "Dev Panel : Ken_R7";
                float textWidth5 = ImGui.CalcTextSize(devText).X;
                float windowWidth5 = ImGui.GetWindowSize().X;
                ImGui.SetCursorPosX((windowWidth5 - textWidth5) / 2f);
                ImGui.Text(devText);


                // Line 2: Press Insert to toggle menu
                string insertHint = "Bấm Insert Để Bật/Tắt Panel";
                float textWidth6 = ImGui.CalcTextSize(insertHint).X;
                ImGui.SetCursorPosX((windowWidth5 - textWidth6) / 2f);
                ImGui.TextColored(new Vector4(0.7f, 0.7f, 0.7f, 1f), insertHint); // Light gray color


            }




            else if (selectedTab == 3)
            {
                ImGui.Text("Brtutal Panel :");
               

              
                ImGui.Separator();
             
                ImGui.Checkbox("No Reload", ref enablePatch);

                if (enablePatch)
                {
                    ImGui.TextColored(new Vector4(0.0f, 1.0f, 0.0f, 1.0f), $"Status : {patchStatus}");
                }
                else
                {
                    ImGui.TextColored(new Vector4(1.0f, 0.5f, 0.0f, 1.0f), $"Status : {patchStatus}");
                }

                // Thực hiện patch khi người dùng bật
                if (enablePatch && patchStatus == "Not yet done.")
                {
                    patchStatus = "Processing...";

                    Task.Run(async () =>
                    {
                        string hex = "99 10 A0 E3 6D 00 00 EB 00 0A";
                        string replace = "99 10 A0 E3 00 00 00 EB 00 0A";
                        string[] processNames = { "HD-Player" };

                        bool success = Gay.SetProcess(processNames);

                        if (!success)
                        {
                            patchStatus = "Process not found!";
                            enablePatch = false;
                            return;
                        }

                        IEnumerable<long> result = await Gay.AoBScan(hex);

                        if (result.Any())
                        {
                            foreach (long addr in result)
                            {
                                Gay.AobReplace(addr, replace);
                            }

                            Console.Beep(800, 400);
                            patchStatus = "Success";
                        }
                        else
                        {
                            patchStatus = "AoB template not found.";
                        }
                    });
                }
                //


                ImGui.Checkbox("Black Sky", ref aobPatch);

                if (aobPatch)
                {
                    ImGui.TextColored(new Vector4(0.0f, 1.0f, 0.0f, 1.0f), $"Status : {statusAob}");
                }
                else
                {
                    ImGui.TextColored(new Vector4(1.0f, 0.5f, 0.0f, 1.0f), $"Status : {statusAob}");
                }

                // Thực hiện patch khi người dùng bật
                if (aobPatch && statusAob == "Not yet done.")
                {
                    statusAob = "Processing...";

                    Task.Run(async () =>
                    {
                        string hex = "F0 8F BD E8 A4 70 7D 3F 3A CD 13 3F 0A D7 23 3C BD 37 86 35";
                        string replace = "F0 8F BD E8 A4 70 7D 3F 3A CD 13 3F 0A D7 23 3C BD 37 86 B5";
                        string[] processNames = { "HD-Player" };

                        bool success = Gay.SetProcess(processNames);

                        if (!success)
                        {
                            statusAob = "Process not found!";
                            aobPatch = false;
                            return;
                        }

                        IEnumerable<long> result = await Gay.AoBScan(hex);

                        if (result.Any())
                        {
                            foreach (long addr in result)
                            {
                                Gay.AobReplace(addr, replace);
                            }

                            Console.Beep(800, 400);
                            statusAob = "Success";
                        }
                        else
                        {
                            statusAob = "AoB template not found.";
                        }
                    });
                }
                //

                ImGui.Checkbox("Glitch Fire", ref noRecoil);

                if (noRecoil)
                {
                    ImGui.TextColored(new Vector4(0.0f, 1.0f, 0.0f, 1.0f), $"Status : {statusRecoil}");
                }
                else
                {
                    ImGui.TextColored(new Vector4(1.0f, 0.5f, 0.0f, 1.0f), $"Status : {statusRecoil}");
                }

                // Thực hiện patch khi người dùng bật
                if (noRecoil && statusRecoil == "Not yet done.")
                {
                    statusRecoil = "Processing...";

                    Task.Run(async () =>
                    {
                        string hex = "C0 3F 00 00 00 3F 00 00 80 3F 00 00 00 40";
                        string replace = "00 00 00 00 00 F8 00 00 80 00 00 00 00 40";
                        string[] processNames = { "HD-Player" };

                        bool success = Gay.SetProcess(processNames);

                        if (!success)
                        {
                            statusRecoil = "Process not found!";
                            noRecoil = false;
                            return;
                        }

                        IEnumerable<long> result = await Gay.AoBScan(hex);

                        if (result.Any())
                        {
                            foreach (long addr in result)
                            {
                                Gay.AobReplace(addr, replace);
                            }

                            Console.Beep(800, 400);
                            statusRecoil = "Success";
                        }
                        else
                        {
                            statusRecoil = "AoB template not found.";
                        }
                    });
                }
                //
                ImGui.Checkbox("Vision 5x", ref godMode);

                if (godMode)
                {
                    ImGui.TextColored(new Vector4(0.0f, 1.0f, 0.0f, 1.0f), $"Status : {statusGod}");
                }
                else
                {
                    ImGui.TextColored(new Vector4(1.0f, 0.5f, 0.0f, 1.0f), $"Status : {statusGod}");
                }

                // Thực hiện patch khi người dùng bật
                if (godMode && statusGod == "Not yet done.")
                {
                    statusGod = "Processing...";

                    Task.Run(async () =>
                    {
                        string hex = "00 00 B4 43 DB 0F 49 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30";
                        string replace = "00 00 B4 43 00 00 A0 40 10 2A 00 EE 00 10 80 E5 10 3A 01 EE 14 10 80 E5 00 2A 30 EE 00 10 00 E3 41 3A 30 EE 80 1F 4B E3 01 0A 30";
                        string[] processNames = { "HD-Player" };

                        bool success = Gay.SetProcess(processNames);

                        if (!success)
                        {
                            statusGod = "Process not found!";
                            godMode = false;
                            return;
                        }

                        IEnumerable<long> result = await Gay.AoBScan(hex);

                        if (result.Any())
                        {
                            foreach (long addr in result)
                            {
                                Gay.AobReplace(addr, replace);
                            }

                            Console.Beep(800, 400);
                            statusGod = "Success";
                        }
                        else
                        {
                            statusGod = "AoB template not found.";
                        }
                    });


















                    }

                //







                ImGui.Checkbox("Speed", ref invisibility);

                if (invisibility)
                {
                    ImGui.TextColored(new Vector4(0.0f, 1.0f, 0.0f, 1.0f), $"Status : {statusInvis}");
                }
                else
                {
                    ImGui.TextColored(new Vector4(1.0f, 0.5f, 0.0f, 1.0f), $"Status : {statusInvis}");
                }

                // Thực hiện patch khi người dùng bật
                if (invisibility && statusInvis == "Not yet done.")
                {
                    statusInvis = "Processing...";

                    Task.Run(async () =>
                    {
                        string hex = "40 02 2B 07 3D 02 2B 07 3D 02 2B 07 3D 00 00 00";
                        string replace = "40 02 2B 9B 3C 02 2B 9B 3C 02 2B 07 3D 00 00 00";
                        string[] processNames = { "HD-Player" };

                        bool success = Gay.SetProcess(processNames);

                        if (!success)
                        {
                            statusInvis = "Process not found!";
                            invisibility = false;
                            return;
                        }

                        IEnumerable<long> result = await Gay.AoBScan(hex);

                        if (result.Any())
                        {
                            foreach (long addr in result)
                            {
                                Gay.AobReplace(addr, replace);
                            }

                            Console.Beep(800, 400);
                            statusInvis = "Success";
                        }
                        else
                        {
                            statusInvis = "AoB template not found.";
                        }
                    });
                }





                }
          

            // ✅ EndChild sau tất cả các tab
            ImGui.EndChild();
            

            ImGui.End();


        }
     

        private float statusDisplayTime = 0f;
        private const float STATUS_DISPLAY_DURATION = 3f;

        private int currentHotkey = -1;
        private bool waitingForHotkey = false;
        private bool hotkeyPressedLastFrame = false;

        private int secondHotkey = -1;
        private bool waitingForSecondHotkey = false;
        private bool secondHotkeyPressedLastFrame = false;

        private int thirdHotkey = -1;
        private bool waitingForThirdHotkey = false;
        private bool thirdHotkeyPressedLastFrame = false;

        private bool isStreamMode = false;
        private bool streamModeKeyPressedLastFrame = false;

        private bool beepEnabled = true;
   
        // Khai báo ở ngoài hàm Render, ví dụ: static hoặc global trong class
        static bool enablePatch = false;
        static string patchStatus = "Not yet done."; // Trạng thái hiển thị

        // Biến trạng thái - khai báo ngoài hàm render
        static bool aobPatch = false;
        static bool noRecoil = false;
        static bool godMode = false;
        static bool invisibility = false;

        static string statusAob = "Not yet done.";
        static string statusRecoil = "Not yet done.";
        static string statusGod = "Not yet done.";
        static string statusInvis = "Not yet done.";
        void HandleHotkeys()
        {
            // Speed Toggle
            if (KeyHelper.IsKeyDown(Config.SpKey))
            {
                if (!Config.KeyAlreadyPressed)
                {
                    Config.speed = !Config.speed;
                    Config.KeyAlreadyPressed = true;
                }
            }
            else Config.KeyAlreadyPressed = false;

            // Teleport Toggle
            if (KeyHelper.IsKeyDown(Config.TelePortKey))
            {
                if (!Config.KeyAlreadyPressed3)
                {
                    Config.telekill = !Config.telekill;
                    Config.KeyAlreadyPressed3 = true;
                }
            }
            else Config.KeyAlreadyPressed3 = false;

            // TeleKill Toggle
            if (KeyHelper.IsKeyDown(Config.TeleKillKey))
            {
                if (!Config.KeyAlreadyPressed4)
                {
                    Config.proxtelekill = !Config.proxtelekill;
                    Config.KeyAlreadyPressed4 = true;
                }
            }
            else Config.KeyAlreadyPressed4 = false;

            // Up Toggle
            if (KeyHelper.IsKeyDown(Config.UpPlayerKey))
            {
                if (!Config.KeyAlreadyPressed6)
                {
                    Config.UpPlayer = !Config.UpPlayer;
                    Config.KeyAlreadyPressed6 = true;
                }
            }
            else Config.KeyAlreadyPressed6 = false;
        }


        private bool waitingForFreezeKey = false;
        private bool waitingForGhostKey = false;
        private bool waitingForTelekillKey = false;

        private void ApplyStyle()
        {
            ImGuiStylePtr style = ImGui.GetStyle();

            style.WindowBorderSize = 0f;
            style.WindowRounding = 6f;
            style.FrameBorderSize = 0f;
            style.FrameRounding = 5f;
            style.GrabRounding = 4f;
            style.ScrollbarRounding = 6f;
            style.PopupRounding = 4f;
            style.ItemSpacing = new Vector2(10, 6);
            style.WindowPadding = new Vector2(10, 8);
            style.FramePadding = new Vector2(10, 6);

            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.08f, 0.08f, 0.08f, 1f);
            style.Colors[(int)ImGuiCol.Text] = new Vector4(1f, 1f, 1f, 1f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(0.15f, 0.15f, 0.15f, 1f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.25f, 0.25f, 0.25f, 1f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.20f, 0.20f, 0.20f, 1f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.10f, 0.10f, 0.10f, 1f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.12f, 0.12f, 0.12f, 1f);
            style.Colors[(int)ImGuiCol.Border] = new Vector4(0.20f, 0.20f, 0.20f, 1f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.13f, 0.13f, 0.13f, 1f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.18f, 0.18f, 0.18f, 1f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.22f, 0.22f, 0.22f, 1f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.12f, 0.12f, 0.12f, 1f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.20f, 0.20f, 0.20f, 1f);
            style.Colors[(int)ImGuiCol.TabActive] = new Vector4(0.18f, 0.18f, 0.18f, 1f);
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.12f, 0.12f, 0.12f, 1f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.10f, 0.10f, 0.10f, 1f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(1f, 1f, 1f, 1f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(1f, 1f, 1f, 0.5f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1f, 1f, 1f, 0.8f);
        }
       
     
       

   
      
        private void DrawMinimap()
        {
            const int detectionRange = 250;
            const float baseRadius = 100f;
            const float playerMarkerSize = 5.0f;
            const float enemyMarkerSize = 4.0f;
            const float margin = 20f;

            float minimapRadius = baseRadius * (detectionRange / 250f);
            Vector2 minimapCenter = new Vector2(margin + minimapRadius, Core.Height - minimapRadius - margin);

            float cameraYaw = -GetCameraYaw(); // Negative to match world rotation
            float cosYaw = MathF.Cos(cameraYaw);
            float sinYaw = MathF.Sin(cameraYaw);

            ImDrawListPtr drawList = ImGui.GetBackgroundDrawList();

            // Background + Border
            drawList.AddCircleFilled(minimapCenter, minimapRadius, ColorToUint32(Color.FromArgb(150, 15, 15, 15)));
            drawList.AddCircle(minimapCenter, minimapRadius, ColorToUint32(Color.White), 64, 1.0f);

            // Grid Circles
            uint gridColor = ColorToUint32(Color.FromArgb(40, 255, 255, 255));
            for (int i = 1; i <= 3; i++)
            {
                drawList.AddCircle(minimapCenter, minimapRadius * i / 3f, gridColor, 64, 0.5f);
            }

            // Compass letters (rotated with yaw)
            string[] compass = { "N", "E", "S", "W" };
            for (int i = 0; i < 4; i++)
            {
                float angle = -MathF.PI / 2 + i * MathF.PI / 2 + cameraYaw; // rotate compass labels
                Vector2 pos = minimapCenter + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * (minimapRadius - 12f);
                drawList.AddText(pos - new Vector2(4, 4), ColorToUint32(Color.White), compass[i]);
            }

            // Player center + arrow (fixed upward)
            drawList.AddCircleFilled(minimapCenter, playerMarkerSize, ColorToUint32(Color.Cyan));
            Vector2 arrowTip = minimapCenter + new Vector2(0, -1) * (playerMarkerSize + 6f);
            drawList.AddLine(minimapCenter, arrowTip, ColorToUint32(Color.Cyan), 1.5f);

            // Count detected enemies
            int detectedEnemies = Core.Entities.Values.Count(e =>
                !e.IsDead && Vector3.Distance(Core.LocalMainCamera, e.Head) <= detectionRange);

            // Text above minimap
            string detectedText = $"Detected: {detectedEnemies}";
           ImGui.PushFont(FontManager.SmallFont);
            Vector2 textSize = ImGui.CalcTextSize(detectedText);
            Vector2 textPos = new Vector2(minimapCenter.X - textSize.X / 2, minimapCenter.Y - minimapRadius - textSize.Y - 10);

            // Shadow text
            uint shadowColor = ColorToUint32(Color.FromArgb(180, 0, 0, 0));
            Vector2[] offsets = { new Vector2(-1, -1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(1, 1) };
            foreach (var offset in offsets)
                drawList.AddText(textPos + offset, shadowColor, detectedText);

            drawList.AddText(textPos, ColorToUint32(Color.White), detectedText);
            ImGui.PopFont();

            // Draw enemies on minimap
            foreach (var entity in Core.Entities.Values)
            {
                if (entity.IsDead) continue;

                float distance = Vector3.Distance(Core.LocalMainCamera, entity.Head);
                if (distance > detectionRange) continue;

                Vector3 relative = entity.Head - Core.LocalMainCamera;

                // Apply map rotation
                float rotatedX = relative.X * cosYaw - relative.Z * sinYaw;
                float rotatedY = relative.X * sinYaw + relative.Z * cosYaw;

                float scale = minimapRadius / detectionRange;
                Vector2 minimapPos = new Vector2(
                    minimapCenter.X + rotatedX * scale,
                    minimapCenter.Y - rotatedY * scale
                );

                if (Vector2.Distance(minimapCenter, minimapPos) <= minimapRadius)
                {
                    uint color = entity.IsKnown
                        ? (entity.IsKnocked ? ColorToUint32(Color.Yellow) : ColorToUint32(Color.Red))
                        : ColorToUint32(Color.Blue);

                    drawList.AddCircleFilled(minimapPos, enemyMarkerSize, color);
                }
            }
        }

        public void KillProcess(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }
        }

        private float rotationAngle = 0f; // Rotation angle variable
        private void DrawShurikenCrosshair()
        {
            if (!Config.CrosshairEnabled) return;

            var drawList = ImGui.GetBackgroundDrawList();
            Vector2 center = new Vector2(Core.Width / 2f, Core.Height / 2f);
            float radius = Config.CrosshairSize;
            uint color = ColorToUint32(Config.CrosshairColor);

            // Number of blades
            int bladeCount = 4;
            float angleStep = 360f / bladeCount;

            // Draw each blade
            for (int i = 0; i < bladeCount; i++)
            {
                float angle = rotationAngle + i * angleStep;
                float angleInRadians = MathF.PI / 180f * angle;

                // Calculate the blade's points
                Vector2 point1 = new Vector2(
                    center.X + MathF.Cos(angleInRadians) * radius,
                    center.Y + MathF.Sin(angleInRadians) * radius
                );

                Vector2 point2 = new Vector2(
                    center.X + MathF.Cos(angleInRadians + MathF.PI / 6) * (radius / 2),
                    center.Y + MathF.Sin(angleInRadians + MathF.PI / 6) * (radius / 2)
                );

                Vector2 point3 = new Vector2(
                    center.X + MathF.Cos(angleInRadians - MathF.PI / 6) * (radius / 2),
                    center.Y + MathF.Sin(angleInRadians - MathF.PI / 6) * (radius / 2)
                );

                // Draw the blade
                drawList.AddTriangleFilled(point1, point2, point3, color);
            }

            // Increment rotation angle for animation
            rotationAngle += Config.CrosshairRotationSpeed;
            if (rotationAngle >= 360f) rotationAngle -= 360f;
        }

        private void DrawSkeleton(Entity entity, uint skeletonColor)
        {
            var drawList = ImGui.GetForegroundDrawList();
            // Lấy tọa độ đầu trên màn hình
            var headScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);

            // Tạo màu động dựa trên thời gian
            float time = (float)ImGui.GetTime();
          //  Color dynamicColor = ColorFromHSV(time * 100 % 360, 1.0f, 1.0f);
          ////  uint wukongColor = ColorToUint32(dynamicColor);





            var vList = ImGui.GetForegroundDrawList();
            void DrawBone(Vector3 start, Vector3 end, uint color)
            {
                var startPos = W2S.WorldToScreen(Core.CameraMatrix, start, Core.Width, Core.Height);
                var endPos = W2S.WorldToScreen(Core.CameraMatrix, end, Core.Width, Core.Height);
                if (startPos.X > 1 && startPos.Y > 1 && endPos.X > 1 && endPos.Y > 1)
                {
                    vList.AddLine(new Vector2(startPos.X, startPos.Y), new Vector2(endPos.X, endPos.Y), color, 1.5f);
                }
            }

            // Desenhar os principais ossos do esqueleto
            DrawBone(entity.Head, entity.Neck, skeletonColor);
            DrawBone(entity.Neck, entity.Pelvis, skeletonColor);

            // Ombros e braços
            DrawBone(entity.Neck, entity.ShoulderR, skeletonColor);
            DrawBone(entity.Neck, entity.ShoulderL, skeletonColor);
            DrawBone(entity.ShoulderR, entity.ElbowR, skeletonColor);
            DrawBone(entity.ElbowR, entity.HandR, skeletonColor);
            DrawBone(entity.ShoulderL, entity.ElbowL, skeletonColor);
            DrawBone(entity.ElbowL, entity.HandL, skeletonColor);

            // Pernas
            DrawBone(entity.Pelvis, entity.FootR, skeletonColor);
            DrawBone(entity.Pelvis, entity.FootL, skeletonColor);
        }


        private float GetCameraYaw()
        {
            return MathF.Atan2(Core.CameraMatrix.M31, Core.CameraMatrix.M33);
        }


  
        void DrawFillBox(Vector2 position, float width, float height, float alpha)
        {
            uint fillColor = ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, alpha));
            ImGui.GetForegroundDrawList().AddRectFilled(
                new Vector2(position.X, position.Y),
                new Vector2(position.X + width, position.Y + height),
                fillColor
            );
        }

        private void DrawFOVCircle(float centerX, float centerY, float radius, Color color)
        {
            var drawList = ImGui.GetForegroundDrawList();
            drawList.AddCircle(new Vector2(centerX, centerY), radius, ColorToUint32(color), 30, 2.0f);
        }

        public void DrawCorneredBox(float X, float Y, float W, float H, uint color, float thickness)
        {
            var vList = ImGui.GetForegroundDrawList();

            float lineW = W / 3;
            float lineH = H / 3;

            vList.AddLine(new Vector2(X, Y), new Vector2(X + W, Y), color, thickness); // Đỉnh
            vList.AddLine(new Vector2(X, Y), new Vector2(X, Y + H), color, thickness); // Trái
            vList.AddLine(new Vector2(X + W, Y), new Vector2(X + W, Y + H), color, thickness); // Phải
            vList.AddLine(new Vector2(X, Y + H), new Vector2(X + W, Y + H), color, thickness); // Dưới
        }
     







        static uint ColorToUint32(Color color)
        {
            return ImGui.ColorConvertFloat4ToU32(new Vector4(
                color.R / 255.0f,
                color.G / 255.0f,
                color.B / 255.0f,
                color.A / 255.0f));
        }


        void CreateHandle()
        {
            if (Config.StreamMode)
            {
                SetWindowDisplayAffinity(hWnd, WDA_EXCLUDEFROMCAPTURE);
            }
            else
            {
                SetWindowDisplayAffinity(hWnd, WDA_NONE);
            }

            RECT rect;
            GetWindowRect(Core.Handle, out rect);
            int x = rect.Left;
            int y = rect.Top;
            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;
            ImGui.SetWindowSize(new Vector2((float)width, (float)height));
            ImGui.SetWindowPos(new Vector2((float)x, (float)y));
            Size = new Size(width, height);
            Position = new Point(x, y);
            Core.Width = width;
            Core.Height = height;
          
        }
        Vector4 HsvToRgb(float h, float s, float v)
        {
            int i = (int)(h * 6);
            float f = h * 6 - i;
            float p = v * (1 - s);
            float q = v * (1 - f * s);
            float t = v * (1 - (1 - f) * s);

            float r = 0, g = 0, b = 0;
            switch (i % 6)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                case 5: r = v; g = p; b = q; break;
            }

            return new Vector4(r, g, b, 1f);
        }


        public void DrawHealthBarHorizontal(short health, short maxHealth, float X, float Y, float width)
        {
            var vList = ImGui.GetForegroundDrawList();
            float healthPercentage = (float)health / maxHealth;
            float barWidth = width * healthPercentage;
            uint barColor;

            if (healthPercentage > 0.8f)
            {
                barColor = ColorToUint32(Color.GreenYellow);
            }
            else if (healthPercentage > 0.4f)
            {
                barColor = ColorToUint32(Color.Orange);
            }
            else
            {
                barColor = ColorToUint32(Color.Red);
            }

            vList.AddRectFilled(new Vector2(X, Y), new Vector2(X + width, Y + 4), 0x90000000);

            vList.AddRectFilled(new Vector2(X, Y), new Vector2(X + barWidth, Y + 4), barColor);
        }

        public void DrawCorneredBox2(float X, float Y, float W, float H, uint color, float thickness)
        {
            var vList = ImGui.GetForegroundDrawList();

            float lineW = W / 3;
            float lineH = H / 3;

            vList.AddRectFilled(new Vector2(X, Y), new Vector2(X + W, Y + H), color & 0x00FFFFFF); // Đặt alpha thành 0 để có độ trong suốt




            vList.AddLine(new Vector2(X, Y - thickness / 2), new Vector2(X, Y + lineH), color, thickness);
            vList.AddLine(new Vector2(X - thickness / 2, Y), new Vector2(X + lineW, Y), color, thickness);
            vList.AddLine(new Vector2(X + W - lineW, Y), new Vector2(X + W + thickness / 2, Y), color, thickness);
            vList.AddLine(new Vector2(X + W, Y - thickness / 2), new Vector2(X + W, Y + lineH), color, thickness);
            vList.AddLine(new Vector2(X, Y + H - lineH), new Vector2(X, Y + H + thickness / 2), color, thickness);
            vList.AddLine(new Vector2(X - thickness / 2, Y + H), new Vector2(X + lineW, Y + H), color, thickness);
            vList.AddLine(new Vector2(X + W - lineW, Y + H), new Vector2(X + W + thickness / 2, Y + H), color, thickness);
            vList.AddLine(new Vector2(X + W, Y + H - lineH), new Vector2(X + W, Y + H + thickness / 2), color, thickness);
        }

        void DrawHealthBar(short health, short maxHealth, float X, float Y, float height, float width)
        {
            var drawList = ImGui.GetForegroundDrawList();
            float hpPercent = Math.Clamp((float)health / maxHealth, 0f, 1f);
            float barWidth = width * hpPercent;

            // Màu nền (đen, mờ)
            var bgColor = new Vector4(0f, 0f, 0f, 0.5f);
            drawList.AddRectFilled(new Vector2(X, Y), new Vector2(X + width, Y + height),
                ImGui.ColorConvertFloat4ToU32(bgColor), 3f);

            // Màu thanh máu (chuyển màu đẹp hơn)
            Vector4 barColor;
            if (hpPercent > 0.75f)
                barColor = new Vector4(0.0f, 1.0f, 0.0f, 1f); // Xanh lá
            else if (hpPercent > 0.5f)
                barColor = Vector4.Lerp(new Vector4(1f, 1f, 0f, 1f), new Vector4(0f, 1f, 0f, 1f), (hpPercent - 0.5f) * 4);
            else if (hpPercent > 0.25f)
                barColor = Vector4.Lerp(new Vector4(1f, 0.5f, 0f, 1f), new Vector4(1f, 1f, 0f, 1f), (hpPercent - 0.25f) * 4);
            else
                barColor = new Vector4(1f, 0f, 0f, 1f); // Đỏ

            drawList.AddRectFilled(new Vector2(X, Y), new Vector2(X + barWidth, Y + height),
                ImGui.ColorConvertFloat4ToU32(barColor), 3f);

            // Viền đen mờ
            drawList.AddRect(new Vector2(X, Y), new Vector2(X + width, Y + height),
                ImGui.ColorConvertFloat4ToU32(new Vector4(0f, 0f, 0f, 1f)), 3f, ImDrawFlags.None, 1.0f);

            // Vẽ chữ nhỏ phía bên phải
            string healthText = $"{health}/{maxHealth}";
            Vector2 textSize = ImGui.CalcTextSize(healthText);
            float textX = X + width + 6f;
            float textY = Y + (height - textSize.Y) / 2;

            drawList.AddText(new Vector2(textX, textY),
                ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.9f)), healthText);
        }

        public void DrawFullBox(float X, float Y, float W, float H, uint color, float alpha)
        {
            var vList = ImGui.GetForegroundDrawList();

            // Vẽ hộp filled box với màu sắc và độ trong suốt
            vList.AddRectFilled(new Vector2(X, Y), new Vector2(X + W, Y + H), color & 0x00FFFFFF | ((uint)(alpha * 255) << 24));
        }
        private void DisplayEnemyCount(float windowWidth, float windowHeight)
        {
            var drawList = ImGui.GetForegroundDrawList();

          //  string enemyCountText = $"{EnemyCount}";


            // Đặt kích thước font lớn hơn
            float fontSize = 220.0f; // Kích thước chữ lớn hơn
            ImGui.GetIO().FontGlobalScale = fontSize / ImGui.GetFontSize();
            // Tính toán kích thước chữ
         //   Vector2 textSize = ImGui.CalcTextSize(enemyCountText);

            // Tính toán vị trí để căn giữa
        //    float x = (windowWidth - textSize.X) / 2;
            // Nâng chữ lên trên giữa một chút
            float y = 40; // Điều chỉnh giá trị 100 tùy theo yêu cầu của bạn

            // Chỉnh sửa màu sắc chữ
            uint textColor = ColorToUint32(Color.White);

            // Hiển thị chữ trên màn hình
         //   drawList.AddText(new Vector2(x, y), textColor, enemyCountText);

            // Khôi phục kích thước font gốc
            ImGui.GetIO().FontGlobalScale = 1.0f;
        }
    }
}
