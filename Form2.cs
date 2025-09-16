using Client;
using Guna.UI2.WinForms;
using Memory;
using SmartMewwxSeww;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Color = System.Drawing.Color;

namespace AotForms
{
    public partial class Form2 : Form
    {
        Mem m = new Mem();

        private System.Timers.Timer animationTimer;
        private Point targetLocation;
        private bool isAnimating = false;
        private const int ANIMATION_SPEED = 15; // Tốc độ animation (cao hơn để mượt hơn)
        private float easingFactor = 0.15f; // Hệ số làm mượt (0.1 - 0.2 là tốt nhất)
        private DateTime lastFrameTime;
        private float deltaTime;

        static SmartxSewww Gay = new SmartxSewww();
        public Form2()
        {
            InitializeComponent();
            trackBarFloatClient4.Event_0 += TrackBarFloat4_ValueChanged;

            // Sự kiện khi cuộn (scroll)
            trackBarFloatClient4.Event_1 += TrackBarFloat4_Scroll;

            trackBarFloatClient5.Event_0 += TrackBarFloat5_ValueChanged;

            // Sự kiện khi cuộn (scroll)
            trackBarFloatClient5.Event_1 += TrackBarFloat5_Scroll;

            trackBarFloatClient6.Event_0 += TrackBarFloat6_ValueChanged;

            // Sự kiện khi cuộn (scroll)
            trackBarFloatClient6.Event_1 += TrackBarFloat6_Scroll;

            // Sự kiện khi cuộn (scroll)


            // Sự kiện khi cuộn (scroll)


        }
        private void TrackBarFloat4_ValueChanged(object sender, EventArgs e)
        {
            var cossize = trackBarFloatClient4.Value;
            Config.CrosshairRotationSpeed = cossize;

            label56.Text = $"{cossize}";
        }

        private void TrackBarFloat4_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }




        private void TrackBarFloat3_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }



        private void TrackBarFloat1_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }

        private void TrackBarFloat5_ValueChanged(object sender, EventArgs e)
        {
            var cossfov = trackBarFloatClient5.Value;
            Config.CrosshairSize = cossfov;

            label44.Text = $"{cossfov}";
        }

        private void TrackBarFloat5_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }


        private void TrackBarFloat8_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }
        private void TrackBarFloat6_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Đang kéo slider: " + e.NewValue);
        }
        private void TrackBarFloat6_ValueChanged(object sender, EventArgs e)
        {
            var fov = trackBarFloatClient6.Value;

            label7.Text = $"{fov}";

            Config.AimbotFOV = fov;
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void pvnsetting_Paint(object sender, PaintEventArgs e)
        {

        }
        // Điều chỉnh độ mượt của animation
        public void SetAnimationSmoothness(float smoothness)
        {
            if (smoothness > 0 && smoothness < 1)
                easingFactor = smoothness;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            setting.Location = new Point(39, 38);

            // Đặt độ mượt mà tối ưu
            SetAnimationSmoothness(0.10f);
        }
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
            Thread.Sleep(2000);
        }
        private void guna2Button33_Click(object sender, EventArgs e)
        {

        }

        private void label71_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Biến CancellationTokenSource toàn cục
        private CancellationTokenSource espResetToken = new CancellationTokenSource();
        private Task espResetTask = null;
        private int espResetDelay = 500; // mặc định 5s


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


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ESP error: " + ex.Message);
                    }

                    await Task.Delay(500); // Delay động từ tham số
                }
            }, espResetToken.Token);
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Tính toán deltaTime để chuyển động mượt mà hơn ở mọi tốc độ máy
            DateTime currentTime = DateTime.Now;
            deltaTime = (float)(currentTime - lastFrameTime).TotalSeconds;
            lastFrameTime = currentTime;

            float distanceX = targetLocation.X - setting.Location.X;
            float stepX = distanceX * easingFactor * (1.0f + deltaTime * ANIMATION_SPEED);

            if (Math.Abs(stepX) < 0.5f)
            {
                stepX = Math.Sign(distanceX) * 0.5f;
            }

            // Di chuyển panel với bước di chuyển mượt hơn
            setting.Location = new Point(
                (int)(setting.Location.X + stepX),
                setting.Location.Y
            );

            // Kiểm tra nếu đã gần đến vị trí mục tiêu
            if (Math.Abs(setting.Location.X - targetLocation.X) <= 1.5f)
            {
                setting.Location = targetLocation; // Đặt đúng vị trí cuối cùng
                animationTimer.Stop();
                isAnimating = false;
            }
        }

        // Phương thức để di chuyển panel với animation
        private void AnimateSettingPanel(Point newLocation)
        {
            if (isAnimating)
            {
                animationTimer.Stop(); // Dừng animation hiện tại nếu có
            }

            // Nếu panel đã ở đúng vị trí thì không cần animation
            if (setting.Location == newLocation)
                return;

            targetLocation = newLocation;
            isAnimating = true;
            lastFrameTime = DateTime.Now; // Khởi tạo thời gian frame đầu tiên
            animationTimer.Start();
        }


        private void thongtinpvn_Paint(object sender, PaintEventArgs e)
        {

        }



        private void guna2CustomCheckBox10_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox9_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox8_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox7_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox5_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox11_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox12_Click(object sender, EventArgs e)
        {

            NoCache();
            UpdateEntities();

        }

        private void guna2Button32_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPLineColor = picker.Color;
            }
        }

        private void guna2Button35_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPBoxColor = picker.Color;
            }
        }

        private void guna2Button39_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPNameColor = picker.Color;
            }
        }

        private void guna2Button36_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {

                Config.ESPSkeletonColor = picker.Color;
            }
        }





        private async void guna2CustomCheckBox14_Click(object sender, EventArgs e)
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
                    IEnumerable<long> wl = await m.AoBScan(search, writable: true);

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

        private async void guna2CustomCheckBox15_Click(object sender, EventArgs e)
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
                    IEnumerable<long> wl = await m.AoBScan(search, writable: true);

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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            thongtinpvn.Visible = true;
            aimbotpvn.Visible = false;
            esppvn.Visible = false;
            setting.Location = new Point(39, 38);



        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            thongtinpvn.Visible = false;
            aimbotpvn.Visible = false;
            esppvn.Visible = true;

            setting.Location = new Point(240, 38);



        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            thongtinpvn.Visible = false;
            aimbotpvn.Visible = true;
            esppvn.Visible = false;
            setting.Location = new Point(139, 38);

        }



        private void guna2CustomCheckBox13_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }





        // Event khi thay đổi giá trị trackbar, cập nhật label hiển thị

        // Hàm fixesp - làm mới dữ liệu ESP (ví dụ)
        private void fixesp()
        {
            Core.LocalPlayer = new();
            Core.Entities.Clear();
            InternalMemory.Cache.Clear();
        }

        // Hàm updateEsp - xử lý hoặc render ESP (ví dụ)
        private void updateEsp()
        {
            foreach (var entity in Core.Entities.Values)
            {
                if (entity.IsTeam != Bool3.False) continue;
                Console.WriteLine($"[ESP] {entity.Name} | HP: {entity.Health} | Pos: {entity.Head}");
            }
        }

        // Khi đóng form dừng task
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            espResetToken.Cancel();
            base.OnFormClosing(e);
        }






        private void guna2Button42_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }


        private void guna2Button9_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {
                guna2Button9.FillColor = picker.Color;
                Config.ESPLineColor = picker.Color;
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {
                guna2Button7.FillColor = picker.Color;
                Config.ESPFillBoxColor = picker.Color;
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {
                guna2Button8.FillColor = picker.Color;
                Config.ESPSkeletonColor = picker.Color;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {
                guna2Button4.FillColor = picker.Color;
                Config.ESPNameColor = picker.Color;
            }
        }

        private void guna2Button27_Click(object sender, EventArgs e)
        {
            var picker = new ColorDialog();
            var result = picker.ShowDialog();

            if (result == DialogResult.OK)
            {
                guna2Button27.FillColor = picker.Color;
                Config.CrosshairColor = picker.Color;
            }
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }




        private async void guna2CustomCheckBox8_Click_1(object sender, EventArgs e)
        {
            string hex = "c0 3f 00 00 00 3f 00 00 80 3f 00 00 00 40 cd cc cc 3d 01 00 00 00 cd cc cc 3d 01 00 00 00 c0"; string replace = "c0 0f 00 00 00 3f 00 00 80 3f 00 00 00 40 cd cc cc 3d 01 00 00 00 cd cc cc 3d 01 00 00 00 e0";
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

        private async void guna2CustomCheckBox7_Click_1(object sender, EventArgs e)
        {
            string hex = "e3 06 00 a0 e1 18 d0 4b e2 02 8b bd ec 70"; string replace = "f3 06 00 a0 e1 18 d0 4b e2 02 8b bd ec 70";
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

        private void guna2Panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomCheckBox9_Click_1(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox9.Checked)
            {
                label55.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label55.ForeColor = Color.DimGray;
            }
            Config.AimbotVisible = guna2CustomCheckBox9.Checked;


        }


        private void guna2CustomCheckBox11_Click_1(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox11.Checked)
            {
                label52.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label52.ForeColor = Color.DimGray;
            }
            Config.NoRecoil = guna2CustomCheckBox11.Checked;
        }

        private void guna2CustomCheckBox12_Click_1(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox12.Checked)
            {
                label53.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label53.ForeColor = Color.DimGray;
            }
            Config.IgnoreKnocked = guna2CustomCheckBox12.Checked;
        }


        private void guna2CustomCheckBox3_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox3.Checked)
            {
                label10.ForeColor = Color.FromArgb(20, 20, 20);

                StartEspAutoReset();

            }
            else
            {
                espResetToken.Cancel(); // Dừng task
                label10.ForeColor = Color.DimGray;
            }
        }

        private void guna2CustomCheckBox20_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox20.Checked)
            {
                label72.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label72.ForeColor = Color.DimGray;
            }
            Config.CrosshairEnabled = guna2CustomCheckBox20.Checked;
        }

        private void guna2CustomCheckBox19_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox19.Checked)
            {
                label47.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label47.ForeColor = Color.DimGray;
            }
            Config.ESPLine = guna2CustomCheckBox19.Checked;
        }

        private void guna2CustomCheckBox18_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox18.Checked)
            {
                label46.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label46.ForeColor = Color.DimGray;
            }
            Config.ESPFillBox = guna2CustomCheckBox18.Checked;
        }

        private void guna2CustomCheckBox17_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox17.Checked)
            {
                label43.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label43.ForeColor = Color.DimGray;
            }
            Config.ESPSkeleton = guna2CustomCheckBox17.Checked;
        }

        private void guna2CustomCheckBox16_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox16.Checked)
            {
                label42.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label42.ForeColor = Color.DimGray;
            }
            Config.ESPHealth = guna2CustomCheckBox16.Checked;
            Config.ESPName = guna2CustomCheckBox16.Checked;
        }

        private void setting_Paint(object sender, PaintEventArgs e)
        {

        }

        private void esppvn_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel12_Paint(object sender, PaintEventArgs e)
        {

        }





        private void guna2Panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void guna2Button6_Click(object sender, EventArgs e)
        {


        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            thongtinpvn.Visible = false;
            aimbotpvn.Visible = true;
            esppvn.Visible = false;
            setting.Location = new Point(139, 38);
        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox1.Checked)
            {
                label1.ForeColor = Color.FromArgb(20, 20, 20);
            }
            else
            {
                label1.ForeColor = Color.DimGray;
            }
            Config.fovaimmemory = guna2CustomCheckBox1.Checked;
        }
        IntPtr hWnd;
        public static extern bool SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);
        public const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;
        public const uint WDA_NONE = 0x00000000;

        private async void guna2CustomCheckBox2_Click(object sender, EventArgs e)
        {
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
                    IEnumerable<long> wl = await m.AoBScan(search, writable: true);

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
    }
}
