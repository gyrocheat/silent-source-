using System.Drawing;
using System.Media;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;

namespace AotForms
{
    internal static class Config
    {
        // Lớp Config để cấu hình FOV và các tùy chọn khác
      
                   // Tắt FOV ban đầu
            public static bool AimbotAutoAim = true;       // Tự động ghim khi trong FOV
            public static float AimbotFOV5 = 100f;          // Bán kính FOV
                                                            // Khoảng cách tối đa
        public static float TeleOffsetX = 0.5f; // Trái/phải
        public static float TeleOffsetY = 1.5f; // Trên/dưới
        public static float TeleOffsetZ = 0.0f; // Trước/sau

        public static bool WaitingForKeybind = false;      // Waiting for user to set a keybind
        public static string AimBotKeyLabel = "None";      // Label for the selected key

        public static bool KeyAlreadyPressed = false;
        public static bool KeyAlreadyPressed3 = false;   // Prevent repeated toggling while holding a key
        public static bool KeyAlreadyPressed4 = false;   // Prevent repeated toggling while holding a key
        public static bool KeyAlreadyPressed5 = false;
        public static bool KeyAlreadyPressed6 = false;  // Prevent repeated toggling while holding a key

        public static Keys SpeedHackKey = Keys.None;
        public static Keys SpeedHackKey1 = Keys.None;  // The selected key for toggling SpeedHack
        public static string SpeedHackKeyLabel = "None"; // Label for the selected SpeedHack key

        public static bool AimbotVisible = false;
        public static bool enableAimBot2 = false;

        public static bool WaitingForKeybind1 = false;      // Waiting for user to set a keybind
        public static string AimBotKeyLabel1 = "None";      // Label for the selected key

        public static bool AimByKewm = false;

        internal static bool DrawFOV2 = false;
        internal static bool PullPlayerEnabled = false;

        public static bool AimbotSafe = false;

        public static bool fovaimmemory = false;


        public static bool flyme = false;


        internal static AimBotType AimBotType;
        
        internal static bool AimBotRage2 = false;
        internal static bool SilentAim2 = false;

        internal static Keys AimbotKey2 = Keys.LButton;



        public static bool AimbotFov = false;
     
       
        public static int AimbotTickRate = 10;
        public static float sensitivity = 1.0f;
        public static float AimBotMaxDistance = 150f;
        public static bool IgnoreKnocked = false;
        // Delay giữa các lần ghi memory (ms)


       
        public static float AimSmooth = 0.15f; // càng nhỏ càng mượt
        public static float PredictValue = 0.12f; // giá trị prediction vị trí
        public static float AimRotationThreshold = 0.005f; // ngưỡng write memory
        public static int AimLogicInterval = 20; // delay logic



   
      
        public static float AimSmoothness = 0.5f; // càng nhỏ càng nhanh
        public static int AimInterval = 15; // delay khi viết góc aim (ms)


        internal static bool enableAimBot = false;


        public static int DelayAim = 100;

        internal static TargetingMode TargetingMode = TargetingMode.ClosestToCrosshair;
        internal static TargetingMode TargetingMode1 = TargetingMode.Target360;
        internal static TargetingMode TargetingMode2 = TargetingMode.ClosestToPlayer;
        internal static TargetingMode TargetingMode3 = TargetingMode.LowestHealth;

        internal static bool FastReload = false;


        public static bool DrawFOV = false;
        public static bool SmoothAim = false; // Bật để aim mượt như người
        public static bool VisibilityCheck = true; // Chỉ aim vào mục tiêu trong tầm nhìn
        internal static bool Aimfovc = false;
        internal static Color Aimfovcolor = Color.White;



        public static bool CrosshairEnabled = false;
        public static Color CrosshairColor = Color.White;
        public static float CrosshairSize = 15f;
        public static float CrosshairThickness = 2f;
        public static float CrosshairRotationSpeed = 2f;

        internal static bool AimBot = false;
       
        internal static bool Aimkill = false;
        internal static Keys AimbotKey = Keys.None;
        internal static Keys AimbotKey1 = Keys.None;

        internal static bool AimBotLeft = false;
        internal static float aimlegit = 0.05f;
        internal static bool StreamMode = false;

        internal static Keys Silent = Keys.LButton;

        internal static Keys Silent2 = Keys.LButton;

        internal static Keys Aim9 = Keys.LButton;
        internal static Keys AimFovKey = Keys.LButton;


        internal static bool WaitingForKeybindSp = false;
        public static Keys SpKey = Keys.None;          // The selected key for toggling AimBot
        public static string SpKeyLabel = "None";
        public static bool SilentAimEnabled = false;
        public static int SilentAimMode = 0; // 0 = 360, 1 = V1


        internal static bool WaitingForKeybindTelePort = false;
        public static Keys TelePortKey = Keys.None;          // The selected key for toggling AimBot
        public static string TelePortKeyLabel = "None";

        internal static bool WaitingForKeybindUpPlayer = false;
        public static Keys UpPlayerKey = Keys.None;          // The selected key for toggling AimBot
        public static string UpPlayerKeyLabel = "None";


        internal static bool WaitingForKeybindDownPlayer = false;
        public static Keys DownPlayerKey = Keys.None;          // The selected key for toggling AimBot
        public static string DownPlayerKeyLabel = "None";

        internal static bool WaitingForKeybindTeleKill = false;
        public static Keys TeleKillKey = Keys.None;          // The selected key for toggling AimBot
        public static string TeleKillKeyLabel = "None";

        // 👉 Thêm dòng này để điều chỉnh độ cao bay
        public static float flymeHeight = 20f;
        public static float downSpeed = 0.8f; // mặc định mỗi vòng giảm 0.8 đơn vị


        internal static bool proxtelekill = false;
        internal static bool UpPlayer = false;
        public static float UpPlayerOffset = 0.4f;
        internal static bool AimLock = false;
        
        public static int AimDelayMs = 80;
        internal static bool Slient = false;
        internal static bool Slient2 = false;
        internal static bool AimFOV = false;
        public static float AimbotFOV = 90;
   
        public static bool Aimbot2 = true;
        public static bool IgnoreKnocked2 = true;
        public static float AimbotFOVvalue = 100f;
        public static float AimBotMaxDistance2 = 300f;
        public static bool EnableSmooth = true;
        
        public static float AimSmoothing = 0.2f;

        public static bool AutoShoot { get; set; }
        public static float AutoShootMaxDistance { get; set; }
        public static bool Debug { get; internal set; }
   
        internal static bool minimap = false;
        internal static bool SilentAim = false;
        internal static bool FixEsp = false;
       

        // DÙNG TRONG TELEPORT
  
        internal static bool telekill = false;
      
        public static bool RenderFov = true;


   
       
        public static float AimFOV1 = 25f;              // FOV tính theo khoảng cách màn hình
        public static float AimTightness = 2.0f;       // Độ chặt (cao = chặt)
        public static float AimBotMaxDistance5 = 200f; // Khoảng cách tối đa đến mục tiêu
      


        internal static bool Speed = false;
        internal static bool NoRecoil = false;
        internal static bool MagicBullet = false;
        internal static bool NoCache = false;
        internal static bool RGB = false;

        internal static Color FovColor = Color.White;
        internal static bool ESPLine = false;
        internal static Color ESPLineColor = Color.White;

        internal static bool ESPBox = false;
        internal static Color ESPBoxColor = Color.White;

        internal static bool ESPName = false;
        internal static Color ESPNameColor = Color.White;

        internal static bool ESPHealth = false;
        internal static bool ESPFillBox = false;
        internal static Color ESPFillBoxColor = Color.White;

        internal static bool ESPCornerLines = false;
        internal static Color ESPBonesColor = Color.FromArgb(254, 255, 159);
        internal static bool ESPBones = false;

        internal static bool ESPLineDuoi = false;
        internal static Color ESPLineDuoiColor = Color.Red;
       
        internal static bool ESPInfo = false;
        internal static bool ESPSkeleton = false;
        internal static Color ESPSkeletonColor = Color.FromArgb(171, 196, 253);
        internal static bool ESPDistance = false;
        internal static bool sound = false;
        // aim ai
        

        internal static bool rgb = false;
        internal static float iconsize = 1.0f;
        public static Vector4 ICONCOLOR = new Vector4(1f, 1f, 1f, 1f);
        public static int MemoryAimRepeat = 2;        // Số lần lặp khi AimByMemory
        public static int MemoryAimDelay = 10;        // Delay giữa các lần ghi memory (ms)

        internal static float AimBotSmooth = 16f;
    
     
        internal static bool teli = false;
        internal static bool AimBotRage = false;

        internal static bool speed = false;
        internal static bool speedx = false;

        internal static bool CameraHackEnabled = false;
        internal static bool down = false;


        internal static float test = 100;

   
        internal static bool UpdateEntities = false;
        internal static bool telekill2 = false;




        internal static float AimFov = 200f;
   
        internal static Color NameCheat = Color.Cyan;
   

      
        internal static bool ESPHealthText = false;
        internal static Color ESPHealthColor = Color.Green;
   
        internal static bool FOVEnabled = false;

        internal static Color FOVColor = Color.White;
       

        internal static float cameraVal = 1.0f;
        internal static float visionVal = 3.141592741f;



        internal static float test1 = 0.01f;

        public static float TeleportRange = 10f;





        public static float AimSpeed = 5f; // Default medium speed




        internal static bool ESPWeapon = false;
        internal static bool ESPWeaponIcon = false;





        internal static bool Enabled = false;
        internal static bool AimbotVisible1 = false;


        internal static float FOV = 90f;
        


        internal static float Smoothness = 12f;

        internal static float SoftnessFactor = 0.4f;
        internal static bool IgnoreKnockedPlayers = true;
        internal static bool VisibilityCheck1 = false;


        internal static float MaxDistance = 500.0f;


        public static float AimMouseSensi = 1.0f;    // tốc độ aim
        public static float AimMouseSmooth = 5.0f;   // độ mượt
        public static float aimfov = 150f;           // FOV


        internal static Keys ActivationKey = Keys.None;


    }
    public enum TargetingMode
    {
        ClosestToCrosshair,
        Target360,
        ClosestToPlayer,
        LowestHealth,
    }
    public enum AimBotType
    {
        Silent,
        AI,
        Mouse,
        XynQaw
    }

}
